using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Nerosoft.Starfish.Toolkit;

/// <summary>
/// Helper utilities for creating, reading and inspecting JSON Web Tokens (JWT).
/// </summary>
/// <remarks>
/// This helper provides simple operations to read tokens without performing signature
/// validation, inspect standard token fields (subject, issued/expiration times),
/// and generate tokens using a symmetric signing key (for development/testing).
/// Methods swallow exceptions and return safe defaults on invalid input.
/// </remarks>
public class TokenHelper
{
	/// <summary>
	/// Parses a JWT string and returns the corresponding <see cref="JwtSecurityToken"/>.
	/// </summary>
	/// <param name="value">The JWT compact serialization (encoded token). May be null or empty.</param>
	/// <returns>
	/// The parsed <see cref="JwtSecurityToken"/> if parsing succeeds; otherwise <c>null</c>.
	/// </returns>
	/// <remarks>
	/// This method reads the token without validating its signature or claims. It will return
	/// <c>null</c> for null/empty input or when parsing fails.
	/// </remarks>
	public static JwtSecurityToken Resolve(string value)
	{
		if (string.IsNullOrEmpty(value))
		{
			return null;
		}

		try
		{
			var handler = new JwtSecurityTokenHandler();
			var token = handler.ReadJwtToken(value);
			return token;
		}
		catch
		{
			return null;
		}
	}

	/// <summary>
	/// Extracts the token subject and attempts to parse it as a 64-bit integer.
	/// </summary>
	/// <param name="value">The JWT compact serialization (encoded token). May be null or empty.</param>
	/// <returns>
	/// The parsed subject as <see cref="long"/> on success; otherwise <c>0</c>.
	/// </returns>
	/// <remarks>
	/// If the token is null, cannot be parsed, or the subject cannot be parsed as a long,
	/// this method returns <c>0</c>. The method does not validate the token's signature.
	/// </remarks>
	public static long GetSubject(string value)
	{
		if (string.IsNullOrEmpty(value))
		{
			return 0;
		}

		try
		{
			var token = Resolve(value);
			return long.Parse(token.Subject);
		}
		catch
		{
			return 0;
		}
	}

	/// <summary>
	/// Determines whether the token is currently valid based on its expiration time.
	/// </summary>
	/// <param name="value">The JWT compact serialization (encoded token). May be null or empty.</param>
	/// <returns>
	/// <c>true</c> if the token has a valid expiration time later than the current UTC time; otherwise <c>false</c>.
	/// </returns>
	/// <remarks>
	/// This validation only checks the <see cref="JwtSecurityToken.ValidTo"/> timestamp and does not
	/// perform signature verification or other claim checks. Exceptions are logged to <see cref="Debug"/>
	/// and result in <c>false</c>.
	/// </remarks>
	public static bool Validate(string value)
	{
		if (string.IsNullOrEmpty(value))
		{
			return false;
		}

		try
		{
			var token = Resolve(value);
			return token.ValidTo > DateTime.UtcNow;
		}
		catch (Exception exception)
		{
			Debug.WriteLine(exception.Message);
			return false;
		}
	}

	/// <summary>
	/// Gets the expiration time (UTC) of the specified JWT.
	/// </summary>
	/// <param name="value">The JWT compact serialization (encoded token). May be null or empty.</param>
	/// <returns>
	/// The token's expiration <see cref="DateTime"/> in UTC if available; otherwise <see cref="DateTime.MinValue"/>.
	/// </returns>
	/// <remarks>
	/// Returns <see cref="DateTime.MinValue"/> when the input is null/empty or when parsing fails.
	/// Exceptions are logged to <see cref="Debug"/>.
	/// </remarks>
	public static DateTime GetExpiration(string value)
	{
		if (string.IsNullOrEmpty(value))
		{
			return DateTime.MinValue;
		}

		try
		{
			var token = Resolve(value);
			return token.ValidTo;
		}
		catch (Exception exception)
		{
			Debug.WriteLine(exception.Message);
			return DateTime.MinValue;
		}
	}

	/// <summary>
	/// Gets the issued-at time (UTC) of the specified JWT.
	/// </summary>
	/// <param name="value">The JWT compact serialization (encoded token). May be null or empty.</param>
	/// <returns>
	/// The token's issued-at <see cref="DateTime"/> in UTC if available; otherwise <c>null</c>.
	/// </returns>
	/// <remarks>
	/// Returns <c>null</c> when the input is null/empty or when parsing fails. Exceptions are logged to <see cref="Debug"/>.
	/// </remarks>
	public static DateTime? GetIssueTime(string value)
	{
		if (string.IsNullOrEmpty(value))
		{
			return null;
		}

		try
		{
			var token = Resolve(value);
			return token.IssuedAt;
		}
		catch (Exception exception)
		{
			Debug.WriteLine(exception.Message);
			return null;
		}
	}

	/// <summary>
	/// Generates a signed JWT from a set of key/value claims.
	/// </summary>
	/// <param name="expires">The desired expiration <see cref="DateTime"/> (UTC) for the token.</param>
	/// <param name="claims">A dictionary of claim type to claim value. Values are converted to strings.</param>
	/// <returns>The generated JWT as a compact serialized string.</returns>
	/// <remarks>
	/// If the specified <paramref name="expires"/> is less than or equal to one day from now,
	/// the method will set the expiration to one day from the current UTC time. The dictionary
	/// values are converted to <see cref="Claim"/> instances using their string representation.
	/// </remarks>
	public static string Generate(DateTime expires, Dictionary<string, object> claims)
	{
		return Generate(expires, claims.Select(t => new Claim(t.Key, t.Value?.ToString() ?? "")));
	}

	/// <summary>
	/// Generates a signed JWT from an enumeration of <see cref="Claim"/> instances.
	/// </summary>
	/// <param name="expires">The desired expiration <see cref="DateTime"/> (UTC) for the token.</param>
	/// <param name="claims">An enumerable of <see cref="Claim"/> objects to include in the token.</param>
	/// <returns>The generated JWT as a compact serialized string.</returns>
	/// <remarks>
	/// If the specified <paramref name="expires"/> is less than or equal to one day from now,
	/// the method will set the expiration to one day from the current UTC time.
	/// </remarks>
	public static string Generate(DateTime expires, IEnumerable<Claim> claims)
	{
		if (expires <= DateTime.UtcNow.AddDays(1))
		{
			expires = DateTime.UtcNow.AddDays(1);
		}

		var identity = new ClaimsIdentity(claims);

		return Generate(expires, identity);
	}

	/// <summary>
	/// Generates a signed JWT from a <see cref="ClaimsIdentity"/>.
	/// </summary>
	/// <param name="expires">The desired expiration <see cref="DateTime"/> (UTC) for the token.</param>
	/// <param name="identity">The <see cref="ClaimsIdentity"/> that supplies claims for the token.</param>
	/// <returns>The generated JWT as a compact serialized string.</returns>
	/// <remarks>
	/// The method uses a symmetric security key derived from a repeated character array
	/// and signs the token using HMAC-SHA256. The token's <c>notBefore</c> is set to the
	/// Unix epoch start (new <see cref="DateTime"/>(1)) and <c>issuedAt</c> to <see cref="DateTime.UtcNow"/>.
	/// This implementation is intended for simple scenarios; replace the key and signing
	/// configuration for production use.
	/// </remarks>
	public static string Generate(DateTime expires, ClaimsIdentity identity)
	{
		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(new string('a', 256)));

		var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
		var handler = new JwtSecurityTokenHandler();
		var token = handler.WriteToken(handler.CreateJwtSecurityToken(notBefore: new DateTime(1), expires: expires, issuedAt: DateTime.UtcNow, subject: identity, signingCredentials: credentials));

		return token;
	}
}
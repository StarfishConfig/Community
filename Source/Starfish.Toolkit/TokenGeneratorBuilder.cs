using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Duende.IdentityModel;
using Microsoft.IdentityModel.Tokens;

namespace Nerosoft.Starfish.Toolkit;

/// <summary>
/// Fluent builder for creating JWT access tokens for tests or runtime usage.
/// Configure claims, times, issuer/audience and signing details, then call <see cref="Build"/>.
/// </summary>
public class TokenGeneratorBuilder
{
    /// <summary>
    /// Internal list of claims that will be embedded in the token.
    /// </summary>
    private readonly List<Claim> _claims = [];

    /// <summary>
    /// The token issue time (UTC). Defaults to <see cref="DateTime.UtcNow"/>.
    /// </summary>
    private DateTime IssueTime { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// How long after the issue time the token will expire. Defaults to 1 day.
    /// </summary>
    private TimeSpan ExpireTime { get; set; } = TimeSpan.FromDays(1);

    /// <summary>
    /// The audience value to set on the token.
    /// </summary>
    private string Audience { get; set; }

    /// <summary>
    /// The issuer value to set on the token.
    /// </summary>
    private string Issuer { get; set; }

    /// <summary>
    /// The symmetric signing key (raw string) used to sign the token.
    /// </summary>
    private string SigningKey { get; set; }

    /// <summary>
    /// The signing algorithm to use. Defaults to HMAC-SHA256.
    /// </summary>
    private string Algorithm { get; set; } = SecurityAlgorithms.HmacSha256Signature;

    /// <summary>
    /// Initializes a new instance of <see cref="TokenGeneratorBuilder"/> and adds a unique token id (jti).
    /// </summary>
    internal TokenGeneratorBuilder()
    {
        //generate token id
        _claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
    }

    /// <summary>
    /// Adds a claim with the given type and value.
    /// </summary>
    /// <param name="type">The claim type (name).</param>
    /// <param name="value">The claim value. If null or whitespace the claim is not added.</param>
    /// <returns>The current <see cref="TokenGeneratorBuilder"/> instance.</returns>
    public TokenGeneratorBuilder AddClaim(string type, string value)
    {
        ArgumentNullException.ThrowIfNull(type);
        if (!string.IsNullOrWhiteSpace(value))
        {
            _claims.Add(new Claim(type, value));
        }

        return this;
    }

    /// <summary>
    /// Adds a claim with an explicit value type.
    /// </summary>
    /// <param name="type">The claim type.</param>
    /// <param name="value">The claim value.</param>
    /// <param name="valueType">The claim value type (e.g. <c>http://www.w3.org/2001/XMLSchema#string</c>).</param>
    /// <returns>The current <see cref="TokenGeneratorBuilder"/> instance.</returns>
    public TokenGeneratorBuilder AddClaim(string type, string value, string valueType)
    {
        ArgumentNullException.ThrowIfNull(type);
        if (!string.IsNullOrWhiteSpace(value))
        {
            _claims.Add(new Claim(type, value, valueType));
        }

        return this;
    }

    /// <summary>
    /// Adds one or more scope claims (<c>scope</c>).
    /// </summary>
    /// <param name="scopes">Scope names to add.</param>
    /// <returns>The current <see cref="TokenGeneratorBuilder"/> instance.</returns>
    public TokenGeneratorBuilder AddScope(params string[] scopes)
    {
        foreach (var scope in scopes)
        {
            AddClaim(JwtClaimTypes.Scope, scope);
        }

        return this;
    }

    /// <summary>
    /// Adds an audience claim (<c>aud</c>).
    /// </summary>
    /// <param name="audience">Audience to add.</param>
    /// <returns>The current <see cref="TokenGeneratorBuilder"/> instance.</returns>
    public TokenGeneratorBuilder AddAudience(string audience)
    {
        return AddClaim(JwtRegisteredClaimNames.Aud, audience);
    }

    /// <summary>
    /// Adds one or more role claims.
    /// </summary>
    /// <param name="roles">Role names to add.</param>
    /// <returns>The current <see cref="TokenGeneratorBuilder"/> instance.</returns>
    public TokenGeneratorBuilder AddRole(params string[] roles)
    {
        if (roles?.Length > 0)
        {
            foreach (var role in roles)
            {
                AddClaim(ClaimTypes.Role, role);
            }
        }

        return this;
    }

    /// <summary>
    /// Adds the configured name claim.
    /// </summary>
    /// <param name="name">The name value.</param>
    /// <param name="type">Optional claim type; defaults to <c>name</c>.</param>
    /// <returns>The current <see cref="TokenGeneratorBuilder"/> instance.</returns>
    public TokenGeneratorBuilder AddClaimName(string name, string type = JwtRegisteredClaimNames.Name)
    {
        return AddClaim(type, name);
    }

    /// <summary>
    /// Adds the configured email claim.
    /// </summary>
    /// <param name="value">The email value.</param>
    /// <param name="type">Optional claim type; defaults to <c>email</c>.</param>
    /// <returns>The current <see cref="TokenGeneratorBuilder"/> instance.</returns>
    public TokenGeneratorBuilder AddClaimEmail(string value, string type = JwtRegisteredClaimNames.Email)
    {
        return AddClaim(type, value);
    }

    /// <summary>
    /// Adds the subject claim (<c>sub</c>).
    /// </summary>
    /// <param name="value">Subject identifier value.</param>
    /// <param name="type">Optional claim type; defaults to <c>sub</c>.</param>
    /// <returns>The current <see cref="TokenGeneratorBuilder"/> instance.</returns>
    public TokenGeneratorBuilder AddClaimSubject(string value, string type = JwtRegisteredClaimNames.Sub)
    {
        return AddClaim(type, value);
    }

    /// <summary>
    /// Sets the token lifetime relative to the issue time.
    /// </summary>
    /// <param name="time">Timespan after issue when token should expire.</param>
    /// <returns>The current <see cref="TokenGeneratorBuilder"/> instance.</returns>
    public TokenGeneratorBuilder ExpiresIn(TimeSpan time)
    {
        ExpireTime = time;
        return this;
    }

    /// <summary>
    /// Sets the token's issued-at time.
    /// </summary>
    /// <param name="time">The issue time to use for the token.</param>
    /// <returns>The current <see cref="TokenGeneratorBuilder"/> instance.</returns>
    public TokenGeneratorBuilder IssuedAt(DateTime time)
    {
        // if (time != null)
        // {
        // 	AddClaim(JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(time.Value).ToString(), ClaimValueTypes.Integer64);
        // }

        IssueTime = time;

        return this;
    }

    /// <summary>
    /// Sets the symmetric signing key string.
    /// </summary>
    /// <param name="key">Signing key material.</param>
    /// <returns>The current <see cref="TokenGeneratorBuilder"/> instance.</returns>
    public TokenGeneratorBuilder WithSigningKey(string key)
    {
        SigningKey = key;
        return this;
    }

    /// <summary>
    /// Sets the signing algorithm to use.
    /// </summary>
    /// <param name="algorithm">Algorithm identifier (e.g. HMAC-SHA256).</param>
    /// <returns>The current <see cref="TokenGeneratorBuilder"/> instance.</returns>
    public TokenGeneratorBuilder WithAlgorithm(string algorithm)
    {
        Algorithm = algorithm;
        return this;
    }

    /// <summary>
    /// Sets the audience value to place on the token.
    /// </summary>
    /// <param name="audience">Audience identifier.</param>
    /// <returns>The current <see cref="TokenGeneratorBuilder"/> instance.</returns>
    public TokenGeneratorBuilder WithAudience(string audience)
    {
        Audience = audience;
        return this;
    }

    /// <summary>
    /// Sets the issuer value to place on the token.
    /// </summary>
    /// <param name="issuer">Issuer identifier.</param>
    /// <returns>The current <see cref="TokenGeneratorBuilder"/> instance.</returns>
    public TokenGeneratorBuilder WithIssuer(string issuer)
    {
        Issuer = issuer;
        return this;
    }

    /// <summary>
    /// Builds and returns a signed JWT access token string using the configured values.
    /// </summary>
    /// <returns>A JWT access token as a string.</returns>
    public string Build()
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(SigningKey);
        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(_claims),
            Expires = IssueTime.Add(ExpireTime),
            Issuer = Issuer,
            IncludeKeyIdInHeader = true,
            Audience = Audience,
            IssuedAt = IssueTime,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), Algorithm)
        };

        var token = handler.CreateToken(descriptor);
        var accessToken = handler.WriteToken(token);

        return accessToken;
    }
}
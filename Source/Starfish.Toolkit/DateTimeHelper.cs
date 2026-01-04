namespace Nerosoft.Starfish.Toolkit;

/// <summary>
/// Provides helper methods to convert between Unix time (seconds since Unix epoch) and <see cref="DateTime"/>.
/// </summary>
/// <remarks>
/// The Unix epoch is defined as 1970-01-01T00:00:00Z (UTC). Methods in this helper use UTC as the reference
/// point. Callers should ensure that input <see cref="DateTime"/> values are in UTC (or call <c>ToUniversalTime()</c>)
/// when converting to Unix time to avoid incorrect results.
/// </remarks>
public class DateTimeHelper
{
	/// <summary>
	/// Converts a Unix timestamp (seconds since 1970-01-01T00:00:00Z) to a <see cref="DateTime"/> instance.
	/// </summary>
	/// <param name="unixTime">The number of seconds since the Unix epoch (1970-01-01T00:00:00Z). Can be negative for dates before the epoch.</param>
	/// <returns>
	/// A <see cref="DateTime"/> representing the specified point in time. The returned value has <see cref="DateTime.Kind"/>
	/// set to <see cref="DateTimeKind.Utc"/>.
	/// </returns>
	/// <remarks>
	/// This method adds <paramref name="unixTime"/> seconds to the UTC epoch. Fractional seconds are not represented;
	/// the input is interpreted as whole seconds.
	/// </remarks>
	/// <example>
	/// <code>
	/// var dt = DateTimeHelper.GetDateTimeFromUnixTime(0); // 1970-01-01T00:00:00Z (UTC)
	/// </code>
	/// </example>
	public static DateTime GetDateTimeFromUnixTime(long unixTime)
	{
		return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTime);
	}

	/// <summary>
	/// Converts a <see cref="DateTime"/> to a Unix timestamp expressed in whole seconds.
	/// </summary>
	/// <param name="dateTime">
	/// The <see cref="DateTime"/> to convert. For accurate results, provide a UTC <see cref="DateTime"/> or call
	/// <c>dateTime.ToUniversalTime()</c> before calling this method.
	/// </param>
	/// <returns>
	/// A <see cref="long"/> representing the total whole seconds between the Unix epoch (1970-01-01T00:00:00Z) and <paramref name="dateTime"/>.
	/// The value is obtained by subtracting the UTC epoch and casting <c>TotalSeconds</c> to <see cref="long"/>, which truncates fractional seconds.
	/// </returns>
	/// <remarks>
	/// This method subtracts a UTC epoch <see cref="DateTime"/> instance from the provided <paramref name="dateTime"/>.
	/// If <paramref name="dateTime"/> has <see cref="DateTimeKind.Local"/> or <see cref="DateTimeKind.Unspecified"/>,
	/// callers should convert it to UTC first to avoid incorrect results.
	/// </remarks>
	/// <example>
	/// <code>
	/// var unix = DateTimeHelper.GetUnixTimeFromDateTime(DateTime.UtcNow);
	/// </code>
	/// </example>
	public static long GetUnixTimeFromDateTime(DateTime dateTime)
	{
		return (long)dateTime.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
	}
}
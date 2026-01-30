namespace Nerosoft.Starfish.Shared;

/// <summary>
/// Common HTTP-related constants used across the application (header names and default query paging values).
/// </summary>
public static class HttpConstants
{
	/// <summary>
	/// HTTP header name constants.
	/// </summary>
	public static class Header
	{
		/// <summary>
		/// Header key used to pass a correlation identifier for distributed tracing.
		/// </summary>
		public const string CorrelationId = "c-correlation-id";

		/// <summary>
		/// Header key used to pass a unique request identifier.
		/// </summary>
		public const string RequestId = "x-request-id";

		/// <summary>
		/// Header key used to communicate total counts for paged resources.
		/// </summary>
		public const string TotalCount = "x-total-count";
	}

	/// <summary>
	/// Default query parameter values for paging.
	/// </summary>
	public static class Parameter
	{
		/// <summary>
		/// Default number of items to skip when paging (offset).
		/// </summary>
		public const int Skip = 0;

		/// <summary>
		/// Default page size for paged queries.
		/// </summary>
		public const int Size = 20;
	}
}
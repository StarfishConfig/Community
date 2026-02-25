namespace Nerosoft.Starfish.Shared;

/// <summary>
/// Contains constants related to configuration properties and constraints.
/// </summary>
public static class ConfigurationConstants
{
	/// <summary>
	/// Configuration table name in the database.
	/// </summary>
	public class TableName
	{
		/// <summary>
		/// Configuration table name in the database.
		/// </summary>
		public const string Configuration = "configuration";

		/// <summary>
		/// Configuration item table name in the database.
		/// </summary>
		public const string ConfigurationItem = "configuration_item";

		/// <summary>
		/// Configuration permission table name in the database.
		/// </summary>
		public const string ConfigurationPermission = "configuration_permission";

		/// <summary>
		/// Configuration reference table name in the database.
		/// </summary>
		public const string ConfigurationReference = "configuration_reference";
	}

	/// <summary>
	/// Column names for the configuration table in the database.
	/// </summary>
	public class ColumnName
	{
		/// <summary>
		/// Column name for team ID property in the database.
		/// </summary>
		public const string TeamId = "team_id";

		/// <summary>
		/// Column name for code property in the database.
		/// </summary>
		public const string Code = "code";

		/// <summary>
		/// Column name for name property in the database.
		/// </summary>
		public const string Name = "name";

		/// <summary>
		/// Column name for description property in the database.
		/// </summary>
		public const string Description = "description";

		/// <summary>
		/// Column name for status property in the database.
		/// </summary>
		public const string Status = "status";

		/// <summary>
		/// Column name for shared property in the database.
		/// </summary>
		public const string Shared = "shared";

		/// <summary>
		/// Column name for configuration ID property in the database.
		/// </summary>
		public const string ConfigurationId = "configuration_id";

		/// <summary>
		/// Column name for user ID property in the database.
		/// </summary>
		public const string UserId = "user_id";

		/// <summary>
		/// Column name for key property in the database.
		/// </summary>
		public const string Key = "key";

		/// <summary>
		/// Column name for value property in the database.
		/// </summary>
		public const string Value = "value";

		/// <summary>
		/// Column name for read permission property in the database.
		/// </summary>
		public const string Read = "read";

		/// <summary>
		/// Column name for write permission property in the database.
		/// </summary>
		public const string Write = "write";

		/// <summary>
		/// Column name for publish permission property in the database.
		/// </summary>
		public const string Publish = "publish";

		/// <summary>
		/// Column name for reference ID property in the configuration reference table in the database.
		/// </summary>
		public const string ReferenceId = "reference_id";
	}

	/// <summary>
	/// Index names for the configuration table in the database.
	/// </summary>
	public class IndexName
	{
		/// <summary>
		/// Index name for unique constraint.
		/// </summary>
		public const string Unique = "idx_unique";

		/// <summary>
		/// Team ID index name in the database.
		/// </summary>
		public const string TeamId = "idx_team_id";

		/// <summary>
		/// Index name for configuration ID.
		/// </summary>
		public const string ConfigurationId = "idx_cfg_id";

		/// <summary>
		/// Index name for user ID.
		/// </summary>
		public const string UserId = "idx_user_id";

		/// <summary>
		/// Index name for code property in the configuration table.
		/// </summary>
		public const string Code = "idx_code";
	}

	/// <summary>
	/// Length constraints for string properties in the configuration table.
	/// </summary>
	public class StringLength
	{
		/// <summary>
		/// Maximum length for configuration code property.
		/// </summary>
		public const int CodeMaximumLength = 100;

		/// <summary>
		/// Maximum length for configuration name property.
		/// </summary>
		public const int NameMaximumLength = 200;

		/// <summary>
		/// Maximum length for configuration description property.
		/// </summary>
		public const int DescriptionMaximumLength = 1000;

		/// <summary>
		/// Maximum length for configuration item key property.
		/// </summary>
		public const int KeyMaximumLength = 200;

		/// <summary>
		/// Maximum length for configuration item value property.
		/// </summary>
		public const int ValueMaximumLength = 4000;
	}
}
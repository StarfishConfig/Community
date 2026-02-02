namespace Nerosoft.Starfish.Repository.Models;

public class ConfigurationBaseInfoModel
{
	public long Id { get; set; }

	public long TeamId { get; set; }

	public string Code { get; set; }

	public string Name { get; set; }

	public string Description { get; set; }
}
using AutoMapper;
using Nerosoft.Starfish.Persistent.Data;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Repository.Models;

namespace Nerosoft.Starfish.Repository.Profiles;

internal class ConfigurationProfile : Profile
{
	public ConfigurationProfile()
	{
		CreateMap<Configuration, ConfigurationBaseInfoModel>();
		CreateMap<Configuration, ConfigurationDetailModel>();

		CreateMap<ConfigurationChangelogData, ConfigurationChangelog>();
	}
}
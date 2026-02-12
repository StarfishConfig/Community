using AutoMapper;
using Nerosoft.Starfish.Domain.Commands;
using Nerosoft.Starfish.Repository.Models;
using Nerosoft.Starfish.Transit;

namespace Nerosoft.Starfish.Facade.Profiles;

internal class ConfigurationProfile : Profile
{
	public ConfigurationProfile()
	{
		CreateMap<ConfigurationCreateDto, ConfigurationCreateCommand>();
		CreateMap<ConfigurationUpdateDto, ConfigurationUpdateCommand>();

		CreateMap<ConfigurationListModel, ConfigurationListDto>();
		CreateMap<ConfigurationDetailModel, ConfigurationDetailDto>();

		CreateMap<ConfigurationEnvironmentModel, ConfigurationEnvironmentDto>();
	}
}
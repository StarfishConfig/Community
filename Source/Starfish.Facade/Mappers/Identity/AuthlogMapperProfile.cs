using AutoMapper;
using Nerosoft.Starfish.Repository.Models;
using Nerosoft.Starfish.Transit;

namespace Nerosoft.Starfish.Facade.Mappers;

internal class AuthlogMapperProfile : Profile
{
	public AuthlogMapperProfile()
	{
		CreateMap<AuthlogListModel, AuthlogListDto>();
		CreateMap<AuthlogCriteriaDto, AuthlogCriteriaModel>();
	}
}

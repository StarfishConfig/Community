using AutoMapper;
using Nerosoft.Starfish.Repository.Models;
using Nerosoft.Starfish.Transit;

namespace Nerosoft.Starfish.Facade.Profiles;

internal class AuthlogProfile : Profile
{
	public AuthlogProfile()
	{
		CreateMap<AuthlogListModel, AuthlogListDto>();
		CreateMap<AuthlogCriteriaDto, AuthlogCriteriaModel>();
	}
}

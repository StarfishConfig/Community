using AutoMapper;
using Nerosoft.Starfish.Domain.Commands;
using Nerosoft.Starfish.Facade.Transit;
using Nerosoft.Starfish.Repository.Models;

namespace Nerosoft.Starfish.Facade.Mappers;

internal class UserMapperProfile : Profile
{
	public UserMapperProfile()
	{
		CreateMap<UserCreateDto, UserCreateCommand>();
		CreateMap<UserUpdateDto, UserUpdateCommand>();

		CreateMap<UserDetailModel, UserDetailDto>();
		CreateMap<UserDetailModel, UserProfileDto>();
		CreateMap<UserListModel, UserListDto>();
	}
}
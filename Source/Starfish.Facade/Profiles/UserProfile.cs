using AutoMapper;
using Nerosoft.Starfish.Domain.Commands;
using Nerosoft.Starfish.Transit;
using Nerosoft.Starfish.Repository.Models;

namespace Nerosoft.Starfish.Facade.Profiles;

internal class UserProfile : Profile
{
	public UserProfile()
	{
		CreateMap<UserCreateDto, UserCreateCommand>();
		CreateMap<UserUpdateDto, UserUpdateCommand>();

		CreateMap<UserDetailModel, UserDetailDto>();
		CreateMap<UserDetailModel, UserProfileDto>();
		CreateMap<UserListModel, UserListDto>();
		CreateMap<UserListModel, UserLookupDto>()
			.AfterMap((src, dest, _) =>
			{
				dest.Name = $"{src.Username}/{src.Nickname}";
			});
	}
}
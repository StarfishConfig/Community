using AutoMapper;
using Nerosoft.Starfish.Domain.Commands;
using Nerosoft.Starfish.Transit;
using Nerosoft.Starfish.Repository.Models;

namespace Nerosoft.Starfish.Facade.Mappers;

internal class TeamMapperProfile : Profile
{
	public TeamMapperProfile()
	{
		CreateMap<TeamListModel, TeamListDto>()
			.AfterMap((src, dest, _) =>
			{
				dest.OwnerName = $"{src.OwnerUsername}/{src.OwnerNickname ?? "--"}";
			});

		CreateMap<TeamDetailModel, TeamDetailDto>()
			.AfterMap((src, dest, _) =>
			{
				dest.OwnerName = $"{src.OwnerUsername}/{src.OwnerNickname ?? "--"}";
			});

		CreateMap<TeamEditDto, TeamCreateCommand>();
		CreateMap<TeamEditDto, TeamUpdateCommand>();
		CreateMap<TeamMemberModel, TeamMemberDto>();
	}
}
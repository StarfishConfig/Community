using AutoMapper;
using Nerosoft.Starfish.Facade.Transit;
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

		CreateMap<TeamDetailModel, TeamDetailDto>();
	}
}
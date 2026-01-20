using AutoMapper;
using Nerosoft.Starfish.Persistent;
using Nerosoft.Starfish.Repository.Entities;

namespace Nerosoft.Starfish.Repository.Mappers;

internal class TeamMapperProfile : Profile
{
	public TeamMapperProfile()
	{
		CreateMap<Team, TeamData>()
			.ForMember(t => t.Members, opt => opt.Ignore())
			.AfterMap((src, dest, _) =>
			{
				dest.Members = src.Members?.Select(t => t.UserId).ToHashSet() ?? [];
			});

		CreateMap<TeamData, Team>()
			.ForMember(t => t.Members, opt => opt.Ignore())
			.AfterMap((src, dest, _) =>
			{
				dest.Members.RemoveWhere(t => src.Members.Contains(t.UserId));
				foreach (var member in src.Members)
				{
					if (dest.Members.All(t => t.UserId != member))
					{
						dest.Members.Add(new TeamMember { UserId = member });
					}
				}

				dest.MembersCount = src.Members?.Count ?? 0;
			});
	}
}
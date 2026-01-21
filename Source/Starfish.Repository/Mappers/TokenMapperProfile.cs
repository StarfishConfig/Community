using AutoMapper;
using Nerosoft.Starfish.Persistent;
using Nerosoft.Starfish.Repository.Entities;

namespace Nerosoft.Starfish.Repository.Mappers;

internal class TokenMapperProfile : Profile
{
	public TokenMapperProfile()
	{
		CreateMap<TokenData, Token>()
			.ForMember(dest => dest.Id, opt => opt.Ignore());

		CreateMap<Token, TokenData>();
	}
}
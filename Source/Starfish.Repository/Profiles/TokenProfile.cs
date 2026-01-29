using AutoMapper;
using Nerosoft.Starfish.Persistent;
using Nerosoft.Starfish.Repository.Entities;

namespace Nerosoft.Starfish.Repository.Profiles;

internal class TokenProfile : Profile
{
	public TokenProfile()
	{
		CreateMap<TokenData, Token>()
			.ForMember(dest => dest.Id, opt => opt.Ignore());

		CreateMap<Token, TokenData>();
	}
}
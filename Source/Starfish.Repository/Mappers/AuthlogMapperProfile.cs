using AutoMapper;
using Nerosoft.Starfish.Persistent;
using Nerosoft.Starfish.Repository.Entities;

namespace Nerosoft.Starfish.Repository.Mappers;

/// <summary>
/// Mapper profile for Authlog entity.
/// </summary>
internal class AuthlogMapperProfile : Profile
{
	public AuthlogMapperProfile()
	{
		CreateMap<AuthlogData, Authlog>();
	}
}
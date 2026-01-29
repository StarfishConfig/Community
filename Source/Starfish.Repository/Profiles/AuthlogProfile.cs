using AutoMapper;
using Nerosoft.Starfish.Persistent;
using Nerosoft.Starfish.Repository.Entities;

namespace Nerosoft.Starfish.Repository.Profiles;

/// <summary>
/// Mapper profile for Authlog entity.
/// </summary>
internal class AuthlogProfile : Profile
{
	public AuthlogProfile()
	{
		CreateMap<AuthlogData, Authlog>();
	}
}
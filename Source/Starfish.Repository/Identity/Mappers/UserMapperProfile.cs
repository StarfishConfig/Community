using AutoMapper;
using Nerosoft.Starfish.Persistent;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Repository.Models;
using Nerosoft.Starfish.Toolkit;

namespace Nerosoft.Starfish.Repository.Mappers;

internal class UserMapperProfile : Profile
{
	public UserMapperProfile()
	{
		CreateMap<UserData, User>()
			.ForMember(dest => dest.Id, opt => opt.Ignore())
			.ForMember(dest => dest.Roles, opt => opt.Ignore())
			.AfterMap((data, entity, _) =>
			{
				if (!string.IsNullOrEmpty(data.Password))
				{
					var salt = RandomUtility.GenerateRandomString();
					var hash = Cryptography.DES.Encrypt(data.Password, Encoding.UTF8.GetBytes(salt));
					entity.PasswordHash = hash;
					entity.PasswordSalt = salt;
				}

				data.Roles ??= [];
				entity.Roles ??= [];
				entity.Roles.RemoveAll(t => data.Roles.Contains(t.Name));
				foreach (var name in data.Roles)
				{
					if (entity.Roles.Any(role => string.Equals(name, role.Name, StringComparison.OrdinalIgnoreCase)))
					{
						continue;
					}

					entity.Roles.Add(new UserRole(name));
				}
			});

		CreateMap<User, UserData>()
			.ForMember(dest => dest.Password, opt => opt.Ignore())
			.ForMember(dest => dest.Roles, opt => opt.Ignore())
			.AfterMap((entity, data, _) =>
			{
				data.Roles = entity.Roles?.Select(r => r.Name).ToHashSet() ?? [];
			});

		CreateMap<User, UserAuthQueryModel>()
			.ForMember(dest => dest.Roles, opt => opt.Ignore())
			.AfterMap((entity, data, _) =>
			{
				data.Roles = entity.Roles?.Select(r => r.Name).ToHashSet() ?? [];
			});

		CreateMap<User, UserDetailQueryModel>()
			.AfterMap((entity, data, _) =>
			{
				data.Roles = entity.Roles?.Select(r => r.Name).ToHashSet() ?? [];
			});
	}
}
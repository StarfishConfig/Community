using AutoMapper;
using Nerosoft.Starfish.Persistent.Data;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Repository.Models;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Profiles;

internal class ConfigurationProfile : Profile
{
	public ConfigurationProfile()
	{
		CreateMap<Configuration, ConfigurationBaseInfoModel>();
		CreateMap<Configuration, ConfigurationDetailModel>();
		CreateMap<Configuration, ConfigurationListModel>();

		CreateMap<ConfigurationChangelogData, ConfigurationChangelog>();

		CreateMap<ConfigurationPermissionData, ConfigurationPermission>();

		CreateMap<ConfigurationData, Configuration>()
			.ForMember(t => t.Permissions, opt => opt.Ignore())
			.AfterMap((src, dest, context) =>
			{
				dest.Permissions ??= [];

				src.Permissions ??= [];
				src.Permissions.RemoveAll(t => t.Value == ConfigurationPermissionGrantState.None);

				dest.Permissions.RemoveAll(t => !src.Permissions.ContainsKey(t.UserId));

				foreach (var (userId, state) in src.Permissions)
				{
					var entity = dest.Permissions.FirstOrDefault(e => e.UserId == userId);
					if (entity == null)
					{
						entity = new ConfigurationPermission
						{
							UserId = userId,
							Read = state.HasFlag(ConfigurationPermissionGrantState.Read) || state.HasFlag(ConfigurationPermissionGrantState.Write) | state.HasFlag(ConfigurationPermissionGrantState.Publish),
							Write = state.HasFlag(ConfigurationPermissionGrantState.Write),
							Publish = state.HasFlag(ConfigurationPermissionGrantState.Publish)
						};
						//entity = context.Mapper.Map<ConfigurationPermission>(data);
						dest.Permissions.Add(entity);
					}
					else
					{
						entity.Read = state.HasFlag(ConfigurationPermissionGrantState.Read) || state.HasFlag(ConfigurationPermissionGrantState.Write) | state.HasFlag(ConfigurationPermissionGrantState.Publish);
						entity.Write = state.HasFlag(ConfigurationPermissionGrantState.Write);
						entity.Publish = state.HasFlag(ConfigurationPermissionGrantState.Publish);
						//context.Mapper.Map(data, entity);
					}
				}
			});
		
		CreateMap<Configuration, ConfigurationData>()
			.ForMember(t => t.Permissions, opt => opt.Ignore())
			.AfterMap((src, dest, context) =>
			{
				if (src.Permissions != null)
				{
					dest.Permissions = src.Permissions.ToDictionary(p => p.UserId, p =>
					{
						var state = ConfigurationPermissionGrantState.None;
						if (p.Read)
						{
							state |= ConfigurationPermissionGrantState.Read;
						}

						if (p.Write)
						{
							state |= ConfigurationPermissionGrantState.Write;
						}

						if (p.Publish)
						{
							state |= ConfigurationPermissionGrantState.Publish;
						}

						return state;
					});
					dest.Permissions.RemoveAll(t => t.Value == ConfigurationPermissionGrantState.None);
				}

				dest.Items = src?.Items.ToDictionary(t => t.Key, t => t.Value);
			});
	}
}
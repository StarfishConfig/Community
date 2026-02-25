using AutoMapper;
using Nerosoft.Starfish.Persistent.Data;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Repository.Models;

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
			.AfterMap((src, dest, context) =>
			{
				dest.Permissions ??= [];

				var ids = src.Permissions?.Select(e => e.Id) ?? [];

				dest.Permissions.RemoveAll(t => !ids.Contains(t.Id));

				if (src.Permissions != null)
				{
					foreach (var data in src.Permissions)
					{
						var entity = dest.Permissions.FirstOrDefault(e => e.Id == data.Id);
						if (entity == null)
						{
							entity = context.Mapper.Map<ConfigurationPermission>(data);
							dest.Permissions.Add(entity);
						}
						else
						{
							context.Mapper.Map(data, entity);
						}
					}
				}

				{
				}
			});
		CreateMap<Configuration, ConfigurationData>();
	}
}
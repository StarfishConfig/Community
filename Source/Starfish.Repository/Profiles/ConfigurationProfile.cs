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

		CreateMap<ConfigurationData, Configuration>()
			.ForMember(dest => dest.Environments, opts => opts.Ignore())
			.AfterMap((src, dest, context) =>
			{
				dest.Environments ??= [];

				var ids = src.Environments?.Select(e => e.Id) ?? [];

				dest.Environments.RemoveAll(t => !ids.Contains(t.Id));

				if (src.Environments != null)
				{
					foreach (var data in src.Environments)
					{
						var entity = dest.Environments.FirstOrDefault(e => e.Id == data.Id);
						if (entity == null)
						{
							entity = context.Mapper.Map<ConfigurationEnvironment>(data);
							dest.Environments.Add(entity);
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

		CreateMap<ConfigurationEnvironmentData, ConfigurationEnvironment>()
			.ReverseMap();
	}
}
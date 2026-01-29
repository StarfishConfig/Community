using AutoMapper;
using Nerosoft.Starfish.Persistent.Data;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Repository.Models;

namespace Nerosoft.Starfish.Repository.Profiles;

internal class TemplateProfile : Profile
{
	public TemplateProfile()
	{
		CreateMap<TemplateData, Template>();
		CreateMap<Template, TemplateData>();

		CreateMap<Template, TemplateDetailModel>();
		CreateMap<Template, TemplateListModel>();
	}
}
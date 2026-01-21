using AutoMapper;
using Nerosoft.Starfish.Persistent.Data;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Repository.Models;

namespace Nerosoft.Starfish.Repository.Mappers;

internal class TemplateMapperProfile : Profile
{
	public TemplateMapperProfile()
	{
		CreateMap<TemplateData, Template>();
		CreateMap<Template, TemplateData>();

		CreateMap<Template, TemplateDetailModel>();
		CreateMap<Template, TemplateListModel>();
	}
}
using AutoMapper;
using Nerosoft.Starfish.Domain.Commands;
using Nerosoft.Starfish.Transit;
using Nerosoft.Starfish.Repository.Models;

namespace Nerosoft.Starfish.Facade.Profiles;

internal class TemplateProfile : Profile
{
	public TemplateProfile()
	{
		// CreateMap<Source, Destination>();
		CreateMap<TemplateEditDto, TemplateCreateCommand>();
		CreateMap<TemplateEditDto, TemplateUpdateCommand>();

		CreateMap<TemplateDetailModel, TemplateDetailDto>();
		CreateMap<TemplateListModel, TemplateListDto>();
	}
}
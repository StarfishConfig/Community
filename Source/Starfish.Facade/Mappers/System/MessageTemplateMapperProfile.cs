using AutoMapper;
using Nerosoft.Starfish.Domain.Commands;
using Nerosoft.Starfish.Facade.Transit;
using Nerosoft.Starfish.Repository.Models;

namespace Nerosoft.Starfish.Facade.Mappers;

internal class MessageTemplateMapperProfile : Profile
{
	public MessageTemplateMapperProfile()
	{
		// CreateMap<Source, Destination>();
		CreateMap<MessageTemplateEditDto, MessageTemplateCreateCommand>();
		CreateMap<MessageTemplateEditDto, MessageTemplateUpdateCommand>();

		CreateMap<MessageTemplateDetailModel, MessageTemplateDetailDto>();
		CreateMap<MessageTemplateListModel, MessageTemplateListDto>();
	}
}

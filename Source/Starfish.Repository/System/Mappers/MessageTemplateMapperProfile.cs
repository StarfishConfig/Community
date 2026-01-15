using AutoMapper;
using Nerosoft.Starfish.Persistent.Data;
using Nerosoft.Starfish.Repository.Entities;

namespace Nerosoft.Starfish.Repository.Mappers;

internal class MessageTemplateMapperProfile : Profile
{
	public MessageTemplateMapperProfile()
	{
		CreateMap<MessageTemplateData, MessageTemplate>();
		CreateMap<MessageTemplate, MessageTemplateData>();
	}
}

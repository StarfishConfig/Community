using AutoMapper;
using Nerosoft.Starfish.Persistent.Data;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Repository.Models;

namespace Nerosoft.Starfish.Repository.Mappers;

internal class MessageTemplateMapperProfile : Profile
{
	public MessageTemplateMapperProfile()
	{
		CreateMap<MessageTemplateData, MessageTemplate>();
		CreateMap<MessageTemplate, MessageTemplateData>();

		CreateMap<MessageTemplate, MessageTemplateDetailModel>();
		CreateMap<MessageTemplate, MessageTemplateListModel>();
	}
}
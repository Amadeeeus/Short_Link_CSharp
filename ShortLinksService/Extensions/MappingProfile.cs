using AutoMapper;
using ShortLinksService.Commands.Create;
using ShortLinksService.Entities;
using ShortLinksService.Models.MongoEntities;

namespace ShortLinksService.Extensions;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<CreateShortLinkRequest, CreateLinkEntity>().ReverseMap();
        CreateMap<CreateLinkEntity, MongoEntity>();
    }
}
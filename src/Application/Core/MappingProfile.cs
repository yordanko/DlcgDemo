using System;
using Application.Dtos;
using AutoMapper;
using Domain;

namespace Application.Core;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<VideoGame, VideoGameDto>().ReverseMap();
        CreateMap<VideoGameDto, VideoGameDto>();
        CreateMap<CreateVideoGameDto, VideoGame>();
        CreateMap<EditVideoGameDto, VideoGame>();
    }

}

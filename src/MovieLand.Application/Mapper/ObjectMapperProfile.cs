using AutoMapper;
using MovieLand.Application.DTOs;
using MovieLand.Domain.Entities;


namespace MovieLand.Application.Mapper
{
    public class ObjectMapperProfile : Profile
    {
        public ObjectMapperProfile()
        {
            CreateMap<Movie, MovieDTO>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title)).ReverseMap();
        }
    }
}

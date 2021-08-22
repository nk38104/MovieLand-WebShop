using AutoMapper;
using MovieLand.Application.DTOs;
using MovieLand.Web.ViewModels;


namespace MovieLand.Web.Mapper
{
    public class ObjectMapperViewModelProfile : Profile
    {
        public ObjectMapperViewModelProfile()
        {
            CreateMap<MovieDTO, MovieViewModel>().ReverseMap();
        }
    }
}

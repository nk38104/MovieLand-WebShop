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
            CreateMap<FavoritesDTO, FavoritesViewModel>().ReverseMap();
            CreateMap<CompareDTO, CompareViewModel>().ReverseMap();
            CreateMap<CartDTO, CartViewModel>().ReverseMap();
            CreateMap<CartItemDTO, CartItemViewModel>().ReverseMap();
            CreateMap<OrderDTO, OrderViewModel>().ReverseMap();
            CreateMap<OrderItemDTO, OrderItemViewModel>().ReverseMap();
            CreateMap<GenreDTO, GenreViewModel>().ReverseMap();
            CreateMap<DirectorDTO, DirectorViewModel>().ReverseMap();
        }
    }
}

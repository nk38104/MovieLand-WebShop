using AutoMapper;
using MovieLand.Application.DTOs;
using MovieLand.Application.DTOs.Account;
using MovieLand.Web.ViewModels;
using MovieLand.Web.ViewModels.Account;


namespace MovieLand.Web.Mapper
{
    public class ObjectMapperViewModelProfile : Profile
    {
        public ObjectMapperViewModelProfile()
        {
            CreateMap<CartDTO, CartViewModel>().ReverseMap();
            CreateMap<CartItemDTO, CartItemViewModel>().ReverseMap();
            CreateMap<CompareDTO, CompareViewModel>().ReverseMap();
            CreateMap<DirectorDTO, DirectorViewModel>().ReverseMap();
            CreateMap<FavoritesDTO, FavoritesViewModel>().ReverseMap();
            CreateMap<GenreDTO, GenreViewModel>().ReverseMap();
            CreateMap<LoginDTO, LoginViewModel>().ReverseMap();
            CreateMap<MovieDTO, MovieViewModel>().ReverseMap();
            CreateMap<OrderDTO, OrderViewModel>().ReverseMap();
            CreateMap<OrderItemDTO, OrderItemViewModel>().ReverseMap();
            CreateMap<RegisterDTO, RegisterViewModel>().ReverseMap();
            CreateMap<ReviewDTO, ReviewViewModel>().ReverseMap();
        }
    }
}

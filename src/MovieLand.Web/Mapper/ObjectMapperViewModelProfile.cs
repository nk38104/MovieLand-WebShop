using AutoMapper;
using MovieLand.Application.DTOs;
using MovieLand.Application.DTOs.Account;
using MovieLand.Application.DTOs.Director;
using MovieLand.Application.DTOs.Genre;
using MovieLand.Web.ViewModels;
using MovieLand.Web.ViewModels.Account;
using MovieLand.Web.ViewModels.Directors;
using MovieLand.Web.ViewModels.Genres;


namespace MovieLand.Web.Mapper
{
    public class ObjectMapperViewModelProfile : Profile
    {
        public ObjectMapperViewModelProfile()
        {
            CreateMap<CartDTO, CartViewModel>().ReverseMap();
            CreateMap<CartItemDTO, CartItemViewModel>().ReverseMap();
            CreateMap<CompareDTO, CompareViewModel>().ReverseMap();
            CreateMap<CreateMovieDTO, CreateMovieViewModel>().ReverseMap();
            CreateMap<DirectorDTO, DirectorViewModel>().ReverseMap();
            CreateMap<EditMovieDTO, EditMovieViewModel>().ReverseMap();
            CreateMap<FavoritesDTO, FavoritesViewModel>().ReverseMap();
            CreateMap<GenreDTO, GenreViewModel>().ReverseMap();
            CreateMap<LoginDTO, LoginViewModel>().ReverseMap();
            CreateMap<MovieDTO, MovieViewModel>().ReverseMap();
            CreateMap<MovieGenreDTO, MovieGenreViewModel>().ReverseMap();
            CreateMap<MovieDirectorDTO, MovieDirectorViewModel>().ReverseMap();
            CreateMap<OrderDTO, OrderViewModel>().ReverseMap();
            CreateMap<OrderItemDTO, OrderItemViewModel>().ReverseMap();
            CreateMap<RegisterDTO, RegisterViewModel>().ReverseMap();
            CreateMap<ReviewDTO, ReviewViewModel>().ReverseMap();
        }
    }
}

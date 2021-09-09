using AutoMapper;
using MovieLand.Application.DTOs;
using MovieLand.Application.DTOs.Director;
using MovieLand.Application.DTOs.Genre;
using MovieLand.Domain.Entities;


namespace MovieLand.Application.Mapper
{
    public class ObjectMapperProfile : Profile
    {
        public ObjectMapperProfile()
        {
            CreateMap<Cart, CartDTO>().ReverseMap();
            CreateMap<CartItem, CartItemDTO>().ReverseMap();
            CreateMap<Compare, CompareDTO>().ReverseMap();
            CreateMap<Director, DirectorDTO>().ReverseMap();
            CreateMap<Favorite, FavoritesDTO>().ReverseMap();
            CreateMap<Genre, GenreDTO>().ReverseMap();
            CreateMap<Movie, MovieDTO>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title)).ReverseMap();
            CreateMap<Movie, CreateMovieDTO>().ReverseMap();
            CreateMap<Movie, EditMovieDTO>().ReverseMap();
            CreateMap<MovieGenre, MovieGenreDTO>().ReverseMap();
            CreateMap<MovieDirector, MovieDirectorDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
            CreateMap<Review, ReviewDTO>().ReverseMap();
        }
    }
}

using AutoMapper;
using MovieLand.Application.DTOs;
using MovieLand.Domain.Entities;
using System.Threading.Tasks;


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
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
            CreateMap<Review, ReviewDTO>().ReverseMap();
        }
    }
}

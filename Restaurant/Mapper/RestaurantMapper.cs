using AutoMapper;
using Restaurant.Models;
using Restaurant.Models.DTO;

namespace Restaurant.Mapper
{
    public class RestaurantMapper : Profile
    {
        public RestaurantMapper()
        {
            CreateMap<Meal, MealDto>().ReverseMap();
            CreateMap<Meal, MealUpdateDto>().ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();

            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, OrderUpdateDto>().ReverseMap();

            CreateMap<ApplicationUser, RegisterDto>().ReverseMap();
            CreateMap<ApplicationUser, EditProfileDto>().ReverseMap();

            CreateMap<Table, TableDto>().ReverseMap();
            CreateMap<Table, TableUpdateDto>().ReverseMap();    

            CreateMap<Reservation, ReservationDto>().ReverseMap();
            CreateMap<Reservation, ReservationUpdateDto>().ReverseMap();

        }
    }
}

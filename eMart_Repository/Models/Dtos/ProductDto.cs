using AutoMapper;
using eMart_Repository.Entities;
using eMart_Repository.Mapping;

namespace eMart_Repository.Models.Dtos;

public class ProductDto : BaseDto, IMapFrom<Product>
{
    public string ProductName { get; set; } 
    public double Weight { get; set; }  
    public double UnitPrice { get; set; }   
    public int UnitsInStock { get; set; }   
    public int CategoryId { get; set; }
    public CategoryDto Category { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryId,
                opt => opt.MapFrom(src => src.CategoryId));
    }
}
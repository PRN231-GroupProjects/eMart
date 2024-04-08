using eMart_Repository.Entities;
using eMart_Repository.Mapping;

namespace eMart_Repository.Models.Dtos;

public class CategoryDto : BaseDto, IMapFrom<Category>
{
    public string CategoryName { get; set; }
}


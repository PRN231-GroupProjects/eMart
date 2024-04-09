using eMart_Repository.Entities;
using eMart_Repository.Mapping;

namespace eMart_Repository.Models.Dtos;

public class MemberDto :  BaseDto, IMapFrom<Member>
{
    public string Email { get; set; }   
    public string CompanyName { get; set; } 
    public string City { get; set; }    
    public string Country { get; set; }
    public string Password { get; set; }
}
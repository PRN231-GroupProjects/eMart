using AutoMapper;
using eMart_Repository.Entities;
using eMart_Repository.Models;
using eMart_Repository.Models.Dtos;
using eMart_Repository.Repository;
using eMart.Payloads.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eMart.Controllers;

[ApiController]
[Route("/member")]

public class MemberController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public MemberController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Result<Member>>> GetMemberById([FromRoute] int id)
    {
        var member = _unitOfWork.MemberRepository.FindByCondition(x => x.Id == id, null).FirstOrDefault();

        if (member is null)
        {
            return BadRequest("Member is not found");
        }

        return Ok(Result<Member>.Succeed(member));
    }

    [HttpPost]
    public async Task<ActionResult<Result<Member>>> CreateMember([FromBody] CreateMemberRequest request)
    {
        var member = await _unitOfWork.MemberRepository.FindByCondition(x => x.Email == request.Email, null)
            .FirstOrDefaultAsync();

        if (member is not null)
        {
            return BadRequest($"A member with the email '{request.Email}' already exists!");
        }

        var entity = new Member()
        {
            City = request.City,
            Country = request.Country,
            CompanyName = request.CompanyName,
            Email = request.Email,
            Password = request.Password,
        };
        
        try
        {
            _unitOfWork.MemberRepository.Insert(entity);
            _unitOfWork.Save();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal Server Error");
        }

        return Ok(Result<Member>.Succeed(entity));
    }

    [HttpPost("/login")]
    public async Task<ActionResult<Result<int>>> Login([FromBody] LoginRequest request)
    {
        if (request.Email is null || request.Password is null)
        {
            return BadRequest("Please enter your email and password");
        }

        var member = _unitOfWork.MemberRepository
            .FindByCondition(x => x.Email == request.Email && x.Password == request.Password, null).FirstOrDefault();

        if (member is null)
        {
            return BadRequest("Wrong account !");
        }
        // return user id
        return Ok(Result<int>.Succeed(member.Id));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Result<Member>>> UpdateMember([FromBody] UpdateMemberRequest request , [FromRoute] int id)
    {
        var member = await _unitOfWork.MemberRepository.FindByCondition(x => x.Id == id, null)
            .FirstOrDefaultAsync();

        if (member is null)
        {
            return BadRequest("Member's not found !");
        }
        
        member.City = request.City;
        member.CompanyName = request.CompanyName;
        member.Country = request.Country;
        member.Password = request.Password;
        
        try
        {
            _unitOfWork.MemberRepository.Update(member);
            _unitOfWork.Save();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal Server Error");
        }

        return Ok(Result<Member>.Succeed(member));
    }
}
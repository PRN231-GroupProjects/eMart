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
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public ActionResult<Result<List<Product>>> Products()
    {
        var products = _unitOfWork.ProductRepository.Get(pageIndex: 1, pageSize: 10, includeProperties: x => x.Category).ToList();
        return Ok(Result<List<Product>>.Succeed(products));
    }

    [HttpPost]
    public async Task<ActionResult<Result<Product>>>CreateProduct([FromBody] CreateProductRequest request)
    {
        var product = await _unitOfWork.ProductRepository.FindByCondition(x => x.ProductName == request.Name, includeProperties: x=> x.Category)
            .FirstOrDefaultAsync();

        if (product is not null)
        {
            return BadRequest($"A product with the name '{request.Name}' already exists.");
        }

        var category = _unitOfWork.CategoryRepository.GetById(request.CategoryId);
        if (category is  null)
        {
            return BadRequest("Category does not exist");
        }

        var entity = new Product()
        {
            CategoryId = category.Id,
            ProductName = request.Name,
            Weight = request.Weigh,
            UnitPrice = request.UnitPrice,
            UnitsInStock = request.UnitsInStock
        };

        try
        {
            _unitOfWork.ProductRepository.Insert(entity);
            _unitOfWork.Save();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal Server Error");
        }

        return Ok(Result<Product>.Succeed(entity));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Result<Product>>> GetProductById([FromRoute]int id)
    {
        var product = await _unitOfWork.ProductRepository.FindByCondition(x => x.Id == id, includeProperties: x => x.Category).FirstOrDefaultAsync();
        if (product is null)
        {
            return BadRequest("Product does not exist!");
        }

        return Ok(Result<Product>.Succeed(product));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Result<Product>>> UpdateProduct([FromBody] UpdateProductRequest request, [FromRoute] int id)
    {
        var product = await _unitOfWork.ProductRepository.FindByCondition(x => x.Id == id, includeProperties: x=> x.Category).FirstOrDefaultAsync();

        if (product is null)
        {
            return BadRequest("Product does not exist");
        }

        var category = _unitOfWork.CategoryRepository.GetById(request.CategoryId);
        if (category is  null)
        {
            return BadRequest("Category does not exist");
        }

        product.ProductName = request.Name;
        product.Weight = request.Weigh;
        product.UnitPrice = request.UnitPrice;
        product.UnitsInStock = request.UnitsInStock;
        product.CategoryId = request.CategoryId;

        try
        {
            _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.Save();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal Server Error");
        }

        return Ok(Result<Product>.Succeed(product));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] int id)
    {
        var product = await _unitOfWork.ProductRepository.FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
        if (product is null)
        {
            return BadRequest("Product does not exist!");
        }

        try
        {
            _unitOfWork.ProductRepository.Delete(product);
            _unitOfWork.Save();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal Server Error");
        }

        return Ok("Succeed");
    }
}
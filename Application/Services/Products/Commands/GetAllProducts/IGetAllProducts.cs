using Application.Interfaces.Context;
using Common.DTOs;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services.Products.Commands.GetAllProducts
{
    public interface IGetAllProducts
    {
        ResultDto<ProductListDto> Execute();
    }
    public class GetAllProducts : IGetAllProducts
    {
        private readonly IDataBaseContext _context;
        public GetAllProducts(IDataBaseContext context)
        {
            _context = context;
        } 
        public ResultDto<ProductListDto> Execute()
        { 
            return new ResultDto<ProductListDto>()
            {
                Data = new ProductListDto()  { _productList = _context.Products.ToList() },
                Message = "All products information received",
                Status = true
            };
        }
    } 

    public class ProductListDto
    {
        public List<Product> _productList { get; set; }
    }
}

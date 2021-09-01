using Application.Interfaces.Context;
using Common.DTOs;
using Domain.Entities;
using System;
using System.Linq;

namespace Application.Services.Products.Commands.GetAllProducts
{
    public interface IAddProduct
    {
        ResultDto<string> Execute(AddProductDto addCartItemDto);
    }
    public class AddProduct : IAddProduct
    {
        private readonly IDataBaseContext _context;
        public AddProduct(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<string> Execute(AddProductDto addProductDto)
        {
             
            Product product = new Product()
            { 
                Name=addProductDto.Name,
                Price=addProductDto.Price,
                DiscountPrice=addProductDto.DiscountPrice,
                DiscountQuorum=addProductDto.DiscountQuorum,
                IsRemove = false,
                InsertTime = DateTime.Now, 
            };
            _context.Products.Add(product);
            _context.SaveChanges();
            return new ResultDto<string>()
            {
                Data = product.Name,
                Message = "Product created successfully",
                Status = true
            };
        }
    }
    public class AddProductDto
    { 
        public string Name { get; set; }
        public long Price { get; set; }
        public long DiscountQuorum { get; set; }
        public long DiscountPrice { get; set; } 
    }

}
      
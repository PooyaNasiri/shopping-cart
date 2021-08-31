using Application.Interfaces.Context;
using Common.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services.Carts.Queries
{
    public interface IGetAllCarts
    {
        ResultDto<CartListDto> Execute();
    }
    public class GetAllCarts : IGetAllCarts
    {
        private readonly IDataBaseContext _context;

        public GetAllCarts(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<CartListDto> Execute()
        { 
            if (_context.Carts.Count() == 0)
            {
                return new ResultDto<CartListDto>()
                {
                    Message = "No carts found",
                    Status = false
                };
            } 

            CartListDto cartListFinal = new CartListDto()
            {
                carts = new List<CartDto>()
            };

            foreach (var cart in _context.Carts.ToList())
            {
                var cartItem = _context.CartItems.AsNoTracking().Where(ci => ci.CartId == cart.Id)
                .Select(ci => new CartItemDto()
                {
                    CartItemId = ci.Id,
                    ProductPrice = ci.Product.Price,
                    ProductName = ci.Product.Name,
                    InsertTime = ci.InsertTime,
                    Number = ci.Number,
                    TotalPriceItem = ci.Product.Price * ci.Number
                }).ToList();

                CartDto cartDto = new CartDto()
                { 
                    CartId = cart.Id,
                    InsertTime = cart.InsertTime,
                    Items = cartItem,
                    TotalPrice = cartItem.Sum(p => p.TotalPriceItem)
                }; 
                cartListFinal.carts.Add(cartDto);
            }
            
            return new ResultDto<CartListDto>()
            {
                Data = cartListFinal,
                Message = "All carts information received",
                Status = true
            };
        }
    }
    public class CartListDto
    {
        public List<CartDto> carts { get; set; }
    }

        public class CartDto
    {
        public Guid CartId { get; set; }
        public DateTime InsertTime { get; set; }
        public long TotalPrice { get; set; }

        public List<CartItemDto> Items { get; set; }
    }

    public class CartItemDto
    {
        public Guid CartItemId { get; set; }
        public DateTime InsertTime { get; set; }
        public long Number { get; set; }
        public string ProductName { get; set; }
        public long ProductPrice { get; set; }
        public long TotalPriceItem { get; set; }
    }
    
}

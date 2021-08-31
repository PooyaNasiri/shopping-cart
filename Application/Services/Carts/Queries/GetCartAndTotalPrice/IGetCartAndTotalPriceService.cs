using Application.Interfaces.Context;
using Common.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services.Carts.Queries.GetCartAndTotalPrice
{
    public interface IGetCartAndTotalPriceService
    {
        ResultDto<CartDto> Execute(Guid CartId);
    }
    public class GetCartAndTotalPriceService : IGetCartAndTotalPriceService
    {
        private readonly IDataBaseContext _context;

        public GetCartAndTotalPriceService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<CartDto> Execute(Guid CartId)
        {
            var cart = _context.Carts.Find(CartId);
            if (cart == null)
            {
                return new ResultDto<CartDto>()
                {
                    Message = "The requested cart was not found",
                    Status = false
                };
            }
            var cartItem = _context.CartItems.AsNoTracking().Where(ci => ci.CartId == CartId)
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
            return new ResultDto<CartDto>()
            {
                Data = cartDto,
                Message = "Cart information received",
                Status = true
            };
        }
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

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
                    DiscountPrice = ci.Product.DiscountPrice,
                    DiscountQuorum = ci.Product.DiscountQuorum,
                    ProductName = ci.Product.Name,
                    InsertTime = ci.InsertTime,
                   // Number = ci.Number, 

                }).ToList();

            long _totalPrice = 0;
            var products = _context.Products.ToList();
            int prodcutsCount = products.Count;
            int[] productsCountInCart = new int[prodcutsCount];

            for (int i = 0; i < prodcutsCount; i++)
            {
                productsCountInCart[i] = 0;
                for (int j = 0; j < cartItem.Count; j++)
                    if (products[i].Name == cartItem[j].ProductName)
                       productsCountInCart[i]++; 
                _totalPrice += ((productsCountInCart[i] / products[i].DiscountQuorum) * products[i].DiscountPrice) 
                             + ((productsCountInCart[i] % products[i].DiscountQuorum) * products[i].Price);
            }
             

            CartDto cartDto = new CartDto()
            {
                CartId = cart.Id,
                InsertTime = cart.InsertTime,
                Items = cartItem,
                TotalPrice = _totalPrice
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
      //  public long Number { get; set; }
        public string ProductName { get; set; }
        public long ProductPrice { get; set; } 
        public long DiscountQuorum { get; set; }
        public long DiscountPrice { get; set; }
    }
}

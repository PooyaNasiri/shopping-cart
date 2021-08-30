using Application.Interfaces.Context;
using Common.DTOs;
using Domain.Entities;
using System;

namespace Application.Services.CartItems.Commands.AddCartItem
{
    public interface IAddCartItemService
    {
        ResultDto<Guid> Execute(AddCartItemDto addCartItemDto);
    }
    public class AddCartItemService:IAddCartItemService
    {
        private readonly IDataBaseContext _context;

        public AddCartItemService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<Guid> Execute(AddCartItemDto addCartItemDto)
        {
            var product=_context.Producs.Find(addCartItemDto.ProductId);
            if (product == null)
            {
                return new ResultDto<Guid>()
                {
                    Message = "The product was not found",
                    Status = false
                };
            }
            var cart=_context.Carts.Find(addCartItemDto.CartId);
            if (cart == null)
            {
                return new ResultDto<Guid>()
                {
                    Message = "The requested cart was not found",
                    Status = false
                };
            }
            CartItem cartItem = new CartItem()
            {
                Id = Guid.NewGuid(),
                IsRemove = false,
                InsertTime = DateTime.Now,
                Cart=cart,
                CartId=cart.Id,
                Product=product,
                ProductId=product.Id,
                Number=addCartItemDto.Number
            };
            _context.CartItems.Add(cartItem);
            _context.SaveChanges();
            return new ResultDto<Guid>()
            {
                Data=cartItem.CartId,
                Message = "Item created successfully",
                Status = true
            };
        }
    }
    public class AddCartItemDto
    {
        public Guid CartId { get; set; }
        public int ProductId { get; set; }
        public long Number { get; set; }
    }
}

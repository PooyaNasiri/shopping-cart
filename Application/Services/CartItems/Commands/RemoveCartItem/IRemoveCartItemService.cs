using Application.Interfaces.Context;
using Common.DTOs;
using System;

namespace Application.Services.CartItems.Commands.RemoveCartItem
{
    public interface IRemoveCartItemService
    {
        ResultDto Execute(Guid Id);
    }
    public class RemoveCartItemService : IRemoveCartItemService
    {
        private readonly IDataBaseContext _context;

        public RemoveCartItemService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(Guid Id)
        {
            var cartItem = _context.CartItems.Find(Id);
            if (cartItem == null)
            {
                return new ResultDto()
                {
                    Message = "The item was not found",
                    Status = false
                };
            }
            cartItem.IsRemove = true;
            _context.SaveChanges();
            return new ResultDto()
            {
                Message = "The item was deleted ",
                Status = true
            };
        }
    }
}

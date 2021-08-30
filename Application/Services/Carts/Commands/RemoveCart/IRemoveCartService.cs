using Application.Interfaces.Context;
using Common.DTOs;
using System;

namespace Application.Services.Carts.Commands.RemoveCart
{
    public interface IRemoveCartService
    {
        ResultDto Execute(Guid Id); 
    }
    public class RemoveCartService: IRemoveCartService
    {
        private readonly IDataBaseContext _context;

        public RemoveCartService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(Guid Id)
        {
            var cart=_context.Carts.Find(Id);
            if(cart==null)
            {
                return new ResultDto()
                {
                    Message = "The requested cart was not found",
                    Status = false
                };
            }
            cart.IsRemove = true;
            _context.SaveChanges();
            return new ResultDto()
            {
                Message = "Cart was cleared successfully",
                Status = true
            };
        }
    }
}

using Application.Interfaces.Context;
using Common.DTOs;
using Domain.Entities;
using System;

namespace Application.Services.Carts.Commands.AddCart
{
    public interface IAddCartService
    {
        ResultDto<Guid> Execute();
    }
    public class AddCartService:IAddCartService
    {
        private readonly IDataBaseContext _context;

        public AddCartService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<Guid> Execute()
        {
            var Id = Guid.NewGuid();
            Cart cart = new Cart()
            {
                Id=Id,
                IsRemove=false,
                InsertTime=DateTime.Now,
            };
            _context.Carts.Add(cart);
            _context.SaveChanges();
            return new ResultDto<Guid>()
            {
                Data=Id,
                Message= "Cart was created",
                Status=true
            };
        }
    }
}

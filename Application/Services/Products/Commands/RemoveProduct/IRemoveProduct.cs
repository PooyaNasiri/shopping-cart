using Application.Interfaces.Context;
using Common.DTOs;
using System;
using System.Linq;

namespace Application.Services.Products.Commands.GetAllProducts
{
    public interface IRemoveProduct
    {
        ResultDto Execute(int Id);
    }
    public class RemoveProduct : IRemoveProduct
    {
        private readonly IDataBaseContext _context;
        public RemoveProduct(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(int Id)
        {
            var product = _context.Products.Find(Id);
            if (product == null)
            {
                return new ResultDto()
                {
                    Message = "The product was not found",
                    Status = false
                };
            }
            product.IsRemove = true;
            _context.SaveChanges();
            return new ResultDto()
            {
                Message = "The item was deleted ",
                Status = true
            };
        }
         
    }

}

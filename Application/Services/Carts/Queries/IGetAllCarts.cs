using Application.Interfaces.Context;
using Application.Services.Carts.Queries.GetCartAndTotalPrice;
using Common.DTOs;
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
                cartListFinal.carts.Add(new GetCartAndTotalPriceService(_context).Execute(cart.Id).Data);
            
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

}

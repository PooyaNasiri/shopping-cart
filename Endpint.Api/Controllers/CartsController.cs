using Application.Services.Carts.Commands.AddCart;
using Application.Services.Carts.Commands.RemoveCart;
using Application.Services.Carts.Queries;
using Application.Services.Carts.Queries.GetCartAndTotalPrice;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Endpint.Api.Controllers
{
    [Route("api/Cart")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly IAddCartService _addCart;
        private readonly IRemoveCartService _removeCart; 
        private readonly IGetCartAndTotalPriceService _getCart;
        private readonly IGetAllCarts _getAllCart;

        public CartsController(IAddCartService addCart, IRemoveCartService removeCart, IGetCartAndTotalPriceService getCart, IGetAllCarts getAllCart)
        {
            _addCart = addCart;
            _removeCart = removeCart;
            _getCart = getCart;
            _getAllCart = getAllCart;
        }

        [HttpGet("{Id}")]
        public IActionResult Get(Guid Id)
        {
            var result = _getCart.Execute(Id);
            return Ok(result);
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _getAllCart.Execute();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post()
        {
            var result = _addCart.Execute();
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(Guid Id)
        {
            var result = _removeCart.Execute(Id);
            return Ok(result);
        }
    }
}

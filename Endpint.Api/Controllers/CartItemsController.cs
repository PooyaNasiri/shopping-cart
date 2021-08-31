using Application.Services.CartItems.Commands.AddCartItem;
using Application.Services.CartItems.Commands.RemoveCartItem;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Endpint.Api.Controllers
{
    [Route("api/Cart/CartItems")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly IAddCartItemService _addCartItem;
        private readonly IRemoveCartItemService _removeCartItem;

        public CartItemsController(IAddCartItemService addCartItem, IRemoveCartItemService removeCartItem)
        {
            _addCartItem = addCartItem;
            _removeCartItem = removeCartItem;
        }

        [HttpPost]
        public IActionResult Post(AddCartItemDto addCartItem)
        {
            var result=_addCartItem.Execute(addCartItem);
            return Ok(result);
        }
        [HttpDelete("{CartItemId}")]
        public IActionResult Delete(Guid CartItemId)
        {
            var result = _removeCartItem.Execute(CartItemId);
            return Ok(result);
        }
    }
}

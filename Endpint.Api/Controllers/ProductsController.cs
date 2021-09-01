using Application.Services.Products.Commands.GetAllProducts;
using Microsoft.AspNetCore.Mvc;

namespace Endpoint.Api.Controllers
{
    [Route("api/Products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IGetAllProducts _getAllProducts;
        private readonly IRemoveProduct _removeProduct;
        private readonly IAddProduct _addProduct;

        public ProductsController(IGetAllProducts getAllProducts, IRemoveProduct removeProduct, IAddProduct addProduct)
        {
            _getAllProducts = getAllProducts;
            _removeProduct = removeProduct;
            _addProduct = addProduct;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _getAllProducts.Execute();
            return Ok(result);
        }

        [HttpDelete("{ProductId}")]
        public IActionResult Delete(int ProductId)
        {
            var result = _removeProduct.Execute(ProductId);
            return Ok(result);
        }
         
        [HttpPost]
        public IActionResult Add(AddProductDto addProduct)
        {
            var result = _addProduct.Execute(addProduct);
            return Ok(result);
        }
    }
}

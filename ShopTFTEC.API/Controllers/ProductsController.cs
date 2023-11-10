using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopTFTEC.API.DTOs;
using VShop.ProductApi.Roles;
using ShopTFTEC.API.Services;

namespace ShopTFTEC.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
    {
        var produtosDto = await _productService.GetProducts();
        if (produtosDto == null)
        {
            return NotFound("Produto não encontrado!");
        }
        return Ok(produtosDto);
    }

    [HttpGet("{id}", Name = "GetProduct")]
    public async Task<ActionResult<ProductDTO>> Get(int id)
    {
        var produtoDto = await _productService.GetProductById(id);
        if (produtoDto == null)
        {
            return NotFound("Produto não encontrado");
        }
        return Ok(produtoDto);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProductDTO produtoDto)
    {
        if (produtoDto == null)
            return BadRequest("Data Inválida");

        await _productService.AddProduct(produtoDto);

        return new CreatedAtRouteResult("GetProduct",
            new { id = produtoDto.Id }, produtoDto);
    }

    [HttpPut]
    public async Task<ActionResult<ProductDTO>> Put([FromBody] ProductDTO produtoDto)
    {
        if (produtoDto == null)
            return BadRequest("Data Inválida");

        await _productService.UpdateProduct(produtoDto);

        return Ok(produtoDto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ProductDTO>> Delete(int id)
    {
        var produtoDto = await _productService.GetProductById(id);

        if (produtoDto == null)
        {
            return NotFound("Produto Não encontrado!");
        }

        await _productService.RemoveProduct(id);

        return Ok(produtoDto);
    }
}
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopTFTEC.WebApp.Models;
using ShopTFTEC.WebApp.Services.Contracts;

namespace ShopTFTEC.WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService _productService;
    private readonly ICartService _cartService;
    private readonly ICategoryService _categoryService;

    public HomeController(ILogger<HomeController> logger,
        IProductService productService,
        ICartService cartService,
        ICategoryService categoryService)
    {
        _logger = logger;
        _productService = productService;
        _cartService = cartService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllProducts();

        if (products is null)
        {
            return View("Error");
        }

        return View(products);
    }

    [HttpGet]
    public async Task<ActionResult<ProductViewModel>> ProductDetails(int id)
    {
        var product = await _productService.FindProductById(id);


        if (product is null)
            return View("Error");

        return View(product);
    }

    [HttpPost]
    [ActionName("ProductDetails")]
    [Authorize]
    public async Task<ActionResult<ProductViewModel>> ProductDetailsPost
        (ProductViewModel productVM)
    {
        CartViewModel cart = new()
        {
            CartHeader = new CartHeaderViewModel
            {
                UserId = User.Claims.FirstOrDefault().Value
            }
        };

        CartItemViewModel cartItem = new()
        {
            Quantity = productVM.Quantity,
            ProductId = productVM.Id,
            Product = await _productService.FindProductById(productVM.Id)
        };

        List<CartItemViewModel> cartItemsVM = new List<CartItemViewModel>();
        cartItemsVM.Add(cartItem);
        cart.CartItems = cartItemsVM;

        var result = await _cartService.AddItemToCartAsync(cart);

        if (result is not null)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(productVM);
    }

    //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    //public IActionResult Error(string message)
    //{
    //    return View(new ErrorViewModel { ErrorMessage = message });
    //}

    [Authorize]
    public async Task<IActionResult> Login()
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        return RedirectToAction(nameof(Index));
    }
    public IActionResult Logout()
    {
        return SignOut("Cookies", "oidc");
    }
}
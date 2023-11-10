using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;
using ShopTFTEC.WebApp.Models;
using ShopTFTEC.WebApp.Services.Contracts;

namespace ShopTFTEC.Admin.Controllers
{
    public class AdminCartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ICouponService _couponService;


        public AdminCartController(ICartService cartService, ICouponService couponService)
        {
            _cartService = cartService;
            _couponService = couponService;
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            CartViewModel? cartVM = await GetCartByUser();
            return View(cartVM);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CartViewModel cartVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _cartService.CheckoutAsync(cartVM.CartHeader);

                if (result is not null)
                {
                    return RedirectToAction(nameof(CheckoutCompleted));
                }
            }
            return View(cartVM);
        }

        [HttpGet]
        public IActionResult CheckoutCompleted()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(CartViewModel cartVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _cartService.ApplyCouponAsync(cartVM);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCoupon()
        {
            var result = await _cartService.RemoveCouponAsync(GetUserId());

            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Nome")
        {
            CartViewModel? cartVM = await get();

            if(cartVM is null)
            {
                ModelState.AddModelError("CartNotFound", "Não existe nenhum produto adicionado ao carrinho!");
                return View("/Views/Cart/CartNotFound.cshtml");
            }

            var model = await PagingList.CreateAsync(cartVM, 3, pageindex, sort, "Nome");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };

            return View(cartVM);
        }



        private async Task<CartViewModel?> GetCartByUser()
        {

            var cart = await _cartService.GetCartByUserIdAsync(GetUserId());

            if(cart?.CartHeader is not null)
            {
                if (!string.IsNullOrEmpty(cart.CartHeader.CouponCode))
                {
                    var coupon = await _couponService.GetDiscountCoupon(cart.CartHeader.CouponCode);
                    if (coupon?.CouponCode is not null)
                    {
                        cart.CartHeader.Discount = coupon.Discount;
                    }
                }
                foreach (var item in cart.CartItems)
                {
                    cart.CartHeader.TotalAmount += (item.Product.Price * item.Quantity);
                }

                cart.CartHeader.TotalAmount = cart.CartHeader.TotalAmount - 
                                             (cart.CartHeader.TotalAmount * 
                                              cart.CartHeader.Discount) / 100;
            }
            return cart;
        }

        public async Task<IActionResult> RemoveItem(int id)
        {
            var result = await _cartService.RemoveItemFromCartAsync(id);

            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(id);
        }

        private async Task<string> GetAccessToken()
        {
            return await HttpContext.GetTokenAsync("access_token");
        }

        private string GetUserId()
        {
            return User.Claims.FirstOrDefault()?.Value;
        }
    }
}

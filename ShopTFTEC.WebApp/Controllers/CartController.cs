using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopTFTEC.WebApp.Models;
using ShopTFTEC.WebApp.Services;
using ShopTFTEC.WebApp.Services.Contracts;

namespace ShopTFTEC.WebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ICouponService _couponService;


        public CartController(ICartService cartService, ICouponService couponService)
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
                var getCar = await GetCartByUser();

                if (result is not null)
                {

                    if (getCar.CartItems.Count() > 0)
                    {
                        foreach (var item in getCar.CartItems)
                        {
                            await _cartService.RemoveItemFromCartAsync(item.Id);
                        }
                    }

                    return View("~/Views/Cart/CheckoutCompleted.cshtml", cartVM);
                }
            }
            return View(cartVM);
        }

        [HttpGet]
        public IActionResult CheckoutCompleted(CartViewModel cartVM)
        {
            
            return View(cartVM);
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
        public async Task<IActionResult> Index()
        {
            CartViewModel? cartVM = await GetCartByUser();

            if(cartVM is null)
            {
                ModelState.AddModelError("CartNotFound", "Não existe nenhum produto adicionado ao carrinho!");
                return View("/Views/Cart/CartNotFound.cshtml");
            }

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

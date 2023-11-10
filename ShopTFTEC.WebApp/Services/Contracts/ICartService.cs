using ShopTFTEC.WebApp.Models;

namespace ShopTFTEC.WebApp.Services.Contracts;

public interface ICartService
{
    Task<CartViewModel> GetCartByUserIdAsync(string userId);
    Task<CartViewModel> AddItemToCartAsync(CartViewModel cartVM);
    Task<CartViewModel> UpdateCartAsync(CartViewModel cartVM);
    Task<bool> RemoveItemFromCartAsync(int cartId);

    //implementação futura
    Task<bool> ApplyCouponAsync(CartViewModel cartVM);
    Task<bool> RemoveCouponAsync(string userId);
    Task<bool> ClearCartAsync(string userId);

    Task<CartHeaderViewModel> CheckoutAsync(CartHeaderViewModel cartHeader);
}

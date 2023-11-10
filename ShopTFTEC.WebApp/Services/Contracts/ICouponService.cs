using ShopTFTEC.WebApp.Models;

namespace ShopTFTEC.WebApp.Services.Contracts;

public interface ICouponService
{
    Task<CouponViewModel> GetDiscountCoupon(string couponCode);
}

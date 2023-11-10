using ShopTFTEC.API.DTOs;

namespace ShopTFTEC.API.Repositories;

public interface ICouponRepository
{
    Task<CouponDTO> GetCouponByCode(string couponCode);
}

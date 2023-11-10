using System.Net.Http.Headers;
using System.Text.Json;
using ShopTFTEC.WebApp.Models;
using ShopTFTEC.WebApp.Services.Contracts;

namespace ShopTFTEC.WebApp.Services;

public class CouponService : ICouponService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly JsonSerializerOptions? _options;
    private const string apiEndpoint = "/api/coupon";
    private CouponViewModel couponVM = new CouponViewModel();

    public CouponService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<CouponViewModel> GetDiscountCoupon(string couponCode)
    {
        var client = _clientFactory.CreateClient("DiscountApi");

        using (var response = await client.GetAsync($"{apiEndpoint}/{couponCode}"))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                couponVM = await JsonSerializer.DeserializeAsync<CouponViewModel>
                              (apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return couponVM;
    }

    private static void PutTokenInHeaderAuthorization(string token, HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
    }

}

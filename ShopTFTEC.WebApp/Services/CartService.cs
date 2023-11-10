using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ShopTFTEC.WebApp.Models;
using ShopTFTEC.WebApp.Services.Contracts;

namespace ShopTFTEC.WebApp.Services;

public class CartService : ICartService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly JsonSerializerOptions? _options;
    private const string apiEndpoint = "/api/cart";
    private CartViewModel cartVM = new CartViewModel();
    private CartHeaderViewModel cartHeaderVM = new CartHeaderViewModel();

    public CartService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<CartViewModel> GetCartByUserIdAsync(string userId)
    {
        var client = _clientFactory.CreateClient("CartApi");

        using (var response = await client.GetAsync($"{apiEndpoint}/getcart/{userId}"))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                cartVM = await JsonSerializer
                              .DeserializeAsync<CartViewModel>
                              (apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return cartVM;
    }

    public async Task<CartViewModel> AddItemToCartAsync(CartViewModel cartVM)
    {
        var client = _clientFactory.CreateClient("CartApi");


        StringContent content = new StringContent(JsonSerializer.Serialize(cartVM),
                                                Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync($"{apiEndpoint}/addcart/", content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                cartVM = await JsonSerializer
                           .DeserializeAsync<CartViewModel>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return cartVM;
    }


    public async Task<CartViewModel> UpdateCartAsync(CartViewModel cartVM)
    {
        var client = _clientFactory.CreateClient("CartApi");

        CartViewModel cartUpdated = new CartViewModel();

        using (var response = await client.PutAsJsonAsync($"{apiEndpoint}/updatecart/", cartVM))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                cartUpdated = await JsonSerializer
                                  .DeserializeAsync<CartViewModel>
                                  (apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return cartUpdated;
    }

    public async Task<bool> RemoveItemFromCartAsync(int cartId)
    {
        var client = _clientFactory.CreateClient("CartApi");

        using (var response = await client.DeleteAsync($"{apiEndpoint}/deletecart/" + cartId))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
        }
        return false;
    }

    private static void PutTokenInHeaderAuthorization(string token, HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
    }


    public Task<bool> ClearCartAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ApplyCouponAsync(CartViewModel cartVM)
    {
        var client = _clientFactory.CreateClient("CartApi");

        StringContent content = new StringContent(JsonSerializer.Serialize(cartVM),
                                         Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync($"{apiEndpoint}/applycoupon/", content))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
        }

        return false;

    }

    public async Task<bool> RemoveCouponAsync(string userId)
    {
        var client = _clientFactory.CreateClient("CartApi");

        using (var response = await client.DeleteAsync($"{apiEndpoint}/deletecoupon/{userId}"))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
        }

        return false;

    }
    public async Task<CartHeaderViewModel> CheckoutAsync(CartHeaderViewModel cartHeaderVM)
    {
        var client = _clientFactory.CreateClient("CartApi");

        StringContent content = new StringContent(JsonSerializer.Serialize(cartHeaderVM),
                                             Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync($"{apiEndpoint}/checkout/", content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                cartHeaderVM = await JsonSerializer
                              .DeserializeAsync<CartHeaderViewModel>
                              (apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return cartHeaderVM;

    }
}

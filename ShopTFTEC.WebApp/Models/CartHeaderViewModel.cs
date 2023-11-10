using System.ComponentModel.DataAnnotations;

namespace ShopTFTEC.WebApp.Models;

public class CartHeaderViewModel
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    [Display(Name = "Cupom")]
    public string CouponCode { get; set; } = string.Empty;

    [Display(Name = "Total")]
    public decimal TotalAmount { get; set; } = 0.00m;

    //desconto
    [Display(Name = "Desconto")]
    public decimal Discount { get; set; } = 0.00m;

    //checkout
    [Display(Name = "Nome")]
    public string FirstName { get; set; } = string.Empty;
    [Display(Name = "Ultimo Nome")]
    public string LastName { get; set; } = string.Empty;
    public DateTime DateTime { get; set; }
    [Display(Name = "Telefone")]
    public string Telephone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    [Display(Name = "Cartão")]
    public string CardNumber { get; set; } = string.Empty;
    [Display(Name = "Nome do Cartão")]
    public string NameOnCard { get; set; } = string.Empty;
    public string CVV { get; set; } = string.Empty;
    [Display(Name = "Expiração")]
    public string ExpireMonthYear { get; set; } = string.Empty;
}

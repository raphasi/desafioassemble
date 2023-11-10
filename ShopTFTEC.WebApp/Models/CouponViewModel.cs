using System.ComponentModel.DataAnnotations;

namespace ShopTFTEC.WebApp.Models;

public class CouponViewModel
{
    public long Id { get; set; }
    [Display(Name = "Cupom")]
    public string? CouponCode { get; set; }
    [Display(Name = "Desconto")]
    public decimal Discount { get; set; }
}

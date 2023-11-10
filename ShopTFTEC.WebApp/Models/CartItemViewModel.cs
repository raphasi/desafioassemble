using System.ComponentModel.DataAnnotations;

namespace ShopTFTEC.WebApp.Models;

public class CartItemViewModel
{
    public int Id { get; set; }
    [Display(Name = "Produto")]
    public ProductViewModel? Product { get; set; }
    [Display(Name = "Quantidade")]
    public int Quantity { get; set; } = 1;
    public int ProductId { get; set; }
    public int CartHeaderId { get; set; }
}

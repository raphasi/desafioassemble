using System.ComponentModel.DataAnnotations;

namespace ShopTFTEC.WebApp.Models;
public class CategoryViewModel
{
    public int CategoryId { get; set; }
    [Required]
    [Display(Name = "Nome da Categoria")]
    public string? Name { get; set; }

    public IEnumerable<ProductViewModel>? Products { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace ShopTFTEC.WebApp.Models;

public class ProductViewModel
{
    public int Id { get; set; }
    [Required]
    [Display(Name = "Nome do Produto")]
    public string? Name { get; set; }
    [Required]
    [Display(Name = "Descrição")]
    public string? Description { get; set; }

    [Required]
    [Range(1,9999)]
    [Display(Name = "Valor")]
    public decimal Price { get; set; }
    [Required]
    [Display(Name = "Image URL")]
    public string? ImageURL { get; set; }
    [Required]
    [Range(1,9999)]
    [Display(Name = "Quantidade")]
    public long Stock { get; set; }
    [Display(Name = "Nome da Categoria")]
    public string? CategoryName { get; set; }

    [Range(1, 100)]
    public int Quantity { get; set; } = 1;

    [Display(Name="Categoria")]
    public int CategoryId { get; set; }
}

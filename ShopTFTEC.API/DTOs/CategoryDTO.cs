using System.ComponentModel.DataAnnotations;
using ShopTFTEC.API.Models;

namespace ShopTFTEC.API.DTOs;

public class CategoryDTO
{
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "O Nome é obrigatório")]
    [MinLength(3)]
    [MaxLength(100)]
    public string? Name { get; set; }

    public ICollection<ProductDTO>? Products { get; set; }
}

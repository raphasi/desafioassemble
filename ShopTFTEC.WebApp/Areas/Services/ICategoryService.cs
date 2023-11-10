using ShopTFTEC.WebApp.Models;

namespace ShopTFTEC.WebApp.Areas.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryViewModel>> GetAllCategories(string token);
    Task<CategoryViewModel> GetCategoryById(int id, string token);
    Task<CategoryViewModel> Create(CategoryViewModel productVM, string token);
    Task<CategoryViewModel> Update(CategoryViewModel productVM, string token);
    Task<bool> DeleteCategorytById(int id, string token);
}

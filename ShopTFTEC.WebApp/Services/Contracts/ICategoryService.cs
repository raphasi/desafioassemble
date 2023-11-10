
using ShopTFTEC.WebApp.Models;

namespace ShopTFTEC.WebApp.Services.Contracts;

public interface ICategoryService
{
    Task<IEnumerable<CategoryViewModel>> GetAllCategories();
    Task<IEnumerable<CategoryViewModel>> GetCategoriesProducts();
    Task<CategoryViewModel> GetCategoryById(int id);
    Task<CategoryViewModel> GetCategoryByName(string name);
    Task<CategoryViewModel> Create(CategoryViewModel productVM);
    Task<CategoryViewModel> Update(CategoryViewModel productVM);
    Task<bool> DeleteCategorytById(int id);
}

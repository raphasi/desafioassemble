using Microsoft.AspNetCore.Mvc;
using ShopTFTEC.WebApp.Services.Contracts;

namespace ShopTFTEC.WebApp.Components
{
    public class CategoriaMenu : ViewComponent
    {
        private readonly ICategoryService _categoriaRepository;

        public CategoriaMenu(ICategoryService categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categorias = _categoriaRepository.GetAllCategories().Result;
            return View(categorias);
        }
    }
}

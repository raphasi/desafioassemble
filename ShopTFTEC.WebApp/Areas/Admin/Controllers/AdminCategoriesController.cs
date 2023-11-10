using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopTFTEC.WebApp.Models;
using ShopTFTEC.WebApp.Services.Contracts;
using ShopTFTEC.WebApp.Context;
using Microsoft.AspNetCore.Identity;

namespace ShopTFTEC.WebApp.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminCategoriesController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private UserManager<ApplicationUser> _userManager;
        public AdminCategoriesController(IProductService productService,
                                ICategoryService categoryService,
                                IWebHostEnvironment webHostEnvironment,
                                 UserManager<ApplicationUser> userManager)
        {
            _productService = productService;
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryViewModel>>> Index()
        {

            var result = await _categoryService.GetAllCategories();

            if (result is null)
                return View("Error");

            return View(result);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(), "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.Create(productVM);

                if (result != null)
                    return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.CategoryId = new SelectList(await
                                     _categoryService.GetAllCategories(), "CategoryId", "Name");
            }
            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewModel catVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.Update(catVM);

                if (result is not null)
                    return RedirectToAction(nameof(Index));
            }
            return View(catVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _categoryService.GetCategoryById(id);

            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpGet]
        public async Task<ActionResult<CategoryViewModel>> Delete(int id)
        {
            var result = await _categoryService.GetCategoryById(id);

            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpPost(), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _categoryService.DeleteCategorytById(id);

            if (!result)
                return View("Error");

            return RedirectToAction("Index");
        }
        private async Task<string> GetAccessToken()
        {
            return await HttpContext.GetTokenAsync("access_token");
        }
    }
}

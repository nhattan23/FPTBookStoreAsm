using FPTBookStore.Models;
using FPTBookStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FPTBookStore.ViewComponents
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly GenreTypeRepository _category;
        public CategoryMenuViewComponent(GenreTypeRepository genreTypeRepository)
        {
            _category = genreTypeRepository;
        }

        public IViewComponentResult Invoke()
        {
            var category = _category.GetAllCategories().OrderBy(x => x.CategoriesName);
            return View(category);
        }
    }
}

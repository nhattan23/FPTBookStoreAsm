using FPTBookStore.Data;
using FPTBookStore.Models;

namespace FPTBookStore.Repository
{
    public class CategoryRepository : GenreTypeRepository
    {
        public readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Categories Add(Categories category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public Categories Delete(long IdCategory)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Categories> GetAllCategories()
        {
            return _context.Categories;
        }

        public Categories GetCate(long IdCategories)
        {
            return _context.Categories.Find(IdCategories);
        }


        public Categories Update(Categories category)
        {
            _context.Update(category);
            _context.SaveChanges();
            return category;
        }
    }
}

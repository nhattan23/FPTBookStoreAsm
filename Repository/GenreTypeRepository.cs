using FPTBookStore.Models;
namespace FPTBookStore.Repository
{
    public interface GenreTypeRepository
    {
        Categories Add(Categories category);
        Categories Update(Categories category);
        Categories Delete(long IdCategory);
        Categories GetCate(long IdCategories);
        IEnumerable<Categories> GetAllCategories();

    }
}

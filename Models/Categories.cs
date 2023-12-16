using System.ComponentModel.DataAnnotations;

namespace FPTBookStore.Models
{
    public class Categories
    {
        [Key]
        public long IdCat { get; set; }
        

        public string? CategoriesName { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FPTBookStore.Models
{
    public class Book
    {
        [Key]
        public long IdBook { get; set; }
        
        
        public string? BookName { get; set; }
        
        public string? Image { get; set; }
        
        public long? Genre { get; set; }
        public string? Author { get; set; }
        
        
        public string? Description { get; set; }
        
        public double Price { get; set; }
        public int? PageSize { get; set; }
    }
}

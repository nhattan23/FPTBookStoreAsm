using System.ComponentModel.DataAnnotations;

namespace FPTBookStore.Models
{
    public class OrderStatus
    {
        [Key]
        public long Id { get; set; }
        
        public string? status { get; set; }
    }
}

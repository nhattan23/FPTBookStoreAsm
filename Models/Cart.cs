using System.ComponentModel.DataAnnotations;

namespace FPTBookStore.Models
{
    public class Cart
    {
        [Key]
        public long IdCart { get; set; }
        
        public long? IdBook { get; set; }
        public long? UserID { get; set; }
        public long? NumberOfProducts { get; set; }
        public long? TotalPrice { get; set; }
        public long? status { get; set; }
    }
}

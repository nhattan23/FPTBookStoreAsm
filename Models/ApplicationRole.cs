using Microsoft.AspNetCore.Identity;

namespace FPTBookStore.Models
{
    public class ApplicationRole:IdentityRole
    {
        public string? Descriptions {  get; set; }
    }
}

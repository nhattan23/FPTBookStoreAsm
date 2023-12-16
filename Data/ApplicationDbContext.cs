using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FPTBookStore.Models;

namespace FPTBookStore.Data
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<FPTBookStore.Models.Book> Book { get; set; } = default!;
        public DbSet<FPTBookStore.Models.Categories> Categories { get; set; } = default!;
        public DbSet<FPTBookStore.Models.Cart> Cart { get; set; } = default!;
        public DbSet<FPTBookStore.Models.ApplicationRole> ApplicationRole { get; set; } = default!;
    }
}
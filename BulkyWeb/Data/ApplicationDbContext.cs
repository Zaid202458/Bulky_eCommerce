using BulkyWeb.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace BulkyWeb.Data
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
    }
}

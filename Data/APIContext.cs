using Microsoft.EntityFrameworkCore;
using CrudUserAPI.Models;

namespace CrudUserAPI.Data
{
    public class APIContext : DbContext
    {
        public APIContext(DbContextOptions<APIContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!; // ✅ Asegurar que se llame Users
    }
}

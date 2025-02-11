using Microsoft.EntityFrameworkCore;
using CrudUserAPI.Models;

namespace CrudUserAPI.Data
{
    public class APIContext : DbContext
    {
        public DbSet<User> Usuarios { get; set; } = null!;
        public APIContext(DbContextOptions<APIContext> options) : base(options)
        {
        }

    }
}

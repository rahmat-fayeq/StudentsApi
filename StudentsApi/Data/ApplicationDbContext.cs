using Microsoft.EntityFrameworkCore;
using StudentsApi.Model;

namespace StudentsApi.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}

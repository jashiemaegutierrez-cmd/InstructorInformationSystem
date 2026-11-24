using Microsoft.EntityFrameworkCore;
using InstructorInformationsSystem.Models;

namespace InstructorInformationsSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserAccount> Admins { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
    }
}
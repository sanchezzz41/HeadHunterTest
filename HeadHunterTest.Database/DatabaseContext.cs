using HeadHunterTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HeadHunterTest.Database
{
    /// <summary>
    /// Контекст базы данных для HHT
    /// </summary>
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> opt) : base(opt)
        {
        }

        public DbSet<User> Users { get; set; }


        public DbSet<Employer> Employers { get; set; }
        public DbSet<Vacancies> Vacancieses { get; set; }


        public DbSet<JobSeeker> JobSeekers { get; set; }
        public DbSet<Resume> Resumes { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<ProfessionalArea> ProfessionalAreas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
    }
}

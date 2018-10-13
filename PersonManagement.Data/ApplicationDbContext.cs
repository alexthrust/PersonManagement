using Microsoft.EntityFrameworkCore;
using PersonManagement.Data.DbModels;

namespace PersonManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Person.Persons");

                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.FirstName).HasColumnName("FirstName");
                entity.Property(e => e.LastName).HasColumnName("LastName");
                entity.Property(e => e.PersonalNumber).HasColumnName("PersonalNumber");
                entity.Property(e => e.Birthdate).HasColumnName("Birthdate");
                entity.Property(e => e.Gender).HasColumnName("Gender");
                entity.Property(e => e.Salary).HasColumnName("Salary");
            });
        }
    }
}
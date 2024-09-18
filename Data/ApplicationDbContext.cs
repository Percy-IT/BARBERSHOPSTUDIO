using BARBERSHOP_STUDIO;
using BARBERSHOP_STUDIO.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BARBERSHOPSTUDIO.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Barber> Barbers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Service> Services { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Service>().HasData(
                new Service { Id = 1, Name = "Haircut", Price = 80.00m },
                new Service { Id = 2, Name = "WomenHaircut", Price = 90.00m },
                new Service { Id = 3, Name = "Tattoos", Price = 200.00m },
                new Service { Id = 4, Name = "EarPiercing", Price = 30.00m }

            );

            modelBuilder.Entity<Barber>().HasData(
       new Barber { BarberId = 1, Name = "Anathi #1", Email="Barber1@gmail.com", PhoneNumber = "0812345678" },
       new Barber { BarberId = 2, Name = "Ntokozo #2", Email = "Barber2@gmail.com", PhoneNumber = "0812345677" },
       new Barber { BarberId = 3, Name = "Lindi #3", Email = "Barber3@gmail.com", PhoneNumber = "0812345676" }
   );

        }
    }
}

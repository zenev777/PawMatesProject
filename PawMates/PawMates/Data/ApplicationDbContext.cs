using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PawMates.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace PawMates.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EventParticipant>()
                .HasKey(ep => new { ep.EventId, ep.HelperId });

            modelBuilder.Entity<EventParticipant>()
                .HasOne(e => e.Event)
                .WithMany(e => e.EventParticipants)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<PetType>()
                .HasData(new PetType()
                {
                    Id = 1,
                    Name = "Dog"
                },
                new PetType()
                {
                    Id = 2,
                    Name = "Cat"
                },
                new PetType()
                {
                    Id = 3,
                    Name = "Fish"
                },
                new PetType()
                {
                    Id = 4,
                    Name = "Bird"
                },
                new PetType()
                {
                    Id = 5,
                    Name = "Hamster"
                },
                new PetType()
                {
                    Id = 6,
                    Name = "Rabbit"
                },
                new PetType()
                {
                    Id = 7,
                    Name = "Reptiles"
                });
        }


        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetType> PetTypes { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
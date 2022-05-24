using Microsoft.EntityFrameworkCore;
using ERCTestApi.Models;
namespace ERCTestApi
{
   

    public class ApplicationContext : DbContext
    {
        public DbSet<PersonalAccounts> PersonalAccounts => Set<PersonalAccounts>();
        public DbSet<Address> Address => Set<Address>();
        public DbSet<Person> Persons => Set<Person>();
        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=ERCData.db");
           
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var address = new Address
                {
                    City = "Екатеринбург",
                    Building = 1,
                    Street = "Пушкина",
                    Flat = 10,
                    Id = 1
                };
            var client = new Person
            {
                Id = 1,
                Name = "Никита",
                Surname = "Козырев",
                Patronymic ="Викторович"
            };
            PersonalAccounts acc = new PersonalAccounts
            {
                ClientId = client.Id,
                Id = 1,
                AddressId = address.Id,
                Square = 20,
                OpenDate = DateTime.Now,
                ResidentsNumber = 2
            };
            modelBuilder.Entity<Person>().HasData(client);
            modelBuilder.Entity<Address>().HasData(address);
            modelBuilder.Entity<PersonalAccounts>().HasData(acc);
        }
    }
}

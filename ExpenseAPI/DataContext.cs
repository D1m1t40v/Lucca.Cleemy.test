using ExpenseAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseAPI
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Stark", SurName = "Anthony", Currency = "USD" }, 
                new User { Id = 2, Name = "Romanova", SurName = "Natasha", Currency = "RUB" }
                );
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Expense> Expenses { get; set; }
    }
}

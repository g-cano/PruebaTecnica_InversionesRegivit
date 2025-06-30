using Microsoft.EntityFrameworkCore;
using PruebaTecnicaInversionesRegivit.Shared.Models;

namespace PruebaTecnicaInversionesRegivit.Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasOne(c => c.CreatedBy)
                .WithMany(u => u.ClientsCreated)
                .HasForeignKey(c => c.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.CreatedBy)
                .WithMany(u => u.AccountsCreated)
                .HasForeignKey(a => a.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.CreatedBy)
                .WithMany(u => u.TransactionsCreated)
                .HasForeignKey(t => t.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
               .HasOne(t => t.TransactionType)
               .WithMany(tt => tt.Transactions)
               .HasForeignKey(t => t.TransactionTypeId)
               .OnDelete(DeleteBehavior.Restrict);

           
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Password = "admin123", 
                    IsActive = true,
                    Role = "ADMIN"
                }
            );

            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    Id = 1,
                    Name = "Cliente Corporativo",
                    Identification = "0101199909876",
                    CreatedById = 1, 
                    CreatedAt = DateTime.UtcNow,
                },
                new Client
                {
                    Id = 2,
                    Name = "Cliente Individual",
                    Identification = "0101199909876",
                    CreatedById = 1,
                    CreatedAt = DateTime.UtcNow,
                }
            );

            modelBuilder.Entity<TransactionType>().HasData(
                new TransactionType
                {
                    Id = 1,
                    Name = "Depósito",
                    Code = "DEP",
                    Description = "Ingreso de fondos a la cuenta",
                },
                new TransactionType
                {
                    Id = 2,
                    Name = "Retiro",
                    Code = "RET",
                    Description = "Extracción de fondos de la cuenta",
                }
                
            );

        }
    }
}

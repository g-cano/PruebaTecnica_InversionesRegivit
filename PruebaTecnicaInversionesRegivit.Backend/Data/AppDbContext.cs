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

        }
    }
}

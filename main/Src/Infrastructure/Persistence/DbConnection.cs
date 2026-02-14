using Microsoft.EntityFrameworkCore;
using PicPay.Infrastructure.Persistence.Schemas;

namespace PicPay.Infrastructure.Persistence;

public class DbConnection : DbContext
{
    public DbSet<UserSchema> userSchema { get; set; }
    public DbSet<TransactionSchema> transactionSchema { get; set; }

    public DbConnection(DbContextOptions<DbConnection> opt) : base(opt)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigUser(modelBuilder);
        ConfigTransaction(modelBuilder);
    }

    private static void ConfigUser(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserSchema>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.Property(u => u.Id)
                .IsRequired();

            entity.Property(u => u.Email)
                .IsRequired();

            entity.Property(u => u.Cpf)
                .IsRequired();
        });
    }

    private static void ConfigTransaction(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TransactionSchema>(entity =>
        {
            entity.HasKey(t => t.Id);

            entity.Property(t => t.FkPayer)
                .IsRequired();

            entity.Property(t => t.FkPayee)
                .IsRequired();

            // Relação Payer (quem paga)
            entity.HasOne(t => t.Payer)
                .WithMany(u => u.TransactionsPaid)
                .HasForeignKey(t => t.FkPayer)
                .OnDelete(DeleteBehavior.Restrict);

            // Relação Payee (quem recebe)
            entity.HasOne(t => t.Payee)
                .WithMany(u => u.TransactionsReceived)
                .HasForeignKey(t => t.FkPayee)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
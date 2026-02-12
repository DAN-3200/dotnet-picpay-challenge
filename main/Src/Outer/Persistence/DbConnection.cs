using Microsoft.EntityFrameworkCore;
using PicPay.Outer.Persistence.Schemas;

namespace PicPay.Outer.Persistence;

public class DbConnection : DbContext
{
    public DbSet<UserSchema> userSchema { get; set; }
    public DbSet<TransactionSchema> transactionSchema { get; set; }
    
    public DbConnection(DbContextOptions<DbConnection> opt) : base(opt) {}
}
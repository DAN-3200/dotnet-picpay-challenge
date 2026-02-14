using Microsoft.EntityFrameworkCore;
using PicPay.Infrastructure.Persistence.Schemas;
using PicPay.Domain.Entity;
using PicPay.Application.Ports;

namespace PicPay.Infrastructure.Persistence.Repository;

public class PaymentRepo(DbConnection db) : IPaymentRepo
{
    public async Task<bool> ConfirmTransaction(TransactionEntity info)
    {
        var payer =  await db.userSchema.FirstAsync(i => i.Id ==  info.FkPayer);
        payer.Balance -= info.Value;
        var payee = await db.userSchema.FirstAsync(i => i.Id ==  info.FkPayee);
        payee.Balance += info.Value;
        await db.transactionSchema.AddAsync(TransactionSchema.ToSchema(info));
        await db.SaveChangesAsync();
        return true;
    }

    public async Task ConfirmDeposit(UserEntity user)
    {
        var userData = await db.userSchema.FirstAsync(i => i.Id == user.Id);
        userData.Balance = user.Balance;
        await db.SaveChangesAsync();
    }

    public async Task<bool> Refund(string id)
    {
        var transaction = await db.transactionSchema.FirstAsync(i => i.Id == id);
        if (transaction.Type == TypeTransaction.REFUND) throw new ArgumentException("Extorno já efetuado");
        
        transaction.Type = TypeTransaction.REFUND;
        
        var payer =  await db.userSchema.FirstAsync(i => i.Id ==  transaction.FkPayer);
        payer.Balance += transaction.Value;
        var payee = await db.userSchema.FirstAsync(i => i.Id ==  transaction.FkPayee);
        payee.Balance -= transaction.Value;
        
        await db.SaveChangesAsync();
        
        return true;
    }

    public async Task<List<TransactionEntity>?> ListTransactions()
    {
        var result = await db.transactionSchema.AsNoTracking().ToListAsync();
        return result.Select(TransactionSchema.ToEntity).ToList();
    }
}
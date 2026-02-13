using PicPay.Inner.Entity;
using PicPay.Inner.Ports;
using PicPay.Outer.Persistence.Schemas;

namespace PicPay.Outer.Persistence.Repository;

public class PaymentRepo(DbConnection db) : IPaymentRepo
{
    public async Task<bool> ConfirmTransaction(TransactionEntity info)
    {
        await db.transactionSchema.AddAsync(TransactionSchema.ToSchema(info));
        await db.SaveChangesAsync();
        return true;
    }

    public async Task ConfirmDeposit(UserEntity user)
    {
        // await db.userSchema.Update(UserSchema.ToSchema(user));
        await db.SaveChangesAsync();
    }
    
    public Task<bool> Refund(string id)
    {
        throw new NotImplementedException();
    }
}
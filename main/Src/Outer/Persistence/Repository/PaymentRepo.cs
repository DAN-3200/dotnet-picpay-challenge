using PicPay.Inner.Entity;
using PicPay.Inner.Ports;
using PicPay.Outer.Persistence.Schemas;

namespace PicPay.Outer.Persistence.Repository;

public class PaymentRepo(DbConnection _db) : IPaymentRepo
{
    public Task<bool> ConfirmTransaction(TransactionEntity info)
    {
        throw new NotImplementedException();
    }

    public async Task ConfirmDeposit(UserEntity user)
    {
        await _db.userSchema.Update(UserSchema.ToSchema(user));
        await _db.SaveChangesAsync();
    }
    
    public Task<bool> Refund(string id)
    {
        throw new NotImplementedException();
    }
}
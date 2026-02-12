using PicPay.Inner.Entity;

namespace PicPay.Inner.Ports;

public interface IPaymentRepo
{
   Task<bool> ConfirmTransaction(TransactionEntity info);
   Task ConfirmDeposit(UserEntity user);
   Task<bool> Refund(string id);
}

public interface IUserRepo
{
   Task Save(UserEntity info);
   Task<UserEntity?> GetByUniqueField(string unique);
}

public interface IHttpServices
{
   Task<T?> RequestExternalApi<T>(string url) where T : class;
}
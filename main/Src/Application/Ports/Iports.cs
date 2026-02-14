using PicPay.Domain.Entity;

namespace PicPay.Application.Ports;

public interface IPaymentRepo
{
   Task<bool> ConfirmTransaction(TransactionEntity info);
   Task ConfirmDeposit(UserEntity user);
   Task<bool> Refund(string id);
   Task<List<TransactionEntity>?> ListTransactions();
}

public interface IUserRepo
{
   Task Save(UserEntity info);
   Task<UserEntity?> GetByUniqueField(string unique);
   Task<List<UserEntity>?> GetList();
}

public interface IHttpServices
{
   Task<T?> RequestExternalApi<T>(string url) where T : class;
}
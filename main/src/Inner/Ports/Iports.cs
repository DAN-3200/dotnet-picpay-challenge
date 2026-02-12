using PicPay.Inner.Entity;

namespace PicPay.Inner.Ports;

public interface IDatabase
{
   Task<bool> ConfirmTransaction(TransactionEntity info);
   Task<UserEntity?> GetByUniqueField(string id);
   Task ConfirmDeposit(UserEntity user);
   Task<bool> Refund(string id);
}

public interface IUserRepo
{
   Task Save(UserEntity info);
}

public interface IService
{
   Task<T?> RequestExternalApi<T>(string url) where T : class;
}
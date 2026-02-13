using PicPay.Inner.Dtos;
using PicPay.Inner.Entity;
using PicPay.Inner.Ports;

namespace PicPay.Inner.Usecase;

public class PaymentUc(IPaymentRepo repoPayment, IUserRepo repoUser, IHttpServices httpServices)
{
   public async Task Transaction(TransactionReq info)
   {
      var res = await httpServices
         .RequestExternalApi<MapJson.AuthorizationTransactionRes>("https://util.devi.tools/api/v2/authorize");

      if (res?.Data.Authorization is not true) throw new ArgumentException("Serviço não autorizado");

      if (info.IdPayer == info.IdPayee) throw new ArgumentException("O Emissor não pode ser o destinario da transação");

      var Payer = await repoUser.GetByUniqueField(info.IdPayer);
      if (Payer is null) throw new ArgumentException("Não há usuário valido para emissão dessa transação");
      if (Payer.Role is Role.LOGISTA) throw new ArgumentException("Logistas não podem efetuar transação");
      if (Payer.Balance < info.Value) throw new ArgumentException("Não há saldo suficiente do emissor para realização dessa transação");

      var Payee = await repoUser.GetByUniqueField(info.IdPayee);
      if (Payee is null) throw new ArgumentException("Não há usuário valido para emissão dessa transação");

      var transactionMold = new TransactionEntity(Payer.Id!, Payee.Id!, info.Value, TypeTransaction.PAYMENT);

      await repoPayment.ConfirmTransaction(transactionMold);
   }

   public async Task Deposit(DepositReq info)
   {
      var user = await repoUser.GetByUniqueField(info.IdAccount);
      if (user is null) throw new ArgumentException("Não existe usuário para realizar o deposito");

      user.Deposit(info.Value);

      await repoPayment.ConfirmDeposit(user);
   }

   public async Task<string> RefundTransaction(string id)
   {
      return await repoPayment.Refund(id) ? "Estorno efetuado com sucesso" : "Estorno recusado";
   }
}
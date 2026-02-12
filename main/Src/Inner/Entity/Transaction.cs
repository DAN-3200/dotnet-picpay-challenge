namespace PicPay.Inner.Entity;

public class TransactionEntity
{
   public string FkPayer;
   public string FkPayee;
   public decimal Value;
   public TypeTransaction? Type;
   public DateTime CreatedAt;

   public TransactionEntity(string fkPayer, string fkPayee, decimal value, TypeTransaction? type)
   {
      this.FkPayer = fkPayer;
      this.FkPayee = fkPayee;
      this.Value = value;
      this.Type = type;
      this.CreatedAt = DateTime.UtcNow;
   }

   public TransactionEntity(string fkPayer, string fkPayee, decimal value) : this(fkPayer, fkPayee, value, null){}
}

public enum TypeTransaction
{
   PAYMENT,
   REFUND
}
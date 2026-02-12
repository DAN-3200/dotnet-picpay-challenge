using PicPay.Inner.Entity;

namespace PicPay.Outer.Persistence.Schemas;

public class TransactionSchema
{
    public string FkPayer;
    public string FkPayee;
    public decimal Value;
    public TypeTransaction? Type;
    public DateTime CreatedAt;
}
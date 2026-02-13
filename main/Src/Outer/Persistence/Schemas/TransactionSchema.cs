using PicPay.Inner.Entity;

namespace PicPay.Outer.Persistence.Schemas;

public class TransactionSchema
{
    public string? Id { get; set; }
    public string FkPayer { get; set; } = null!;
    public string FkPayee { get; set; } = null!;
    public decimal Value { get; set; }
    public TypeTransaction? Type { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public static TransactionSchema ToSchema(TransactionEntity entity)
    {
        return new TransactionSchema
        {
            Id = entity.Id ?? Guid.NewGuid().ToString(),
            FkPayee = entity.FkPayee,
            FkPayer = entity.FkPayer,
            Type = entity.Type,
            Value = entity.Value,
            CreatedAt = entity.CreatedAt,
        };
    }

}
using PicPay.Domain.Entity;

namespace PicPay.Infrastructure.Persistence.Schemas;

public class TransactionSchema
{
    public string? Id { get; set; }
    
    public decimal Value { get; set; }
    public TypeTransaction? Type { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public string FkPayer { get; set; } = null!;
    public string FkPayee { get; set; } = null!;
    
    public UserSchema Payer { get; set; } = null!;
    public UserSchema Payee { get; set; } = null!;
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

    public static TransactionEntity ToEntity(TransactionSchema schema)
    {
        return TransactionEntity.Restore(
            schema.Id!,
            schema.FkPayer,
            schema.FkPayee,
            schema.Value,
            schema.Type,
            schema.CreatedAt
        );
    }

}
namespace PicPay.Domain.Entity;

public class TransactionEntity
{
    public string? Id { get; private set; }
    public string FkPayer { get; private set; }
    public string FkPayee { get; private set; }
    public decimal Value { get; private set; }
    public TypeTransaction? Type { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private TransactionEntity()
    {
    }

    private TransactionEntity(
        string id,
        string fkPayer,
        string fkPayee,
        decimal value,
        TypeTransaction? type,
        DateTime createdAt
    )
    {
        this.Id = id;
        this.FkPayer = fkPayer;
        this.FkPayee = fkPayee;
        this.Value = value;
        this.Type = type;
        this.CreatedAt = createdAt;
    }

    public static TransactionEntity Create(string fkPayer, string fkPayee, decimal value, TypeTransaction? type)
    {
        return new TransactionEntity
            (
                Guid.NewGuid().ToString(),
                fkPayer,
                fkPayee,
                value,
                type,
                DateTime.UtcNow
            )
            ;
    }

    public static TransactionEntity Restore(
        string id,
        string fkPayer,
        string fkPayee,
        decimal value,
        TypeTransaction? type,
        DateTime createdAt
    )
    {
        return new TransactionEntity
        (
            id,
            fkPayer,
            fkPayee,
            value,
            type,
            createdAt
        );
    }
}

public enum TypeTransaction
{
    TRANSFER,
    REFUND
}
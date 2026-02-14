using PicPay.Domain.Entity;

namespace PicPay.Infrastructure.Persistence.Schemas;

public class UserSchema
{
    public string? Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Role Role { get; set; }
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<TransactionSchema> TransactionsPaid { get; set; } = new List<TransactionSchema>();
    public ICollection<TransactionSchema> TransactionsReceived { get; set; } = new List<TransactionSchema>();

    public static UserSchema ToSchema(UserEntity entity)
    {
        return new UserSchema
        {
            Id = entity.Id,
            Name = entity.Name,
            Cpf = entity.Cpf,
            Email = entity.Email,
            Password = entity.Password,
            Role = entity.Role,
            Balance = entity.Balance,
            CreatedAt = entity.CreatedAt,
        };
    }

    public static UserEntity ToEntity(UserSchema schema)
    {
        return UserEntity.Restore(
            schema.Id!,
            schema.Name,
            schema.Cpf,
            schema.Email,
            schema.Password,
            schema.Role,
            schema.Balance,
            schema.CreatedAt
        );
    }
}
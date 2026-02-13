using PicPay.Inner.Entity;

namespace PicPay.Outer.Persistence.Schemas;

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

    public List<TransactionSchema> TransactionSent { get; set; } = new();
    public List<TransactionSchema> TransactionReceived { get; set; } = new();
    
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
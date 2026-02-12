using PicPay.Inner.Entity;

namespace PicPay.Outer.Persistence.Schemas;

public class UserSchema
{
    public string Id;
    public string Name = string.Empty;
    public string Cpf = string.Empty;
    public string Email = string.Empty;
    public string Password = string.Empty;
    public Role Role;
    public decimal Balance;
    public DateTime CreatedAt;

    public static UserSchema ToSchema(UserEntity entity)
    {
        return new UserSchema
        {
            Id = entity.Id ?? "",
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
            schema.Id,
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
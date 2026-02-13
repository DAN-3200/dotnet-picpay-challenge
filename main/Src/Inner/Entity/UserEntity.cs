namespace PicPay.Inner.Entity;

public class UserEntity
{
    public string? Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Cpf { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;
    public Role Role { get; private set; }
    public decimal Balance { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private UserEntity()
    {
    }

    private UserEntity(
        string id,
        string name,
        string cpf,
        string email,
        string password,
        Role role,
        decimal balance,
        DateTime createdAt
    )
    {
        Id = id;
        Name = name.ToLower();
        Cpf = cpf;
        Email = email.ToLower();
        Password = password;
        Role = role;
        Balance = balance;
        CreatedAt = createdAt;
    }

    public static UserEntity Create(
        string name,
        string cpf,
        string email,
        string password,
        Role role)
    {
        return new UserEntity(
            Guid.NewGuid().ToString(),
            name,
            cpf,
            email,
            password,
            role,
            0,
            DateTime.UtcNow
        );
    }

    public static UserEntity Restore(
        string id,
        string name,
        string cpf,
        string email,
        string password,
        Role role,
        decimal balance,
        DateTime createdAt)
    {
        return new UserEntity(
            id,
            name,
            cpf,
            email,
            password,
            role,
            balance,
            createdAt
        );
    }

    public void Deposit(decimal value)
    {
        if (value <= 0)
            throw new ArgumentException("Valor inválido");

        Balance += value;
    }
}

public enum Role
{
    COMUM,
    LOGISTA
}
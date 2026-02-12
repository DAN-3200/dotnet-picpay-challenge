namespace PicPay.Inner.Entity;

public class UserEntity
{
   public string? Id;
   public string Name = string.Empty;
   public string Cpf = string.Empty;
   public string Email = string.Empty;
   public string Password = string.Empty;
   public Role Role;
   public decimal Balance;
   public DateTime CreatedAt;

   public UserEntity(string? id, string name, string cpf, string email, string password, Role role,  decimal? balance,  DateTime? createdAt)
   {
      this.Name = name.ToLower();
      this.Cpf = cpf;
      this.Email = email.ToLower();
      this.Password = password;
      this.Balance = balance ?? 0;
      this.CreatedAt = createdAt ?? DateTime.Now;
      this.Role = role;
   }
   public UserEntity(string name, string cpf, string email, string password, Role role) : this(null, name, cpf, email, password, role, null, null)  {}

   public void Deposit(decimal value)
   {
      if (value < 0) throw new ArgumentException("Valor inválido para deposito");
      this.Balance += value;
   }
}

public enum Role
{
   COMUM,
   LOGISTA
}
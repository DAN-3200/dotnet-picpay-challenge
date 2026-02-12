using Microsoft.EntityFrameworkCore;
using PicPay.Inner.Entity;
using PicPay.Inner.Ports;
using PicPay.Outer.Persistence.Schemas;

namespace PicPay.Outer.Persistence.Repository;

public class UserRepo(DbConnection _db) : IUserRepo
{
    public async Task Save(UserEntity info)
    {
        await _db.userSchema.AddAsync(UserSchema.ToSchema(info));
        await _db.SaveChangesAsync();
    }

    public async Task<UserEntity?> GetByUniqueField(string unique)
    {
        var response = await _db.userSchema
            .Where(i => i.Email == unique || i.Cpf == unique).ToListAsync();
        return UserSchema.ToEntity(response[0]) ?? null;
    }
}
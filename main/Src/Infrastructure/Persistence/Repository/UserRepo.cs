using Microsoft.EntityFrameworkCore;
using PicPay.Infrastructure.Persistence.Schemas;
using PicPay.Domain.Entity;
using PicPay.Application.Ports;

namespace PicPay.Infrastructure.Persistence.Repository;

public class UserRepo(DbConnection db) : IUserRepo
{
    public async Task Save(UserEntity info)
    {
        await db.userSchema.AddAsync(UserSchema.ToSchema(info));
        await db.SaveChangesAsync();
    }

    public async Task<UserEntity?> GetByUniqueField(string unique)
    {
        var response = await db.userSchema
            .Where(i => i.Email == unique || i.Cpf == unique).ToListAsync();
        return UserSchema.ToEntity(response[0]) ?? null;
    }
    
    public async Task<List<UserEntity>?> GetList()
    {
        var response = await db.userSchema
            .AsNoTracking()
            .ToListAsync();
        return response.Select(UserSchema.ToEntity).ToList();
    }
}
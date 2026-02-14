using PicPay.Application.Dtos;
using PicPay.Application.Ports;
using PicPay.Domain.Entity;

namespace PicPay.Application.Usecase;

public class UserUsecase(IUserRepo repoUser)
{
   public async Task SaveUser(RegisterUserReq info)
   {
      var user = UserEntity.Create(info.Name, info.Cpf, info.Email, info.Password, info.Role);
      await repoUser.Save(user);
   }

   public async Task<List<UserEntity>?> GetUserList()
   {
      var users = await repoUser.GetList();
      return users;
   }
}
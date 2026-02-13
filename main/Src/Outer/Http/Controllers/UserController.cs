using Microsoft.AspNetCore.Mvc;
using PicPay.Inner.Dtos;
using PicPay.Inner.Usecase;

namespace PicPay.Outer.Http.Controllers;

[ApiController]
public class UserController(UserUc usecase) : ControllerBase
{
    [HttpPost("/user")]
    public async Task<IActionResult> SaveUser([FromBody] RegisterUserReq info)
    {
        await usecase.SaveUser(info);
        return Ok();
    }
}
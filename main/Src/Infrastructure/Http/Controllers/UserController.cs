using Microsoft.AspNetCore.Mvc;
using PicPay.Application.Dtos;
using PicPay.Application.Usecase;

namespace PicPay.Infrastructure.Http.Controllers;

[ApiController]
public class UserController(UserUsecase usecase) : ControllerBase
{
    [HttpPost("/user")]
    public async Task<IActionResult> SaveUser([FromBody] RegisterUserReq info)
    {
        await usecase.SaveUser(info);
        return Ok();
    }

    [HttpGet("/user")]
    public async Task<IActionResult> GetUserList()
    {
        var response =await usecase.GetUserList();
        return Ok(response);
    }
}
using Microsoft.AspNetCore.Mvc;
using PicPay.Application.Dtos;
using PicPay.Application.Usecase;

namespace PicPay.Infrastructure.Http.Controllers;

[ApiController]
public class PaymentController (PaymentUsecase usecase) : ControllerBase
{
    [HttpPost("/payment/transfer")]
    public async Task<IActionResult> Transfer([FromBody] TransactionReq transaction)
    {
        await usecase.Transaction(transaction);
        return Ok();
    }

    [HttpPost("/payment/deposit")]
    public async Task<IActionResult> Deposit([FromBody] DepositReq transaction)
    {
        await usecase.Deposit(transaction);
        return Ok();
    }

    [HttpPost("/payment/refund")]
    public async Task<IActionResult> RefundTransaction([FromQuery] string IdTransaction)
    {
        var res = await usecase.RefundTransaction(IdTransaction);
        return Ok(res);
    }

    [HttpGet("payment/list")]
    public async Task<IActionResult> ListTransactions()
    {
        var result = await usecase.ListTransactions();
        return Ok(result);
    }
}
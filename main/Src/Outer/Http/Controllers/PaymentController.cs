using Microsoft.AspNetCore.Mvc;
using PicPay.Inner.Dtos;
using PicPay.Inner.Usecase;

namespace PicPay.Outer.Http.Controllers;

[ApiController]
public class PaymentController (PaymentUc usecase) : ControllerBase
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
    public async Task<IActionResult> RefundTransaction([FromBody] string IdTransaction)
    {
        await usecase.RefundTransaction(IdTransaction);
        return Ok();
    }
}
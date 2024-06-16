using System.Diagnostics;
using Control_Estoque.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class MailController : ControllerBase
{
    private readonly IMailService mailService;
    public MailController(IMailService mailService)
    {
        this.mailService = mailService;
    }
    [HttpPost("send")]
    public async Task<IActionResult> SendMail([FromForm] MailRequest request)
    {
        await mailService.SendEmailAsync(request);
        return Ok();

    }
}
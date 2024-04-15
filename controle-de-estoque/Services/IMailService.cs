namespace Control_Estoque.Models;
public interface IMailService
{
    Task SendEmailAsync(MailRequest mailRequest);
}

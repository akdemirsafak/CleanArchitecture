using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Infrastructure.Options;
using MailKit.Net.Smtp;
using MimeKit;

namespace CleanArchitecture.Infrastructure.Services;

public sealed class MailService : IMailService
{
    private readonly EmailConfiguration _emailConfiguration;

    public MailService(EmailConfiguration emailConfiguration)
    {
        _emailConfiguration = emailConfiguration;
    }

    public async Task SendAsync(SendMailDto sendMailDto)
    {
        using var smtpClient = new SmtpClient();
        await smtpClient.ConnectAsync(_emailConfiguration.SmtpServer, 
            _emailConfiguration.Port, 
            false); //ssl aktifleştirilecekse port 465 aktif değilse 587
        smtpClient.Authenticate(_emailConfiguration.From, _emailConfiguration.Password);
        var message= CreateEmailMessage(sendMailDto);
        await smtpClient.SendAsync(message);
        await smtpClient.DisconnectAsync(true);
    }
    private MimeMessage CreateEmailMessage(SendMailDto sendMailDto)
    {
        MailboxAddress mailboxAddressFrom=new MailboxAddress("Maili gönderenin ismi", _emailConfiguration.From); //Mail i gönderen kişi
        
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(mailboxAddressFrom);
        
        emailMessage.To.Add(MailboxAddress.Parse(sendMailDto.to));
        emailMessage.Subject = sendMailDto.subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = sendMailDto.body };

        //BodyBuilder bodyBuilder = new BodyBuilder();
        //bodyBuilder.TextBody = sendMailDto.body;
        //emailMessage.Body=bodyBuilder.ToMessageBody();
     
        return emailMessage;
    }
}

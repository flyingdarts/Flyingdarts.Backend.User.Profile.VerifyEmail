using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using MediatR;

public class SendVerifyUserEmailCommandHandler : IRequestHandler<SendVerifyUserEmailCommand>
{
    private readonly IAmazonSimpleEmailService _emailService;
    public SendVerifyUserEmailCommandHandler(IAmazonSimpleEmailService emailService)
    {
        _emailService = emailService;
    }
    public async Task Handle(SendVerifyUserEmailCommand request, CancellationToken cancellationToken)
    {
        await SendEmail();
    }

    public async Task SendEmail()
    {
        // Build the email request
        var sendRequest = new SendEmailRequest
        {
            Source = "support@flyingdarts.net", // Replace with your email address
            Destination = new Destination
            {
                ToAddresses = new List<string> { "mike@flyingdarts.net" } // Replace with the recipient's email address
            },
            Message = new Message
            {
                Subject = new Content("Hello from Amazon SES"), // Replace with your desired subject
                Body = new Body
                {
                    Text = new Content
                    {
                        Charset = "UTF-8",
                        Data = "This is the body of the email." // Replace with your desired email content
                    }
                }
            },
            Tags = new List<MessageTag>
            {
                new MessageTag
                {
                    Name = "SES-Environment",
                    Value = "sandbox"
                }
            }
        };

        try
        {
            await _emailService.SendEmailAsync(sendRequest);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Email sending failed. Error message: " + ex.Message);
        }
    }
}
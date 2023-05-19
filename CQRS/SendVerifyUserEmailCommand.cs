using Amazon.Lambda.Core;
using MediatR;

public class SendVerifyUserEmailCommand : IRequest
{
    public string Email { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    internal readonly ILambdaContext Context;
    public SendVerifyUserEmailCommand(ILambdaContext context)
    {
        Context = context;
    } 
}
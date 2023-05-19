using Amazon.Lambda.Core;
using MediatR;

public class SendVerifyUserEmailCommand : IRequest
{
    internal readonly ILambdaContext Context;
    public SendVerifyUserEmailCommand(ILambdaContext context)
    {
        Context = context;
    } 
}
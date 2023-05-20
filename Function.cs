using System.Text.Json;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.Serialization.SystemTextJson;
using Amazon.Lambda.SQSEvents;
using Flyingdarts.Lambdas.Shared;

// Get the service provider
var services = ServiceFactory.GetServiceProvider();

// Create an instance of the InnerHandler using the service provider
var innerHandler = new InnerHandler(services);

// Create a serializer for JSON serialization and deserialization
var serializer = new DefaultLambdaJsonSerializer(x => x.PropertyNameCaseInsensitive = true);

// Define the Lambda function handler
var handler = async (MySQSEvent sqsEvent, ILambdaContext context) =>
{
    context.Logger.Log(JsonSerializer.Serialize(sqsEvent));
    var command = JsonSerializer.Deserialize<SendVerifyUserEmailCommand>(sqsEvent.Message.Body);
    await innerHandler.Handle(command, context);
};

// Create and run the Lambda function
await LambdaBootstrapBuilder.Create(handler, serializer)
    .Build()
    .RunAsync();

public class MySQSEvent
{
    public SQSEvent.SQSMessage Message { get; set; }
}
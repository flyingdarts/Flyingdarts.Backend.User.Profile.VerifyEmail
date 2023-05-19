using System.Text.Json;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.Serialization.SystemTextJson;
using Flyingdarts.Lambdas.Shared;

// Get the service provider
var services = ServiceFactory.GetServiceProvider();

// Create an instance of the InnerHandler using the service provider
var innerHandler = new InnerHandler(services);

// Create a serializer for JSON serialization and deserialization
var serializer = new DefaultLambdaJsonSerializer(x => x.PropertyNameCaseInsensitive = true);

// Define the Lambda function handler
var handler = async (string input, ILambdaContext context) =>
{
    var test = JsonSerializer.Deserialize<VerifyEmailCommandOptions>(input);
    context.Logger.Log(JsonSerializer.Serialize(test));
    // Handle the socketRequest using the innerHandler
    await innerHandler.Handle(context);
};

// Create and run the Lambda function
await LambdaBootstrapBuilder.Create(handler, serializer)
    .Build()
    .RunAsync();

public class VerifyEmailCommandOptions
{
    public string Email { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}
using Grpc.Core;
using hammerservice;

namespace hammerservice.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        Console.WriteLine($"V2:Ho ricevuto questo nome {request.Name}");

        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }
}

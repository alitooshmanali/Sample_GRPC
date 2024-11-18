using Domain;
using Grpc.Core;
using Grpc.Core.Interceptors;
using System.Text.Json;

public class ExceptionHandler : Interceptor
{
    private readonly ILogger<ExceptionHandler> _logger;
    private readonly IConfiguration _configuration;

    public ExceptionHandler(ILogger<ExceptionHandler> logger, IConfiguration configuration)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        var logs = new Dictionary<string, string>();

        logs.Add("Method", context.Method);
        logs.Add("RequestUTCDateTime", $"{DateTime.UtcNow}");
        logs.Add("RequestBody", $"{JsonSerializer.Serialize(request)}");

        try
        {
            var result = await continuation(request, context);

            logs.Add("ResponseUTCDateTime", $"{DateTime.UtcNow}");
            logs.Add("ResponseBody", $"{JsonSerializer.Serialize(result)}");

            return result;
        }
        catch (DomainException exception)
        {
            var metaData = new Metadata
            {
                { "ExceptionName", nameof(DomainException) },
                { "ExceptionMessage", exception.Message },
            };

            throw new RpcException(Status.DefaultCancelled, metaData);
        }
        catch (Exception exception)
        {
            var metaData = new Metadata
            {
                { "ExceptionName", exception.GetType().Name },
                { "ExceptionMessage", exception.Message }
            };

            throw new RpcException(Status.DefaultCancelled, metaData);
        }
    }

    public string DictionaryToString<TKey, TValue>
  (IDictionary<TKey, TValue> dictionary)
    {
        var items = from kvp in dictionary
                    select "\"" + kvp.Key + "\":\"" + kvp.Value + "\"";

        return "{" + string.Join(",", items) + "}";
    }
}
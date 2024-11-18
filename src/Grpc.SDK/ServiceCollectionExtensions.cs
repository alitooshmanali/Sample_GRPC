using Grpc.SDK.Channels;
using GrpcServer;
using Microsoft.Extensions.DependencyInjection;

namespace Grpc.SDK
{
    public static class ServiceCollectionExtensions
    {
        public static void AddGrpcSDK(this IServiceCollection services)
        {
            services.AddScoped<ClientsGrpcChannel>();
            services.AddScoped<IUserGrpcService, UserGrpcService>();
        }
    }
}

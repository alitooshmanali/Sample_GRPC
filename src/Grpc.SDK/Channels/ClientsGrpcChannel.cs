using Grpc.Net.Client;
using GrpcServer;

namespace Grpc.SDK.Channels
{
    internal class ClientsGrpcChannel
    {
        private readonly GrpcChannel _channel;

        public ClientsGrpcChannel()
        {
            var serverUrl = "http://localhost:5091";

            _channel = GrpcChannel.ForAddress(serverUrl);
        }

        public Users.UsersClient UserServer => new(_channel);
    }
}

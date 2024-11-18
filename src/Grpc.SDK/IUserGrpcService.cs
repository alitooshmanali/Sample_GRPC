using Grpc.Core;
using Grpc.SDK.Channels;
using Grpc.SDK.Models;
using Grpc.SDK.Models.UserResponse;
using Grpc.SDK.Models.UsersRequest;
using GrpcServer;

namespace Grpc.SDK
{
    public interface IUserGrpcService
    {
        Task<Response<string>> CreateUserAsync(UserRequest request, CancellationToken cancellationToken);

        Task<Response> UpdateUserAsync(UserRequest request, CancellationToken cancellationToken);

        Task<Response> DeleteUserAsync(DeleteRequest request, CancellationToken cancellationToken);

        Task<Response<UserQueryResponse>> GetByNationalCode(UserByNationalCodeRequest request, CancellationToken cancellationToken);

        Task<Response<List<UserQueryResponse>>> GetCollection(UserCollectionRequest request, CancellationToken cancellationToken);
    }

    internal class UserGrpcService : IUserGrpcService
    {
        private readonly ClientsGrpcChannel _server;

        public UserGrpcService(ClientsGrpcChannel server) => _server = server;

        public async Task<Response<string>> CreateUserAsync(UserRequest request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();
            CreateUserResponse result = new();

            var createUserRequest = new CreateUserRequest
            {
                Name = request.Name,
                Surname = request.Surname,
                NationalCode = request.NationalCode,
                BirthDay = request.BirthDay
            };

            try
            {
                result = await _server.UserServer.CreateUserAsync(createUserRequest, cancellationToken: cancellationToken);

                response.Value = result.SuccessfullyResponse.NationalCode;
                response.Message = "Successfully create user.";
            }
            catch (RpcException ex)
            {
                response.Value = string.Empty;
                response.Message = result.FailureResponse.Message;
            }

            return response;
        }

        public async Task<Response> DeleteUserAsync(DeleteRequest request, CancellationToken cancellationToken)
        {
            var response = new Response();
            var deleteUserRequest = new DeleteUserRequest
            {
                NationalCode = request.NationalCode
            };

            try
            {
                await _server.UserServer.DeleteUserAsync(deleteUserRequest, cancellationToken: cancellationToken);
                response.Message = "Successfully Deleted";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<UserQueryResponse>> GetByNationalCode(UserByNationalCodeRequest request, CancellationToken cancellationToken)
        {
            var response = new Response<UserQueryResponse>();
            var getByNationalCodeRequest = new GetUserByNationalCodeRequest
            {
                NationalCode = request.NationalCode
            };

            var result = await _server.UserServer.GetUserByNationalCodeAsync(getByNationalCodeRequest, cancellationToken: cancellationToken);

            if (result.SuccessfullyResponse is not null)
                response.Value = new UserQueryResponse
                {
                    Name = result.SuccessfullyResponse.Name,
                    Surname = result.SuccessfullyResponse.Surname,
                    NationalCode = result.SuccessfullyResponse.NationalCode,
                    BirthDay = result.SuccessfullyResponse.BirthDay
                };
            else response.Message = result.FailureResponse.Message;

            return response;
        }

        public async Task<Response<List<UserQueryResponse>>> GetCollection(UserCollectionRequest request, CancellationToken cancellationToken)
        {
            var response = new Response<List<UserQueryResponse>>();
            var getUserCollectionRequest = new GetUserCollectionRequest
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            };

            var result = await _server.UserServer.GetUserCollectionAsync(getUserCollectionRequest, cancellationToken: cancellationToken);

            if (result.SuccessfullyResponse is not null)
                response.Value.AddRange(result.SuccessfullyResponse.Select(x => new UserQueryResponse
                {
                    Name = x.Name,
                    Surname = x.Surname,
                    NationalCode = x.NationalCode,
                    BirthDay = x.BirthDay
                }).ToList());

            else response.Message = result.FailureResponse.Message;

            return response;
        }

        public async Task<Response> UpdateUserAsync(UserRequest request, CancellationToken cancellationToken)
        {
            var response = new Response();
            var updateUserRequest = new UpdateUserRequest
            {
                Name = request.Name,
                Surname = request.Surname,
                NationalCode = request.NationalCode,
                BirthDay = request.BirthDay
            };


            try
            {
                await _server.UserServer.UpdateUserAsync(updateUserRequest, cancellationToken: cancellationToken);

                response.Message = "Successfully Updated";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}

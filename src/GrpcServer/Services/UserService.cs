using Domain;
using Grpc.Core;
using GrpcServer.Helper.UserExtensions;
using MediatR;

namespace GrpcServer.Services
{
    public class UserService : Users.UsersBase
    {
        private readonly IMediator _mediator;

        public UserService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<CreateUserResponse> CreateUser(CreateUserRequest request, ServerCallContext context)
        {
            var successfullyResponse = new CreateUserSuccessfullyResponse();
            var failureResponse = new UserFailureResponse();

            var command = request.ToCreateUserCommand();

            try
            {
                var result = await _mediator.Send(command, context.CancellationToken);

                successfullyResponse.NationalCode = result;
            }
            catch (RpcException ex)
            {
                failureResponse.Message = ex.Message;
            }
            catch (DomainException ex)
            {
                failureResponse.Message = ex.Message;
            }
            catch (Exception ex)
            {
                failureResponse.Message = ex.Message;
            }

            return new CreateUserResponse
            {
                SuccessfullyResponse = successfullyResponse,
                FailureResponse = failureResponse
            };
        }

        public override async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request, ServerCallContext context)
        {
            var successfullyResponse = new UpdateUserSuccessfullyResponse();
            var failureResponse = new UserFailureResponse();

            var command = request.ToUpdateUserCommand();

            try
            {
                await _mediator.Send(command, context.CancellationToken);
            }
            catch (RpcException ex)
            {
                failureResponse.Message = ex.Message;
            }
            catch (DomainException ex)
            {
                failureResponse.Message = ex.Message;
            }
            catch (Exception ex)
            {
                failureResponse.Message = ex.Message;
            }

            return new UpdateUserResponse
            {
                SuccessfullyResponse = successfullyResponse,
                FailureResponse = failureResponse
            };
        }

        public override async Task<DeleteUserResponse> DeleteUser(DeleteUserRequest request, ServerCallContext context)
        {
            var successfullyResponse = new DeleteUserSuccessfullyResponse();
            var failureResponse = new UserFailureResponse();

            var command = request.ToDeleteUserCommand();

            try
            {
                await _mediator.Send(command, context.CancellationToken);
            }
            catch (RpcException ex)
            {
                failureResponse.Message = ex.Message;
            }
            catch (DomainException ex)
            {
                failureResponse.Message = ex.Message;
            }
            catch (Exception ex)
            {
                failureResponse.Message = ex.Message;
            }

            return new DeleteUserResponse
            {
                SuccessfullyResponse = successfullyResponse,
                FailureResponse = failureResponse
            };
        }

        public override async Task<GetUserByNationalCodeResponse> GetUserByNationalCode(GetUserByNationalCodeRequest request, ServerCallContext context)
        {
            var successfullyResponse = new UserSuccessfullyResponse();
            var failureResponse = new UserFailureResponse();
            var query = request.ToGetUserByNationalCodeQuery();

            try
            {
                var result = await _mediator.Send(query, context.CancellationToken);

                successfullyResponse.Name = result.Name;
                successfullyResponse.Surname = result.Surname;
                successfullyResponse.NationalCode = result.NationalCode;
                successfullyResponse.BirthDay = result.BirthDay;
            }
            catch (RpcException ex)
            {
                failureResponse.Message = ex.Message;
            }
            catch (DomainException ex)
            {
                failureResponse.Message = ex.Message;
            }
            catch (Exception ex)
            {
                failureResponse.Message = ex.Message;
            }

            return new GetUserByNationalCodeResponse { 
                SuccessfullyResponse = successfullyResponse, 
                FailureResponse = failureResponse 
            };
        }

        public override async Task<GetUserCollectionResponse> GetUserCollection(GetUserCollectionRequest request, ServerCallContext context)
        {
            var response = new GetUserCollectionResponse();
            var query = request.ToGetUserCollectionQuery();

            try
            {
                var result = await _mediator.Send(query, context.CancellationToken);

                response.SuccessfullyResponse.AddRange(result.Result.Select(x => new UserSuccessfullyResponse
                {
                    Name = x.Name,
                    Surname = x.Surname,
                    NationalCode = x.NationalCode,
                    BirthDay = x.BirthDay
                }).ToList());

                response.TotalCount = result.TotalCount;
                response.PageIndex = request.PageIndex.HasValue ? request.PageIndex.Value : 1;
                response.PageSize = request.PageSize.HasValue ? request.PageSize.Value : 30;
            }
            catch (RpcException ex)
            {
                response.FailureResponse.Message = ex.Message;
            }
            catch (DomainException ex)
            {
                response.FailureResponse.Message = ex.Message;
            }
            catch (Exception ex)
            {
                response.FailureResponse.Message = ex.Message;
            }

            return response;
        }
    }
}

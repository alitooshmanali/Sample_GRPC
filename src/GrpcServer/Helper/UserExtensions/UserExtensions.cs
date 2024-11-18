using Application.Aggregates.Users.Commands.CreateUser;
using Application.Aggregates.Users.Commands.DeleteUser;
using Application.Aggregates.Users.Commands.UpdateUser;
using Application.Aggregates.Users.Queries.GetUserByNationalCode;
using Application.Aggregates.Users.Queries.GetUserCollections;

namespace GrpcServer.Helper.UserExtensions
{
    public static class UserExtensions
    {
        public static CreateUserCommand ToCreateUserCommand(this CreateUserRequest request)
            => new CreateUserCommand
            {
                Name = request.Name,
                Surname = request.Surname,
                NationalCode = request.NationalCode,
                BirthDay = request.BirthDay
            };

        public static UpdateUserCommand ToUpdateUserCommand(this UpdateUserRequest request)
            => new UpdateUserCommand
            {
                Name = request.Name,
                Surname = request.Surname,
                NationalCode = request.NationalCode,
                BirthDay = request.BirthDay
            };

        public static DeleteUserCommand ToDeleteUserCommand(this DeleteUserRequest request)
            => new DeleteUserCommand
            {
                NationalCode = request.NationalCode
            };

        public static GetUserByNationalCodeQuery ToGetUserByNationalCodeQuery(this GetUserByNationalCodeRequest request)
            => new GetUserByNationalCodeQuery
            {
                NationalCode = request.NationalCode
            };

        public static GetUserCollectionQuery ToGetUserCollectionQuery(this GetUserCollectionRequest request)
            => new GetUserCollectionQuery
            {
                PageIndex = request.PageIndex.HasValue ? request.PageIndex.Value : 1,
                PageSize = request.PageSize.HasValue ? request.PageSize.Value : 30,
            };
    }
}

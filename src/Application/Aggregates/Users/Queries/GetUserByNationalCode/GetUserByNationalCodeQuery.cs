using MediatR;

namespace Application.Aggregates.Users.Queries.GetUserByNationalCode
{
    public class GetUserByNationalCodeQuery : IRequest<UserQueryResult>
    {
        public string NationalCode { get; set; }
    }
}

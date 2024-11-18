using Application.Aggregates.Users.Commands.CreateUser;
using Application.Aggregates.Users.Commands.UpdateUser;
using Application.Aggregates.Users.Queries;
using Application.Aggregates.Users.Queries.GetUserCollections;
using AutoMapper;
using RestAPI.V1.Models;

namespace RestAPI.V1.Aggregates.Users.Models
{
    public class UserMappingProfile: Profile
    {
        public UserMappingProfile()
        {
            CreateMap<SearchModel, GetUserCollectionQuery>();
            CreateMap<UserRequest, CreateUserCommand>();
            CreateMap<UserRequest, UpdateUserCommand>();
            CreateMap<UserQueryResult, UserResponse>();
        }
    }
}

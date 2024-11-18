using Application.Aggregates.Users.Commands.CreateUser;
using Application.Aggregates.Users.Commands.DeleteUser;
using Application.Aggregates.Users.Commands.UpdateUser;
using Application.Aggregates.Users.Queries.GetUserByNationalCode;
using Application.Aggregates.Users.Queries.GetUserCollections;
using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestAPI.V1.Aggregates.Users.Models;
using RestAPI.V1.Models;

namespace RestAPI.V1.Aggregates.Users.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("rest/api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper mapper;

        private readonly IMediator mediator;

        public UsersController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<UserResponse>>> CreateUser(
            [FromBody] UserRequest request,
            CancellationToken cancellationToken)
        {
            var command = mapper.Map<CreateUserCommand>(request);
            var nationalCode = await mediator.Send(command, cancellationToken).ConfigureAwait(false);
            var query = new GetUserByNationalCodeQuery { NationalCode = nationalCode };
            var queryResult = await mediator.Send(query, cancellationToken).ConfigureAwait(false);

            return CreatedAtAction(
                nameof(GetUserByNationalCode),
                new { nationalCode, version = "1" },
                new ResponseModel<UserResponse> { Values = mapper.Map<UserResponse>(queryResult) });
        }

        [HttpGet]
        public async Task<ActionResult<ResponseCollectionModel<UserResponse[]>>> GetAllUsers(
            [FromQuery] SearchModel searchModel,
            CancellationToken cancellationToken)
        {
            var query = mapper.Map<GetUserCollectionQuery>(searchModel);
            var queryResult = await mediator.Send(query, cancellationToken).ConfigureAwait(false);

            return Ok(new ResponseCollectionModel<UserResponse>
            {
                Values = mapper.Map<UserResponse[]>(queryResult.Result),
                TotalCount = queryResult.TotalCount
            });
        }

        [HttpGet("{nationalCode}")]
        public async Task<ActionResult<ResponseModel<UserResponse>>> GetUserByNationalCode(
            string nationalCode,
            CancellationToken cancellationToken)
        {
            var query = new GetUserByNationalCodeQuery { NationalCode = nationalCode };
            var queryResult = await mediator.Send(query, cancellationToken).ConfigureAwait(false);

            if (queryResult is null)
                return NotFound();

            return Ok(new ResponseModel<UserResponse> { Values = mapper.Map<UserResponse>(queryResult) });
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(
            UserRequest request,
            CancellationToken cancellationToken)
        {
            var command = mapper.Map<UpdateUserCommand>(request);

            await mediator.Send(command, cancellationToken).ConfigureAwait(false);

            return NoContent();
        }

        [HttpDelete("{nationalCode}")]
        public async Task<ActionResult> DeleteUser(string nationalCode, CancellationToken cancellationToken)
        {
            await mediator.Send(new DeleteUserCommand { NationalCode = nationalCode }, cancellationToken)
                .ConfigureAwait(false);

            return Ok();
        }

    }
}

using MediatR;

namespace Application.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IUnitOfWork unitOfWork;

        public TransactionBehavior(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (request.GetType().Name.StartsWith("Get"))
                return await next().ConfigureAwait(false);

            try
            {
                await unitOfWork.BeginTransaction(cancellationToken).ConfigureAwait(false);
                var response = await next().ConfigureAwait(false);
                await unitOfWork.CommitTransaction(cancellationToken).ConfigureAwait(false);

                return response;

            }
            catch (Exception)
            {
                await unitOfWork.RollbackTransaction(cancellationToken).ConfigureAwait(false);
                throw;
            }
        }
    }
}

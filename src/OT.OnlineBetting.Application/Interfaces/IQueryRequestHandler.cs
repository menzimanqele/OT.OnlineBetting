namespace OT.OnlineBetting.Application.Interfaces;

public interface IQueryRequestHandler<TRequest>
{
    TRequest Handle(TRequest request, CancellationToken cancellationToken);
}
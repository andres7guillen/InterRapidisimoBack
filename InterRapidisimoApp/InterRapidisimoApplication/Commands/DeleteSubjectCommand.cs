using CSharpFunctionalExtensions;
using InterRapidisimoDomain.Repositories;
using MediatR;

namespace InterRapidisimoApplication.Commands;

public class DeleteSubjectCommand : IRequest<Result<bool>>
{
    public Guid Id { get; }

    public DeleteSubjectCommand(Guid id)
    {
        Id = id;
    }

    public class DeleteSubjectCommandHandler : IRequestHandler<DeleteSubjectCommand, Result<bool>>
    {
        private readonly ISubjectRepository _repository;

        public DeleteSubjectCommandHandler(ISubjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<bool>> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.Delete(request.Id);
            return result.IsSuccess
                ? Result.Success(result.Value)
                : Result.Failure<bool>(result.Error);
        }
    }

}

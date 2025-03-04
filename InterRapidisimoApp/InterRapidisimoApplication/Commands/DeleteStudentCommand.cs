using CSharpFunctionalExtensions;
using InterRapidisimoDomain.Repositories;
using MediatR;

namespace InterRapidisimoApplication.Commands
{
    public class DeleteStudentCommand : IRequest<Result<bool>>
    {
        public Guid Id { get; }

        public DeleteStudentCommand(Guid id)
        {
            Id = id;
        }

        public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, Result<bool>>
        {
            private readonly IStudentRepository _repository;

            public DeleteStudentCommandHandler(IStudentRepository repository)
            {
                _repository = repository;
            }

            public async Task<Result<bool>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
            {
                var result = await _repository.Delete(request.Id);
                return result.IsSuccess
                    ? Result.Success(result.Value)
                    : Result.Failure<bool>(result.Error);
            }
        }
    }
}

using CSharpFunctionalExtensions;
using InterRapidisimoDomain.Repositories;
using MediatR;

namespace InterRapidisimoApplication.Commands
{
    public class DeleteProfessorCommand : IRequest<Result<bool>>
    {
        public Guid Id { get; }
        public DeleteProfessorCommand(Guid id) => Id = id;

        public class DeleteProfessorCommandHandler : IRequestHandler<DeleteProfessorCommand, Result<bool>>
        {
            private readonly IProfessorRepository _repository;

            public DeleteProfessorCommandHandler(IProfessorRepository repository)
            {
                _repository = repository;
            }

            public async Task<Result<bool>> Handle(DeleteProfessorCommand request, CancellationToken cancellationToken)
            {
                var professor = await _repository.GetById(request.Id);
                if (professor.HasNoValue)
                    return Result.Failure<bool>("Profesor no encontrado");

                var result = await _repository.Delete(professor.Value);
                return result.IsSuccess
                    ? Result.Success<bool>(result.Value)
                    : Result.Failure<bool>("Error emoving the professor");
            }
        }

    }
}

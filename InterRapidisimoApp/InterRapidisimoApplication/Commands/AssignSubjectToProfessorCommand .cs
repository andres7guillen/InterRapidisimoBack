using CSharpFunctionalExtensions;
using InterRapidisimoDomain.Repositories;
using MediatR;

namespace InterRapidisimoApplication.Commands;

public class AssignSubjectToProfessorCommand : IRequest<Result>
{
    public Guid ProfessorId { get; }
    public Guid SubjectId { get; }

    public AssignSubjectToProfessorCommand(Guid professorId, Guid subjectId)
    {
        ProfessorId = professorId;
        SubjectId = subjectId;
    }

    public class AssignSubjectToProfessorCommandHandler : IRequestHandler<AssignSubjectToProfessorCommand, Result>
    {
        private readonly IProfessorRepository _professorRepository;
        private readonly ISubjectRepository _subjectRepository;
        private const int MaxProfessorsWithTwoSubjects = 5;

        public AssignSubjectToProfessorCommandHandler(
            IProfessorRepository professorRepository,
            ISubjectRepository subjectRepository)
        {
            _professorRepository = professorRepository;
            _subjectRepository = subjectRepository;
        }

        public async Task<Result> Handle(AssignSubjectToProfessorCommand request, CancellationToken cancellationToken)
        {
            var professor = await _professorRepository.GetById(request.ProfessorId);
            if (professor.HasNoValue)
                return Result.Failure("Professor not found.");

            var subject = await _subjectRepository.GetById(request.SubjectId);
            if (subject.HasNoValue)
                return Result.Failure("Subject not found.");

            var professorsWithTwoSubjects = await _professorRepository.CountWithTwoSubjectsAsync();

            if (professorsWithTwoSubjects.Value >= MaxProfessorsWithTwoSubjects && professor.Value.ProfessorSubjects.Count >= 1)
                return Result.Failure("Already there are 5 professors teaching 2 subjects");

            var result = professor.Value.AssignSubject(subject.Value);
            if (result.IsFailure)
                return Result.Failure(result.Error);

            await _professorRepository.Update(professor.Value);
            return Result.Success();
        }
    }

}


using CSharpFunctionalExtensions;
using InterRapidisimoDomain.Entities;
using InterRapidisimoDomain.Repositories;
using MediatR;

namespace InterRapidisimoApplication.Commands;

public class EnrollStudentInSubjectCommand : IRequest<Result<bool>>
{
    public Guid StudentId { get; }
    public Guid SubjectId { get; }
    public Guid ProfessorId { get; }

    public EnrollStudentInSubjectCommand(Guid studentId, Guid subjectId, Guid professorId)
    {
        StudentId = studentId;
        SubjectId = subjectId;
        ProfessorId = professorId;
    }

    public class EnrollStudentInSubjectHandler : IRequestHandler<EnrollStudentInSubjectCommand, Result<bool>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly IStudentSubjectRepository _studentSubjectRepository;

        public EnrollStudentInSubjectHandler(
        IStudentRepository studentRepository,
        ISubjectRepository subjectRepository,
        IProfessorRepository professorRepository,
        IStudentSubjectRepository studentSubjectRepository)
        {
            _studentRepository = studentRepository;
            _subjectRepository = subjectRepository;
            _professorRepository = professorRepository;
            _studentSubjectRepository = studentSubjectRepository;
        }
        public async Task<Result<bool>> Handle(EnrollStudentInSubjectCommand request, CancellationToken cancellationToken)
        {
            var studentResult = await _studentRepository.GetStudentWithSubjectsAsync(request.StudentId);
            if (studentResult.IsFailure) 
                return Result.Failure<bool>(studentResult.Error);
            var student = studentResult.Value;
            
            var subjectResult = await _subjectRepository.GetById(request.SubjectId);
            if (subjectResult.HasNoValue) return Result.Failure<bool>("Subject not found");
            var subject = subjectResult.Value;

            var professorResult = await _professorRepository.GetById(request.ProfessorId);
            if (professorResult.HasNoValue) return Result.Failure<bool>("Professor not found");
            var professor = professorResult.Value;

            // Un estudiante solo puede tomar 3 materias
            if (student.StudentSubjects.Count >= 3)
                return Result.Failure<bool>("Student cannot enroll on/in more than 3 subjects");

            // El estudiante no puede tener clases con el mismo profesor
            if (student.StudentSubjects.Any(ss => ss.ProfessorId == request.ProfessorId))
                return Result.Failure<bool>("Student already have a subject with the professor.");

            var studentSubject = new StudentSubject(student, subject, professor);
            student.AddStudentSubject(studentSubject);
            var creationResult = await _studentSubjectRepository.CreateStudentSubjectAsync(studentSubject);
            return Result.Success(creationResult.IsSuccess);

        }

    }

}

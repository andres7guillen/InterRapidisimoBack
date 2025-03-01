using CSharpFunctionalExtensions;
using InterRapidisimoDomain.Entities;
using InterRapidisimoDomain.Repositories;
using InterRapidisimoDomain.Services;

namespace InterRapidisimoInfrastructure.Services
{
    public class ProfessorService : IProfessorService
    {
        private readonly IProfessorRepository _professorRepository;
        private readonly ISubjectRepository _subjectRepository;

        public ProfessorService(IProfessorRepository professorRepository, ISubjectRepository subjectRepository)
        {
            _professorRepository = professorRepository;
            _subjectRepository = subjectRepository;
        }

        public async Task<Result> AssignSubjectToProfessor(Guid professorId, Guid subjectId)
        {
            var professor = await _professorRepository.GetById(professorId);
            if (!professor.HasValue)
                return Result.Failure("Professor does not exist.");

            var subject = await _subjectRepository.GetById(subjectId);
            if (!subject.HasValue)
                return Result.Failure("Subject does not exist");

            // Contar cuántos profesores ya tienen 2 materias asignadas
            var allProfessors = await _professorRepository.GetAll();
            int professorsWithTwoSubjects = allProfessors.Value.Count(p => p.ProfessorSubjects.Count >= 2);

            if (professorsWithTwoSubjects >= 5 && professor.Value.ProfessorSubjects.Count < 2)
                return Result.Failure("Already there are 5 professors teaching 2 subjects");

            var result = professor.Value.AssignSubject(subject.Value);
            if (result.IsFailure)
                return Result.Failure(result.Error);

            await _professorRepository.Update(professor.Value);
            return Result.Success();
        }

        public async Task<Result<Professor>> CreateProfessor(string name, string surname, string email)
        {
            var professor = Professor.Create(name, surname, email);
            if (professor.IsFailure)
                return Result.Failure<Professor>(professor.Error);

            await _professorRepository.CreateProfessor(professor.Value);
            return Result.Success(professor.Value);
        }
    }
}

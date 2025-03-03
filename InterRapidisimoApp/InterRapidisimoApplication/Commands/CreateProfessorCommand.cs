using AutoMapper;
using CSharpFunctionalExtensions;
using InterRapidisimoDomain.DTOs;
using InterRapidisimoDomain.Entities;
using InterRapidisimoDomain.Repositories;
using MediatR;

namespace InterRapidisimoApplication.Commands;

public class CreateProfessorCommand : IRequest<Result<ProfessorDto>>
{
    public string Name { get; }
    public string Surname { get; set; }
    public string Email{ get; set; }


    public CreateProfessorCommand(string name, string surname, string email)
    {
        Name = name;
        Surname = surname;
        Email = email;
    }

    public class CreateProfessorCommandHandler : IRequestHandler<CreateProfessorCommand, Result<ProfessorDto>>
    {
        private readonly IProfessorRepository _professorRepository;
        private readonly IMapper _mapper;

        public CreateProfessorCommandHandler(IProfessorRepository professorRepository, IMapper mapper)
        {
            _professorRepository = professorRepository;
            _mapper = mapper;
        }

        public async Task<Result<ProfessorDto>> Handle(CreateProfessorCommand request, CancellationToken cancellationToken)
        {
            var professor = Professor.Create(name:request.Name,surname: request.Surname, email: request.Email);
            if (professor.IsFailure)
                return Result.Failure<ProfessorDto>(professor.Error);

            await _professorRepository.CreateProfessor(professor.Value);
            return Result.Success(_mapper.Map<ProfessorDto>(professor.Value));
        }
    }

}

using AutoMapper;
using CSharpFunctionalExtensions;
using InterRapidisimoDomain.DTOs;
using InterRapidisimoDomain.Entities;
using InterRapidisimoDomain.Repositories;
using MediatR;

namespace InterRapidisimoApplication.Commands;

public class CreateSubjectCommand : IRequest<Result<SubjectDto>>
{
    public string Name { get; }
    public int Credits { get; }

    public CreateSubjectCommand(string name, int credits)
    {
        Name = name;
        Credits = credits;
    }

    public class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand, Result<SubjectDto>>
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public CreateSubjectCommandHandler(ISubjectRepository subjectRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }

        public async Task<Result<SubjectDto>> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = Subject.CreateSubject(request.Name);
            if (subject.IsFailure)
                return Result.Failure<SubjectDto>(subject.Error);

            await _subjectRepository.Create(subject.Value);
            return Result.Success(_mapper.Map<SubjectDto>(subject.Value));
        }
    }

}

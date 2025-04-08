using AutoMapper;
using CSharpFunctionalExtensions;
using InterRapidisimoDomain.DTOs;
using InterRapidisimoDomain.Entities;
using InterRapidisimoDomain.Repositories;
using InterRapidisimoEventBus.Abstractions;
using InterRapidisimoEventBus.Events;
using MediatR;

namespace InterRapidisimoApplication.Commands;

public class CreateSubjectCommand : IRequest<Result<SubjectDto>>
{
    public string Name { get; }

    public CreateSubjectCommand(string name)
    {
        Name = name;
    }

    public class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand, Result<SubjectDto>>
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;

        public CreateSubjectCommandHandler(ISubjectRepository subjectRepository, IMapper mapper, IEventBus eventBus)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
            _eventBus = eventBus;
        }

        public async Task<Result<SubjectDto>> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = Subject.CreateSubject(request.Name);
            if (subject.IsFailure)
                return Result.Failure<SubjectDto>(subject.Error);

            await _subjectRepository.Create(subject.Value);
            var subjectCreatedEvent = new SubjectCreatedEvent(subject.Value.Id, subject.Value.Name);
            await _eventBus.Publish(subjectCreatedEvent);

            return Result.Success(_mapper.Map<SubjectDto>(subject.Value));
        }
    }

}

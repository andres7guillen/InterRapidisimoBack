using AutoMapper;
using CSharpFunctionalExtensions;
using InterRapidisimoDomain.DTOs;
using InterRapidisimoDomain.Repositories;
using MediatR;

namespace InterRapidisimoApplication.Queries;

public class GetStudentsBySubjectQuery : IRequest<Result<List<StudentDto>>>
{
    public Guid SubjectId { get; }

    public GetStudentsBySubjectQuery(Guid subjectId)
    {
        SubjectId = subjectId;
    }

    public class GetStudentsBySubjectQueryHandler : IRequestHandler<GetStudentsBySubjectQuery, Result<List<StudentDto>>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public GetStudentsBySubjectQueryHandler(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<StudentDto>>> Handle(GetStudentsBySubjectQuery request, CancellationToken cancellationToken)
        {
            var students = await _studentRepository.GetStudentsBySubjectIdAsync(request.SubjectId);
            return Result.Success(_mapper.Map<List<StudentDto>>(students.Value));
        }
    }

}

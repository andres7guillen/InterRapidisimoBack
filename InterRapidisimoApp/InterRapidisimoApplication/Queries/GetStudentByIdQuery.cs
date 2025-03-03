using AutoMapper;
using CSharpFunctionalExtensions;
using InterRapidisimoData.Context;
using InterRapidisimoDomain.DTOs;
using InterRapidisimoDomain.Repositories;
using MediatR;

namespace InterRapidisimoApplication.Queries;

public class GetStudentByIdQuery : IRequest<Result<StudentDto>>
{
    public Guid StudentId { get; }

    public GetStudentByIdQuery(Guid studentId)
    {
        StudentId = studentId;
    }

    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, Result<StudentDto>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public GetStudentByIdQueryHandler(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<Result<StudentDto>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.StudentId);
            if (student == null)
                return Result.Failure<StudentDto>("Student not found.");

            return Result.Success(_mapper.Map<StudentDto>(student.Value));
        }
    }

}

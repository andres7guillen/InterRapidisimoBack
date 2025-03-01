using AutoMapper;
using CSharpFunctionalExtensions;
using InterRapidisimoDomain.DTOs;
using InterRapidisimoDomain.Repositories;
using MediatR;

namespace InterRapidisimoApplication.Queries;

public class GetAllStudentsQuery : IRequest<Result<IEnumerable<StudentDto>>>
{
    public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, Result<IEnumerable<StudentDto>>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public GetAllStudentsQueryHandler(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<StudentDto>>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var students = await _studentRepository.GetAllAsync();
            var studentsDto = _mapper.Map<IEnumerable<StudentDto>>(students);
            return Result.Success(studentsDto);
        }
    }
}

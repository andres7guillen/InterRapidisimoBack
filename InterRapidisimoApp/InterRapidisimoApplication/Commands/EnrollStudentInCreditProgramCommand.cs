using AutoMapper;
using CSharpFunctionalExtensions;
using InterRapidisimoDomain.DTOs;
using InterRapidisimoDomain.Entities;
using InterRapidisimoDomain.Repositories;
using MediatR;

namespace InterRapidisimoApplication.Commands;

public class EnrollStudentInCreditProgramCommand : IRequest<Result<StudentDto>>
{
    public Guid StudentId { get; }
    public Guid CreditProgramId { get; }

    public EnrollStudentInCreditProgramCommand(Guid studentId, Guid creditProgramId)
    {
        StudentId = studentId;
        CreditProgramId = creditProgramId;
    }

    public class EnrollStudentInCreditProgramHandler : IRequestHandler<EnrollStudentInCreditProgramCommand, Result<StudentDto>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICreditProgramRepository _creditProgramRepository;
        private readonly IStudentCreditProgramRepository _studentCreditProgramRepository;
        private readonly IMapper _mapper;

        public EnrollStudentInCreditProgramHandler(
            IStudentRepository studentRepository,
            ICreditProgramRepository creditProgramRepository,
            IMapper mapper)
        {
            _studentRepository = studentRepository;
            _creditProgramRepository = creditProgramRepository;
            _mapper = mapper;
        }

        public async Task<Result<StudentDto>> Handle(EnrollStudentInCreditProgramCommand request, CancellationToken cancellationToken)
        {
            var studentResult = await _studentRepository.GetByIdAsync(request.StudentId);
            if (studentResult.HasNoValue)
                return Result.Failure<StudentDto>("Student not found.");

            var student = studentResult.Value;

            
            var creditProgramResult = await _creditProgramRepository.GetByIdAsync(request.CreditProgramId);
            if (creditProgramResult.HasNoValue)
                return Result.Failure<StudentDto>("Credit program not found.");

            var creditProgram = creditProgramResult.Value;

            var studentCreditProgram = StudentCreditProgram.Create(student.Id, creditProgram.Id);
            if (studentCreditProgram.IsFailure)
                return Result.Failure<StudentDto>(studentCreditProgram.Error);

            var enrollmentResult = student.EnrollInCreditProgram(studentCreditProgram.Value);
            if (enrollmentResult.IsFailure)
                return Result.Failure<StudentDto>(enrollmentResult.Error);
            
            await _studentCreditProgramRepository.CreateAsync(studentCreditProgram.Value);

            await _studentRepository.UpdateStudent(student);

            var studentDto = _mapper.Map<StudentDto>(student);
            return Result.Success(studentDto);
        }
    }

}

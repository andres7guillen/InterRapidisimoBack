using AutoMapper;
using CSharpFunctionalExtensions;
using InterRapidisimoDomain.DTOs;
using InterRapidisimoDomain.Entities;
using InterRapidisimoDomain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRapidisimoApplication.Commands;

public class RegisterStudentCommand : IRequest<Result<StudentDto>>
{
    public string Name { get; set; }
    public string SurName { get; set; }
    public string Email { get; set; }

    public RegisterStudentCommand(string name, string surName, string email)
    {
        Name = name;
        SurName = surName;
        Email = email;
    }

    public class RegisterStudentHandler : IRequestHandler<RegisterStudentCommand, Result<StudentDto>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public RegisterStudentHandler(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<Result<StudentDto>> Handle(RegisterStudentCommand request, CancellationToken cancellationToken)
        {
            var student = Student.CreateStudent(request.Name, request.SurName, request.Email);
            if (student.IsFailure)
                return Result.Failure<StudentDto>(student.Error);

            await _studentRepository.Create(student.Value);
            var studentDto = _mapper.Map<StudentDto>(student.Value);

            return Result.Success(studentDto);
        }
    }

}

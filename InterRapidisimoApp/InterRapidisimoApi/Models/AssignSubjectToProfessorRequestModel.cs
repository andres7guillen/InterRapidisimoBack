namespace InterRapidisimoApi.Models;

public class AssignSubjectToProfessorRequestModel
{
    public Guid ProfessorId { get; set; }
    public Guid SubjectId { get; set; }
}

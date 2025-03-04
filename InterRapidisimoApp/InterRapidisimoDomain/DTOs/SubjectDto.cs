namespace InterRapidisimoDomain.DTOs;

public class SubjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Credits { get; set; }

    public List<ProfessorDto> Professors { get; set; } = new();
    public List<StudentDto> Students { get; set; } = new();
}

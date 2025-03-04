namespace InterRapidisimoDomain.DTOs;

public class CreditProgramDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int TotalCredits { get; set; }
}

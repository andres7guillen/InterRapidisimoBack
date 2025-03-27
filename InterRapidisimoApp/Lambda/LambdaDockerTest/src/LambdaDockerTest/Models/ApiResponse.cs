namespace LambdaDockerTest.Models;

public class ApiResponse
{
    public Info Info { get; set; } = new Info();  // Información sobre la paginación
    public List<Character> Results { get; set; } = new List<Character>();
}

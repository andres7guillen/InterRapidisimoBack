namespace LambdaDockerTest.Models;

public class Info
{
    public int Count { get; set; }  // Total de personajes disponibles
    public int Pages { get; set; }  // Total de páginas
    public string? Next { get; set; }  // URL para la siguiente página de resultados
    public string? Prev { get; set; }  // URL para la página anterior, si existe
}

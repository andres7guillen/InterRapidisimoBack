namespace LambdaDockerTest.Models
{
    public class Character
    {
        public int Id { get; set; }  // ID del personaje
        public string Name { get; set; } = string.Empty;  // Nombre del personaje
        public string Status { get; set; } = string.Empty;  // Estado (Alive, Dead, etc.)
        public string Species { get; set; } = string.Empty;  // Especie del personaje
        public string Type { get; set; } = string.Empty;  // Tipo (puede estar vacío)
        public string Gender { get; set; } = string.Empty;  // Género del personaje
        public OriginLocation Origin { get; set; } = new OriginLocation();  // Origen del personaje
        public OriginLocation Location { get; set; } = new OriginLocation();  // Ubicación actual
        public string Image { get; set; } = string.Empty;  // URL de la imagen del personaje
        public List<string> Episode { get; set; } = new List<string>();  // Lista de episodios en los que aparece
        public string Url { get; set; } = string.Empty;  // URL del personaje en la API
        public DateTime Created { get; set; }  // Fecha de creación del personaje
    }
}

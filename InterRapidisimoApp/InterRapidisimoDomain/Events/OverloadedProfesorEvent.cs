using InterRapidisimoDomain.Entities;

namespace InterRapidisimoDomain.Events;

public class ProfesorSobrecargadoEvent
{
    public Professor Profesor { get; }
    public DateTime FechaOcurrencia { get; }
    public int NumeroMaximoMaterias { get; }

    public ProfesorSobrecargadoEvent(Professor profesor, int numeroMaximoMaterias)
    {
        Profesor = profesor ?? throw new ArgumentNullException(nameof(profesor));
        FechaOcurrencia = DateTime.UtcNow;
        NumeroMaximoMaterias = numeroMaximoMaterias;
    }
}

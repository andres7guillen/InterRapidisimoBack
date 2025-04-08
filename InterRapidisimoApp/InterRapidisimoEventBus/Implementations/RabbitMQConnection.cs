using RabbitMQ.Client;

namespace InterRapidisimoEventBus.Implementations;

public class RabbitMQConnection : IDisposable
{
    private readonly ConnectionFactory _connectionFactory;
    private IConnection _connection;
    private bool _disposed;

    public RabbitMQConnection(string hostname) // Puedes extender esto con usuario, contraseña, etc.
    {
        _connectionFactory = new ConnectionFactory() { HostName = hostname };
    }

    public bool IsConnected => _connection != null && _connection.IsOpen && !_disposed;

    public RabbitMQ.Client.IModel CreateModel() // Especificando completamente el tipo IModel
    {
        if (!IsConnected)
        {
            TryConnect();
        }

        return _connection.CreateModel();
    }

    public bool TryConnect()
    {
        try
        {
            if (_connection == null || !_connection.IsOpen)
            {
                _connection = _connectionFactory.CreateConnection();
            }
            return true;
        }
        catch (Exception ex)
        {
            // Log de la excepción
            return false;
        }
    }

    public void Dispose()
    {
        if (_disposed) return;

        _disposed = true;
        try
        {
            _connection?.Dispose();
        }
        catch (IOException ex)
        {
            // Log de la excepción
        }
    }
}

using Amazon.Lambda.Core;
using CharacterDetail.Models;
using RestSharp;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace CharacterDetail;

public class Function
{

    /// <summary>
    /// A simple function that takes a string and returns both the upper and lower case version of the string.
    /// </summary>
    /// <param name="input">The event for the Lambda function handler to process.</param>
    /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
    /// <returns></returns>
    public async Task<CharacterDetailResponse> FunctionHandler(string id, ILambdaContext context)
    {
        string url = $"https://rickandmortyapi.com/api/character/{id}";
        var client = new RestClient(new RestClientOptions(url));
        var requestApi = new RestRequest();

        try
        {
            // Realiza la solicitud HTTP GET a la API externa
            var response = await client.GetAsync<CharacterDetailResponse>(requestApi);

            // Si la respuesta es null, registra un error
            if (response == null)
            {
                context.Logger.LogInformation("❌ API Response is null");
                return new CharacterDetailResponse();
            }

            // Si la respuesta no es null, la retorna
            return response;
        }
        catch (Exception ex)
        {
            // Si hay un error en la solicitud, lo captura y lo registra
            context.Logger.LogError($"🚨 Error calling API: {ex.Message}");
            return new CharacterDetailResponse();
        }
    }
}
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using LambdaDockerTest.Models;
using Newtonsoft.Json;
using RestSharp;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace LambdaDockerTest;

public class Function
{
    public async Task<APIGatewayProxyResponse?> FunctionHandler(RequestModel request, ILambdaContext context)
    {
        var client = new RestClient(new RestClientOptions(request.Url));
        var requestApi = new RestRequest();

        try
        {
            // Realiza la solicitud HTTP GET a la API externa
            var response = await client.GetAsync<ApiResponse>(requestApi);

            var jsonResponse = JsonConvert.SerializeObject(response);

            return new APIGatewayProxyResponse
            {
                StatusCode = 200,
                Body = jsonResponse, // Asigna el objeto serializado al Body
                Headers = new Dictionary<string, string>
                    {
                        { "Access-Control-Allow-Origin", "*" },
                        { "Access-Control-Allow-Methods", "GET, POST, OPTIONS" },
                        { "Access-Control-Allow-Headers", "Content-Type" }
                    }
            };
        }
        catch (Exception ex)
        {
            context.Logger.LogError($"🚨 Error calling API: {ex.Message}");
            return new APIGatewayProxyResponse
            {
                StatusCode = 500,
                Body = "{\"message\": \"Error processing request\"}", // Mensaje de error serializado a JSON
                Headers = new Dictionary<string, string>
                    {
                        { "Access-Control-Allow-Origin", "*" },
                        { "Access-Control-Allow-Methods", "GET, POST, OPTIONS" },
                        { "Access-Control-Allow-Headers", "Content-Type" }
                    }
            };
        }
    }
}


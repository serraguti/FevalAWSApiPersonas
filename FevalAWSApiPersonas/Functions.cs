using System.Net;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using FevalAWSApiPersonas.Models;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
//[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace FevalAWSApiPersonas;

public class Functions
{
    List<Persona> personasList;
    public Functions()
    {
        this.personasList = new List<Persona>
        {
            new Persona { Nombre = "Lucia", Email = "lucia@gmail.com", Edad = 21},
            new Persona {Nombre = "Adrian", Email = "adrian@gmail.com", Edad=23},
            new Persona { Nombre = "Carlos", Email = "carlos@gmail.com", Edad = 45},
            new Persona { Nombre = "Antonio", Email = "antonio@gmail.com", Edad = 41}
        };
    }

    [LambdaFunction(Policies = "AWSLambdaBasicExecutionRole", MemorySize = 256, Timeout = 30)]
    [RestApi(LambdaHttpMethod.Get, "/")]
    public IHttpResult Get(ILambdaContext context)
    {
        context.Logger.LogInformation("Handling the 'Get' Request");

        string jsonPersonas = JsonConvert.SerializeObject(this.personasList);
        var response = new
        {
            StatusCode = 200,
            Body = jsonPersonas
        };

        return HttpResults.Ok(response);
    }
}

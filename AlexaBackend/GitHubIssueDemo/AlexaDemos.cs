using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET;
using Alexa.NET.Response;

namespace GitHubIssueDemo
{
    public class AlexaDemos
    {
        private readonly ILogger _logger;

        public AlexaDemos(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<AlexaDemos>();
        }

        [Function("Test")]
        public HttpResponseData RunTest([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req, FunctionContext executionContext)
        {
            var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
            response.WriteString("42");
            return response;
        }

        [Function("Alexa")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            string json = await req.ReadAsStringAsync();
            var skillRequest = JsonConvert.DeserializeObject<SkillRequest>(json);

            var requestType = skillRequest.GetRequestType();
            SkillResponse response = null;

            if (requestType == typeof(LaunchRequest))
            {
                response = ResponseBuilder.Tell("Herzlich willkommen zur großartigsten Konferenz in Mannheim the one and only Developer Week 2025 mit dem großartigsten Publikum der Welt. Muuuuhhhhhaaaaaa.");
                response.Response.ShouldEndSession = false;
            }
            else if (requestType == typeof(IntentRequest))
            {
                var intentRequest = skillRequest.Request as IntentRequest;
                if (intentRequest.Intent.Name == "erstellegithubissue")
                {
                    response = ResponseBuilder.Tell($"Ich habe ein Ticket für {intentRequest.Intent.Slots["name"].Value} erstellt. Das ist so episch. Muuuuuuuuuhhhhhhhaaaaaaa.");
                    response.Response.ShouldEndSession = true;

                    GitHubClient ghClient = new GitHubClient();
                    ghClient.CallRepoWebHook(intentRequest.Intent.Slots["name"].Value);
                }
            }

            var httpResponse = req.CreateResponse(System.Net.HttpStatusCode.OK);
            httpResponse.Headers.Add("Content-Type", "application/json; charset=utf-8");
            await httpResponse.WriteStringAsync(JsonConvert.SerializeObject(response));
            return httpResponse;
        }
    }

}


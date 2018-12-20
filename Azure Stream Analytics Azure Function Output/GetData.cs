using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Azure_Stream_Analytics_Azure_Function_Output
{
    public static class GetData
    {
        [FunctionName("GetData")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")]HttpRequestMessage req,
            ILogger log)
        {
            log.LogInformation($"GetData function triggered with Uri {req.RequestUri}");

            string content = await req.Content.ReadAsStringAsync();
            log.LogInformation($"String content is {content}");
            dynamic data = JsonConvert.DeserializeObject(content);

            log.LogInformation($"Data count is {data?.Count}");

            if (data?.ToString()?.Length > 262144)
            {
                return new HttpResponseMessage(HttpStatusCode.RequestEntityTooLarge);
            }

            return req.CreateResponse(HttpStatusCode.OK, "Success");
        }
    }
}

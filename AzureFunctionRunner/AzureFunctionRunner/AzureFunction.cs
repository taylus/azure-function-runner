using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json.Linq;

namespace AzureFunctionRunner
{
    /// <summary>
    /// An Azure Function implementation, the contents of which can be pasted into the portal.
    /// </summary>
    public static class AzureFunction
    {
        /// <summary>
        /// Main Azure Function entry point.
        /// </summary>
        public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
        {
            log.Verbose("Request:\n" + req);
            if (req.Content != null) log.Verbose("Request body:\n" + await req.Content.ReadAsStringAsync());

            var name = await GetNameFrom(req);
            var response = BuildResponseFor(name);

            log.Verbose("Response:\n" + response);
            if (response.Content != null) log.Verbose("Response body:\n" + await response.Content.ReadAsStringAsync());
            return response;
        }

        /// <summary>
        /// Parse a name from the given JSON request, returning null if one isn't found.
        /// </summary>
        private async static Task<string> GetNameFrom(HttpRequestMessage req)
        {
            if (req.Content == null) return null;

            string requestData = await req.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(requestData)) return null;

            var requestObject = JToken.Parse(requestData);
            return requestObject.Type == JTokenType.Object? requestObject.Value<string>("name") : null;
        }

        /// <summary>
        /// Build a response for the given name:
        /// - HTTP 200 if the name is not null or empty.
        /// - HTTP 400 if the name is a null or empty string.
        /// </summary>
        private static HttpResponseMessage BuildResponseFor(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("Hello, " + name + "!") };
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("Please provide a name in your request, e.g. {\"name\": \"Azure\"}.") };
            }
        }
    }
}

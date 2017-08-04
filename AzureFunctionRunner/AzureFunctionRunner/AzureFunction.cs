using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host;

namespace AzureFunctionRunner
{
    /// <summary>
    /// An Azure Function implementation, the contents of which can be pasted into the portal.
    /// </summary>
    public static class AzureFunction
    {
        public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
        {
            log.Verbose("Request:\n" + req);
            if (req.Content != null) log.Verbose("Request body:\n" + await req.Content.ReadAsStringAsync());

            //parse request for a name to greet
            dynamic requestData = await req.Content.ReadAsAsync<object>();
            string name = requestData.name;

            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("Hello, " + name + "!") };
            //var response = new HttpResponseMessage(HttpStatusCode.NoContent);

            log.Verbose("Response:\n" + response);
            if (response.Content != null) log.Verbose("Response body:\n" + await response.Content.ReadAsStringAsync());
            return response;
        }
    }
}

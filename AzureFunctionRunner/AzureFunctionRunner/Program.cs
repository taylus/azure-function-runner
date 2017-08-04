using System;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace AzureFunctionRunner
{
    /// <summary>
    /// A command line-based Azure Function simulator/runner.
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            var request = BuildRequest("Azure");
            var log = new MemoryTraceWriter();

            try
            {
                Console.WriteLine("Received response: {0}\n", AzureFunction.Run(request, log).Result.StatusCode);
            }
            finally
            {
                Console.WriteLine("Azure Function's log messages:\n" + string.Join("\n\n", log.Events));
            }
        }

        /// <summary>
        /// Builds a sample request to send to the Azure Function.
        /// </summary>
        private static HttpRequestMessage BuildRequest(string name)
        {
            return new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                Content = new ObjectContent(typeof(object), new { name = name }, new JsonMediaTypeFormatter())
            };
        }
    }
}

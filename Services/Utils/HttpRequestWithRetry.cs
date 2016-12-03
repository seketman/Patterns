namespace Services.Utils
{
    using System;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web;
    using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;

    /// <summary>
    ///     Http utilities
    /// </summary>
    public static class HttpUtils
    {
        /// <summary>
        ///     Http request with transient fault handling block.
        /// </summary>
        /// <param name="httpClient">Http client</param>
        /// <param name="uri">Resource uri</param>
        /// <returns>Returned data</returns>
        public static Task<HttpResponseMessage> HttpRequestWithRetryAsync(HttpClient httpClient, string uri)
        {
            // Retry strategy: retry 3 times, starting 1 second apart and adding 2 seconds to the interval each retry.
            var retryStrategy = new Incremental(3, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));

            // Retry policy using the retry strategy and the http transient fault detection strategy.
            var retryPolicy = new RetryPolicy<HttpTransientErrorDetectionStrategy>(retryStrategy);

            // Receive notifications about retries.
            retryPolicy.Retrying += (sender, args) =>
            {
                // Log details of the retry.
                Trace.WriteLine($"Retry - Count: {args?.CurrentRetryCount}, Delay: {args?.Delay}, Exception: {args?.LastException?.Message}", "Information");
            };

            // Call http service with and retry in transient fault.
            return retryPolicy.ExecuteAsync(async () =>
            {
                var response = await httpClient.GetAsync(uri);

                if (!response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    throw new HttpException((int)response.StatusCode, responseBody);
                }

                return response;
            });
        }
    }
}

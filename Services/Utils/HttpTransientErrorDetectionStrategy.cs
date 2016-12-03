namespace Services.Utils
{
    using System;
    using System.Net;
    using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;

    /// <summary>
    ///     Custom Transient Error Detection Strategy for httpClient
    /// </summary>
    public class HttpTransientErrorDetectionStrategy : ITransientErrorDetectionStrategy
    {
        /// <summary>
        ///     Determines whether the specified exception represents a transient failure that
        ///     can be compensated by a retry.
        /// </summary>
        /// <param name="ex">The exception object to be verified.</param>
        /// <returns>true if the specified exception is considered as transient; otherwise, false.</returns>
        public bool IsTransient(Exception ex)
        {
            if (ex != null)
            {
                var httpException = ex as System.Web.HttpException;
                if (httpException != null)
                {
                    if (httpException.GetHttpCode() == (int)HttpStatusCode.ServiceUnavailable)
                    {
                        return true;
                    }

                    if (httpException.GetHttpCode() == (int)HttpStatusCode.GatewayTimeout)
                    {
                        return true;
                    }
#if DEBUG
                    if (httpException.GetHttpCode() == (int)HttpStatusCode.InternalServerError)
                    {
                        return true;
                    }
#endif
                    return false;
                }
            }

            return false;
        }
    }
}

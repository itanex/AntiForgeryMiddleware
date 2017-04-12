using AntiForgeryMiddleware.Exceptions;
using System;
using System.Linq;

namespace AntiForgeryMiddleware.Http
{
    internal static class SupportedHttpMethods
    {
        private static readonly string[] HttpMethods = new string[]
        {
            // RFC 7231, Section 4: Request Methods - http://tools.ietf.org/html/7231#section-4
            "CONNECT",
            "DELETE",
            "GET",
            "HEAD",
            "OPTIONS",
            "POST",
            "PUT",
            
            // RFC 5789, section 2: Patch method - http://tools.ietf.org/html/5789#section-2
            "PATCH",
        };

        /// <summary>
        /// Evaluates the provided Array of HttpMethods, returning the strings not supported
        /// </summary>
        /// <param name="httpMethods">Array of HttpMethod to examines</param>
        /// <returns>An array of strings not in the supported list of HttpMethods</returns>
        internal static string[] Unsupported(string[] httpMethods)
        {
            return HttpMethods.Except(httpMethods, StringComparer.OrdinalIgnoreCase).ToArray();
        }
        
        /// <summary>
        /// Tests provided string array for any unsupported HttpMethods
        /// </summary>
        /// <param name="httpMethods">Array of HttpMethods to test</param>
        /// <exception cref="UnsupportedHttpMethod">Thrown if any strings are not in the supported list of HttpMethods</exception>
        internal static void ValidateHttpMethods(string[] httpMethods)
        {
            var results = Unsupported(httpMethods);

            if (results.Any())
            {
                throw new UnsupportedHttpMethod(String.Join(@", ", results));
            }
        }
    }
}

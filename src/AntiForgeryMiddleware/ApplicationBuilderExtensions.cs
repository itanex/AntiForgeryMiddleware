using Microsoft.AspNetCore.Builder;

namespace AntiForgeryMiddleware
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Enable Antiforgery Token to be set for AJAX requests using using Header only HTTP Methods
        /// </summary>
        /// <param name="builder">IApplicationBuilder reference</param>
        /// <param name="requestTokenCookieName">The name to apply to the HttpCookie for use with Ajax Requests</param>
        /// <returns>Assembled Middleware instance</returns>
        public static IApplicationBuilder UseAjaxAntiforgeryToken(
            this IApplicationBuilder builder, 
            string requestTokenCookieName)
        {
            return builder.UseMiddleware<AjaxAntiforgeryToken>(requestTokenCookieName);
        }

        /// <summary>
        /// Enable Antiforgery Token to be set for AJAX requests using using specified Http Methods
        /// </summary>
        /// <param name="builder">IApplicationBuilder reference</param>
        /// <param name="requestTokenCookieName">The name to apply to the HttpCookie for use with Ajax Requests</param>
        /// <param name="httpMethods">Specific HttpMethods on which to create the HttpCookie</param>
        /// <returns>Assembled Middleware instance</returns>
        public static IApplicationBuilder UseAjaxAntiforgeryToken(
            this IApplicationBuilder builder, 
            string requestTokenCookieName,
            string[] httpMethods)
        {
            return builder.UseMiddleware<AjaxAntiforgeryToken>(requestTokenCookieName, httpMethods);
        }
    }
}

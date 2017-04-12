using AntiForgeryMiddleware.Http;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AntiForgeryMiddleware
{
    public sealed class AjaxAntiforgeryToken
    {
        private readonly RequestDelegate _next;
        private readonly string _requestTokenCookieName;
        private readonly string[] _httpMethods;

        /// <summary>
        /// Enable Antiforgery Token to be set for AJAX requests using Header only HTTP Methods
        /// </summary>
        /// <param name="next">Middleware Request Delegate</param>
        /// <param name="requestTokenCookieName">The name to apply to the HttpCookie for use with Ajax Requests</param>
        public AjaxAntiforgeryToken(RequestDelegate next, string requestTokenCookieName)
        {
            _next = next;
            _httpMethods = new string[] { "GET", "HEAD", "OPTIONS", "TRACE" };
            _requestTokenCookieName = requestTokenCookieName;
        }

        /// <summary>
        /// Enable Antiforgery Token to be set for AJAX requests using specified Http Methods
        /// </summary>
        /// <param name="next">Middleware Request Delegate</param>
        /// <param name="requestTokenCookieName">The name to apply to the HttpCookie for use with Ajax Requests</param>
        /// <param name="httpMethods">Specific HttpMethods on which to create the HttpCookie</param>
        public AjaxAntiforgeryToken(RequestDelegate next, string requestTokenCookieName, string[] httpMethods)
        {
            SupportedHttpMethods.ValidateHttpMethods(httpMethods);

            _next = next;
            _httpMethods = httpMethods;
            _requestTokenCookieName = requestTokenCookieName;
        }
        
        public async Task Invoke(HttpContext context, IAntiforgery antiforgery)
        {
            if (_httpMethods.Contains(context.Request.Method, StringComparer.OrdinalIgnoreCase))
            {
                var tokens = antiforgery.GetAndStoreTokens(context);

                context.Response.Cookies.Append(_requestTokenCookieName, tokens.RequestToken, new CookieOptions()
                {
                    HttpOnly = false
                });
            }

            await _next.Invoke(context);
        }
    }
}

# AntiForgery Middleware

Due to the needs of Angular, React and other UI frameworks which are not processed through an MVC Razor view the need to present a solution where the AntiForgery token that is normally written into a `form`.

## What is Antiforgery

Antiforgery is the process of establishing a protocol between a client and a server to verify that the proceeding requests from a client are from the same trusted client that the server trusted prior.

## Angular v1.x.x

Specifically with Angular v1.x.x the HTTP Service object `$http` has a built in process that seeks out a cookie with the name `XSRF-TOKEN` when an HTTP Request is made and attaches the cookies value into a pre-defined header, `X-XSRF-TOKEN`. 

## ASP.NET Core Antiforgery

ASP.NET has provided the Antiforgery service for nearly its entire existance; with ASP.NET Core the newest iteration was released. Though in principal much has not changed. In the simplest of descriptions, the service generates an HTTPOnly cookie that is used to verify the request process and the suggested practice is to produce a second token and place it into the body of the HTTP Response, in turn both of these values are passed back to the server and validated.

## The Solution

As with Single Page Apps and other similarly architected web solutions, requests are not regularly or even plausibly servicable through a rendered response. An alternative needed to be found.

Many different solutions for this process involve re-creating a majority of the Antiforgery service. The most common solution is to create a new `ValidateAntiforgery` attribute. As with any security service created to solve problems it recreates the appearance of a secure process, yet it also removes the viable testing that has already went into a system we can already utilize.

The solution provided herein has not internal mechanism for validating tokens. It leaves this up to the Antiforgery service that is already managed by the ASP.NET teams. All it does is generates a new Antiforgery Token cookie using the `Antiforgery Request Token`, which is the same token value that is expected on either a form field, or an HTTP Header.

### Implementation

1) Include the `Microsoft.AspNetCore.Antiforgery` library so that you can change the name of the header that it will expect to validate.

````csharp
startup.cs

    public void ConfigureServices(IServiceCollection services)
    {
        ...
        services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");
        ...
    }
````
2) Include this AntiforgeryMiddleware and establish the name of the cookie it will generate

````csharp
startup.cs

    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
        ...
        app.UseAjaxAntiforgeryToken("XSRF-TOKEN");
        ...
    }
````
3) Utilize the `[ValidateAntiForgeryToken]` and `[AutoValidateAntiforgeryToken]` as they are designed and documented. There are no changes!


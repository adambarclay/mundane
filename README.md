<img align="left" width="116" src="https://raw.githubusercontent.com/adambarclay/mundane/main/build/Mundane.png"/>

# Mundane

[![License: MIT](https://img.shields.io/github/license/adambarclay/mundane?color=blue)](https://github.com/adambarclay/mundane/blob/main/LICENSE) [![nuget](https://img.shields.io/nuget/v/Mundane)](https://www.nuget.org/packages/Mundane/) [![build](https://img.shields.io/github/actions/workflow/status/adambarclay/mundane/build.yml?branch=main)](https://github.com/adambarclay/mundane/actions?query=workflow%3ABuild+branch%3Amain) [![coverage](https://img.shields.io/codecov/c/github/adambarclay/mundane/main)](https://codecov.io/gh/adambarclay/mundane/branch/main)

Mundane is a lightweight "no magic" web framework for .NET.

```c#
    routeConfiguration.Get("/hello-world", () => Response.Ok(o => o.Write("Hello World!"))));
```

## Getting Started

For ASP.NET, install the [Mundane.Hosting.AspNet](https://www.nuget.org/packages/Mundane.Hosting.AspNet/) nuget package, then in your ASP.NET startup code call `app.UseMundane();`, passing in the routing and dependencies configuration.

### Example
```c#
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        var dependencies = new Dependencies(
            new Dependency<Configuration>(new Configuration(env)),
            new Dependency<DataRepository>(request => new DataRepositorySqlServer(
                request.Dependency<Configuration>().ConnectionString)));

        var routing = new Routing(
            routeConfiguration =>
            {
                routeConfiguration.Get("/", HomeController.HomePage);
                routeConfiguration.Get("/data/{id}", DataController.GetData);
                routeConfiguration.Post("/data/{id}", DataController.UpdateData);
            });

        app.UseMundane(dependencies, routing);
    }
```

## Request/Response Model

Mundane uses a simple request/response model to handle HTTP requests. There is no automatic dependency injection or model validation, so all request parameters and dependencies must be retrieved from the `Request` object. Passing the `Request` object to the endpoint is optional but the endpoint must return a `Response` object.

### Example
```c#
    internal static class HomeController
    {
        internal static Response HomePage()
        {
            return Response.Ok(o => o.Write(Views.HomePage));
        }
    }

    internal static class DataController
    {
        internal static async ValueTask<Response> GetData(Request request)
        {
            var dataRepository = request.Dependency<DataRepository>();

            var data = await dataRepository.GetData(request.Route("id"));
			
            return Response.Json(async o => await JsonSerializer.SerializeAsync(o.Stream, data));
        }
    }
```

### The Request

The `Request` object contains all of the parameters passed to the request, and the dependencies registered at startup.

```c#
    routeConfiguration.Get("/example/{id}", request =>
    {
        var cookieValue = request.Cookie("cookieName");
        var uploadedFile = request.File("fileParameterName");
        var formValue = request.Form("formParameterName");
        var headerValue = request.Header("headerName");
        var queryValue= request.Query("queryParameterName");
        var routeParameter = request.Route("id");

        var dataRepository = request.Dependency<DataRepository>();

        return Response.Ok();
    }
```

All of the request parameter methods return empty string if the parameter was not sent (or `FileUpload.Unknown` in the case of `request.File()`). It is possible to check if a parameter was sent with the `*Exists()` methods.

```c#
    routeConfiguration.Get("/example", request =>
    {
        var cookieExists = request.CookieExists("cookieName");
        var uploadedFileExists = request.FileExists("fileParameterName");
        var formExists = request.FormExists("formParameterName");
        var headerExists = request.HeaderExists("headerName");
        var queryExists = request.QueryExists("queryParameterName");

        return Response.Ok();
    }
```

### The Response

The `Response` object is made up of two parts, the header and the body.

The header includes the HTTP status code, and the HTTP response headers.

The body is supplied to the `Response` constructor as a lambda expression, which is executed after the response is returned, to guarantee the body is only written to the output after all of the headers have been set.

##### Status Code
The `Response` can be constructed with a custom status code value

```c#
    return new Response(200, o => o.Write("Eveything is OK!"));
```

or by using one of the static helper methods on the `Response` object.

```c#
    // Status code 200, content-type=text/html;charset=utf-8
    return Response.Ok(o => o.Write("Eveything is OK!"));
```

##### Response Headers
Response headers can be added using the `AddHeader()` method and either a custom `HeaderValue` e.g. `new HeaderValue("MyHeader", "MyValue")`, or by using the static helper methods of `HeaderValue`.

```c#
    return Response.File(o => o.Write(imageData.Bytes)), "image/png")
        .AddHeader(HeaderValue.CacheControl("public, max-age=31536000"))
        .AddHeader(HeaderValue.LastModified(imageData.LastModified));
```

##### Cookies

Cookies are HTTP headers so they are also added with the `AddHeader()` method.
```c#
    internal static async ValueTask<Response> Authenticate(Request request)
    {
        var userRepository = request.Dependency<UserRepository>();

        var user = await userRepository.FindUser(request.Form("userName"), request.Form("password"));

        if (user == User.Unknown)
        {
            return Response.Unauthorized()
                .AddHeader(HeaderValue.DeleteCookie("auth"));
        }

        return Response.RedirectSeeOther("/my-account")
            .AddHeader(HeaderValue.PersistentCookie(
                "auth",
                userRepository.GenerateAuthToken(user),
                TimeSpan.FromHours(24)));
    }
```

## Routing

### Route Configuration

Mundane has four endpoint signatures:

```c#
    Response Endpoint();
    Response Endpoint(Request request);
    ValueTask<Response> Endpoint();
    ValueTask<Response> Endpoint(Request request);
```

Endpoints are added to the `Routing` configuration using `RouteConfiguration`. The HTTP methods `DELETE`, `GET`, `POST`, and `PUT` are supported, along with any custom method using `Endpoint()`. A custom 404 handler can optionally be specified as the second parameter of the `Routing` constructor.

```c#
    new Routing(
        routeConfiguration =>
        {
            routeConfiguration.Delete("/delete", () => Response.Ok());
            routeConfiguration.Get("/get", request => Response.Ok());
            routeConfiguration.Post("/post", () => ValueTask.FromResult(Response.Ok());
            routeConfiguration.Put("/put", request => ValueTask.FromResult(Response.Ok());
            routeConfiguration.Endpoint("PATCH", "/patch", PatchController.Patch);
        },
        NotFoundController.NotFound);
```

### Routes

Routes must begin with a forward slash `"/"`.

A route with a trailing slash will be treated differently to a route without a trailing slash,  
e.g. `"/my-route"` and `"/my-route/"` are two different routes.

#### Capture Parameters

Routes can include capture parameters specified with curly braces e.g. `"/my-route/{id}"`.

When a request is made to `"/my-route/123"`, `request.Route("id") == "123"`.

#### Greedy Capture Parameters

Routes can also include greedy capture parameters, which will capture the rest of the URL, including any additional forward slashes.

Greedy parameters are specified with an asterisk before the closing curly brace e.g. `"/my-route/{path*}"`.

When a request is made to `"/my-route/123/456"`, `request.Route("path") == "123/456"`.

Greedy capture parameters can only appear once in a route and must be at the end.

#### Multiple Capture Parameters
A route may contain any number of capture parameters, as long as there is no more than one greedy parameter.

```c#
    routeConfiguration.Get("/shop/{productType}/{id}", ProductController.ShowProduct);
    routeConfiguration.Get("/shop/{productType}/search/{searchTerm*}", SearchController.Search);
```

#### Custom Routing

Mundane routing is deliberately kept simple. If more complicated routing is required, use a greedy capture parameter and your own route processing.

```c#
    routeConfiguration.Get(
        "/{customRouting*}",
        request =>
        {
            var routeParameters = MyCustomRouting.Execute(request.Route("customRouting"));

            if (routeParameters.IsAFooRoute)
            {
                return FooController.Foo(request, routeParameters);
            }
            
            if (routeParameters.IsABarRoute)
            {
                return BarController.Bar(request, routeParameters);
            }
        });
```

## Dependencies

The `Request` object uses the `DependencyFinder` interface to find registered dependencies. `Find()` is passed the current `Request` object in case any dependencies rely on parameters contained in the request.

```c#
    public interface DependencyFinder
    {
        T Find<T>(Request request);
    }
```

Mundane comes with a default implementation of `DependencyFinder` called `Dependencies`.

The constructor for `Dependencies` takes zero or more `Dependency<T>` objects as parameters.

```c#
    var dependencies = new Dependencies(
        new Dependency<Configuration>(new Configuration(env)),
        new Dependency<DataRepository>(request => new DataRepositorySqlServer(
            request.Dependency<Configuration>().ConnectionString)),
        new Dependency<EmailSender>(() => new EmailSenderSmtp()));
```

The `Dependency<T>` type parameter is the type which will be requested (often an interface or a base class).

The `Dependency<T>` constructor parameter is either an object instance (which will exist for the whole lifetime of the application), or a lambda expression to create an instance of the requested type, which will create a new instance each time it is called.

The lambda expression optionally takes the current `Request` as a parameter.

`Dependencies` does not automatically find dependencies of the object being created, so chained dependencies must be requested explicitly by calling `request.Dependency()` during creation.

## Validation

Mundane includes some helpers to validate parameters in the request.

In this example for updating a user profile, the controller makes use of the `Validator` class to convert the parameters in the request into a command object.

```c#
    internal static class ProfileController
    {
        // POST /profile/edit
        internal static async ValueTask<Response> UpdateProfile(Request request)
        {
            (var invalid, var updateProfileCommand) = Validator.Validate(
                validator => new UpdateProfileCommand(request, validator));

            if (invalid)
            {
                // Show the input form prefilled with the values and error messages in updateProfileCommand.
                return Response.Ok(...);
            }

            var profileRepository = request.Dependency<ProfileRepository>();

            await profileRepository.UpdateProfile(updateProfileCommand);

            // Success.
            return Response.RedirectSeeOther("/profile");
        }
    }
```

In this example, the validation takes place in the command object constructor by calling the `Validate()` method.

The Validated&lt;T&gt; properties contain the validated value and the list of errors which occurred for that property during validation.

```c#
    internal sealed class UpdateProfileCommand
    {
        // Initalise all of the validated properties to strings which will be displayed on the form.
        internal UpdateProfileCommand()
        {
            this.Name = string.Empty;
            this.Email = string.Empty;
            this.FavouriteNumber = (0, string.Empty); // Initial value with initial display string.
            /* etc... */
        }

        internal UpdateProfileCommand(Request request, Validator validator)
        {
            this.Name = validator.Value(request.Form("Name"))
                .Validate(name => name.Length > 0, "You must enter a name.")
                .Validate(name => name.Length <= 200, "Name must be no more than 200 characters.");

            this.Email = validator.Value(request.Form("Email"))
                .ValidEmail();

            // Value() can also convert from string to another type.
            this.FavouriteNumber = validator.Value(
                request.Form("FavouriteNumber"),
                int.Parse,
                0,
                "Favourite number must be an integer");

            /* etc... */
        }

        internal Validated<string> Name { get; }

        internal Validated<string> Email { get; }

        internal Validated<int> FavouriteNumber { get; }

        /* etc... */
    }
```

Use extension methods to create custom reusable validation methods.

```c#
    internal static class ValidationExtensions
    {
        internal static Validated<string> ValidEmail(this Validated<string> validated)
        {
            return validated.Validate(
                email => email.Length > 0 &&
                    email.Length <= 254 &&
                    email.Contains('@', StringComparison.Ordinal) &&
                    email.All(c => !char.IsWhiteSpace(c)),
                "You must supply a valid email address.");
        }
    }
```

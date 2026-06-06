using HomeServer.Services;
using Newtonsoft.Json;

namespace HomeServer.Middleware;

public class ExceptionMiddleware(RequestDelegate next) {
    public async Task InvokeAsync(HttpContext context) {
        try {
            await next(context);
        } catch (Exception e) when (e is RegisterDataException or LoginDataException) {
            context.Response.StatusCode = 400;
            //context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(FormatErrorResponse(e.Message));
        } catch (UserAlreadyExistsException e) {
            context.Response.StatusCode = 409;
            //context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(FormatErrorResponse(e.Message));
        } catch (Exception e) {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(FormatErrorResponse(e.Message));
        }
    }

    private string FormatErrorResponse(string messages) {
        return JsonConvert.SerializeObject(new {
            errors = messages,
        });
    }
}
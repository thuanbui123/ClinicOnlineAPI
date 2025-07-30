using ClinicOnline.Core.DTOs;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ClinicOnline.Core.Exceptions;

public class HandleExceptionMiddleware
{
    private RequestDelegate _next;
    public HandleExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ClinicValidateException ex)
        {
            var serviceResult = new ClinicServiceResult();
            context.Response.StatusCode = 400;
            serviceResult.Errors.Add(ex.Message);
            var res = JsonConvert.SerializeObject(serviceResult);
            await context.Response.WriteAsync(res);
        }
        catch (Exception ex)
        {
            var serviceResult = new ClinicServiceResult();
            context.Response.StatusCode = 500;
            serviceResult.Errors.Add(ex.Message);
            var res = JsonConvert.SerializeObject(serviceResult);
            await context.Response.WriteAsync(res);
        }
    }
}

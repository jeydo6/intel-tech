using System;
using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IntelTech.Users.Presentation.Filters;

internal sealed class ApplicationExceptionFilter : IExceptionFilter
{
    private readonly IWebHostEnvironment _environment;

    public ApplicationExceptionFilter(IWebHostEnvironment environment)
        => _environment = environment;

    public void OnException(ExceptionContext context)
    {
        if (_environment.IsDevelopment() || context.Exception is not ApplicationException)
            return;

        var problemDetailsFactory = context.HttpContext.RequestServices.GetRequiredService<ProblemDetailsFactory>();

        var problemDetails = problemDetailsFactory.CreateProblemDetails(
            context.HttpContext,
            (int)HttpStatusCode.BadRequest,
            detail: context.Exception.Message);

        context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
        context.Result = new ObjectResult(problemDetails) { StatusCode = problemDetails.Status };
    }
}

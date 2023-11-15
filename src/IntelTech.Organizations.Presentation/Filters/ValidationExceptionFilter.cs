using System.Net;
using System.Net.Mime;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IntelTech.Organizations.Presentation.Filters;

internal sealed class ValidationExceptionFilter : IExceptionFilter
{
    private readonly IWebHostEnvironment _environment;

    public ValidationExceptionFilter(IWebHostEnvironment environment)
        => _environment = environment;

    public void OnException(ExceptionContext context)
    {
        if (_environment.IsDevelopment() || context.Exception is not ValidationException)
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

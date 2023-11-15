using System.Net;
using System.Net.Mime;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace IntelTech.Users.Presentation.Filters;

internal sealed class ValidationExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is not ValidationException)
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

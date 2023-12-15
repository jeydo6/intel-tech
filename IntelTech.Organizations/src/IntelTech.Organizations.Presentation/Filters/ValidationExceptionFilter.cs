using System.Net;
using System.Net.Mime;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace IntelTech.Organizations.Presentation.Filters
{
    internal sealed class ValidationExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception as ValidationException;
            if (exception is null)
                return;

            var problemDetailsFactory = context.HttpContext.RequestServices.GetRequiredService<ProblemDetailsFactory>();

            var problemDetails = problemDetailsFactory.CreateProblemDetails(
                context.HttpContext,
                (int)HttpStatusCode.BadRequest,
                detail: exception.Message);

            context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
            context.Result = new ObjectResult(problemDetails) { StatusCode = problemDetails.Status };
        }
    }
}

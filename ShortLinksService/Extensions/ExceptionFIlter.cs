using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ShortLinksService.Extensions;

public class ExceptionFIlter:IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        int statusCode = context.Exception switch
        {
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            KeyNotFoundException => StatusCodes.Status404NotFound,
            ValidationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        context.Result = new JsonResult(context.Exception.Message)
        {
            StatusCode = statusCode
        };
        
        context.ExceptionHandled = true;
    }
}
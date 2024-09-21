using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Track.Application.Common.Exceptions;

namespace Track.Api.Filters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

    public ApiExceptionFilterAttribute()
    {
        // Register known exception types and handlers.
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(ValidationException), HandleValidationException },
                { typeof(NotFoundException), HandleNotFoundException }
               // { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException },
               // { typeof(ForbiddenAccessException), HandleForbiddenAccessException },
            };
    }

    public override void OnException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        Type type = context.Exception.GetType();
        if (_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type].Invoke(context);
            return;
        }

        if (!context.ModelState.IsValid)
        {
            HandleInvalidModelStateException(context);
            return;
        }
    }

    private void HandleValidationException(ExceptionContext context)
    {
        var exception = (ValidationException)context.Exception;

        //var details = new ValidationProblemDetails(exception.Errors)
        //{
        //    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        //};
        var result = new ApiResult<IDictionary<string, string[]>>(exception.Errors,
            string.Empty, ApiResultStatusCode.BadRequest, false);
        context.Result = new BadRequestObjectResult(result);

        context.ExceptionHandled = true;
    }

    private void HandleInvalidModelStateException(ExceptionContext context)
    {
        var details = new ValidationProblemDetails(context.ModelState)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };
        var apiResponse =
            new ApiResult<ModelStateDictionary>(context.ModelState, string.Empty, ApiResultStatusCode.BadRequest,
                false);
        context.Result = new BadRequestObjectResult(apiResponse);

        context.ExceptionHandled = true;
    }

    private void HandleNotFoundException(ExceptionContext context)
    {
        var exception = (NotFoundException)context.Exception;

        //var details = new ProblemDetails()
        //{
        //    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
        //    Title = "The specified resource was not found.",
        //    Detail = exception.Message
        //};
        var apiResponse =
            new ApiResult<string>(exception.Message, string.Empty, ApiResultStatusCode.NotFound, false);
        context.Result = new NotFoundObjectResult(apiResponse);

        context.ExceptionHandled = true;
    }

    private void HandleUnauthorizedAccessException(ExceptionContext context)
    {
        //var details = new ProblemDetails
        //{
        //    Status = StatusCodes.Status401Unauthorized,
        //    Title = "Unauthorized",
        //    Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
        //};

        var result = new ApiResult(string.Empty, ApiResultStatusCode.UnAuthorized, false);

        context.Result = new ObjectResult(result)
        {
            StatusCode = StatusCodes.Status401Unauthorized
        };

        context.ExceptionHandled = true;
    }

    private void HandleForbiddenAccessException(ExceptionContext context)
    {
        //var details = new ProblemDetails
        //{
        //    Status = StatusCodes.Status403Forbidden,
        //    Title = "Forbidden",
        //    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
        //};

        var result = new ApiResult(string.Empty, ApiResultStatusCode.Forbidden, false);
        context.Result = new ObjectResult(result)
        {
            StatusCode = StatusCodes.Status403Forbidden
        };

        context.ExceptionHandled = true;
    }
}
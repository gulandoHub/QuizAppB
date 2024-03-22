using System.Net;
using Exceptionless;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using QuizData;
using QuizService;


namespace QuizMvc.Helpers
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogService logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    #region logError

                    if (contextFeature != null)
                    {
                        logger.LogError($"Something went wrong: {contextFeature.Error}");
                    }

                    #endregion

                    #region log to ExceptionLess

                    app.UseExceptionless("jWD5J6z5bAhRROVzXQYETeaqvg6yMtOnyh1JqFhi");
                    if (contextFeature != null)
                    {
                        var exceptionLess = contextFeature.Error.ToExceptionless();
                        exceptionLess.Submit();
                    }

                    #endregion

                    #region Redirect to Error Page

                    context.Response.Redirect("/Error");

                    #endregion
                });
            });

        }
    }
}



//await context.Response.WriteAsync(new ErrorDetails()
//{
//    StatusCode = context.Response.StatusCode,
//    Message = contextFeature.Error.Message
//}.ToString());
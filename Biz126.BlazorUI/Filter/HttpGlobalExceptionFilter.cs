using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Biz126.BlazorUI
{
    public class HttpGlobalExceptionFilter: IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var eventId = new EventId(context.Exception.HResult);
            Logger.Log4Net.ErrorInfo($"异常信息,EventId:{eventId},描述:{context.Exception.Message}", context.Exception);
            context.Result = new ApplicationErrorResult(new Models.ReturnModel<object> { Status = false, Message = $"发生错误，请与管理员联系。" });
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }

    public class ApplicationErrorResult : ObjectResult
    {
        public ApplicationErrorResult(object value) : base(value)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}

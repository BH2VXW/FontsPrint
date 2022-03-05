using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Biz126.WebUI
{
    public class ModelValidationFilterAttribute: ActionFilterAttribute
    {        

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false)
            {
                var result = new Models.ReturnModel<object>()
                {
                    Status = false
                };

                // Return the validation errors in the response body.
                // 在响应体中返回验证错误

                result.Message = "提交的数据不符合规则";
                var errors = new Dictionary<string, IEnumerable<string>>();
                foreach (KeyValuePair<string, ModelStateEntry> keyValue in actionContext.ModelState)
                {
                    errors[keyValue.Key] = keyValue.Value.Errors.Select(e => e.ErrorMessage);
#pragma warning disable CS8601 // 可能的 null 引用赋值。
                    result.Message = errors[keyValue.Key].FirstOrDefault();
#pragma warning restore CS8601 // 可能的 null 引用赋值。
                    break;
                }
                result.Data = errors;         
                ContentResult Content = new ContentResult()
                {
                    Content = JsonConvert.SerializeObject(result, new JsonSerializerSettings
                    {
                        ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()     //驼峰名称
                    }),
                    ContentType = "application/json",
                    StatusCode = (int?)HttpStatusCode.OK
                };
                actionContext.Result = Content;
                //actionContext.Result.CreateResponse(HttpStatusCode.BadRequest, result, "application/json");
                
            }
        }
    }
}

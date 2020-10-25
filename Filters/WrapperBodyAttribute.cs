using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NSql2Table.Models.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NSql2Table.Filters
{

    public class WrapperBodyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Exception exception = context.Exception;
            if (exception == null)
            {
                //不是跳转也不是出错
                if (context.HttpContext.Response.StatusCode < (int)HttpStatusCode.Ambiguous)
                {
                    if (context.Result is ObjectResult)
                    {
                        var objectResult = context.Result as ObjectResult;
                        if (objectResult.Value == null)
                        {
                            context.Result = new ObjectResult(new 
                            { 
                                code = 404,
                                success = false,
                                msg = "找不到资源请求" 
                            });
                        }
                        else
                        {
                            if (!(objectResult.Value is ReturnModel))
                            {
                                context.Result = new ObjectResult(new ReturnModel<object>
                                {
                                    msg = "请求成功",
                                    code = 200,
                                    success = true,
                                    data = objectResult.Value
                                });
                            }
                        }
                    }

                    else if (context.Result is EmptyResult)
                    {
                        context.Result = new ObjectResult(new { code = 404, msg = "未找到资源" });
                    }
                    else if (context.Result is ContentResult)
                    {
                        context.Result = new ObjectResult(new { code = 200, msg = "请求成功", result = (context.Result as ContentResult).Content });
                    }
                    else if (context.Result is StatusCodeResult)
                    {
                        context.Result = new ObjectResult(new { code = (context.Result as StatusCodeResult).StatusCode,  msg = "" });
                    }
                }
            }
        }
    }
}

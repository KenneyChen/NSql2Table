
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Kulv.YCF.API.Attributes
{
    public class CoresAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
            actionExecutedContext.HttpContext.Response.Headers.Remove("Access-Control-Allow-Origin");

            actionExecutedContext.HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "x-requested-with,content-type");
            actionExecutedContext.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            actionExecutedContext.HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "OPTIONS,POST,GET");
        }
    }
}
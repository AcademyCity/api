using Core.Common;
using Core.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Core.ExceptionHandler
{
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        //重写基类的异常处理方法
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            //1.异常日志记录
            Log.Error(actionExecutedContext.Exception, actionExecutedContext.Exception.Message.ToString() +
                " -> "); //+actionExecutedContext.Exception.StackTrace.ToString()
            Log.Warn(actionExecutedContext.Exception, actionExecutedContext.Exception.Message.ToString() +
                " -> " + actionExecutedContext.Exception.StackTrace.ToString());

            actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(JsonConvert.SerializeObject(new { success = false, message = "服务器发生错误！请联系相关人员..." }))
            };

            base.OnException(actionExecutedContext);
        }
    }
}

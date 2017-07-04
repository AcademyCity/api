using Core.Authorize;
using Core.ExceptionHandler;
using Core.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApi.Controllers
{
    [RequestAuthorize]
    [WebApiExceptionFilter]
    public class BaseApiController : ApiController
    {
        protected RedisManage _redisManager;
        public BaseApiController()
        {
            _redisManager = new RedisManage();
        }

    }
}

using Core.Common;
using Core.Entity;
using Core.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class VipController : BaseApiController
    {

        [HttpGet]
        public IHttpActionResult GetVip(string openId)
        {
            return Json<Result>(new Result { success = true, message = "xxx" });
        }
    }
}

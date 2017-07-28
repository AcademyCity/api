using Core.Common;
using Core.Entity;
using Core.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Application;

namespace WebApi.Controllers
{
    public class VipController : BaseApiController
    {


        VipService _vipService;

        public VipController()
        {
            _vipService = new VipService();
        }

        [HttpGet]
        public IHttpActionResult GetVip(string openId)
        {
            return Json<Result>(new Result { success = true, message = _vipService.GetVipInfo(openId) });
        }

        [HttpOptions]
        public string Options()
        {
            return null; // HTTP 200 response with empty body
        }
    }
}

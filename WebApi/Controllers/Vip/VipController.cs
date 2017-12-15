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

        //[HttpGet]
        //public IHttpActionResult GetVip(string openId = "")
        //{
        //    var vip = _vipService.GetVipInfo(openId);
        //    if (vip != null)
        //    {
        //        return Json(new { success = true, message = vip });
        //    }
        //    return Json(new { success = false, message = "未注册，请注册后登录！" });
        //}
    }
}

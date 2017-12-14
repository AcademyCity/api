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
    [AllowAnonymous]
    public class PointController : ApiController
    {
        VipService _vipService;
        PointService _pointService;
        CouponService _couponService;

        public PointController()
        {
            _vipService = new VipService();
            _pointService = new PointService();
            _couponService = new CouponService();
        }

        [HttpGet]
        public IHttpActionResult GetVipPoint(string openId = "")
        {
            var vip = _vipService.GetVipInfo(openId);
            var point = _pointService.GetVipPoint(vip);
            if (point != null)
            {
                var pointRecord = _pointService.GetVipPointRecord(vip);
                var couponName = _couponService.GetCouponHint((int)point.VipPoint);
                return Json(new { success = true, message = new { score = point.VipPoint, pointRecord = pointRecord , couponName = couponName } });
            }
            return Json(new { success = false, message = "发生错误！" });
        }

        [HttpGet]
        public IHttpActionResult UsePoint(string openId = "", int score = 0)
        {
            var vip = _vipService.GetVipInfo(openId);
            var point = _pointService.GetVipPoint(vip);

            if (point != null && score > 0)
            {
                if (point.VipPoint > score)
                {
                    if (_pointService.UsePoint(vip, score))
                    {
                        return Json(new { success = true, message = "兑换成功！" });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "积分不足！" });
                }

            }
            return Json(new { success = false, message = "发生错误！" });
        }

    }
}

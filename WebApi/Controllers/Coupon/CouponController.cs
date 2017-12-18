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
    
    public class CouponController : BaseApiController
    {
        VipService _vipService;
        CouponService _couponService;
        PointService _pointService;

        public CouponController()
        {
            _vipService = new VipService();
            _couponService = new CouponService();
            _pointService = new PointService();
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetVipCoupon(string openId = "")
        {
            var vip = _vipService.GetVipInfo(openId);
            var coupon = _couponService.GetVipCoupon(vip);

            if (coupon != null)
            {
                return Json(new { success = true, message = coupon });
            }
            return Json(new { success = false, message = "发生错误！" });
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetCoupon(string couponId = "")
        {
            var coupon = _couponService.GetCoupon(couponId);

            if (coupon != null)
            {
                return Json(new { success = true, message = coupon });
            }
            return Json(new { success = false, message = "发生错误！" });
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetShowCoupon()
        {
            var coupon = _couponService.GetShowCoupon();

            if (coupon != null)
            {
                return Json(new { success = true, message = coupon });
            }
            return Json(new { success = false, message = "发生错误！" });
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetShowCouponInfo(string couponConfigId = "")
        {
            var coupon = _couponService.GetShowCoupon(couponConfigId);

            if (coupon != null)
            {
                return Json(new { success = true, message = coupon });
            }
            return Json(new { success = false, message = "发生错误！" });
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult ExChangeCoupon(string openId = "", string couponConfigId = "")
        {
            var vip = _vipService.GetVipInfo(openId);
            var coupon = _couponService.GetShowCoupon(couponConfigId);

            if (coupon != null && vip != null)
            {
                var point = _pointService.GetVipPoint(vip);
                if (point != null)
                {
                    if (point.VipPoint >= coupon.CouponPoint)
                    {
                        if (_couponService.ExChangeCoupon(vip.VipId, coupon.CouponConfigId))
                        {
                            return Json(new { success = true, message = "兑换成功！" });
                        }
                    }
                    else
                    {
                        return Json(new { success = false, message = "积分不足！" });
                    }
                }
            }
            return Json(new { success = false, message = "发生错误！" });
        }
    }
}

using Core.Common;
using Core.Entity;
using Core.Redis;
using Repository.VipManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Dto;
using Newtonsoft.Json;
using Repository.CouponManage;
using Repository.PointManage;

namespace Application
{
    public class CouponService
    {
        //程序逻辑锁
        private static object locker = new object();

        private CouponRepository _couponRepository;

        public CouponService()
        {
            _couponRepository = new CouponRepository();
        }

        /// <summary>
        /// 获取会员积分
        /// </summary>
        /// <param name="vipId"></param>
        /// <returns></returns>
        public List<CouponDto> GetVipCoupon(Vip vip)
        {
            if (vip != null)
            {
                return _couponRepository.QueryCouponByVipId(vip.VipId);
            }
            return null;
        }

        /// <summary>
        /// 获取会员积分
        /// </summary>
        /// <param name="vipId"></param>
        /// <returns></returns>
        public CouponDto GetCoupon(string couponId)
        {
            return _couponRepository.QueryCouponByCouponId(couponId);
        }

        /// <summary>
        /// 获取商城优惠券
        /// </summary>
        /// <param name="vipId"></param>
        /// <returns></returns>
        public List<CouponDto> GetShowCoupon()
        {
            var ShowCouponList = _couponRepository.QueryCouponIsShow();
            foreach (var item in ShowCouponList)
            {
                var count = _couponRepository.QueryCouponCount(item.CouponConfigId);
                if (item.CouponNum != 0)
                {
                    item.CouponNum = item.CouponNum - int.Parse(count);
                    if (item.CouponNum <= 0)
                    {
                        item.CouponNum = -1;
                    }
                }
            }
            return ShowCouponList;
        }

        /// <summary>
        /// 获取商城优惠券
        /// </summary>
        /// <param name="vipId"></param>
        /// <returns></returns>
        public CouponDto GetShowCoupon(string couponConfigId)
        {
            return _couponRepository.QueryCouponByCouponConfigId(couponConfigId);
        }

        /// <summary>
        /// 获取提示优惠券
        /// </summary>
        /// <param name="vipId"></param>
        /// <returns></returns>
        public string GetCouponHint(int vipPoint)
        {
            return _couponRepository.GetCouponHint(vipPoint);
        }

        /// <summary>
        /// 兑换商城优惠券
        /// </summary>
        /// <param name="CouponConfigId"></param>
        /// <returns></returns>
        public bool ExChangeCoupon(string vipId, string couponConfigId)
        {
            var count = _couponRepository.QueryCouponCount(couponConfigId);
            var coupon = _couponRepository.QueryCouponByCouponConfigId(couponConfigId);
            if (coupon != null)
            {
                var couponNum = coupon.CouponNum - int.Parse(count);
                if (couponNum > 0)
                {
                    lock (locker)
                    {
                        string couponCode = Utils.GetCodeNo();
                        return _couponRepository.ExChangeCoupon(vipId, couponConfigId, couponCode);
                    }
                }
            }
            return false;
        }

    }
}

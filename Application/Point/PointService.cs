using Core.Common;
using Core.Entity;
using Core.Redis;
using Repository.VipManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Repository.PointManage;

namespace Application
{
    public class PointService
    {
        private PointRepository _pointRepository;

        public PointService()
        {
            _pointRepository = new PointRepository();
        }

        /// <summary>
        /// 获取会员积分
        /// </summary>
        /// <param name="vipId"></param>
        /// <returns></returns>
        public Point GetVipPoint(Vip vip)
        {
            if (vip != null)
            {
                return _pointRepository.QueryPointByVipId(vip.VipId);
            }
            return null;
        }

        /// <summary>
        /// 获取会员积分
        /// </summary>
        /// <param name="vipId"></param>
        /// <returns></returns>
        public List<PointRecord> GetVipPointRecord(Vip vip)
        {
            if (vip != null)
            {
                return _pointRepository.QueryPointRecordByVipId(vip.VipId);
            }
            return null;
        }

        /// <summary>
        /// 使用积分
        /// </summary>
        /// <param name="vipId"></param>
        /// <returns></returns>
        public bool UsePoint(Vip vip, int score)
        {
            if (vip != null)
            {
                return _pointRepository.UpdatePoint(vip.VipId, score, "兑换优惠券");
            }
            return false;
        }
    }
}

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

namespace Application
{
    public class VipService
    {
        private RedisManage _redisManage;
        private VipRepository _vipRepository;

        public VipService()
        {
            _redisManage = new RedisManage(0);
            _vipRepository = new VipRepository();
        }

        /// <summary>
        /// 获取会员信息
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public Vip GetVipInfo(string openId)
        {
            var IsExist = !string.IsNullOrEmpty(_redisManage.StringGet(openId));
            if (IsExist)
            {
                return JsonConvert.DeserializeObject<Vip>(_redisManage.StringGet(openId));
            }

            if (_vipRepository.QueryVipIsExist(openId))
            {
                var vip = _vipRepository.QueryVipByOpenId(openId);
                _redisManage.StringSet(openId, JsonConvert.SerializeObject(vip), TimeSpan.FromSeconds(600));
                return vip;
            }

            return null;

        }
    }
}

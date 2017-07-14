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
    public class LoginService
    {
        private RedisManage _redisManage;
        private VipRepository _vipRepository;

        //程序逻辑锁
        private static object locker = new object();

        public LoginService()
        {
            _redisManage = new RedisManage(0);
            _vipRepository = new VipRepository();
        }

        /// <summary>
        /// 微信登陆
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public bool WeChatLogin(string openId)
        {
            var IsExist = !string.IsNullOrEmpty(_redisManage.StringGet(openId));
            if (IsExist)
            {
                return true;
            }

            if (_vipRepository.QueryVipIsExist(openId))
            {
                var vip = _vipRepository.QueryVipByOpenId(openId);
                _redisManage.StringSet(openId, JsonConvert.SerializeObject(vip), TimeSpan.FromSeconds(600));
                return true;
            }

            return false;

        }

        /// <summary>
        /// 微信注册
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public bool WeChatRegist(Vip v, string openId)
        {
            var IsExist = !string.IsNullOrEmpty(_redisManage.StringGet(openId));
            if (IsExist)
            {
                return true;
            }

            lock (locker)
            {
                //生产会员Id
                v.VipId = Guid.NewGuid().ToString();
                //生成会员编号
                v.VipCode = _vipRepository.QueryNewVipCode();
                v.VipCode = "012" + v.VipCode.PadLeft(10 - v.VipCode.Length, '0');

                if (_vipRepository.InsertVip(v, openId))
                {
                    var vip = _vipRepository.QueryVipByOpenId(openId);
                    _redisManage.StringSet(openId, JsonConvert.SerializeObject(vip), TimeSpan.FromSeconds(600));
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}

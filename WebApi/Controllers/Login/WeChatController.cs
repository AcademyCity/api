using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Core.Common;
using Core.Entity;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.Entities.Request;
using System.Text;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin.Exceptions;
using Application;

namespace WebApi.Controllers
{
    public class WeChatController : ApiController
    {

        LoginService _loginService;
        VipService _vipService;

        public WeChatController()
        {
            _loginService = new LoginService();
            _vipService = new VipService();
        }

        /// <summary>
        /// 微信后台验证地址（使用Get），微信后台的“接口配置信息”的Url填写如：
        /// </summary>
        [HttpGet]
        public HttpResponseMessage Index(string signature, string timestamp, string nonce, string echostr)
        {
            if (CheckSignature.Check(signature, timestamp, nonce, Config.Token))
            {
                var result = new StringContent(echostr, UTF8Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = new HttpResponseMessage { Content = result };
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "failed:" + signature + "," + CheckSignature.GetSignature(timestamp, nonce, Config.Token) + "。" +
                    "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
        }

        [HttpGet]
        public IHttpActionResult GetBaseUrl(string openId = "", string backUrl = "")
        {

            var state = "Base-" + DateTime.Now.Millisecond;//随机数，用于识别请求可靠性
            var BackUrl = Config.BackUrl + backUrl;
            string WeChatUrl = OAuthApi.GetAuthorizeUrl(Config.AppId, BackUrl, state, OAuthScope.snsapi_base);

            return Json(new { success = false, message = WeChatUrl });
        }

        /// <summary>
        /// OAuthScope.snsapi_base方式回调
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="returnUrl">用户最初尝试进入的页面</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult BaseCallback(string code, string state, string backUrl = "")
        {
            var result = OAuthApi.GetAccessToken(Config.AppId, Config.Secret, code);
            if (result.errcode == Senparc.Weixin.ReturnCode.请求成功)
            {
                if (_loginService.WeChatLogin(result.openid))
                {
                    var vip = _vipService.GetVipInfo(result.openid);
                    return Json(new { success = true, message = new { success = true, message = new { token = Utils.MD5Encrypt(result.openid + vip.Sign), openId = result.openid, vipCode = vip.VipCode } } });
                }
                else
                {
                    state = "UserInfo-" + DateTime.Now.Millisecond;//随机数，用于识别请求可靠性
                    var BackUrl = Config.BackUrl + backUrl;
                    string WeChatUrl = OAuthApi.GetAuthorizeUrl(Config.AppId, BackUrl, state, OAuthScope.snsapi_userinfo);
                    return Json(new { success = true, message = new { success = false, message = WeChatUrl } });
                }
            }
            else
            {
                return Json(new { success = false, message = "错误：" + result.errmsg });
            }
        }

        /// <summary>
        /// OAuthScope.snsapi_userinfo方式回调
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="returnUrl">用户最初尝试进入的页面</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult UserInfoCallback(string code, string state)
        {
            if (string.IsNullOrEmpty(code))
            {
                return Json(new { success = false, message = "您拒绝了授权！" });
            }


            var result = OAuthApi.GetAccessToken(Config.AppId, Config.Secret, code);
            if (result.errcode != Senparc.Weixin.ReturnCode.请求成功)
            {
                return Json(new { success = false, message = "错误：" + result.errmsg });
            }

            try
            {
                OAuthUserInfo userInfo = OAuthApi.GetUserInfo(result.access_token, result.openid);
                Vip v = new Vip()
                {
                    VipSex = userInfo.sex == 1 ? true : false,
                    VipName = userInfo.nickname,
                    VipBirthday = null,
                    VipPhone = null,
                    VipCountry = userInfo.country,
                    VipProvince = userInfo.province,
                    VipCity = userInfo.city,
                    VipHeadImg = userInfo.headimgurl
                };

                if (_loginService.WeChatRegist(v, result.openid))
                {
                    var vip = _vipService.GetVipInfo(result.openid);
                    return Json(new { success = true, message = new { token = Utils.MD5Encrypt(result.openid + vip.Sign), openId = result.openid, vipCode = vip.VipCode } });
                }
                else
                {
                    return Json(new { success = false, message = "错误：注册失败！" });
                }
            }
            catch (ErrorJsonResultException)
            {
                return Json(new { success = false, message = "错误：" + result.errmsg });
            }
        }
    }
}

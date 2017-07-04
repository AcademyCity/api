using Application.Login;
using Core.Common;
using Core.Entity;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;

namespace WebApi.Controllers
{
    public class LoginController : BaseApiController
    {

        LoginService _loginService;

        public LoginController()
        {
            _loginService = new LoginService();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Core.Common
{
    public class Config
    {
        public static string AppId = ConfigurationManager.AppSettings["WeChat_APPID"];
        public static string Secret = ConfigurationManager.AppSettings["WeChat_SECRET"];
        public static string Token = ConfigurationManager.AppSettings["WeChat_TOKEN"];//与微信公众账号后台的Token设置保持一致，区分大小写。
        public static string Key = ConfigurationManager.AppSettings["WeChat_KEY"];//与微信公众账号后台的EncodingAESKey设置保持一致，区分大小写。
        public static string BackUrl = ConfigurationManager.AppSettings["WeChat_BACK_URL"];

        public static string RegisterPoint = ConfigurationManager.AppSettings["RegisterPoint"];
    }
}

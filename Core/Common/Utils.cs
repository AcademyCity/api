using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Core.Common
{
    public class Utils
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="opneId">用户OpenId</param>
        /// <returns></returns>
        public static string EncryptTicket(string opneId)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(0, opneId, DateTime.Now, 
                DateTime.Now.AddHours(1), true, opneId, FormsAuthentication.FormsCookiePath);

            return FormsAuthentication.Encrypt(ticket);
        }

    }
}

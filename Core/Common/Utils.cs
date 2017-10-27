using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Core.Common
{
    public class Utils
    {
        /// <summary>
        /// Md5加密
        /// </summary>
        /// <param name="source">加密字符串</param>
        /// <returns></returns>
        public static string MD5Encrypt(string source)
        {
            source += "012";
            byte[] sor = Encoding.UTF8.GetBytes(source);
            MD5 md5 = MD5.Create();
            byte[] result = md5.ComputeHash(sor);
            StringBuilder strbul = new StringBuilder(40);
            for (int i = 0; i < result.Length; i++)
            {
                strbul.Append(result[i].ToString("x2"));//加密结果"x2"结果为32位,"x3"结果为48位,"x4"结果为64位
            }
            return strbul.ToString();
        }

        public static string GetCodeNo()
        {
            string codeNo = "";
            for (int i = 0; i < 7; i++)//循环添加四个随机生成数
            {

                byte[] buffer = Guid.NewGuid().ToByteArray();
                int iSeed = BitConverter.ToInt32(buffer, 0);
                Random r = new Random(iSeed);
                codeNo += r.Next(0, 9).ToString();
            }
            DateTime now = DateTime.Now;
            string ms = now.ToString("fff");
            string s = now.ToString("ss");
            return ms + s + codeNo;
        }

        public static string GetVipCode(string code)
        {
            return "012" + code.PadLeft(10 - code.Length, '0');
        }

    }
}

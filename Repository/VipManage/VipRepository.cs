using Core.Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.VipManage
{
    public class VipRepository : BaseRepository
    {
        public bool InsertVip(Vip v, string openId)
        {
            string sql = @"INSERT INTO [IndexCRM].[dbo].[Vip]
                ([VipId],[VipCode],[VipName],[VipSex],[VipBirthday],[VipPhone],[VipCountry],[VipProvince],[VipCity],[VipHeadImg])  
                VALUES (@VipId,@VipCode,@VipName,@VipSex,@VipBirthday,@VipPhone,@VipCountry,@VipProvince,@VipCity,@VipHeadImg)";

            sql += @"INSERT INTO [IndexCRM].[dbo].[VipSource]
                ([VipId],[SourceName],[SourceId]) 
                VALUES (@VipId,@SourceName,@SourceId)";
            object obj = new
            {
                VipId = v.VipId,
                VipCode = v.VipCode,
                VipName = v.VipName,
                VipSex = v.VipSex,
                VipBirthday = v.VipBirthday,
                VipPhone = "",
                VipCountry = v.VipCountry,
                VipProvince = v.VipProvince,
                VipCity = v.VipCity,
                VipHeadImg = v.VipHeadImg,
                SourceName = "WeChat",
                SourceId = openId
            };
            return this.ExecuteTransactionSql(sql, obj);

        }

        public string GetNewVipCode()
        {
            string sql = @"SELECT ISNULL(MAX(CAST((SUBSTRING([VipCode],4,LEN([VipCode])-3)) AS INT)),0)+1 FROM [IndexCRM].[dbo].[Vip]";
            return this.ExecuteScalarSql(sql).ToString();
        }

        public bool GetVipIsExist(string openId)
        {
            string sql = @"SELECT ISNULL((SELECT TOP(1) 1 FROM [IndexCRM].[dbo].[VipSource] WHERE [SourceId]=@SourceId), 0)";

            object obj = new
            {
                SourceId = openId
            };

            return this.ExecuteScalarSql(sql, obj).ToString() != "0";
        }

        public string GetVipIdByOpenId(string openId)
        {
            string sql = @"SELECT b.VipId FROM [IndexCRM].[dbo].[VipSource] a
                JOIN [IndexCRM].[dbo].[Vip] b on a.VipId=b.VipId
                WHERE a.SourceId=@SourceId";

            object obj = new
            {
                SourceId = openId
            };

            return this.ExecuteScalarSql(sql, obj).ToString();
        }
    }
}

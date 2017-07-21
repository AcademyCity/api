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
        #region 添加
        /// <summary>
        /// 添加会员
        /// </summary>
        /// <param name="v"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
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
                VipId = v.VipId.ToUpper(),
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
        #endregion

        #region 查询
        /// <summary>
        /// 查询最新的会员编号
        /// </summary>
        /// <returns></returns>
        public string QueryNewVipCode()
        {
            string sql = @"SELECT ISNULL(MAX(CAST((SUBSTRING([VipCode],4,LEN([VipCode])-3)) AS INT)),0)+1 FROM [IndexCRM].[dbo].[Vip]";
            return this.ExecuteScalarSql(sql).ToString();
        }

        /// <summary>
        /// 查询会员信息
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public Vip QueryVipByOpenId(string openId)
        {
            string sql = @"SELECT b.*FROM[IndexCRM].[dbo].[VipSource] a
                JOIN[IndexCRM].[dbo].[Vip] b ON a.VipId=b.VipId
                WHERE a.SourceId=@SourceId";

            object obj = new
            {
                SourceId = openId
            };

            return this.ExecuteSql<Vip>(sql, obj);
        }

        /// <summary>
        /// 查询会员是否存在
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public bool QueryVipIsExist(string openId)
        {
            string sql = @"SELECT ISNULL((SELECT TOP(1) 1 FROM [IndexCRM].[dbo].[VipSource] WHERE [SourceId]=@SourceId), 0)";

            object obj = new
            {
                SourceId = openId
            };

            return this.ExecuteScalarSql(sql, obj).ToString() != "0";
        }
        #endregion
    }
}

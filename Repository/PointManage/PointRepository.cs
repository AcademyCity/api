using Core.Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.PointManage
{
    public class PointRepository : BaseRepository
    {
        #region 更新
        /// <summary>
        /// 更新积分(使用积分)
        /// </summary>
        /// <param name="vipId"></param>
        /// <param name="score"></param>
        /// <param name="pointExplain"></param>
        /// <returns></returns>
        public bool UpdatePoint(string vipId, int score, string pointExplain)
        {
            string sql = @"INSERT INTO [IndexCRM].[dbo].[PointRecord]
                ([VipId],[ScoreRecord],[PointExplain],[PosNo])  
                VALUES (@VipId,@ScoreRecord,@PointExplain,@PosNo)";

            sql += @"UPDATE [IndexCRM].[dbo].[Point]
                SET [Score] = [Score] - @Score
                WHERE[VipId] = @VipId";

            object obj = new
            {
                VipId = vipId,
                ScoreRecord = -score,
                Score = score,
                PointExplain = pointExplain
            };
            return this.ExecuteTransactionSql(sql, obj);

        }
        #endregion

        #region 查询

        /// <summary>
        /// 查询会员积分
        /// </summary>
        /// <param name="VipId"></param>
        /// <returns></returns>
        public Point QueryPointByVipId(string VipId)
        {
            string sql = @"SELECT [VipPoint] 
                FROM [IndexCRM].[dbo].[Point] 
                WHERE VipId=@VipId";

            object obj = new
            {
                VipId = VipId
            };

            return this.ExecuteSql<Point>(sql, obj);
        }

        /// <summary>
        /// 查询会员积分记录
        /// </summary>
        /// <param name="VipId"></param>
        /// <returns></returns>
        public List<PointRecord> QueryPointRecordByVipId(string VipId)
        {
            string sql = @"SELECT [PointChange],[PointExplain],[AddTime] 
                FROM [IndexCRM].[dbo].[PointRecord] 
                WHERE VipId=@VipId 
                ORDER BY AddTime DESC";

            object obj = new
            {
                VipId = VipId
            };

            return this.ExecuteSqlToList<PointRecord>(sql, obj);
        }

        #endregion
    }
}

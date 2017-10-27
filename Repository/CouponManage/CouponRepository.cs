using Core.Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Dto;

namespace Repository.CouponManage
{
    public class CouponRepository : BaseRepository
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
        /// 查询会员优惠券
        /// </summary>
        /// <param name="VipId"></param>
        /// <returns></returns>
        public List<CouponDto> QueryCouponByVipId(string VipId)
        {
            string sql = @"SELECT a.[CouponId],b.[CouponName],b.[CouponImg],a.[StartTime],a.[EndTime],a.[IsUse]
                FROM [IndexCRM].[dbo].[Coupon] a
                JOIN [IndexCRM].[dbo].[CouponConfig] b ON a.[CouponConfigId]=b.[CouponConfigId]
                WHERE a.[VipId]=@VipId
                ORDER BY a.[AddTime] DESC";

            object obj = new
            {
                VipId = VipId
            };

            return this.ExecuteSqlToList<CouponDto>(sql, obj);
        }

        /// <summary>
        /// 查询会员优惠券
        /// </summary>
        /// <param name="VipId"></param>
        /// <returns></returns>
        public CouponDto QueryCouponByCouponId(string CouponId)
        {
            string sql = @"SELECT a.[CouponId],b.[CouponName],b.[CouponImg],b.[CouponExplain],a.[CouponCode],a.[StartTime],a.[EndTime],a.[IsUse]
                FROM [IndexCRM].[dbo].[Coupon] a
                JOIN [IndexCRM].[dbo].[CouponConfig] b ON a.[CouponConfigId]=b.[CouponConfigId]
                WHERE a.[CouponId]=@CouponId";

            object obj = new
            {
                CouponId = CouponId
            };

            return this.ExecuteSql<CouponDto>(sql, obj);
        }

        /// <summary>
        /// 查询会员优惠券
        /// </summary>
        /// <param name="VipId"></param>
        /// <returns></returns>
        public List<CouponDto> QueryCouponIsShop()
        {
            string sql = @"SELECT [CouponConfigId],[CouponName],[CouponImg],[CouponPoint],[ValidityMode],[StartTime],[EndTime],[EffectDate],[ValidDate]
                FROM [IndexCRM].[dbo].[CouponConfig] 
                WHERE [IsShop]=1 AND [IsDelete]=0
                ORDER BY [Sort]";

            return this.ExecuteSqlToList<CouponDto>(sql);
        }

        /// <summary>
        /// 查询会员优惠券
        /// </summary>
        /// <param name="VipId"></param>
        /// <returns></returns>
        public CouponDto QueryCouponByCouponConfigId(string CouponConfigId)
        {
            string sql = @"SELECT [CouponConfigId],[CouponName],[CouponNum],[CouponExplain],[CouponImg],[CouponPoint],[ValidityMode],[StartTime],[EndTime],[EffectDate],[ValidDate]
                FROM [IndexCRM].[dbo].[CouponConfig] 
                WHERE [IsShop]=1 AND [IsDelete]=0 AND [CouponConfigId]=@CouponConfigId
                ORDER BY [Sort]";

            object obj = new
            {
                CouponConfigId = CouponConfigId
            };

            return this.ExecuteSql<CouponDto>(sql, obj);
        }

        /// <summary>
        /// 查询会员优惠券
        /// </summary>
        /// <param name="VipId"></param>
        /// <returns></returns>
        public string QueryCouponCount(string CouponConfigId)
        {
            string sql = @"SELECT COUNT(1) 
                FROM [IndexCRM].[dbo].[Coupon] 
                WHERE [CouponConfigId]=@CouponConfigId";

            object obj = new
            {
                CouponConfigId = CouponConfigId
            };

            return this.ExecuteScalarSql(sql, obj).ToString();
        }


        /// <summary>
        /// 兑换优惠券
        /// </summary>
        /// <param name="VipId"></param>
        /// <returns></returns>
        public bool ExChangeCoupon(string vipId,string couponConfigId,string couponCode)
        {
            string sql = @"
                DECLARE @Score INT
                DECLARE @ValidityMode INT
                DECLARE @StartTime DATETIME
                DECLARE @EndTime DATETIME
                DECLARE @EffectDate INT
                DECLARE @ValidDate INT

                SELECT @Score =[CouponPoint],@ValidityMode =[ValidityMode],
                @StartTime =[StartTime],@EndTime =[EndTime],
                @EffectDate =[EffectDate],@ValidDate =[ValidDate]
                FROM[IndexCRM].[dbo].[CouponConfig] WHERE[CouponConfigId] = @CouponConfigId ";

            sql += @"IF(@ValidityMode = 2)
                BEGIN
                    SET @StartTime = GETDATE() + @EffectDate
                    SET @EndTime = GETDATE() + @ValidDate
                END ";

            sql += @"
                INSERT INTO[IndexCRM].[dbo].Coupon
                (CouponConfigId, VipId, CouponCode, StartTime, EndTime, AddExplain, IsUse)
                VALUES
                (@CouponConfigId, @VipId, @CouponCode, @StartTime, @EndTime, '积分兑换', 0) ";

            sql += @"UPDATE[IndexCRM].[dbo].[Point]
                SET[Score] = [Score] - @Score
                WHERE[VipId] = @VipId ";

            sql += @"INSERT INTO [IndexCRM].[dbo].[PointRecord]
                ([VipId],[ScoreRecord],[PointExplain],[PosNo])  
                VALUES
                (@VipId,-@Score,'兑换优惠券','') ";

            object obj = new
            {
                VipId = vipId,
                CouponConfigId = couponConfigId,
                CouponCode = couponCode
            };
            return this.ExecuteTransactionSql(sql, obj);
        }
        #endregion
    }
}

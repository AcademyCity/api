using Core.Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Dto;
using Core.Entity.Input;

namespace Repository.SuggestManage
{
	public class SuggestRepository : BaseRepository
	{
		#region 添加

		public bool AddSuggest(SuggestInput suggest, string vipId)
		{
			string sql = @"
				INSERT INTO[dbo].[Suggest] 
				([SuggestId],[VipId],[StoreNo],[PosNo],[EatDate],[EatTime],[AddTime])
				VALUES 
				(@SuggestId, @VipId, @StoreNo, @PosNo, @EatDate, @EatTime, @AddTime)";

			sql += @"
				INSERT INTO[dbo].[SuggestContent]
				([SuggestId],[Reply1],[Reply2],[Reply3],[Reply4],[Reply5],[Reply6],[Reply7],[Reply8],[AddTime])
				VALUES
				(@SuggestId, @Reply1, @Reply2, @Reply3, @Reply4, @Reply5, @Reply6, @Reply7, @Reply8, @AddTime)";

			sql += @"
				INSERT INTO[dbo].[SuggestDetail]
				([SuggestId],[Reply],[ReplyDetail],[AddTime])
				VALUES 
				(@SuggestId,@Reply,@ReplyDetail,@AddTime)";

			object obj = new
			{
				SuggestId = Guid.NewGuid(),
				VipId = vipId,
				StoreNo = suggest.Suggest.StoreNo,
				PosNo = suggest.Suggest.PosNo,
				EatDate = suggest.Suggest.EatDate,
				EatTime = suggest.Suggest.EatTime,
				AddTime = DateTime.Now,
				Reply1 = suggest.SuggestContent.Reply1,
				Reply2 = suggest.SuggestContent.Reply2,
				Reply3 = suggest.SuggestContent.Reply3,
				Reply4 = suggest.SuggestContent.Reply4,
				Reply5 = suggest.SuggestContent.Reply5,
				Reply6 = suggest.SuggestContent.Reply6,
				Reply7 = suggest.SuggestContent.Reply7,
				Reply8 = suggest.SuggestContent.Reply8,
				Reply = suggest.SuggestDetail.Reply,
				ReplyDetail = suggest.SuggestDetail.ReplyDetail
			};
			return this.ExecuteTransactionSql(sql, obj);
		}

		#endregion
	}
}

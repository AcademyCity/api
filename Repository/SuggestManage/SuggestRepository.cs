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
				StoreNo = suggest.StoreNo,
				PosNo = suggest.PosNo,
				EatDate = suggest.EatDate,
				EatTime = suggest.EatTime,
				AddTime = DateTime.Now,
				Reply1 = suggest.Reply1,
				Reply2 = suggest.Reply2,
				Reply3 = suggest.Reply3,
				Reply4 = suggest.Reply4,
				Reply5 = suggest.Reply5,
				Reply6 = suggest.Reply6,
				Reply7 = suggest.Reply7,
				Reply8 = suggest.Reply8,
				Reply = suggest.Reply,
				ReplyDetail = suggest.ReplyDetail
			};
			return this.ExecuteTransactionSql(sql, obj);
		}

		#endregion
	}
}

using Application;
using Core.Entity.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers.Suggest
{
	public class SuggestController : BaseApiController
	{
		VipService _vipService;
		SuggestService _suggestService;

		public SuggestController()
		{
			_vipService = new VipService();
			_suggestService = new SuggestService(); ;
		}

		[HttpPost]
		[AllowAnonymous]
		public IHttpActionResult AddSuggest(SuggestInput suggest, string openId)
		{
			var vip = _vipService.GetVipInfo(openId);

			if (vip != null)
			{
				if (_suggestService.AddSuggest(suggest, vip.VipId))
				{
					return Json(new { success = true, message = "添加成功" });
				}
				else
				{
					return Json(new { success = false, message = "添加失败" });
				}

			}
			return Json(new { success = false, message = "发生错误" });
		}
	}
}

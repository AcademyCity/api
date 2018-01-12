using Core.Common;
using Core.Entity;
using Core.Redis;
using Repository.VipManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Repository.SuggestManage;
using Core.Entity.Input;

namespace Application
{
	public class SuggestService
	{
		private SuggestRepository _suggestRepository;

		public SuggestService()
		{
			_suggestRepository = new SuggestRepository();
		}

		public bool AddSuggest(SuggestInput suggest, string vipId)
		{
			return _suggestRepository.AddSuggest(suggest, vipId);
		}
	}
}

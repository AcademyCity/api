namespace Core.Entity.Input
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;

	public class SuggestInput
	{
		public string StoreNo { get; set; }

		public string PosNo { get; set; }

		public DateTime? EatDate { get; set; }

		public string EatTime { get; set; }

		public string Reply { get; set; }

		public string ReplyDetail { get; set; }

		public string Reply1 { get; set; }

		public string Reply2 { get; set; }

		public string Reply3 { get; set; }

		public string Reply4 { get; set; }

		public string Reply5 { get; set; }

		public string Reply6 { get; set; }

		public string Reply7 { get; set; }

		public string Reply8 { get; set; }

	}
}

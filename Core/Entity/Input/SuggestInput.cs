namespace Core.Entity.Input
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class SuggestInput
	{
        public Suggest Suggest { get; set; }

		public SuggestContent SuggestContent { get; set; }

		public SuggestDetail SuggestDetail { get; set; }

	}
}

using System;
namespace YannikG.TSBE.Webcrawler.Core.Models
{
	public class BasicArticleModel
	{
		public string? Name { get; set; } = "";
		public string? ArticleNumber { get; set; } = "";
        public string? ArticleManufacturer { get; set; } = "";
        public string? Url { get; set; } = "";
		public string? ImageUrl { get; set; } = "";
	}
}


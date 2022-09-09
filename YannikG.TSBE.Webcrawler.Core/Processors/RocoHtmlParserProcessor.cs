using System;
using System.Xml;
using HtmlAgilityPack;
using YannikG.TSBE.Webcrawler.Core.Models;

namespace YannikG.TSBE.Webcrawler.Core.Processors
{
	public class RocoHtmlParserProcessor
	{
		private const string HTML_ELEMENT_PRODUCT = "strong";
		private const string CSS_CLASS_PRODUCT = "product";
		private const string CSS_CLASS_ARTICLE_NUMBER = "product-article-number";
		private const string CSS_CLASS_ARTICLE_LINK = "product-item-link";

		public RocoHtmlParserProcessor()
		{

		}

		public IReadOnlyCollection<BasicArticleModel> Process(string html)
		{
			var result = new List<BasicArticleModel>();


            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

			htmlDoc.DocumentNode.Descendants(HTML_ELEMENT_PRODUCT)
					.Where(node => node.GetAttributeValue("class", "")
						.Contains(CSS_CLASS_PRODUCT)
						)
					.ToList()
					.ForEach(node => result.Add(processProduct(node)));

            return result;
		}

		private BasicArticleModel processProduct(HtmlNode htmlNode)
		{
			var result = new BasicArticleModel();

			result.Name = htmlNode.SelectSingleNode($".//a[@class='{CSS_CLASS_ARTICLE_LINK}']").InnerText;
			result.Url = htmlNode.SelectSingleNode($".//a[@class='{CSS_CLASS_ARTICLE_LINK}']").GetAttributeValue("href", string.Empty);

            return result;
		}
    }
}


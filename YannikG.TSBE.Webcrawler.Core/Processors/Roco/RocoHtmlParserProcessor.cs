﻿using System;
using System.Xml;
using HtmlAgilityPack;
using YannikG.TSBE.Webcrawler.Core.Models;
using YannikG.TSBE.Webcrawler.Core.Utilities;

namespace YannikG.TSBE.Webcrawler.Core.Processors.Roco;

	public class RocoHtmlParserProcessor
	{
		private const string HTML_ELEMENT_PRODUCT = "li";
		private const string CSS_CLASS_PRODUCT = "product-item";
		private const string CSS_CLASS_ARTICLE_NUMBER = "product-article-number";
		private const string CSS_CLASS_ARTICLE_LINK = "product-item-link";
		private const string CSS_CLASS_ARTICLE_IMAGE = "product-image-photo";

    private const string DEFAULT_MANUFACTURER = "Roco";

		public RocoHtmlParserProcessor()
		{

		}

		public ICollection<BasicArticleModel> Process(string html)
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

			result.Name = htmlNode.SelectSingleNode($".//a[@class='{CSS_CLASS_ARTICLE_LINK}']")?.InnerText;
			result.Url = htmlNode.SelectSingleNode($".//a[@class='{CSS_CLASS_ARTICLE_LINK}']")?.GetAttributeValue("href", string.Empty);
        result.ArticleNumber = htmlNode.SelectSingleNode($".//p[@class='{CSS_CLASS_ARTICLE_NUMBER}']")?.InnerText;

        result.ImageUrl = htmlNode.SelectSingleNode($".//img[@class='{CSS_CLASS_ARTICLE_IMAGE}']")?.GetAttributeValue("src", string.Empty);

			result.ArticleManufacturer = DEFAULT_MANUFACTURER;

			// cleanup
			result.Name = result.Name != null ? result.Name!.RemoveNewLine().TrimStart().TrimEnd() : string.Empty;
        result.ArticleNumber = result.ArticleNumber != null ? result.ArticleNumber!.RemoveNewLine().TrimStart().TrimEnd() : string.Empty;

        return result;
		}
}

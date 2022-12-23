using HtmlAgilityPack;
using YannikG.TSBE.Webcrawler.Core.Models;
using YannikG.TSBE.Webcrawler.Core.Utilities;

namespace YannikG.TSBE.Webcrawler.Core.Collectors.Handlers.Roco;

public class RocoHtmlArticleParserHandler
{
    private const string HTML_ELEMENT_PRODUCT = "li";
    private const string CSS_CLASS_PRODUCT = "product-item";
    private const string CSS_CLASS_ARTICLE_NUMBER = "product-article-number";
    private const string CSS_CLASS_ARTICLE_LINK = "product-item-link";
    private const string CSS_CLASS_ARTICLE_IMAGE = "product-image-photo";

    public List<BasicArticleModel> Handle(string html)
    {
        HtmlDocument htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);

        var result = htmlDoc.DocumentNode.Descendants(HTML_ELEMENT_PRODUCT)
                .Where(node => node.GetAttributeValue("class", "")
                    .Contains(CSS_CLASS_PRODUCT)
                      )
                .Select(node => (processProduct(node)))
                .ToList();

        return result;
    }

    private BasicArticleModel processProduct(HtmlNode htmlNode)
    {
        var result = new BasicArticleModel();

        result.Name = htmlNode.SelectSingleNode($".//a[@class='{CSS_CLASS_ARTICLE_LINK}']")?.InnerText;
        result.Url = htmlNode.SelectSingleNode($".//a[@class='{CSS_CLASS_ARTICLE_LINK}']")?.GetAttributeValue("href", string.Empty);
        result.ArticleNumber = htmlNode.SelectSingleNode($".//p[@class='{CSS_CLASS_ARTICLE_NUMBER}']")?.InnerText;

        result.ImageUrl = htmlNode.SelectSingleNode($".//img[@class='{CSS_CLASS_ARTICLE_IMAGE}']")?.GetAttributeValue("src", string.Empty);

        // cleanup
        result.Name = result.Name != null ? result.Name!.RemoveNewLine().TrimStart().TrimEnd() : string.Empty;
        result.ArticleNumber = result.ArticleNumber != null ? result.ArticleNumber!.RemoveNewLine().TrimStart().TrimEnd() : string.Empty;

        return result;
    }
}
using System;
using HtmlAgilityPack;

namespace YannikG.TSBE.Webcrawler.Core.Collectors.Handlers.Roco;

public class RocoHtmlNextUrlHandler
{
    private const string CSS_CLASS_NEXT_PAGE = "action  next";
    private const string HTML_ELEMENT_NEXT_PAGE = "a";

    public RocoHtmlNextUrlHandler()
    {
    }

    public string Handle(string html)
    {
        HtmlDocument htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);

        var nextPageNode = htmlDoc.DocumentNode.Descendants(HTML_ELEMENT_NEXT_PAGE)
                .Where(node => node.GetAttributeValue("class", "")
                    .Contains(CSS_CLASS_NEXT_PAGE)
                    )
                .FirstOrDefault();

        if (nextPageNode != null)
            return nextPageNode.GetAttributeValue("href", string.Empty);
        return string.Empty;
    }
}


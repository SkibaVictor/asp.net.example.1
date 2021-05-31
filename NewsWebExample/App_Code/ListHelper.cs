using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NewsWebExample.App_Code
{
    public static class ListHelper
    {
        public static HtmlString GenerateSimpleList(
            this IHtmlHelper helper,
            List<ListItem> items)
        {
            var ul = new TagBuilder("ul");
            ul.AddCssClass("list-group");
            foreach (var item in items)
            {
                var li = new TagBuilder("li");
                li.AddCssClass("list-group-item");
                var title = new TagBuilder("div");
                var bold = new TagBuilder("b");
                bold.InnerHtml.Append(item.Title);
                title.InnerHtml.AppendHtml(bold);
                var content = new TagBuilder("div");
                content.InnerHtml.Append(item.Content);
                li.InnerHtml.AppendHtml(title);
                li.InnerHtml.AppendHtml(content);
                ul.InnerHtml.AppendHtml(li);
            }
            var writer = new System.IO.StringWriter();
            ul.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }
    }

    public class ListItem
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}

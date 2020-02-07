using HtmlTags;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

public class PaginationTagHelper : TagHelper
{
    public int CurrentPage { get; set; }

    public int TotalPageCount { get; set; }

    public int MaxPageCount { get; set; } = 5;

    private readonly IHttpContextAccessor contextAccessor;

    private HttpRequest httpRequest
    {
        get
        {
            return contextAccessor.HttpContext.Request;
        }
    }


    public PaginationTagHelper(IHttpContextAccessor contextAccessor)
    {
        this.contextAccessor = contextAccessor;
    }

    private HtmlTag getPageLink(int pageNumber)
    {
        var liHtml = new HtmlTag("li").AddClass("page-item");

        if (pageNumber == CurrentPage)
        {
            liHtml.AddClass("active");
        }

        var aHtml = new HtmlTag("a")
            .AddClass("page-link")
            .Attr("href", "#")
            .AppendText(pageNumber.ToString());

        liHtml.Append(aHtml);

        return liHtml;
    }

    private HtmlTag getPrevLink()
    {
        var liHtml = new HtmlTag("li").AddClass("page-item");

        var aHtml = new HtmlTag("a")
            .AddClass("page-link")
            .Attr("href", "#");

        aHtml.Append(new HtmlTag("span").AddClass("mdi mdi-chevron-left"));
        aHtml.Append(new HtmlTag("span").AddClass("sr-only").AppendText("Previous"));

        liHtml.Append(aHtml);

        return liHtml;
    }

    private HtmlTag getNextLink()
    {
        var liHtml = new HtmlTag("li").AddClass("page-item");

        var aHtml = new HtmlTag("a")
            .AddClass("page-link")
            .Attr("href", "#");

        aHtml.Append(new HtmlTag("span").AddClass("mdi mdi-chevron-right"));
        aHtml.Append(new HtmlTag("span").AddClass("sr-only").AppendText("Previous"));

        liHtml.Append(aHtml);

        return liHtml;
    }

    private HtmlTag getDots()
    {
        return new HtmlTag("li").AddClass("page-item dots").AppendText("...");
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "";

        if (TotalPageCount < 2)
        {
            return;
        }

        var navHtml = new HtmlTag("nav").Attr("aria-label", "Page navigation example");

        var ulHtml = new HtmlTag("ul").AddClass("pagination pagination-flat pagination-flat-rounded");

        if (CurrentPage > 1)
        {
            ulHtml.Append(getPrevLink());
        }

        var startPage = Convert.ToInt32((CurrentPage - Math.Floor((double)MaxPageCount / 2)));
        if (startPage <= 1)
        {
            startPage = 1;
        }

        var endPage = (startPage + MaxPageCount - 1);
        if (endPage > TotalPageCount)
        {
            endPage = TotalPageCount;
        }

        if (startPage > 1)
        {
            ulHtml.Append(getDots());
        }

        for (int i = startPage; i <= endPage; i++)
        {
            ulHtml.Append(getPageLink(i));
        }

        if (endPage < TotalPageCount)
        {
            ulHtml.Append(getDots());
        }

        if (CurrentPage < TotalPageCount)
        {
            ulHtml.Append(getNextLink());
        }

        navHtml.Append(ulHtml);

        output.Content.SetHtmlContent(navHtml.ToHtmlString());

        base.Process(context, output);
    }
}

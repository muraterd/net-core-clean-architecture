using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;

[HtmlTargetElement("menu-item")]
public class MenuItemTagHelper : TagHelper
{
    public string Url { get; set; }

    public string IconClassName { get; set; }

    public string Title { get; set; }

    public bool Exact { get; set; }

    private readonly IHttpContextAccessor contextAccessor;

    private HttpRequest httpRequest
    {
        get
        {
            return contextAccessor.HttpContext.Request;
        }
    }

    private string tagClass
    {
        get
        {
            var cssClass = new List<string> { "has-sub" };

            var isActive = (Exact && httpRequest.Path.ToUriComponent() == Url) || (!Exact && httpRequest.Path.StartsWithSegments(Url));

            if (isActive)
            {
                cssClass.Add("active");
            };

            return string.Join(" ", cssClass);
        }
    }


    public MenuItemTagHelper(IHttpContextAccessor contextAccessor)
    {
        this.contextAccessor = contextAccessor;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "";

        var html = $@"<li class=""{tagClass}"">
                      <a class=""sidenav-item-link"" href=""{Url}"">
                        <i class=""{IconClassName}""></i>
                        <span class=""nav-text"">{Title}</span>
                    </a>
                </li>";

        output.Content.SetHtmlContent(html);

        base.Process(context, output);
    }
}

using HtmlTags;
using Microsoft.AspNetCore.Razor.TagHelpers;
using UI.Areas.Admin.Models.Base;

public class ScreenStateTagHelper : TagHelper
{
    public ScreenState ScreenState { get; set; }
    public ScreenState EqualTo { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (ScreenState != EqualTo)
        {
            output.Content.SetHtmlContent("");
        }

        base.Process(context, output);
    }
}

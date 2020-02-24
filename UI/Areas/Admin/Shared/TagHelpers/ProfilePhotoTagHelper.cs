using HtmlTags;
using Microsoft.AspNetCore.Razor.TagHelpers;

public class ProfilePhotoTagHelper : TagHelper
{
    public string FileName { get; set; }
    public string Path { get; set; }
    public string CssClass { get; set; }

    private string ImageSource
    {
        get
        {
            return !string.IsNullOrEmpty(FileName)
                ? $"/{Path}/{FileName}"
                : "/admin/img/user-avatar-default.png";
        }
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "";

        var image = new HtmlTag("img").Attr("src", ImageSource).Attr("alt", "user_avatar");

        if (CssClass != null)
        {
            image.AddClass(CssClass);
        }

        output.Content.SetHtmlContent(image);

        base.Process(context, output);
    }
}

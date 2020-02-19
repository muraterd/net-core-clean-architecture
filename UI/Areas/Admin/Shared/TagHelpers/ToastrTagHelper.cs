using HtmlTags;
using Microsoft.AspNetCore.Razor.TagHelpers;
using WebCMS.Areas.Admin.Models.Base;

public class ToastrTagHelper : TagHelper
{
    public BaseViewModel Model { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "";
        string html = "";

        if (Model.Toastr != null)
        {
            html += $@"<script>
                          showToaster(""{Model.Toastr.Type.ToString().ToLower()}"", ""{Model.Toastr.Title}"", ""{Model.Toastr.Message}"");
                       </script> ";
        }

        output.Content.SetHtmlContent(html);

        base.Process(context, output);
    }
}

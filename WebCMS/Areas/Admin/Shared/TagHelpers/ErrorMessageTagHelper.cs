using HtmlTags;
using Microsoft.AspNetCore.Razor.TagHelpers;
using WebCMS.Areas.Admin.Models.Base;

public class ErrorMessageTagHelper : TagHelper
{
    public BaseViewModel Model { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "";
        string html = "";

        if (Model.ErrorMessage != null)
        {
            html += $@"<div class=""alert alert-danger alert-highlighted"" role=""alert"">
                        {Model.ErrorMessage}
                        <button type=""button"" class=""close"" data-dismiss=""alert"" aria-label=""Close"">
                            <span aria-hidden=""true"">×</span>
                        </button>
                    </div>";
        }

        if (Model.SuccessMessage != null)
        {
            html += $@"<div class=""alert alert-success alert-highlighted"" role=""alert"">
                        {Model.SuccessMessage}
                        <button type=""button"" class=""close"" data-dismiss=""alert"" aria-label=""Close"">
                            <span aria-hidden=""true"">×</span>
                        </button>
                    </div>";
        }

        output.Content.SetHtmlContent(html);

        base.Process(context, output);
    }
}

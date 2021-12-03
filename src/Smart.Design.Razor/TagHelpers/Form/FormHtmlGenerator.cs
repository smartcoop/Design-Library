using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Smart.Design.Razor.TagHelpers.Form;

public class FormHtmlGenerator : IFormHtmlGenerator
{
    /// <inheritdoc />
    public TagBuilder GenerateForm(FormLayout layout, IHtmlContent content)
    {
        var form = new TagBuilder("form");

        var formGroupLayout = new TagBuilder("div");
        formGroupLayout.AddCssClass("o-form-group-layout");
        formGroupLayout.AddCssClass(LayoutCssClass(ref layout));

        formGroupLayout.InnerHtml.SetHtmlContent(content);
        form.InnerHtml.SetHtmlContent(formGroupLayout);

        return form;
    }

    private string LayoutCssClass(ref FormLayout layout)
    {
        return layout switch
        {
            FormLayout.Horizontal => "o-form-group-layout--horizontal",
            FormLayout.Standard   => "o-form-group-layout--standard",
            FormLayout.Inline     => "o-form-group-layout--inline",
            _                     => "o-form-group-layout--standard"
        };
    }
}

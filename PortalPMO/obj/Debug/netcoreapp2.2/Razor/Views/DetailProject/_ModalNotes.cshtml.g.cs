#pragma checksum "C:\work\Portal PMO\PortalPMO\Views\DetailProject\_ModalNotes.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "01aec24fd3e6db5c885638aa158b54acdcae2bdc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_DetailProject__ModalNotes), @"mvc.1.0.view", @"/Views/DetailProject/_ModalNotes.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/DetailProject/_ModalNotes.cshtml", typeof(AspNetCore.Views_DetailProject__ModalNotes))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\work\Portal PMO\PortalPMO\Views\_ViewImports.cshtml"
using PortalPMO;

#line default
#line hidden
#line 2 "C:\work\Portal PMO\PortalPMO\Views\_ViewImports.cshtml"
using PortalPMO.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"01aec24fd3e6db5c885638aa158b54acdcae2bdc", @"/Views/DetailProject/_ModalNotes.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6ba151424cb7e4ff7d935c55cfcaa12c04b783fe", @"/Views/_ViewImports.cshtml")]
    public class Views_DetailProject__ModalNotes : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PortalPMO.ViewModels.DataProjectNotes_ViewModels>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("FormDataNotes"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("padding-top:20px;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(57, 287, true);
            WriteLiteral(@"<style>
    .modal-dialog {
        min-width: 90%;
    }
</style>
<div class=""modal-header"">
    <h5 class=""modal-title"">Form Data</h5>
    <button type=""button"" class=""close"" data-dismiss=""modal"" aria-hidden=""true"">×</button>
</div>
<div class=""modal-body"" id=""id-body"">
    ");
            EndContext();
            BeginContext(344, 994, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "01aec24fd3e6db5c885638aa158b54acdcae2bdc4396", async() => {
                BeginContext(395, 10, true);
                WriteLiteral("\r\n        ");
                EndContext();
                BeginContext(406, 23, false);
#line 13 "C:\work\Portal PMO\PortalPMO\Views\DetailProject\_ModalNotes.cshtml"
   Write(Html.AntiForgeryToken());

#line default
#line hidden
                EndContext();
                BeginContext(429, 10, true);
                WriteLiteral("\r\n        ");
                EndContext();
                BeginContext(440, 25, false);
#line 14 "C:\work\Portal PMO\PortalPMO\Views\DetailProject\_ModalNotes.cshtml"
   Write(Html.HiddenFor(m => m.Id));

#line default
#line hidden
                EndContext();
                BeginContext(465, 10, true);
                WriteLiteral("\r\n        ");
                EndContext();
                BeginContext(476, 32, false);
#line 15 "C:\work\Portal PMO\PortalPMO\Views\DetailProject\_ModalNotes.cshtml"
   Write(Html.HiddenFor(m => m.ProjectId));

#line default
#line hidden
                EndContext();
                BeginContext(508, 250, true);
                WriteLiteral("\r\n        <div class=\"form-group row m-b-15\">\r\n            <label class=\"col-md-3 col-form-label\">Judul <span class=\"labelMandatory\">*</span></label>\r\n            <div class=\"col-md-9\">\r\n                <div class=\"input-group\">\r\n                    ");
                EndContext();
                BeginContext(759, 98, false);
#line 20 "C:\work\Portal PMO\PortalPMO\Views\DetailProject\_ModalNotes.cshtml"
               Write(Html.TextBoxFor(m => m.Judul, new { @class = "form-control", @value = "", @autocomplete = "off" }));

#line default
#line hidden
                EndContext();
                BeginContext(857, 295, true);
                WriteLiteral(@"
                </div>
            </div>
        </div>
        <div class=""form-group row m-b-15"">
            <label class=""col-md-3 col-form-label"">Keterangan <span class=""labelMandatory"">*</span></label>
            <div class=""col-md-9"">
                <div>
                    ");
                EndContext();
                BeginContext(1153, 112, false);
#line 28 "C:\work\Portal PMO\PortalPMO\Views\DetailProject\_ModalNotes.cshtml"
               Write(Html.TextAreaFor(m => m.Notes, new { @class = "form-control", @value = "", @autocomplete = "off", @rows = "5" }));

#line default
#line hidden
                EndContext();
                BeginContext(1265, 66, true);
                WriteLiteral("\r\n                </div>\r\n            </div>\r\n        </div>\r\n    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1338, 222, true);
            WriteLiteral("\r\n</div>\r\n<div class=\"modal-footer\">\r\n    <a href=\"javascript:;\" class=\"btn btn-sm btn-white\" data-dismiss=\"modal\">Tutup</a>\r\n    <a href=\"javascript:;\" class=\"btn btn-sm btn-primary\" id=\"BtnSubmitNotes\">Simpan</a>\r\n</div>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PortalPMO.ViewModels.DataProjectNotes_ViewModels> Html { get; private set; }
    }
}
#pragma warning restore 1591

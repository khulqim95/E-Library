#pragma checksum "C:\work\Portal PMO\PortalPMO\Views\TaskComplete\_ModalDetailProgress.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "aacc961fbc532c36b183549b12e72790e75c92f6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_TaskComplete__ModalDetailProgress), @"mvc.1.0.view", @"/Views/TaskComplete/_ModalDetailProgress.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/TaskComplete/_ModalDetailProgress.cshtml", typeof(AspNetCore.Views_TaskComplete__ModalDetailProgress))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"aacc961fbc532c36b183549b12e72790e75c92f6", @"/Views/TaskComplete/_ModalDetailProgress.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6ba151424cb7e4ff7d935c55cfcaa12c04b783fe", @"/Views/_ViewImports.cshtml")]
    public class Views_TaskComplete__ModalDetailProgress : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PortalPMO.ViewModels.ProgressKerjaMember_ViewModels>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("FormDataProgresPekerjaan"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(60, 287, true);
            WriteLiteral(@"<style>
    .modal-dialog {
        min-width: 70%;
    }
</style>
<div class=""modal-header"">
    <h5 class=""modal-title"">Form Data</h5>
    <button type=""button"" class=""close"" data-dismiss=""modal"" aria-hidden=""true"">×</button>
</div>
<div class=""modal-body"" id=""id-body"">
    ");
            EndContext();
            BeginContext(347, 1815, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "aacc961fbc532c36b183549b12e72790e75c92f64467", async() => {
                BeginContext(409, 10, true);
                WriteLiteral("\r\n        ");
                EndContext();
                BeginContext(420, 23, false);
#line 13 "C:\work\Portal PMO\PortalPMO\Views\TaskComplete\_ModalDetailProgress.cshtml"
   Write(Html.AntiForgeryToken());

#line default
#line hidden
                EndContext();
                BeginContext(443, 10, true);
                WriteLiteral("\r\n        ");
                EndContext();
                BeginContext(454, 25, false);
#line 14 "C:\work\Portal PMO\PortalPMO\Views\TaskComplete\_ModalDetailProgress.cshtml"
   Write(Html.HiddenFor(m => m.Id));

#line default
#line hidden
                EndContext();
                BeginContext(479, 10, true);
                WriteLiteral("\r\n        ");
                EndContext();
                BeginContext(490, 32, false);
#line 15 "C:\work\Portal PMO\PortalPMO\Views\TaskComplete\_ModalDetailProgress.cshtml"
   Write(Html.HiddenFor(m => m.Deskripsi));

#line default
#line hidden
                EndContext();
                BeginContext(522, 10, true);
                WriteLiteral("\r\n        ");
                EndContext();
                BeginContext(533, 38, false);
#line 16 "C:\work\Portal PMO\PortalPMO\Views\TaskComplete\_ModalDetailProgress.cshtml"
   Write(Html.HiddenFor(m => m.ProjectMemberId));

#line default
#line hidden
                EndContext();
                BeginContext(571, 559, true);
                WriteLiteral(@"


        <div class=""form-group row m-b-15"">
            <label class=""col-md-3 col-form-label"">Tanggal <span class=""labelMandatory"">*</span></label>
            <div class=""col-md-9"">
                <div class=""input-group"">
                    <div class=""input-group-prepend datepicker-trigger"" id=""TanggalSearch"" data-date-format=""dd/mm/yyyy"">
                        <div class=""input-group-text"">
                            <i class=""fa fa-calendar-alt""></i>
                        </div>
                    </div>
                    ");
                EndContext();
                BeginContext(1131, 116, false);
#line 28 "C:\work\Portal PMO\PortalPMO\Views\TaskComplete\_ModalDetailProgress.cshtml"
               Write(Html.TextBoxFor(m => m.Tanggal, new { @class = "datepicker-here form-control", @value = "", @autocomplete = "off" }));

#line default
#line hidden
                EndContext();
                BeginContext(1247, 310, true);
                WriteLiteral(@"
                </div>
            </div>
        </div>
        <div class=""form-group row m-b-15"">
            <label class=""col-md-3 col-form-label"">Judul <span class=""labelMandatory"">*</span></label>
            <div class=""col-md-9"">
                <div class=""input-group"">
                    ");
                EndContext();
                BeginContext(1558, 98, false);
#line 36 "C:\work\Portal PMO\PortalPMO\Views\TaskComplete\_ModalDetailProgress.cshtml"
               Write(Html.TextBoxFor(m => m.Judul, new { @class = "form-control", @value = "", @autocomplete = "off" }));

#line default
#line hidden
                EndContext();
                BeginContext(1656, 499, true);
                WriteLiteral(@"

                </div>
            </div>
        </div>
        <div class=""form-group row m-b-15"">
            <label class=""col-md-3 col-form-label"">Deskripsi <span class=""labelMandatory"">*</span></label>
            <div class=""col-md-9"">
                <div class=""input-group"">
                    <textarea id=""KeteranganSummernote"" name=""KeteranganSummernote"" class=""form-control"" style=""width:100%""></textarea>
                </div>
            </div>
        </div>

    ");
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
            BeginContext(2162, 239, true);
            WriteLiteral("\r\n</div>\r\n<div class=\"modal-footer\">\r\n    <a href=\"javascript:;\" class=\"btn btn-sm btn-white\" data-dismiss=\"modal\">Tutup</a>\r\n    <a href=\"javascript:;\" class=\"btn btn-sm btn-primary\" id=\"BtnSubmitDetailProgresPekerjaan\">Simpan</a>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PortalPMO.ViewModels.ProgressKerjaMember_ViewModels> Html { get; private set; }
    }
}
#pragma warning restore 1591

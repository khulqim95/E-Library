#pragma checksum "C:\work\Portal PMO\PortalPMO\Views\CreateProject\_ModalClient.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1631ccc6ad3175626fdc69d25b673083353f5708"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_CreateProject__ModalClient), @"mvc.1.0.view", @"/Views/CreateProject/_ModalClient.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/CreateProject/_ModalClient.cshtml", typeof(AspNetCore.Views_CreateProject__ModalClient))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1631ccc6ad3175626fdc69d25b673083353f5708", @"/Views/CreateProject/_ModalClient.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6ba151424cb7e4ff7d935c55cfcaa12c04b783fe", @"/Views/_ViewImports.cshtml")]
    public class Views_CreateProject__ModalClient : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PortalPMO.ViewModels.DataProjectUser_ViewModels>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("FormDataClient"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(56, 287, true);
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
            BeginContext(343, 2764, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1631ccc6ad3175626fdc69d25b673083353f57084404", async() => {
                BeginContext(395, 10, true);
                WriteLiteral("\r\n        ");
                EndContext();
                BeginContext(406, 23, false);
#line 13 "C:\work\Portal PMO\PortalPMO\Views\CreateProject\_ModalClient.cshtml"
   Write(Html.AntiForgeryToken());

#line default
#line hidden
                EndContext();
                BeginContext(429, 10, true);
                WriteLiteral("\r\n        ");
                EndContext();
                BeginContext(440, 25, false);
#line 14 "C:\work\Portal PMO\PortalPMO\Views\CreateProject\_ModalClient.cshtml"
   Write(Html.HiddenFor(m => m.Id));

#line default
#line hidden
                EndContext();
                BeginContext(465, 253, true);
                WriteLiteral("\r\n\r\n        <div class=\"form-group row m-b-15\">\r\n            <label class=\"col-md-3 col-form-label\">Client <span class=\"labelMandatory\">*</span></label>\r\n            <div class=\"col-md-9\">\r\n                <div class=\"input-group\">\r\n                    ");
                EndContext();
                BeginContext(719, 200, false);
#line 20 "C:\work\Portal PMO\PortalPMO\Views\CreateProject\_ModalClient.cshtml"
               Write(Html.DropDownListFor(m => m.ClientId, (IEnumerable<SelectListItem>)ViewBag.Client, "-Silahkan pilih-", new { @class = "js-example-placeholder-multiple js-states form-control", @style = "width:100%" }));

#line default
#line hidden
                EndContext();
                BeginContext(919, 2, true);
                WriteLiteral("\r\n");
                EndContext();
                BeginContext(1077, 310, true);
                WriteLiteral(@"                </div>
            </div>
        </div>
        <div class=""form-group row m-b-15"">
            <label class=""col-md-3 col-form-label"">Npp PIC <span class=""labelMandatory"">*</span></label>
            <div class=""col-md-9"">
                <div class=""input-group"">
                    ");
                EndContext();
                BeginContext(1388, 99, false);
#line 29 "C:\work\Portal PMO\PortalPMO\Views\CreateProject\_ModalClient.cshtml"
               Write(Html.TextBoxFor(m => m.NppPic, new { @class = "form-control", @value = "", @autocomplete = "off" }));

#line default
#line hidden
                EndContext();
                BeginContext(1487, 313, true);
                WriteLiteral(@"
                </div>
            </div>
        </div>
        <div class=""form-group row m-b-15"">
            <label class=""col-md-3 col-form-label"">Nama PIC <span class=""labelMandatory"">*</span></label>
            <div class=""col-md-9"">
                <div class=""input-group"">
                    ");
                EndContext();
                BeginContext(1801, 100, false);
#line 37 "C:\work\Portal PMO\PortalPMO\Views\CreateProject\_ModalClient.cshtml"
               Write(Html.TextBoxFor(m => m.NamaPic, new { @class = "form-control", @value = "", @autocomplete = "off" }));

#line default
#line hidden
                EndContext();
                BeginContext(1901, 273, true);
                WriteLiteral(@"
                </div>
            </div>
        </div>
        <div class=""form-group row m-b-15"">
            <label class=""col-md-3 col-form-label"">Email </label>
            <div class=""col-md-9"">
                <div class=""input-group"">
                    ");
                EndContext();
                BeginContext(2175, 98, false);
#line 45 "C:\work\Portal PMO\PortalPMO\Views\CreateProject\_ModalClient.cshtml"
               Write(Html.TextBoxFor(m => m.Email, new { @class = "form-control", @value = "", @autocomplete = "off" }));

#line default
#line hidden
                EndContext();
                BeginContext(2273, 278, true);
                WriteLiteral(@"
                </div>
            </div>
        </div>
        <div class=""form-group row m-b-15"">
            <label class=""col-md-3 col-form-label"">No Telepon </label>
            <div class=""col-md-9"">
                <div class=""input-group"">
                    ");
                EndContext();
                BeginContext(2552, 97, false);
#line 53 "C:\work\Portal PMO\PortalPMO\Views\CreateProject\_ModalClient.cshtml"
               Write(Html.TextBoxFor(m => m.NoHp, new { @class = "form-control", @value = "", @autocomplete = "off" }));

#line default
#line hidden
                EndContext();
                BeginContext(2649, 267, true);
                WriteLiteral(@"
                </div>
            </div>
        </div>
        <div class=""form-group row m-b-15"">
            <label class=""col-md-3 col-form-label"">Keterangan </label>
            <div class=""col-md-9"">
                <div class="""">
                    ");
                EndContext();
                BeginContext(2917, 117, false);
#line 61 "C:\work\Portal PMO\PortalPMO\Views\CreateProject\_ModalClient.cshtml"
               Write(Html.TextAreaFor(m => m.Keterangan, new { @class = "form-control", @value = "", @autocomplete = "off", @rows = "5" }));

#line default
#line hidden
                EndContext();
                BeginContext(3034, 66, true);
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
            BeginContext(3107, 221, true);
            WriteLiteral("\r\n</div>\r\n<div class=\"modal-footer\">\r\n    <a href=\"javascript:;\" class=\"btn btn-sm btn-white\" data-dismiss=\"modal\">Tutup</a>\r\n    <a href=\"javascript:;\" class=\"btn btn-sm btn-primary\" id=\"BtnSubmitUser\">Simpan</a>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PortalPMO.ViewModels.DataProjectUser_ViewModels> Html { get; private set; }
    }
}
#pragma warning restore 1591

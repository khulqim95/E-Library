#pragma checksum "C:\work\E-Library\E-Library\PortalPMO\Views\DataKompleksitasProject\_Create.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "637829c6b54fb2c9436ef93c624999f11b50c820"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_DataKompleksitasProject__Create), @"mvc.1.0.view", @"/Views/DataKompleksitasProject/_Create.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/DataKompleksitasProject/_Create.cshtml", typeof(AspNetCore.Views_DataKompleksitasProject__Create))]
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
#line 1 "C:\work\E-Library\E-Library\PortalPMO\Views\_ViewImports.cshtml"
using PortalPMO;

#line default
#line hidden
#line 2 "C:\work\E-Library\E-Library\PortalPMO\Views\_ViewImports.cshtml"
using PortalPMO.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"637829c6b54fb2c9436ef93c624999f11b50c820", @"/Views/DataKompleksitasProject/_Create.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6ba151424cb7e4ff7d935c55cfcaa12c04b783fe", @"/Views/_ViewImports.cshtml")]
    public class Views_DataKompleksitasProject__Create : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PortalPMO.Models.dbPortalPMO.TblMasterKompleksitasProject>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("FormData"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(66, 172, true);
            WriteLiteral("\r\n<div class=\"row\" style=\"padding-top:20px;\">\r\n    <div class=\"col-lg-12\">\r\n        <div class=\"main-card mb-3 card\">\r\n            <div class=\"card-body\">\r\n                ");
            EndContext();
            BeginContext(238, 3398, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "637829c6b54fb2c9436ef93c624999f11b50c8204358", async() => {
                BeginContext(284, 22, true);
                WriteLiteral("\r\n                    ");
                EndContext();
                BeginContext(307, 23, false);
#line 8 "C:\work\E-Library\E-Library\PortalPMO\Views\DataKompleksitasProject\_Create.cshtml"
               Write(Html.AntiForgeryToken());

#line default
#line hidden
                EndContext();
                BeginContext(330, 313, true);
                WriteLiteral(@"
                    <div class=""form-group row m-b-15"">
                        <label class=""col-md-3 col-form-label"">Kategori <span class=""labelMandatory"">*</span></label>
                        <div class=""col-md-9"">
                            <div class=""input-group"">
                                ");
                EndContext();
                BeginContext(644, 228, false);
#line 13 "C:\work\E-Library\E-Library\PortalPMO\Views\DataKompleksitasProject\_Create.cshtml"
                           Write(Html.DropDownListFor(m => m.KategoriKompleksitasId, (IEnumerable<SelectListItem>)ViewBag.KategoriKompleksitas, "-Silahkan pilih-", new { @class = "js-example-placeholder-multiple js-states form-control", @style = "width:100%" }));

#line default
#line hidden
                EndContext();
                BeginContext(872, 367, true);
                WriteLiteral(@"
                            </div>
                        </div>
                    </div>
                    <div class=""form-group row m-b-15"">
                        <label class=""col-md-3 col-form-label"">Kode</label>
                        <div class=""col-md-9"">
                            <div class=""input-group"">
                                ");
                EndContext();
                BeginContext(1240, 97, false);
#line 21 "C:\work\E-Library\E-Library\PortalPMO\Views\DataKompleksitasProject\_Create.cshtml"
                           Write(Html.TextBoxFor(m => m.Kode, new { @class = "form-control", @value = "", @autocomplete = "off" }));

#line default
#line hidden
                EndContext();
                BeginContext(1337, 405, true);
                WriteLiteral(@"
                            </div>
                        </div>
                    </div>
                    <div class=""form-group row m-b-15"">
                        <label class=""col-md-3 col-form-label"">Nama <span class=""labelMandatory"">*</span></label>
                        <div class=""col-md-9"">
                            <div class=""input-group"">
                                ");
                EndContext();
                BeginContext(1743, 97, false);
#line 29 "C:\work\E-Library\E-Library\PortalPMO\Views\DataKompleksitasProject\_Create.cshtml"
                           Write(Html.TextBoxFor(m => m.Nama, new { @class = "form-control", @value = "", @autocomplete = "off" }));

#line default
#line hidden
                EndContext();
                BeginContext(1840, 374, true);
                WriteLiteral(@"
                            </div>
                        </div>
                    </div>
                    <div class=""form-group row m-b-15"">
                        <label class=""col-md-3 col-form-label"">Keterangan </label>
                        <div class=""col-md-9"">
                            <div class=""input-group"">
                                ");
                EndContext();
                BeginContext(2215, 117, false);
#line 37 "C:\work\E-Library\E-Library\PortalPMO\Views\DataKompleksitasProject\_Create.cshtml"
                           Write(Html.TextAreaFor(m => m.Keterangan, new { @class = "form-control", @value = "", @autocomplete = "off", @rows = "4" }));

#line default
#line hidden
                EndContext();
                BeginContext(2332, 410, true);
                WriteLiteral(@"
                            </div>
                        </div>
                    </div>
                    <div class=""form-group row m-b-15"">
                        <label class=""col-md-3 col-form-label"">Urutan Ke <span class=""labelMandatory"">*</span></label>
                        <div class=""col-md-9"">
                            <div class=""input-group"">
                                ");
                EndContext();
                BeginContext(2743, 118, false);
#line 45 "C:\work\E-Library\E-Library\PortalPMO\Views\DataKompleksitasProject\_Create.cshtml"
                           Write(Html.TextBoxFor(m => m.OrderBy, new { @type = "number", @class = "form-control", @value = "", @autocomplete = "off" }));

#line default
#line hidden
                EndContext();
                BeginContext(2861, 407, true);
                WriteLiteral(@"
                            </div>
                        </div>
                    </div>
                    <div class=""form-group row m-b-15"">
                        <label class=""col-md-3 col-form-label"">Status <span class=""labelMandatory"">*</span></label>
                        <div class=""col-md-9"">
                            <div class=""input-group"">
                                ");
                EndContext();
                BeginContext(3269, 59, false);
#line 53 "C:\work\E-Library\E-Library\PortalPMO\Views\DataKompleksitasProject\_Create.cshtml"
                           Write(Html.RadioButtonFor(model => model.IsActive, true, new { }));

#line default
#line hidden
                EndContext();
                BeginContext(3328, 77, true);
                WriteLiteral("&nbsp; Aktif &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\r\n                                ");
                EndContext();
                BeginContext(3406, 60, false);
#line 54 "C:\work\E-Library\E-Library\PortalPMO\Views\DataKompleksitasProject\_Create.cshtml"
                           Write(Html.RadioButtonFor(model => model.IsActive, false, new { }));

#line default
#line hidden
                EndContext();
                BeginContext(3466, 163, true);
                WriteLiteral("&nbsp; Tidak Aktif &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n                ");
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
            BeginContext(3636, 316, true);
            WriteLiteral(@"
            </div>
            <div class=""modal-footer"">
                <a href=""javascript:;"" class=""btn btn-sm btn-white"" onclick=""BackDraft()"">Tutup</a>
                <a href=""javascript:;"" class=""btn btn-sm btn-primary"" id=""BtnSubmit"">Simpan</a>
            </div>
        </div>
    </div>
</div>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PortalPMO.Models.dbPortalPMO.TblMasterKompleksitasProject> Html { get; private set; }
    }
}
#pragma warning restore 1591

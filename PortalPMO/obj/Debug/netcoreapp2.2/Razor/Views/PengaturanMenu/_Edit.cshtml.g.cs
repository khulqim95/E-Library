#pragma checksum "C:\work\E-Library\E-Library\PortalPMO\Views\PengaturanMenu\_Edit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a2b73b6481f462cb3cf9c0b2dc420e1a2ef7f1eb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_PengaturanMenu__Edit), @"mvc.1.0.view", @"/Views/PengaturanMenu/_Edit.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/PengaturanMenu/_Edit.cshtml", typeof(AspNetCore.Views_PengaturanMenu__Edit))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a2b73b6481f462cb3cf9c0b2dc420e1a2ef7f1eb", @"/Views/PengaturanMenu/_Edit.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6ba151424cb7e4ff7d935c55cfcaa12c04b783fe", @"/Views/_ViewImports.cshtml")]
    public class Views_PengaturanMenu__Edit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PortalPMO.ViewModels.PengaturanMenu_ViewModels>
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
            BeginContext(55, 172, true);
            WriteLiteral("\r\n<div class=\"row\" style=\"padding-top:20px;\">\r\n    <div class=\"col-lg-12\">\r\n        <div class=\"main-card mb-3 card\">\r\n            <div class=\"card-body\">\r\n                ");
            EndContext();
            BeginContext(227, 5187, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a2b73b6481f462cb3cf9c0b2dc420e1a2ef7f1eb4270", async() => {
                BeginContext(273, 22, true);
                WriteLiteral("\r\n                    ");
                EndContext();
                BeginContext(296, 23, false);
#line 8 "C:\work\E-Library\E-Library\PortalPMO\Views\PengaturanMenu\_Edit.cshtml"
               Write(Html.AntiForgeryToken());

#line default
#line hidden
                EndContext();
                BeginContext(319, 22, true);
                WriteLiteral("\r\n                    ");
                EndContext();
                BeginContext(342, 25, false);
#line 9 "C:\work\E-Library\E-Library\PortalPMO\Views\PengaturanMenu\_Edit.cshtml"
               Write(Html.HiddenFor(m => m.Id));

#line default
#line hidden
                EndContext();
                BeginContext(367, 309, true);
                WriteLiteral(@"
                    <div class=""form-group row m-b-15"">
                        <label class=""col-md-3 col-form-label"">Tipe <span class=""labelMandatory"">*</span></label>
                        <div class=""col-md-9"">
                            <div class=""input-group"">
                                ");
                EndContext();
                BeginContext(677, 200, false);
#line 14 "C:\work\E-Library\E-Library\PortalPMO\Views\PengaturanMenu\_Edit.cshtml"
                           Write(Html.DropDownListFor(m => m.TipeId, (IEnumerable<SelectListItem>)ViewBag.TypeMenu, "-Silahkan pilih-", new { @class = "js-example-placeholder-multiple js-states form-control", @style = "width:100%" }));

#line default
#line hidden
                EndContext();
                BeginContext(877, 405, true);
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
                BeginContext(1283, 155, false);
#line 22 "C:\work\E-Library\E-Library\PortalPMO\Views\PengaturanMenu\_Edit.cshtml"
                           Write(Html.TextBoxFor(m => m.Nama, new { @class = "form-control", @value = "", @autocomplete = "off", @placeholder = "Masukkan Nama", @autofocus = "autofocus" }));

#line default
#line hidden
                EndContext();
                BeginContext(1438, 476, true);
                WriteLiteral(@"

                            </div>
                        </div>
                    </div>
                    <div class=""form-group row m-b-15"">
                        <label class=""col-md-3 col-form-label"">Induk Menu <span class=""labelMandatory"">*</span></label>
                        <div class=""col-md-9"">
                            <div class=""input-group"">
                                <div class=""input-group"">
                                    ");
                EndContext();
                BeginContext(1915, 204, false);
#line 32 "C:\work\E-Library\E-Library\PortalPMO\Views\PengaturanMenu\_Edit.cshtml"
                               Write(Html.DropDownListFor(m => m.ParentId, (IEnumerable<SelectListItem>)ViewBag.ParentMenu, "-Silahkan pilih-", new { @class = "js-example-placeholder-multiple js-states form-control", @style = "width:100%" }));

#line default
#line hidden
                EndContext();
                BeginContext(2119, 444, true);
                WriteLiteral(@"
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class=""form-group row m-b-15"">
                        <label class=""col-md-3 col-form-label"">Url <span class=""labelMandatory"">*</span></label>
                        <div class=""col-md-9"">
                            <div class=""input-group"">
                                ");
                EndContext();
                BeginContext(2564, 162, false);
#line 41 "C:\work\E-Library\E-Library\PortalPMO\Views\PengaturanMenu\_Edit.cshtml"
                           Write(Html.TextBoxFor(m => m.Route, new { @class = "form-control", @value = "", @autocomplete = "off", @placeholder = "Masukkan Alamat Url", @autofocus = "autofocus" }));

#line default
#line hidden
                EndContext();
                BeginContext(2726, 450, true);
                WriteLiteral(@"

                            </div>
                        </div>
                    </div>
                    <div class=""form-group row m-b-15"">
                        <label class=""col-md-3 col-form-label"">Role <span class=""labelMandatory"">*</span></label>
                        <div class=""col-md-9"">
                            <div class=""input-group"">
                                <input class=""form-control"" id=""RolesValue""");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 3176, "\"", 3213, 1);
#line 50 "C:\work\E-Library\E-Library\PortalPMO\Views\PengaturanMenu\_Edit.cshtml"
WriteAttributeValue("", 3184, ViewBag.NavigationAssignment, 3184, 29, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(3214, 69, true);
                WriteLiteral(" style=\"display:none\" readonly />\r\n\r\n                                ");
                EndContext();
                BeginContext(3284, 244, false);
#line 52 "C:\work\E-Library\E-Library\PortalPMO\Views\PengaturanMenu\_Edit.cshtml"
                           Write(Html.DropDownListFor(m => m.Role, (IEnumerable<SelectListItem>)ViewBag.RolePegawai, "-Silahkan pilih-", new { @class = "js-example-placeholder-multiple js-states form-control", @name = "Roles[]", @style = "width:100%", @multiple = "multiple" }));

#line default
#line hidden
                EndContext();
                BeginContext(3528, 409, true);
                WriteLiteral(@"

                            </div>
                        </div>
                    </div>
                    <div class=""form-group row m-b-15"">
                        <label class=""col-md-3 col-form-label"">Urutan <span class=""labelMandatory"">*</span></label>
                        <div class=""col-md-9"">
                            <div class=""input-group"">
                                ");
                EndContext();
                BeginContext(3938, 183, false);
#line 61 "C:\work\E-Library\E-Library\PortalPMO\Views\PengaturanMenu\_Edit.cshtml"
                           Write(Html.TextBoxFor(m => m.OrderBy, new { @type = "number", @class = "form-control", @value = "", @autocomplete = "off", @placeholder = "Masukkan urutan menu", @autofocus = "autofocus" }));

#line default
#line hidden
                EndContext();
                BeginContext(4121, 373, true);
                WriteLiteral(@"

                            </div>
                        </div>
                    </div>
                    <div class=""form-group row m-b-15"">
                        <label class=""col-md-3 col-form-label"">Initial </label>
                        <div class=""col-md-9"">
                            <div class=""input-group"">
                                ");
                EndContext();
                BeginContext(4495, 160, false);
#line 70 "C:\work\E-Library\E-Library\PortalPMO\Views\PengaturanMenu\_Edit.cshtml"
                           Write(Html.TextBoxFor(m => m.Icon, new { @class = "form-control", @value = "", @autocomplete = "off", @placeholder = "Masukkan Icon Menu", @autofocus = "autofocus" }));

#line default
#line hidden
                EndContext();
                BeginContext(4655, 409, true);
                WriteLiteral(@"

                            </div>
                        </div>
                    </div>
                    <div class=""form-group row m-b-15"">
                        <label class=""col-md-3 col-form-label"">Aktif? <span class=""labelMandatory"">*</span></label>
                        <div class=""col-md-9"">
                            <div class=""input-group"">
                                ");
                EndContext();
                BeginContext(5065, 55, false);
#line 79 "C:\work\E-Library\E-Library\PortalPMO\Views\PengaturanMenu\_Edit.cshtml"
                           Write(Html.RadioButtonFor(model => model.Visible, 1, new { }));

#line default
#line hidden
                EndContext();
                BeginContext(5120, 74, true);
                WriteLiteral("&nbsp; Ya &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\r\n                                ");
                EndContext();
                BeginContext(5195, 55, false);
#line 80 "C:\work\E-Library\E-Library\PortalPMO\Views\PengaturanMenu\_Edit.cshtml"
                           Write(Html.RadioButtonFor(model => model.Visible, 0, new { }));

#line default
#line hidden
                EndContext();
                BeginContext(5250, 157, true);
                WriteLiteral("&nbsp; Tidak &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n                ");
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
            BeginContext(5414, 318, true);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PortalPMO.ViewModels.PengaturanMenu_ViewModels> Html { get; private set; }
    }
}
#pragma warning restore 1591

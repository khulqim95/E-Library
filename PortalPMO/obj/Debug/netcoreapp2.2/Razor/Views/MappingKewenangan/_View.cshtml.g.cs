#pragma checksum "C:\work\E-Library\E-Library\PortalPMO\Views\MappingKewenangan\_View.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "85ffb879b02cff8ea571208fd642d326f8316962"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_MappingKewenangan__View), @"mvc.1.0.view", @"/Views/MappingKewenangan/_View.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/MappingKewenangan/_View.cshtml", typeof(AspNetCore.Views_MappingKewenangan__View))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"85ffb879b02cff8ea571208fd642d326f8316962", @"/Views/MappingKewenangan/_View.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6ba151424cb7e4ff7d935c55cfcaa12c04b783fe", @"/Views/_ViewImports.cshtml")]
    public class Views_MappingKewenangan__View : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PortalPMO.ViewModels.MappingKewenangan_VM>
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
            BeginContext(50, 268, true);
            WriteLiteral(@"
<div class=""row"" style=""padding-top:20px;"">
    <div class=""col-lg-12"">
        <div class=""main-card mb-3 card"">
            <div class=""headerSearch"">
                Formulir Ubah Data
            </div>
            <div class=""card-body"">
                ");
            EndContext();
            BeginContext(318, 2832, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "85ffb879b02cff8ea571208fd642d326f83169624375", async() => {
                BeginContext(364, 22, true);
                WriteLiteral("\r\n                    ");
                EndContext();
                BeginContext(387, 23, false);
#line 11 "C:\work\E-Library\E-Library\PortalPMO\Views\MappingKewenangan\_View.cshtml"
               Write(Html.AntiForgeryToken());

#line default
#line hidden
                EndContext();
                BeginContext(410, 22, true);
                WriteLiteral("\r\n                    ");
                EndContext();
                BeginContext(433, 25, false);
#line 12 "C:\work\E-Library\E-Library\PortalPMO\Views\MappingKewenangan\_View.cshtml"
               Write(Html.HiddenFor(m => m.Id));

#line default
#line hidden
                EndContext();
                BeginContext(458, 280, true);
                WriteLiteral(@"
                    <div class=""form-group row m-b-15"">
                        <label class=""col-md-3 col-form-label"">Jabatan <span class=""labelMandatory"">*</span></label>
                        <div class=""col-md-9"">
                            <div class=""input-group"">
");
                EndContext();
                BeginContext(877, 32, true);
                WriteLiteral("                                ");
                EndContext();
                BeginContext(910, 157, false);
#line 18 "C:\work\E-Library\E-Library\PortalPMO\Views\MappingKewenangan\_View.cshtml"
                           Write(Html.TextBoxFor(m => m.JabatanName, new { @class = "js-example-placeholder-multiple js-states form-control", @style = "width:100%", @readonly = "readonly" }));

#line default
#line hidden
                EndContext();
                BeginContext(1067, 448, true);
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
                BeginWriteAttribute("value", " value=\"", 1515, "\"", 1552, 1);
#line 26 "C:\work\E-Library\E-Library\PortalPMO\Views\MappingKewenangan\_View.cshtml"
WriteAttributeValue("", 1523, ViewBag.NavigationAssignment, 1523, 29, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1553, 67, true);
                WriteLiteral(" style=\"display:none\" readonly />\r\n                                ");
                EndContext();
                BeginContext(1621, 246, false);
#line 27 "C:\work\E-Library\E-Library\PortalPMO\Views\MappingKewenangan\_View.cshtml"
                           Write(Html.DropDownListFor(m => m.Roles, (IEnumerable<SelectListItem>)ViewBag.RolePegawai, "-Silahkan pilih-", new { @class = "js-example-placeholder-multiple js-states form-control", @name = "RoleId[]", @style = "width:100%", @multiple = "multiple" }));

#line default
#line hidden
                EndContext();
                BeginContext(1867, 374, true);
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
                BeginContext(2242, 142, false);
#line 35 "C:\work\E-Library\E-Library\PortalPMO\Views\MappingKewenangan\_View.cshtml"
                           Write(Html.TextAreaFor(m => m.Keterangan, new { @class = "form-control", @value = "", @autocomplete = "off", @placeholder = "Masukkan Keterangan" }));

#line default
#line hidden
                EndContext();
                BeginContext(2384, 407, true);
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
                BeginContext(2792, 59, false);
#line 43 "C:\work\E-Library\E-Library\PortalPMO\Views\MappingKewenangan\_View.cshtml"
                           Write(Html.RadioButtonFor(model => model.IsActive, true, new { }));

#line default
#line hidden
                EndContext();
                BeginContext(2851, 74, true);
                WriteLiteral("&nbsp; Ya &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\r\n                                ");
                EndContext();
                BeginContext(2926, 60, false);
#line 44 "C:\work\E-Library\E-Library\PortalPMO\Views\MappingKewenangan\_View.cshtml"
                           Write(Html.RadioButtonFor(model => model.IsActive, false, new { }));

#line default
#line hidden
                EndContext();
                BeginContext(2986, 157, true);
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
            BeginContext(3150, 219, true);
            WriteLiteral("\r\n            </div>\r\n            <div class=\"modal-footer\">\r\n                <a href=\"javascript:;\" class=\"btn btn-sm btn-white\" onclick=\"BackDraft()\">Tutup</a>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PortalPMO.ViewModels.MappingKewenangan_VM> Html { get; private set; }
    }
}
#pragma warning restore 1591

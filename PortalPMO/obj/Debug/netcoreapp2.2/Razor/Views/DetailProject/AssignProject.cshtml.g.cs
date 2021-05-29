#pragma checksum "C:\work\Portal PMO\PortalPMO\Views\DetailProject\AssignProject.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3af12e39b2788b20b81da9c765219374ed6a19df"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_DetailProject_AssignProject), @"mvc.1.0.view", @"/Views/DetailProject/AssignProject.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/DetailProject/AssignProject.cshtml", typeof(AspNetCore.Views_DetailProject_AssignProject))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3af12e39b2788b20b81da9c765219374ed6a19df", @"/Views/DetailProject/AssignProject.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6ba151424cb7e4ff7d935c55cfcaa12c04b783fe", @"/Views/_ViewImports.cshtml")]
    public class Views_DetailProject_AssignProject : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PortalPMO.Models.dbPortalPMO.TblMasterSistem>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("FormData"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("padding-top:20px;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/plugin/select2/dist/js/select2.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(53, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\work\Portal PMO\PortalPMO\Views\DetailProject\AssignProject.cshtml"
  
    ViewData["Title"] = "Create Project";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(152, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            DefineSection("styles", async() => {
                BeginContext(170, 1022, true);
                WriteLiteral(@"
    <style>
        .fab {
            position: fixed;
            bottom: 20px;
            right: 20px;
            clip-path: circle();
            height: 40px;
            width: 40px;
            z-index: 999;
            box-shadow: 0 0.46875rem 2.1875rem rgba(4,9,20,0.03), 0 0.9375rem 1.40625rem rgba(4,9,20,0.03), 0 0.25rem 0.53125rem rgba(4,9,20,0.05), 0 0.125rem 0.1875rem rgba(4,9,20,0.03);
        }

        .addButton {
            position: fixed;
            bottom: 30px;
            right: 30px;
            border-radius: 50%;
            width: 50px;
            height: 50px;
        }

        .select2 {
            height: 30px;
        }

        .dataTables_wrapper {
            zoom: 0.9 !important;
        }

        table.dataTable.dtr-inline.collapsed > tbody > tr[role=""row""] > td:first-child:before, table.dataTable.dtr-inline.collapsed > tbody > tr[role=""row""] > th:first-child:before {
            background-color: #3f6ad8;
        }
    </style>
");
                EndContext();
            }
            );
            BeginContext(1195, 1039, true);
            WriteLiteral(@"<div class=""app-main__outer"">
    <div class=""app-main__inner"">
        <div class=""app-page-title"">
            <div class=""page-title-wrapper"">
                <div class=""page-title-heading"">
                    <div class=""page-title-icon"">
                        <i class=""pe-7s-note icon-gradient bg-night-fade"">
                        </i>
                    </div>
                    <div>
                        Assignment Project
                        <div class=""page-title-subheading"">
                            Detail informasi assignment project
                        </div>
                    </div>
                </div>

            </div>
        </div>
        <div class=""row"" style=""padding-top:20px;"">
            <div class=""col-lg-12"">
                <div class=""main-card mb-3 card"">
                    <div class=""headerSearch"">
                        Formulir Assign Project
                    </div>
                    <div class=""card-body"">
         ");
            WriteLiteral("               ");
            EndContext();
            BeginContext(2234, 4067, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3af12e39b2788b20b81da9c765219374ed6a19df7501", async() => {
                BeginContext(2280, 30, true);
                WriteLiteral("\r\n                            ");
                EndContext();
                BeginContext(2311, 23, false);
#line 70 "C:\work\Portal PMO\PortalPMO\Views\DetailProject\AssignProject.cshtml"
                       Write(Html.AntiForgeryToken());

#line default
#line hidden
                EndContext();
                BeginContext(2334, 355, true);
                WriteLiteral(@"
                            <div class=""form-group row m-b-15"">
                                <label class=""col-md-3 col-form-label"">No Project <span class=""labelMandatory"">*</span></label>
                                <div class=""col-md-9"">
                                    <div class=""input-group"">
                                        ");
                EndContext();
                BeginContext(2690, 121, false);
#line 75 "C:\work\Portal PMO\PortalPMO\Views\DetailProject\AssignProject.cshtml"
                                   Write(Html.TextBoxFor(m => m.Kode, new { @class = "form-control", @value = "", @autocomplete = "off", @readonly = "readonly" }));

#line default
#line hidden
                EndContext();
                BeginContext(2811, 477, true);
                WriteLiteral(@"
                                    </div>
                                </div>
                            </div>
                            <div class=""form-group row m-b-15"">
                                <label class=""col-md-3 col-form-label"">Nama Project <span class=""labelMandatory"">*</span></label>
                                <div class=""col-md-9"">
                                    <div class=""input-group"">
                                        ");
                EndContext();
                BeginContext(3289, 121, false);
#line 83 "C:\work\Portal PMO\PortalPMO\Views\DetailProject\AssignProject.cshtml"
                                   Write(Html.TextBoxFor(m => m.Kode, new { @class = "form-control", @value = "", @autocomplete = "off", @readonly = "readonly" }));

#line default
#line hidden
                EndContext();
                BeginContext(3410, 483, true);
                WriteLiteral(@"
                                    </div>
                                </div>
                            </div>
                            <div class=""form-group row m-b-15"">
                                <label class=""col-md-3 col-form-label"">Deskripsi Aplikasi <span class=""labelMandatory"">*</span></label>
                                <div class=""col-md-9"">
                                    <div class=""input-group"">
                                        ");
                EndContext();
                BeginContext(3894, 142, false);
#line 91 "C:\work\Portal PMO\PortalPMO\Views\DetailProject\AssignProject.cshtml"
                                   Write(Html.TextAreaFor(m => m.Keterangan, new { @class = "form-control", @value = "", @autocomplete = "off", @readonly = "readonly", @rows = "10" }));

#line default
#line hidden
                EndContext();
                BeginContext(4036, 659, true);
                WriteLiteral(@"
                                    </div>
                                </div>
                            </div>
                            <div class=""form-group row m-b-15"">
                                <label class=""col-md-3 col-form-label"">Lanjut Ke Proses<span class=""labelMandatory"">*</span></label>
                                <div class=""col-md-9"">
                                    <div class=""input-group"">
                                        <select class=""js-example-placeholder-multiple js-states form-control"" id=""MasterSistemId"" name=""MasterSistemId"" style=""width:100%;"">
                                            ");
                EndContext();
                BeginContext(4695, 38, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3af12e39b2788b20b81da9c765219374ed6a19df11741", async() => {
                    BeginContext(4703, 21, true);
                    WriteLiteral("Silahkan Pilih Proses");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(4733, 703, true);
                WriteLiteral(@"
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class=""form-group row m-b-15"">
                                <label class=""col-md-3 col-form-label"">Assign To<span class=""labelMandatory"">*</span></label>
                                <div class=""col-md-9"">
                                    <div class=""input-group"">
                                        <select class=""js-example-placeholder-multiple js-states form-control"" id=""MasterSistemId"" name=""MasterSistemId"" style=""width:100%;"">
                                            ");
                EndContext();
                BeginContext(5436, 39, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3af12e39b2788b20b81da9c765219374ed6a19df13733", async() => {
                    BeginContext(5444, 22, true);
                    WriteLiteral("Silahkan Pilih Pegawai");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(5475, 523, true);
                WriteLiteral(@"
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class=""form-group row m-b-15"">
                                <label class=""col-md-3 col-form-label"">Catatan <span class=""labelMandatory"">*</span></label>
                                <div class=""col-md-9"">
                                    <div class=""input-group"">
                                        ");
                EndContext();
                BeginContext(5999, 149, false);
#line 119 "C:\work\Portal PMO\PortalPMO\Views\DetailProject\AssignProject.cshtml"
                                   Write(Html.TextAreaFor(m => m.Keterangan, new { @class = "form-control", @value = "", @autocomplete = "off", @rows = "4",@placeholder="Masukkan Catatan" }));

#line default
#line hidden
                EndContext();
                BeginContext(6148, 146, true);
                WriteLiteral("\r\n                                    </div>\r\n                                </div>\r\n                            </div>\r\n                        ");
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
            BeginContext(6301, 406, true);
            WriteLiteral(@"
                    </div>
                    <div class=""modal-footer"">
                        <a href=""javascript:;"" class=""btn btn-sm btn-white"" onclick=""BackDraft()"">Tutup</a>
                        <a href=""javascript:;"" class=""btn btn-sm btn-primary"" id=""BtnSubmit"">Simpan</a>
                    </div>
                </div>
            </div>
        </div>
    </div>

</div>


");
            EndContext();
            DefineSection("scripts", async() => {
                BeginContext(6724, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(6730, 63, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3af12e39b2788b20b81da9c765219374ed6a19df18023", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(6793, 2, true);
                WriteLiteral("\r\n");
                EndContext();
            }
            );
            BeginContext(6798, 2, true);
            WriteLiteral("\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PortalPMO.Models.dbPortalPMO.TblMasterSistem> Html { get; private set; }
    }
}
#pragma warning restore 1591

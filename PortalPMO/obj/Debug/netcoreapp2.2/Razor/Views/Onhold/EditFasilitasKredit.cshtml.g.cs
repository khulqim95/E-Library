#pragma checksum "C:\work\Portal PMO\PortalPMO\Views\Onhold\EditFasilitasKredit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8a8180ec1502855d6598b79414460a659733670b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Onhold_EditFasilitasKredit), @"mvc.1.0.view", @"/Views/Onhold/EditFasilitasKredit.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Onhold/EditFasilitasKredit.cshtml", typeof(AspNetCore.Views_Onhold_EditFasilitasKredit))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8a8180ec1502855d6598b79414460a659733670b", @"/Views/Onhold/EditFasilitasKredit.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6ba151424cb7e4ff7d935c55cfcaa12c04b783fe", @"/Views/_ViewImports.cshtml")]
    public class Views_Onhold_EditFasilitasKredit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PortalPMO.ViewModels.TblFasilitasKredit_ViewModels>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("FormData"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("padding-top:20px;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/plugin/select2/dist/css/select2.min.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/plugin/daterangepicker/daterangepicker.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/plugin/javascript-fluid-meter-master/js/js-fluid-meter.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/plugin/select2/dist/js/select2.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/plugin/daterangepicker/moment.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/plugin/daterangepicker/daterangepicker.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(59, 357, true);
            WriteLiteral(@"<style>
    .modal-dialog {
        min-width: 900px;
    }

    .select2 {
        width: 100% !important;
    }
</style>
<div class=""modal-header"">
    <h4 class=""modal-title"">Detail Fasilitas Kredit</h4>
    <button type=""button"" class=""close"" data-dismiss=""modal"" aria-hidden=""true"">×</button>
</div>
<div class=""modal-body"" id=""id-body"">
");
            EndContext();
            BeginContext(461, 4, true);
            WriteLiteral("    ");
            EndContext();
            BeginContext(465, 2172, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8a8180ec1502855d6598b79414460a659733670b7378", async() => {
                BeginContext(511, 15, true);
                WriteLiteral("\r\n        <div>");
                EndContext();
                BeginContext(527, 25, false);
#line 18 "C:\work\Portal PMO\PortalPMO\Views\Onhold\EditFasilitasKredit.cshtml"
        Write(Html.HiddenFor(m => m.Id));

#line default
#line hidden
                EndContext();
                BeginContext(552, 228, true);
                WriteLiteral("</div>\r\n        <div class=\"form-group row m-b-15\">\r\n            <label class=\"col-md-3 col-form-label\">Jenis Fasilitas</label>\r\n            <div class=\"col-md-9\">\r\n                <div class=\"input-group\">\r\n                    ");
                EndContext();
                BeginContext(781, 125, false);
#line 23 "C:\work\Portal PMO\PortalPMO\Views\Onhold\EditFasilitasKredit.cshtml"
               Write(Html.TextBoxFor(m => m.JenisFasilitas, new { @readOnly = true, @class = "form-control", @value = "", @autocomplete = "off" }));

#line default
#line hidden
                EndContext();
                BeginContext(906, 253, true);
                WriteLiteral("\r\n                </div>\r\n            </div>\r\n        </div>\r\n        <div class=\"form-group row m-b-15\">\r\n            <label class=\"col-md-3 col-form-label\">Valuta</label>\r\n            <div class=\"col-md-9\">\r\n                <div class=\"input-group\">\r\n");
                EndContext();
                BeginContext(1402, 20, true);
                WriteLiteral("                    ");
                EndContext();
                BeginContext(1423, 117, false);
#line 32 "C:\work\Portal PMO\PortalPMO\Views\Onhold\EditFasilitasKredit.cshtml"
               Write(Html.TextBoxFor(m => m.Valuta, new { @readOnly = true, @class = "form-control", @value = "", @autocomplete = "off" }));

#line default
#line hidden
                EndContext();
                BeginContext(1540, 282, true);
                WriteLiteral(@"
                </div>
            </div>
        </div>
        <div class=""form-group row m-b-15"">
            <label class=""col-md-3 col-form-label"">Maksimum Kredit</label>
            <div class=""col-md-9"">
                <div class=""input-group"">
                    ");
                EndContext();
                BeginContext(1823, 107, false);
#line 40 "C:\work\Portal PMO\PortalPMO\Views\Onhold\EditFasilitasKredit.cshtml"
               Write(Html.TextBoxFor(m => m.MaksimumKredit, new { @class = "form-control", @value = "", @autocomplete = "off" }));

#line default
#line hidden
                EndContext();
                BeginContext(1930, 253, true);
                WriteLiteral("\r\n                </div>\r\n            </div>\r\n        </div>\r\n        <div class=\"form-group row m-b-15\">\r\n            <label class=\"col-md-3 col-form-label\">Tujuan</label>\r\n            <div class=\"col-md-9\">\r\n                <div class=\"input-group\">\r\n");
                EndContext();
                BeginContext(2426, 20, true);
                WriteLiteral("                    ");
                EndContext();
                BeginContext(2447, 117, false);
#line 49 "C:\work\Portal PMO\PortalPMO\Views\Onhold\EditFasilitasKredit.cshtml"
               Write(Html.TextBoxFor(m => m.Tujuan, new { @readOnly = true, @class = "form-control", @value = "", @autocomplete = "off" }));

#line default
#line hidden
                EndContext();
                BeginContext(2564, 66, true);
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
            BeginContext(2637, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(2655, 189, true);
            WriteLiteral("    <a href=\"#\" class=\"mt-2 btn btn-success pull-right\" id=\"BtnSubmitFasilitas\">Simpan</a>\r\n</div>\r\n\r\n<style>\r\n    .sw > .tab-content {\r\n        height: auto !important\r\n    }\r\n</style>\r\n\r\n");
            EndContext();
            DefineSection("styles", async() => {
                BeginContext(2860, 11, true);
                WriteLiteral("\r\n    <link");
                EndContext();
                BeginWriteAttribute("href", " href=\"", 2871, "\"", 2950, 1);
#line 65 "C:\work\Portal PMO\PortalPMO\Views\Onhold\EditFasilitasKredit.cshtml"
WriteAttributeValue("", 2878, Url.Content("~/plugin/DataTables-1.10.20/media/css/datatables.min.css"), 2878, 72, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(2951, 31, true);
                WriteLiteral(" rel=\"stylesheet\" />\r\n    <link");
                EndContext();
                BeginWriteAttribute("href", " href=\"", 2982, "\"", 3072, 1);
#line 66 "C:\work\Portal PMO\PortalPMO\Views\Onhold\EditFasilitasKredit.cshtml"
WriteAttributeValue("", 2989, Url.Content("~/plugin/DataTables-1.10.20/media/css/responsive.dataTables.min.css"), 2989, 83, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(3073, 26, true);
                WriteLiteral(" rel=\"stylesheet\" />\r\n    ");
                EndContext();
                BeginContext(3099, 74, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "8a8180ec1502855d6598b79414460a659733670b14072", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(3173, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(3179, 77, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "8a8180ec1502855d6598b79414460a659733670b15407", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(3256, 2040, true);
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


        .s");
                WriteLiteral(@"elect2-container .select2-selection--single {
            height: 35px;
        }

        .select2-container--default .select2-selection--single .select2-selection__rendered {
            padding-top: 5px;
            color: #495057;
            /*border: 1px solid #ced4da;*/
        }


        select[name=Table_length] {
            height: 35px;
        }

        .select2-container--default .select2-selection--multiple .select2-selection__choice {
            background-color: white;
            border: 1px solid teal;
            border-radius: 4px;
            cursor: default;
            float: left;
            margin-right: 5px;
            margin-top: 5px;
            padding: 0 5px;
            height: 22px;
        }

        .odd {
            background-color: white !important;
        }

        .hoverParent:hover {
            background-color: white;
        }

        .hoverParent {
            background-color: #f1d2d2;
        }
    </style>
");
                EndContext();
            }
            );
            DefineSection("scripts", async() => {
                BeginContext(5316, 13, true);
                WriteLiteral("\r\n    <script");
                EndContext();
                BeginWriteAttribute("src", " src=\"", 5329, "\"", 5412, 1);
#line 145 "C:\work\Portal PMO\PortalPMO\Views\Onhold\EditFasilitasKredit.cshtml"
WriteAttributeValue("", 5335, Url.Content("~/plugin/DataTables-1.10.20/media/js/jquery.dataTables.min.js"), 5335, 77, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(5413, 23, true);
                WriteLiteral("></script>\r\n    <script");
                EndContext();
                BeginWriteAttribute("src", " src=\"", 5436, "\"", 5523, 1);
#line 146 "C:\work\Portal PMO\PortalPMO\Views\Onhold\EditFasilitasKredit.cshtml"
WriteAttributeValue("", 5442, Url.Content("~/plugin/DataTables-1.10.20/media/js/dataTables.responsive.min.js"), 5442, 81, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(5524, 16, true);
                WriteLiteral("></script>\r\n    ");
                EndContext();
                BeginContext(5540, 83, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8a8180ec1502855d6598b79414460a659733670b20011", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(5623, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(5629, 63, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8a8180ec1502855d6598b79414460a659733670b21267", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(5692, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(5698, 62, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8a8180ec1502855d6598b79414460a659733670b22523", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(5760, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(5766, 71, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8a8180ec1502855d6598b79414460a659733670b23779", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(5837, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(5844, 23, false);
#line 151 "C:\work\Portal PMO\PortalPMO\Views\Onhold\EditFasilitasKredit.cshtml"
Write(Html.Partial("_Script"));

#line default
#line hidden
                EndContext();
                BeginContext(5867, 2, true);
                WriteLiteral("\r\n");
                EndContext();
            }
            );
            BeginContext(5872, 4, true);
            WriteLiteral("\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PortalPMO.ViewModels.TblFasilitasKredit_ViewModels> Html { get; private set; }
    }
}
#pragma warning restore 1591

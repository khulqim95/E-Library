#pragma checksum "C:\work\E-Library\E-Library\PortalPMO\Views\PengaturanLookup\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "493e2c621612541d65f81277c96654c2fb4e9f33"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_PengaturanLookup_Index), @"mvc.1.0.view", @"/Views/PengaturanLookup/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/PengaturanLookup/Index.cshtml", typeof(AspNetCore.Views_PengaturanLookup_Index))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"493e2c621612541d65f81277c96654c2fb4e9f33", @"/Views/PengaturanLookup/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6ba151424cb7e4ff7d935c55cfcaa12c04b783fe", @"/Views/_ViewImports.cshtml")]
    public class Views_PengaturanLookup_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString(""), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "C:\work\E-Library\E-Library\PortalPMO\Views\PengaturanLookup\Index.cshtml"
  
    ViewData["Title"] = "Pengaturan Menu";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(100, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            DefineSection("styles", async() => {
                BeginContext(118, 11, true);
                WriteLiteral("\r\n    <link");
                EndContext();
                BeginWriteAttribute("href", " href=\"", 129, "\"", 208, 1);
#line 8 "C:\work\E-Library\E-Library\PortalPMO\Views\PengaturanLookup\Index.cshtml"
WriteAttributeValue("", 136, Url.Content("~/plugin/DataTables-1.10.20/media/css/datatables.min.css"), 136, 72, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(209, 31, true);
                WriteLiteral(" rel=\"stylesheet\" />\r\n    <link");
                EndContext();
                BeginWriteAttribute("href", " href=\"", 240, "\"", 330, 1);
#line 9 "C:\work\E-Library\E-Library\PortalPMO\Views\PengaturanLookup\Index.cshtml"
WriteAttributeValue("", 247, Url.Content("~/plugin/DataTables-1.10.20/media/css/responsive.dataTables.min.css"), 247, 83, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(331, 1209, true);
                WriteLiteral(@" rel=""stylesheet"" />
    <style>
        .dataTables_wrapper {
            zoom: 0.9 !important;
        }

        table.dataTable.dtr-inline.collapsed > tbody > tr[role=""row""] > td:first-child:before, table.dataTable.dtr-inline.collapsed > tbody > tr[role=""row""] > th:first-child:before {
            background-color: #3f6ad8;
        }

        .select2-container .select2-selection--single {
            height: 35px;
        }

        .select2-container--default .select2-selection--single .select2-selection__rendered {
            padding-top: 5px;
            color: #495057;
            /*border: 1px solid #ced4da;*/
        }

        .input-group {
            margin-bottom: 5px;
        }

        select[name=Table_length] {
            height: 35px;
        }

        .select2-container--default .select2-selection--multiple .select2-selection__choice {
            background-color: white;
            border: 1px solid teal;
            border-radius: 4px;
            cu");
                WriteLiteral("rsor: default;\r\n            float: left;\r\n            margin-right: 5px;\r\n            margin-top: 5px;\r\n            padding: 0 5px;\r\n            height: 22px;\r\n        }\r\n    </style>\r\n");
                EndContext();
            }
            );
            BeginContext(1543, 240, true);
            WriteLiteral("<div class=\"app-main__outer\">\r\n    <div class=\"app-main__inner\">\r\n        <div class=\"row\">\r\n            <h5><b>Pengaturan Lookup</b></h5>\r\n        </div>\r\n        <hr />\r\n\r\n        <div id=\"DivContentBody\">\r\n            <div class=\"row\">\r\n");
            EndContext();
            BeginContext(2726, 312, true);
            WriteLiteral(@"                <div class=""col-lg-12"">
                    <div class=""main-card mb-3 card"" id=""cardParameterSearch"">
                        <div class=""card-body"">
                            <p class=""LabelSearchParam"">Parameter Search</p>
                            <hr />
                            ");
            EndContext();
            BeginContext(3038, 1344, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "493e2c621612541d65f81277c96654c2fb4e9f337249", async() => {
                BeginContext(3053, 1322, true);
                WriteLiteral(@"
                                <div class=""form-row"">
                                    <div class=""col-md-6"">
                                        <div class=""position-relative form-group"">
                                            <label class="""">Tipe</label>
                                            <input  id=""txtTipeSearchParam"" type=""text"" class=""form-control"" placeholder=""Masukkan Tipe Lookup"">
                                        </div>
                                    </div>
                                    <div class=""col-md-6"">
                                        <div class=""position-relative form-group"">
                                            <label class="""">Nama</label>
                                            <input  id=""txtNamaSearchParam"" type=""text"" class=""form-control"" placeholder=""Masukkan Nama"">
                                        </div>
                                    </div>

                                </div>
                   ");
                WriteLiteral(@"             <a href=""javascript:void(0)"" class=""mt-2 btn btn-success pull-right"" style=""margin-left:10px;"" id=""BtnSearch"">Cari</a>
                                <a href=""javascript:void(0)"" class=""mt-2 btn btn-danger pull-right"" id=""BtnClearSearch"">Bersihkan</a>

                            ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(4382, 1682, true);
            WriteLiteral(@"
                        </div>
                    </div>
                </div>
            </div>

            <div class=""row"" style=""padding-top:20px;"">
                <div class=""col-lg-12"">
                    <div class=""main-card mb-3 card"">
                        <div class=""card-body"">
                            <a href=""javascript:void(0)"" class=""mt-2 btn btn-success"" style=""margin-left:10px;"" onclick=""Create()"">Tambah Data</a>
                            <table id=""Table"" class=""display nowrap"" style=""width:100%"">
                                <thead>
                                    <tr>
                                        <th style=""max-width:25px"">No</th>
                                        <th style=""max-width:13%"">Aksi</th>
                                        <th>Tipe</th>
                                        <th>Nama</th>
                                        <th>Nilai</th>
                                        <th>Urutan</th>
                 ");
            WriteLiteral(@"                       <th>Aktif?</th>
                                        <th>Dibuat Oleh</th>
                                        <th>Waktu Dibuat</th>
                                        <th>Diubah Oleh</th>
                                        <th>Waktu Diubah</th>
                                    </tr>
                                </thead>
                                <tbody></tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id=""DivFormBody"">
        </div>
    </div>
</div>




");
            EndContext();
            DefineSection("scripts", async() => {
                BeginContext(6081, 13, true);
                WriteLiteral("\r\n    <script");
                EndContext();
                BeginWriteAttribute("src", " src=\"", 6094, "\"", 6177, 1);
#line 138 "C:\work\E-Library\E-Library\PortalPMO\Views\PengaturanLookup\Index.cshtml"
WriteAttributeValue("", 6100, Url.Content("~/plugin/DataTables-1.10.20/media/js/jquery.dataTables.min.js"), 6100, 77, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(6178, 23, true);
                WriteLiteral("></script>\r\n    <script");
                EndContext();
                BeginWriteAttribute("src", " src=\"", 6201, "\"", 6288, 1);
#line 139 "C:\work\E-Library\E-Library\PortalPMO\Views\PengaturanLookup\Index.cshtml"
WriteAttributeValue("", 6207, Url.Content("~/plugin/DataTables-1.10.20/media/js/dataTables.responsive.min.js"), 6207, 81, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(6289, 16, true);
                WriteLiteral("></script>\r\n    ");
                EndContext();
                BeginContext(6306, 23, false);
#line 140 "C:\work\E-Library\E-Library\PortalPMO\Views\PengaturanLookup\Index.cshtml"
Write(Html.Partial("_Script"));

#line default
#line hidden
                EndContext();
                BeginContext(6329, 4, true);
                WriteLiteral("\r\n\r\n");
                EndContext();
            }
            );
            BeginContext(6336, 2, true);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591

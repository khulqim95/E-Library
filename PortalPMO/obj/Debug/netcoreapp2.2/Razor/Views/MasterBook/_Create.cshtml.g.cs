#pragma checksum "C:\work\Portal PMO\PortalPMO\Views\MasterBook\_Create.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "540bd457dbf5c5d6f1cf47c1008dd58e84431106"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_MasterBook__Create), @"mvc.1.0.view", @"/Views/MasterBook/_Create.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/MasterBook/_Create.cshtml", typeof(AspNetCore.Views_MasterBook__Create))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"540bd457dbf5c5d6f1cf47c1008dd58e84431106", @"/Views/MasterBook/_Create.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6ba151424cb7e4ff7d935c55cfcaa12c04b783fe", @"/Views/_ViewImports.cshtml")]
    public class Views_MasterBook__Create : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PortalPMO.Models.dbPortalPMO.TblMasterBook>
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
            BeginContext(51, 172, true);
            WriteLiteral("\r\n<div class=\"row\" style=\"padding-top:20px;\">\r\n    <div class=\"col-lg-12\">\r\n        <div class=\"main-card mb-3 card\">\r\n            <div class=\"card-body\">\r\n                ");
            EndContext();
            BeginContext(223, 4400, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "540bd457dbf5c5d6f1cf47c1008dd58e844311064225", async() => {
                BeginContext(269, 22, true);
                WriteLiteral("\r\n                    ");
                EndContext();
                BeginContext(292, 23, false);
#line 8 "C:\work\Portal PMO\PortalPMO\Views\MasterBook\_Create.cshtml"
               Write(Html.AntiForgeryToken());

#line default
#line hidden
                EndContext();
                BeginContext(315, 2, true);
                WriteLiteral("\r\n");
                EndContext();
                BeginContext(369, 307, true);
                WriteLiteral(@"                    <div class=""form-group row m-b-15"">
                        <label class=""col-md-3 col-form-label"">Name <span class=""labelMandatory"">*</span></label>
                        <div class=""col-md-9"">
                            <div class=""input-group"">
                                ");
                EndContext();
                BeginContext(677, 97, false);
#line 14 "C:\work\Portal PMO\PortalPMO\Views\MasterBook\_Create.cshtml"
                           Write(Html.TextBoxFor(m => m.Name, new { @class = "form-control", @value = "", @autocomplete = "off" }));

#line default
#line hidden
                EndContext();
                BeginContext(774, 407, true);
                WriteLiteral(@"
                            </div>
                        </div>
                    </div>
                    <div class=""form-group row m-b-15"">
                        <label class=""col-md-3 col-form-label"">Author <span class=""labelMandatory"">*</span></label>
                        <div class=""col-md-9"">
                            <div class=""input-group"">
                                ");
                EndContext();
                BeginContext(1182, 99, false);
#line 22 "C:\work\Portal PMO\PortalPMO\Views\MasterBook\_Create.cshtml"
                           Write(Html.TextBoxFor(m => m.Author, new { @class = "form-control", @value = "", @autocomplete = "off" }));

#line default
#line hidden
                EndContext();
                BeginContext(1281, 413, true);
                WriteLiteral(@"
                            </div>
                        </div>
                    </div>
                    <div class=""form-group row m-b-15"">
                        <label class=""col-md-3 col-form-label"">Release Date <span class=""labelMandatory"">*</span></label>
                        <div class=""col-md-9"">
                            <div class=""input-group"">
                                ");
                EndContext();
                BeginContext(1695, 121, false);
#line 30 "C:\work\Portal PMO\PortalPMO\Views\MasterBook\_Create.cshtml"
                           Write(Html.TextBoxFor(m => m.RealeaseDate, new { @class = "form-control", @value = "", @autocomplete = "off", @type = "date" }));

#line default
#line hidden
                EndContext();
                BeginContext(1816, 412, true);
                WriteLiteral(@"
                            </div>
                        </div>
                    </div>
                    <div class=""form-group row m-b-15"">
                        <label class=""col-md-3 col-form-label"">Description <span class=""labelMandatory"">*</span></label>
                        <div class=""col-md-9"">
                            <div class=""input-group"">
                                ");
                EndContext();
                BeginContext(2229, 117, false);
#line 38 "C:\work\Portal PMO\PortalPMO\Views\MasterBook\_Create.cshtml"
                           Write(Html.TextAreaFor(m => m.Description, new { @class = "form-control", @value = "", @autocomplete = "off", @rows = "7"}));

#line default
#line hidden
                EndContext();
                BeginContext(2346, 411, true);
                WriteLiteral(@"
                            </div>
                        </div>
                    </div>
                    <div class=""form-group row m-b-15"">
                        <label class=""col-md-3 col-form-label"">BestSeller <span class=""labelMandatory"">*</span></label>
                        <div class=""col-md-9"">
                            <div class=""input-group"">
                                ");
                EndContext();
                BeginContext(2758, 63, false);
#line 46 "C:\work\Portal PMO\PortalPMO\Views\MasterBook\_Create.cshtml"
                           Write(Html.RadioButtonFor(model => model.IsBestSeller, true, new { }));

#line default
#line hidden
                EndContext();
                BeginContext(2821, 75, true);
                WriteLiteral("&nbsp; Yes &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\r\n                                ");
                EndContext();
                BeginContext(2897, 64, false);
#line 47 "C:\work\Portal PMO\PortalPMO\Views\MasterBook\_Create.cshtml"
                           Write(Html.RadioButtonFor(model => model.IsBestSeller, false, new { }));

#line default
#line hidden
                EndContext();
                BeginContext(2961, 1655, true);
                WriteLiteral(@"&nbsp; No &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </div>
                        </div>
                    </div>
                    <div class=""form-group row m-b-15"">
                        <label class=""col-md-3 col-form-label"">Upload Picture <span class=""labelMandatory"">*</span></label>
                        <div class=""col-md-9"">
                            <div class=""input-group"">
                                <input type=""file"" class=""custom-file-input form-control"" style=""position:fixed!important"" id=""InputDokumenBook"" name=""file"" aria-describedby=""inputGroupFileAddon01"" onchange=""inputGroupFileBook()"">
                                <label class=""custom-file-label form-control"" style=""position:relative !important"" for=""InputDokumenBook"" id=""fileLabelBook"">Choose File</label>
                            </div>
                            <div id=""warning"">
                                <div class=""form-group"">
                                    <div style=""p");
                WriteLiteral(@"adding: 1px;"">
                                        <p class=""col-sm-12 col-form-label"" style=""color:red; "">*Besar File Maximum untuk Upload 2000 KB</p>
                                        <p class=""col-sm-12 col-form-label"" style=""color:red; "">*Jenis File yang di perbolehkan untuk Upload PNG/JPG</p>
                                        <p id=""error-file-text"" class=""col-sm-12 col-form-label"" style=""color:red; ""></p>
                                    </div>
                                </div>
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
            BeginContext(4623, 318, true);
            WriteLiteral(@"
            </div>
            <div class=""modal-footer"">
                <a href=""javascript:;"" class=""btn btn-sm btn-white"" onclick=""BackDraft()"">Tutup</a>
                <a href=""javascript:;"" class=""btn btn-sm btn-success"" id=""BtnSubmit"">Simpan</a>
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PortalPMO.Models.dbPortalPMO.TblMasterBook> Html { get; private set; }
    }
}
#pragma warning restore 1591

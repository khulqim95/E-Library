#pragma checksum "C:\work\Portal PMO\PortalPMO\Views\TaskComplete\_UpdateProgress.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d567651bdf5be6392e7eefd2d377408375d587c2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_TaskComplete__UpdateProgress), @"mvc.1.0.view", @"/Views/TaskComplete/_UpdateProgress.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/TaskComplete/_UpdateProgress.cshtml", typeof(AspNetCore.Views_TaskComplete__UpdateProgress))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d567651bdf5be6392e7eefd2d377408375d587c2", @"/Views/TaskComplete/_UpdateProgress.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6ba151424cb7e4ff7d935c55cfcaa12c04b783fe", @"/Views/_ViewImports.cshtml")]
    public class Views_TaskComplete__UpdateProgress : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PortalPMO.Models.dbPortalPMO.TblProjectMember>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(54, 881, true);
            WriteLiteral(@"

<div class=""row"" style=""padding-top:20px;"">
    <div class=""col-lg-12"">
        <div class=""main-card mb-3 card"">
            <div class=""card-body"">
                <br />
                <h5><b>Detail Progres Pekerjaan</b></h5>
                <hr/>
                <table id=""TableProgress"" class=""display nowrap"" style=""width:98%"">
                    <thead>
                        <tr>
                            <th style=""max-width:25px"">No</th>
                            <th style=""max-width:25px"">Aksi</th>
                            <th>Judul</th>
                            <th>Deskripsi</th>
                            <th style=""max-width:20%"">Tanggal</th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
            </div>
            <div class=""modal-footer"">
");
            EndContext();
            BeginContext(1040, 160, true);
            WriteLiteral("                <a href=\"javascript:;\" class=\"btn btn-sm btn-white\" onclick=\"ReloadTable()\">Tutup </a>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PortalPMO.Models.dbPortalPMO.TblProjectMember> Html { get; private set; }
    }
}
#pragma warning restore 1591
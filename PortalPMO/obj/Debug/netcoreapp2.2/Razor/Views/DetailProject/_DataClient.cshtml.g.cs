#pragma checksum "C:\work\E-Library\E-Library\PortalPMO\Views\DetailProject\_DataClient.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3b86de01b7ff7e46fe2a63fa499232474df1a2ef"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_DetailProject__DataClient), @"mvc.1.0.view", @"/Views/DetailProject/_DataClient.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/DetailProject/_DataClient.cshtml", typeof(AspNetCore.Views_DetailProject__DataClient))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3b86de01b7ff7e46fe2a63fa499232474df1a2ef", @"/Views/DetailProject/_DataClient.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6ba151424cb7e4ff7d935c55cfcaa12c04b783fe", @"/Views/_ViewImports.cshtml")]
    public class Views_DetailProject__DataClient : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PortalPMO.Models.dbPortalPMO.TblMasterSkorProject>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(58, 874, true);
            WriteLiteral(@"
<div class=""row"">
    <div class=""col-lg-12"">
        <a href=""javascript:void(0)"" class=""mt-2 btn btn-success"" style=""margin-left:10px;"" onclick=""CreateClient()"">Tambah Data</a>
        <table id=""TableClient"" class=""display nowrap"" style=""width:100%"">
            <thead>
                <tr>
                    <th style=""max-width:25px"">No</th>
                    <th style=""max-width:13%"">Aksi</th>
                    <th>Client</th>
                    <th>Npp PIC</th>
                    <th>Nama PIC</th>
                    <th>Email PIC</th>
                    <th>Telepon PIC</th>
                    <th>Keterangan</th>
                </tr>
            </thead>
            <tbody></tbody>
        </table>
    </div>
</div>
<div class=""modal-footer"">
    <a href=""../DataProject/Index"" class=""btn btn-sm btn-white"">Tutup</a>
</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PortalPMO.Models.dbPortalPMO.TblMasterSkorProject> Html { get; private set; }
    }
}
#pragma warning restore 1591

#pragma checksum "C:\work\Portal PMO\PortalPMO\Views\DetailProject\_ModalUpdateProgress.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e65281487ac7151a218b6da3db0c5eb9de61cc1d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_DetailProject__ModalUpdateProgress), @"mvc.1.0.view", @"/Views/DetailProject/_ModalUpdateProgress.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/DetailProject/_ModalUpdateProgress.cshtml", typeof(AspNetCore.Views_DetailProject__ModalUpdateProgress))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e65281487ac7151a218b6da3db0c5eb9de61cc1d", @"/Views/DetailProject/_ModalUpdateProgress.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6ba151424cb7e4ff7d935c55cfcaa12c04b783fe", @"/Views/_ViewImports.cshtml")]
    public class Views_DetailProject__ModalUpdateProgress : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PortalPMO.ViewModels.DataProjectNotes_ViewModels>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(57, 783, true);
            WriteLiteral(@"<style>
    .modal-dialog {
        min-width: 70%;
    }
</style>
<div class=""modal-header"">
    <h5 class=""modal-title"">Progress Pekerjaan</h5>
    <button type=""button"" class=""close"" data-dismiss=""modal"" aria-hidden=""true"">×</button>
</div>
<div class=""modal-body"" id=""id-body"">
    <table id=""TableProgress"" class=""display nowrap"" style=""width:100%"">
        <thead>
            <tr>
                <th style=""max-width:25px"">No</th>
                <th style=""max-width:10%"">Tanggal</th>
                <th>Judul</th>
                <th>Deskripsi</th>
            </tr>
        </thead>
        <tbody></tbody>
    </table>
</div>
<div class=""modal-footer"">
    <a href=""javascript:;"" class=""btn btn-sm btn-white"" data-dismiss=""modal"">Tutup</a>
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PortalPMO.ViewModels.DataProjectNotes_ViewModels> Html { get; private set; }
    }
}
#pragma warning restore 1591
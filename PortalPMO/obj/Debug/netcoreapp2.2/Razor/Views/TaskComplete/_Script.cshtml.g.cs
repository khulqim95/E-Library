#pragma checksum "C:\work\E-Library\E-Library\PortalPMO\Views\TaskComplete\_Script.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ccef923f934161df343e092423c79a2670bd3b1a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_TaskComplete__Script), @"mvc.1.0.view", @"/Views/TaskComplete/_Script.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/TaskComplete/_Script.cshtml", typeof(AspNetCore.Views_TaskComplete__Script))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ccef923f934161df343e092423c79a2670bd3b1a", @"/Views/TaskComplete/_Script.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6ba151424cb7e4ff7d935c55cfcaa12c04b783fe", @"/Views/_ViewImports.cshtml")]
    public class Views_TaskComplete__Script : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 21565, true);
            WriteLiteral(@"<script type=""text/javascript"">
    var State = '';
    var TableProgressKerja;
    var ProjectMemberId;

    $(document).ready(function () {
        $.fn.datepicker.language['en'] = {
            days: ['Minggu', 'Senin', 'Selasa', 'Rabu', 'Kamis', 'Jumat', 'Sabtu'],
            daysShort: ['Min', 'Sen', 'Sel', 'Rab', 'Kam', 'Jum', 'Sab'],
            daysMin: ['Mn', 'Sn', 'Sl', 'Rb', 'Km', 'Jm', 'Sa'],
            months: ['Januari', 'Februari', 'Maret', 'April', 'Mei', 'Juni', 'Juli', 'Agustus', 'September', 'Oktober', 'November', 'Desember'],
            monthsShort: ['Jan', 'Feb', 'Mar', 'Apr', 'Mei', 'Jun', 'Jul', 'Aug', 'Sep', 'Okt', 'Nov', 'Dec'],
            today: 'Today',
            clear: 'Clear',
            dateFormat: 'dd/mm/yyyy',
            firstDay: 0
        };

        ValidationForm();
        $(""input[name=IsActive][value=True]"").prop(""checked"", true);


        CreateContentTanggal(""TanggalMemo"");
        CreateContentTanggal(""TanggalKlarifikasi"");
        Crea");
            WriteLiteral(@"teContentTanggal(""TanggalDisposisi"");
        $('#Keterangan').summernote({
            placeholder: 'Masukkan Diskripsi Project',
            tabsize: 2,
            height: 300,
            toolbar: [
                ['style', ['style']],
                ['font', ['bold', 'underline', 'clear']],
                ['color', ['color']],
                ['para', ['ul', 'ol', 'paragraph']],
                ['table', ['table']],
                ['insert', ['link', 'picture', 'video']],
                ['view', ['fullscreen', 'codeview', 'help']]
            ]
        });
        LoadDatatable();
    });

    //Untuk Validasi Form data Pinjaman
    function ValidationForm() {
        //Untuk validasi form
        var $validator = $(""#FormData"").validate({
            ignore: [],
            rules: {
                Nama: {
                    required: true
                },
                OrderBy: {
                    required: true
                },
                Visible: {
  ");
            WriteLiteral(@"                  required: true
                },
            },
            messages: {

            },

            highlight: function (element) {
                $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
            },
            unhighlight: function (element) {
                $(element).closest('.form-group').removeClass('has-error').addClass('has-success');
            },
            errorElement: 'span',
            errorClass: 'help-block',
            errorPlacement: function (error, element) {
                if (element.is('textarea')) {
                    element.next().css('border', '1px solid red');
                    error.insertAfter(element.parent());
                } else if (element.is(':radio')) {
                    element.next().css('border', '1px solid red');
                    error.insertAfter(element.parent());
                }
                else if (element.parent('.input-group').length) {
               ");
            WriteLiteral(@"     error.insertAfter(element.parent());
                }
                else {
                    error.insertAfter(element);
                }
            }
        });
        $.validator.messages.required = ""Harap isi field ini terlebih dahulu!"";

        //check apakah form sudah valid atau belum
        $('#BtnSubmit').click(function () {
            var url = '';
            var $valid = $(""#FormData"").valid();
            if (!$valid) {
                //alert(""False"");
                $validator.focusInvalid();
                return false;
            } else {
                if (State == ""edit"") {
                    url = '../TaskComplete/SubmitEdit';
                }
                else {
                    url = '../TaskComplete/SubmitCreate';
                }
                //Cek session terlebih dahulu
                $.ajax({
                    type: 'GET',
                    url: '../Login/CekSession',
                    success: function (hasil) {
  ");
            WriteLiteral(@"                      if (hasil == true) {
                            $.ajax({
                                url: url,
                                type: 'POST',
                                data: $('#FormData').serialize(),
                                success: function (d) {
                                    //Tampilkan alert status
                                    if (d == """") {
                                        BackDraft();
                                        NotifikasiSukses(""Sukses"", ""Data berhasil disimpan!"");
                                        Table.draw();
                                    } else {
                                        NotifikasiError(""Error"", d)
                                    }
                                }
                            });
                        }
                        else {
                            window.location.href = ""../Login/Login?a=true"";
                        }
                    }
 ");
            WriteLiteral(@"               });
            }
        });
    }

    function LoadDatatable() {
        $.ajax({
            type: 'GET',
            url: '../Login/CekSession',
            success: function (hasil) {
                if (hasil == true) {
                    Table = $('#Table').DataTable({
                        fixedHeader: {
                            header: true
                        },
                        pageLength: 10,
                        destroy: true,
                        orderCellsTop: true,
                        ""scrollY"": ""270px"",
                        ""scrollCollapse"": true,
                        responsive: false,
                        scrollX: true,
                        processing: true, // untuk menampilkan bar prosessing
                        serverSide: true, // untuk proses server side datatable harus diset true
                        orderMulti: false, // untuk disable multi order column
                        retrieve: true,
     ");
            WriteLiteral(@"                   //autoWidth: true,
                        dom: '<""top""i>rt<""bottom""lp><""clear"">', // untuk menghilangkan search global
                        ajax: {
                            ""url"": '../TaskComplete/LoadData',
                            ""type"": ""POST"",
                            ""datatype"": ""json"",
                            error: function (jqXHR, textStatus, errorThrown) {
                                window.location.href = ""../Error/HttpStatusErrorLayout?statusCode=500""
                            }
                        },
                        language: {
                            processing: '<i class=""fa fa-spinner fa-spin fa-3x fa-fw""></i><span class=""sr-only"">Loading...</span> ',
                            info: ""Menampilkan _START_ Sampai _END_ Dari _TOTAL_ Data"",
                            paginate: {
                                first: ""Halaman Awal"",
                                last: ""Halaman Akhir"",
                                prev");
            WriteLiteral(@"ious: ""Halaman Sebelumnya"",
                                next: ""Halaman Selanjutnya"",
                            },
                            lengthMenu: ""Menampilkan _MENU_ Data"",
                            emptyTable: ""Data tidak ditemukan"",
                            infoEmpty: ""Menampilkan 0 Sampai 0 Dari 0 Data"",
                            zeroRecords: ""Data tidak ditemukan""
                        },
                        columns: [
                            { responsivePriority: 1, ""data"": ""Number"", ""name"": ""Number"" },
                            {
                                responsivePriority: 2,
                                ""data"": ""Id"", ""name"": ""Id"", ""orderable"": false, ""visible"": true, ""render"": function (data, type, full, meta) {
                                    return '<div class=""btn-group""><a href=""javascript:void(0)"" class=""btn btn-small btn-facebook"" id=""button-edit"" onclick=""DetailProject(' + full.ProjectId + ')"" style=""margin-right:5px;"" data-toggle=""too");
            WriteLiteral(@"ltip"" title=""Detail Project""><i class=""fa fa-edit btnEdit""></i></a>'
                                        + '<a href=""javascript:void(0)"" class=""btn btn-small btn-facebook"" id=""button-print"" onclick=""UpdateStatusProject(' + full.Id + ')"" data-toggle=""tooltip"" title=""Laporan Detail Pekerjaan""><i class=""fa fa-clipboard btnPrint""></i></a>'
                                        + '<a href=""javascript:void(0)"" class=""btn btn-small btn-facebook"" id=""button-print"" onclick=""PrintDataProject(' + full.ProjectId + ')"" data-toggle=""tooltip"" title=""Print Data""><i class=""fa fa-print btnPrint""></i></a></div>'
                                }
                            },
                            { responsivePriority: 5, ""data"": ""ProjectNo"", ""name"": ""ProjectNo"" },
                            { responsivePriority: 4, ""data"": ""NamaProject"", ""name"": ""NamaProject"" },
                            { responsivePriority: 3, ""data"": ""Periode"", ""name"": ""Periode"" },
                            { responsivePriority: 7, ");
            WriteLiteral(@"""data"": ""Selisih"", ""name"": ""Selisih"" },
                            { responsivePriority: 7, ""data"": ""TanggalPenyelesaian"", ""name"": ""TanggalPenyelesaian"" },
                            { responsivePriority: 7, ""data"": ""JumlahHariPengerjaan"", ""name"": ""JumlahHariPengerjaan"" },
                            //{ responsivePriority: 8, ""data"": ""StatusProject"", ""name"": ""StatusProject"" },
                            {
                                responsivePriority: 6, ""data"": ""StatusProject"", ""name"": ""StatusProject"",
                                ""render"": function (data, type, full, meta) {
                                    if (full.Warna == 'Kuning') {
                                        return '<p class=""mt-2 btn btn-block btn-warning disabled""><b>' + data + '</b></p>'
                                    }
                                    else if (full.Warna == 'Biru') {
                                        return '<p class=""mt-2 btn btn-block btn-info disabled""><b>' + data + '</b></p>'");
            WriteLiteral(@"
                                    }
                                    else if (full.Warna == 'Hijau') {
                                        return '<p class=""mt-2 btn btn-block btn-success disabled""><b>' + data + '</b></p>'
                                    }
                                    else if (full.Warna == 'Merah') {
                                        return '<p class=""mt-2 btn btn-block btn-danger disabled""><b>' + data + '</b></p>'
                                    }
                                    else {
                                        return '<p class=""mt-2 btn btn-block disabled""><b>' + data + '</b></p>'
                                    }
                                }
                            }
                        ],
                        ""order"": [[1, ""desc""]]
                    });

                    //--------------------------Function untuk melempar parameter search ---------------------//
                    //Untuk melempa");
            WriteLiteral(@"r parameter search
                    //oTable = $('#Table').DataTable();
                    $('#BtnSearch').click(function () {
                        Table.columns(2).search($('#txtNoProjectSearchParam').val().trim());
                        Table.columns(3).search($('#txtNamaSearchParam').val().trim());

                        //hit search ke server
                        Table.draw();
                    });


                    //---------------------Function untuk reset data pencarian----------------//
                    $('#BtnClearSearch').click(function () {
                        $('#txtNoProjectSearchParam').val("""");
                        $('#txtNamaSearchParam').val("""");

                        Table.columns(2).search($('#txtNoProjectSearchParam').val().trim());
                        Table.columns(3).search($('#txtNamaSearchParam').val().trim());
                        //hit search ke server
                        Table.draw();
                    });

       ");
            WriteLiteral(@"             Table.columns.adjust().responsive.recalc();
                }
                else {
                    window.location.href = ""../Login/Login?a=true"";
                }
            }
        });

        
    }

    function DetailProject(Id) {
        window.open('../DetailProject/View?ProjectId=' + Id, '_blank');
    }

    function UpdateStatusProject(Id) {
        ShowModal('bd-example-modal-lg');
        State = ""create"";
        UrlPartialView = '../TaskComplete/UpdateProgress';
        //Cek session masih aktif atau tidak
        $.ajax({
            type: 'GET',
            url: '../Login/CekSession',
            success: function (hasil) {
                if (hasil == true) {
                    LoadPartialViewData(UrlPartialView, function (data) {
                        //ShowModal('bd-example-modal-lg');
                        $('#DivContentBody').hide();
                        document.getElementById(""DivFormBody"").innerHTML = data;
                  ");
            WriteLiteral(@"      ProjectMemberId = Id;
                        LoadDataTableProgressKerja();
                        $(""input[name=IsActive][value=True]"").prop(""checked"", true);

                    });
                }
                else {
                    window.location.href = ""../Login/Login?a=true"";
                }
            }
        });
    }

     function LoadDataTableProgressKerja(data) {
        $.ajax({
            type: 'GET',
            url: '../Login/CekSession',
            success: function (hasil) {
                if (hasil == true) {
                    TableProgressKerja = $('#TableProgress').DataTable({
                        fixedHeader: {
                            header: true
                        },
                        pageLength: 10,
                        destroy: true,
                        orderCellsTop: true,
                        responsive: true,
                        processing: true, // untuk menampilkan bar prosessing
            ");
            WriteLiteral(@"            serverSide: true, // untuk proses server side datatable harus diset true
                        orderMulti: false, // untuk disable multi order column
                        retrieve: true,
                        //autoWidth: true,
                        dom: '<""top""i>rt<""bottom""lp><""clear"">', // untuk menghilangkan search global
                        //data: data,
                        ajax: {
                            ""url"": '../Utility/LoadDataProgressKerjaMember',
                            ""type"": ""POST"",
                            ""datatype"": ""json"",
                            ""data"": { ""ProjectMemberId"": ProjectMemberId },
                            error: function (jqXHR, textStatus, errorThrown) {
                                window.location.href = ""../Error/HttpStatusErrorLayout?statusCode=500""
                            }
                        },
                        language: {
                            processing: '<i class=""fa fa-spinner fa-sp");
            WriteLiteral(@"in fa-3x fa-fw""></i><span class=""sr-only"">Loading...</span> ',
                            info: ""Menampilkan _START_ Sampai _END_ Dari _TOTAL_ Data"",
                            paginate: {
                                first: ""Halaman Awal"",
                                last: ""Halaman Akhir"",
                                previous: ""Halaman Sebelumnya"",
                                next: ""Halaman Selanjutnya"",
                            },
                            lengthMenu: ""Menampilkan _MENU_ Data"",
                            emptyTable: ""Data tidak ditemukan"",
                            infoEmpty: ""Menampilkan 0 Sampai 0 Dari 0 Data"",
                            zeroRecords: ""Data tidak ditemukan""
                        },
                        columns: [
                            { responsivePriority: 1, ""data"": ""Number"", ""name"": ""Number"" },
                            {
                                responsivePriority: 2,
                                ""data"": ");
            WriteLiteral(@"""Id"", ""name"": ""Id"", ""orderable"": false, ""visible"": true, ""render"": function (data, type, full, meta) {
                                    return '<div class=""btn-group""><a href=""javascript:void(0)"" class=""btn btn-small btn-facebook"" id=""button-view"" onclick=""ViewProgress(' + full.Id + ')"" style=""margin-right:5px;"" data-toggle=""tooltip"" title=""View Data""><i class=""fa fa-eye btnView""></i></a>'
                                }
                            },
                            { responsivePriority: 3, ""data"": ""Judul"", ""name"": ""Judul"" },
                            { responsivePriority: 4, ""data"": ""Deskripsi"", ""name"": ""Deskripsi"" },
                            { responsivePriority: 5, ""data"": ""Tanggal"", ""name"": ""Tanggal"" }
                        ],
                        ""order"": [[1, ""desc""]]
                    });

                    //$.fn.dataTable.ext.errMode = 'throw';

                    //--------------------------Function untuk melempar parameter search ---------------------//");
            WriteLiteral(@"
                    //Untuk melempar parameter search
                    //oTable = $('#Table').DataTable();
                    $('#BtnSearch').click(function () {
                        Table.columns(2).search($('#txtTipeSearchParam').val().trim());
                        Table.columns(3).search($('#txtNamaSearchParam').val().trim());


                        //hit search ke server
                        Table.draw();
                    });


                    //---------------------Function untuk reset data pencarian----------------//
                    $('#BtnClearSearch').click(function () {
                        $('#txtTipeSearchParam').val("""");
                        $('#txtNamaSearchParam').val("""");

                        Table.columns(2).search($('#txtTipeSearchParam').val().trim());
                        Table.columns(3).search($('#txtNamaSearchParam').val().trim());
                        //hit search ke server
                        Table.draw();
          ");
            WriteLiteral(@"          });

                    Table.columns.adjust().responsive.recalc();
                }
                else {
                    window.location.href = ""../Login/Login?a=true"";
                }
            }
        });
    }

    function ReloadTable() {
        $('#DivContentBody').show();
        document.getElementById(""DivFormBody"").innerHTML = """";
        Table.draw();
    }
    
    function ViewProgress(Id) {
        ShowModal('bd-example-modal-lg');
        State = ""view"";
        UrlPartialView = '../TaskComplete/ViewUpdateProgressPekerjaan';
        //Cek session masih aktif atau tidak
        $.ajax({
            type: 'GET',
            url: '../Login/CekSession',
            success: function (hasil) {
                if (hasil == true) {
                    var searchData = { ""id"": Id }
                    LoadPartialViewData(UrlPartialView, function (data) {
                        //ShowModal('bd-example-modal-lg');
                        //StateTemp");
            WriteLiteral(@"Client = ""create"";
                        var modalbody = $('#LoadContent');
                        modalbody.html(data);
                        $('#ModalForm').modal({
                            backdrop: 'static',
                            keyboard: false
                        });
                        $(""#FormDataProgresPekerjaan :input"").prop(""disabled"", true);
                        $('#KeteranganSummernote').summernote({
                            placeholder: 'Masukkan Deskripsi Pekerjaan',
                            height: 200,
                            toolbar: [
                                ['style', ['style']],
                                ['font', ['bold', 'underline', 'clear']],
                                ['color', ['color']],
                                ['para', ['ul', 'ol', 'paragraph']],
                                ['table', ['table']],
                                ['insert', ['link', 'picture', 'video']],
                                ");
            WriteLiteral(@"['view', ['fullscreen', 'codeview', 'help']]
                            ]
                        });
                        $('#KeteranganSummernote').summernote('code', $(""#Deskripsi"").val());
                        $('#KeteranganSummernote').summernote('disable');
                        $(""#BtnSubmitDetailProgresPekerjaan"").hide();
                    }, searchData);
                }
                else {
                    window.location.href = ""../Login/Login?a=true"";
                }
            }
        });
    }

    function PrintDataProject(Id) {

        $.ajax({
            type: 'GET',
            url: '../Login/CekSession',
            success: function (hasil) {
                if (hasil == true) {
                    var popup = window.open('../DetailProject/Print?ProjectId=' + Id, '_blank');
                    popupBlockerChecker.check(popup);
                }
                else {
                    window.location.href = ""../Account/Login?a=true"";
  ");
            WriteLiteral("              }\r\n            }\r\n        });\r\n    }\r\n</script>");
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

#pragma checksum "C:\work\E-Library\E-Library\PortalPMO\Views\DataProject\_Script.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3377ac7a72489fc2831fb6292aada7365427fbee"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_DataProject__Script), @"mvc.1.0.view", @"/Views/DataProject/_Script.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/DataProject/_Script.cshtml", typeof(AspNetCore.Views_DataProject__Script))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3377ac7a72489fc2831fb6292aada7365427fbee", @"/Views/DataProject/_Script.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6ba151424cb7e4ff7d935c55cfcaa12c04b783fe", @"/Views/_ViewImports.cshtml")]
    public class Views_DataProject__Script : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 20605, true);
            WriteLiteral(@"<script type=""text/javascript"">
    var State = '';
    var Table;
    var ProjectId;
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


        //CreateContentTanggal(""TanggalMemo"");
        //CreateContentTanggal(""TanggalKlarifikasi"");
        //CreateContentTangga");
            WriteLiteral(@"l(""TanggalDisposisi"");
        //CreateContentTanggalMonthYear(""tanggalDeadlineSearchParam"");
        //DropdownProjectStatusServerSide(""ProjectStatusId"", ""100"", false);

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
                    required: true");
            WriteLiteral(@"
                },
                OrderBy: {
                    required: true
                },
                Visible: {
                    required: true
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
                    err");
            WriteLiteral(@"or.insertAfter(element.parent());
                }
                else if (element.parent('.input-group').length) {
                    error.insertAfter(element.parent());
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
                url = '../DataProject/SubmitEdit';
                //Cek session terlebih dahulu
                $.ajax({
                    type: 'GET',
                    url: '../Login/CekSession',
                    success: function (hasil) {
                        if (hasi");
            WriteLiteral(@"l == true) {
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
                });
          ");
            WriteLiteral(@"  }
        });
    }

    function LoadDatatable() {
        if (Table != undefined) {
            Table.DataTable.destroy();
        }

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
                        responsive: false,
                        scrollX:true,
                        processing: true, // untuk menampilkan bar prosessing
                        serverSide: true, // untuk proses server side datatable harus diset true
                        orderMulti: false, // untuk disable multi order column
                        retrieve: true,
                        //autoWidth: tru");
            WriteLiteral(@"e,
                        dom: '<""top""i>rt<""bottom""lp><""clear"">', // untuk menghilangkan search global
                        //data: dataUpcomingProject,
                        ajax: {
                            ""url"": '../DataProject/LoadData',
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
                  ");
            WriteLiteral(@"              previous: ""Halaman Sebelumnya"",
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
                                    return '<div class=""btn-group""><a href=""javascript:void(0)"" class=""btn btn-small btn-facebook"" id=""button-edit"" onclick=""DetailProject(' + full.Id + ')"" style=""margin-right:5px;"" data-");
            WriteLiteral(@"toggle=""tooltip"" title=""Detail Project""><i class=""fa fa-edit btnEdit""></i></a>'
                                        + '<a href=""javascript:void(0)"" class=""btn btn-small btn-facebook"" id=""button-assignment"" onclick=""UpdateStatusProject(' + full.Id + ')"" style=""margin-right:5px;"" data-toggle=""tooltip"" title=""Update Project""><i class=""fa fa-paper-plane ""></i></a>'
                                        + '<a href=""javascript:void(0)"" class=""btn btn-small btn-facebook"" id=""button-print"" onclick=""PrintDataProject(' + full.Id + ')"" data-toggle=""tooltip"" title=""Print Data""><i class=""fa fa-print btnPrint""></i></a></div>'
                                }
                            },
                            { responsivePriority: 3, ""data"": ""NoProject"", ""name"": ""NoProject"" },
                            { responsivePriority: 4, ""data"": ""NamaProject"", ""name"": ""NamaProject"" },
                            { responsivePriority: 5, ""data"": ""KategoriProject"", ""name"": ""KategoriProject"" },
                  ");
            WriteLiteral(@"          { responsivePriority: 6, ""data"": ""SubKategoriProject"", ""name"": ""SubKategoriProject"" },
                            { responsivePriority: 7, ""data"": ""SkorProject"", ""name"": ""SkorProject"" },
                            { responsivePriority: 8, ""data"": ""KompleksitasProject"", ""name"": ""KompleksitasProject"" },
                            { responsivePriority: 8, ""data"": ""KlasifikasiProject"", ""name"": ""KlasifikasiProject"" },
                            { responsivePriority: 9, ""data"": ""Mandatory"", ""name"": ""Mandatory"" },
                            { responsivePriority: 10, ""data"": ""PeriodeProject"", ""name"": ""PeriodeProject"" },
                            { responsivePriority: 11, ""data"": ""ProjectStatus"", ""name"": ""ProjectStatus"" },
                            { responsivePriority: 12, ""data"": ""NoMemo"", ""name"": ""NoMemo"" },
                            { responsivePriority: 13, ""data"": ""TanggalMemo"", ""name"": ""TanggalMemo"" },

                            { responsivePriority: 14, ""data"": ""TanggalEstimasi");
            WriteLiteral(@"Project"", ""name"": ""TanggalEstimasiProject"" },
                            {
                                responsivePriority: 15, ""data"": ""Selisih"", ""name"": ""Selisih""
                                //""render"": function (data, type, full, meta) {
                                //    if (data > 10) {
                                //        return '<p class=""mt-2 btn btn-primary disabled"" style=""min-width:100px;""><b>' + data + ' Hari </b></p>'
                                //    }
                                //    else {
                                //        return '<p class=""mt-2 btn btn-danger disabled"" style=""min-width:100px;""><b>' + data + ' Hari </b></p>'
                                //    }
                                //}
                            },
                            { responsivePriority: 16, ""data"": ""TanggalSelesaiProject"", ""name"": ""TanggalSelesaiProject"" },
                            { responsivePriority: 17, ""data"": ""SLA"", ""name"": ""SLA"" },

           ");
            WriteLiteral(@"                 { responsivePriority: 18, ""data"": ""ProjectStatus"", ""name"": ""ProjectStatus"" },
                            {
                                responsivePriority: 19, ""data"": ""CloseOpenStatus"", ""name"": ""CloseOpenStatus"",
                                ""render"": function (data, type, full, meta) {
                                    if (full.Warna == 'Kuning') {
                                        return '<p class=""mt-2 btn btn-block btn-warning disabled""><b>' + data + '</b></p>'
                                    }
                                    else if (full.Warna == 'Biru') {
                                        return '<p class=""mt-2 btn btn-block btn-info disabled""><b>' + data + '</b></p>'
                                    }
                                    else if (full.Warna == 'Hijau') {
                                        return '<p class=""mt-2 btn btn-block btn-success disabled""><b>' + data + '</b></p>'
                                    }
          ");
            WriteLiteral(@"                          else if (full.Warna == 'Merah') {
                                        return '<p class=""mt-2 btn btn-block btn-danger disabled""><b>' + data + '</b></p>'
                                    }
                                    else {
                                        return '<p class=""mt-2 btn btn-block disabled""><b>' + data + '</b></p>'
                                    }
                                }
                            }
                        ],
                        //""rowCallback"": function (row, data, index) {
                        //    var selisih = parseFloat(data.Selisih)
                        //    if (selisih > 0) {
                        //        $('td', row).addClass('red');
                        //    }
                        //    $('td', row).removeClass('sorting_1')
                        //} 
                        ""order"": [[1, ""desc""]]
                    });

                    //------------------------");
            WriteLiteral(@"--Function untuk melempar parameter search ---------------------//
                    //Untuk melempar parameter search
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
                  ");
            WriteLiteral(@"      //hit search ke server
                        Table.draw();
                    });

                    //Table.columns.adjust().responsive.recalc();
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
        $.ajax({
            type: 'GET',
            url: '../Login/CekSession',
            success: function (hasil) {
                if (hasil == true) {
                    UrlPartialView = ""../DataProject/UpdateProject"";
                    SearchId = { ""ProjectId"": Id }
                    LoadPartialViewData(UrlPartialView, function (data) {
                        //ShowModal('bd-example-modal-lg');
                        ProjectId = Id;
                        $('#DivContentBody').hide();
                  ");
            WriteLiteral(@"      document.getElementById(""DivFormBody"").innerHTML = data;
                        DropdownProjectStatusServerSide(""ProjectStatusIdValue"", ""100"", false);
                        $('#CatatanUser').summernote({
                            placeholder: 'Masukkan Keterangan',
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
                        $(""#CatatanUser"").summernote(""code"", """");

                        ValidationFormUpdateProject();
             ");
            WriteLiteral(@"       }, SearchId);
                }
                else {
                    window.location.href = ""../Login/Login?a=true"";
                }
            }
        });
    }


    function ValidationFormUpdateProject() {
        //Untuk validasi form
        var $validator = $(""#FormData"").validate({
            ignore: [],
            rules: {
                ProjectStatusId: {
                    required: true
                },
                CatatanUser: {
                    required: true
                }
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
            erro");
            WriteLiteral(@"rPlacement: function (error, element) {
                if (element.is('textarea')) {
                    element.next().css('border', '1px solid red');
                    error.insertAfter(element.parent());
                } else if (element.is(':radio')) {
                    element.next().css('border', '1px solid red');
                    error.insertAfter(element.parent());
                }
                else if (element.parent('.input-group').length) {
                    error.insertAfter(element.parent());
                }
                else {
                    error.insertAfter(element);
                }
            }
        });
        $.validator.messages.required = ""Harap isi field ini terlebih dahulu!"";

        //check apakah form sudah valid atau belum
        $('#BtnSubmitUpdateProgress').click(function () {
            var Catatan = $('#CatatanUser').summernote('code');
            var url = '';
            var $valid = $(""#FormData"").valid();
            i");
            WriteLiteral(@"f (!$valid) {
                //alert(""False"");
                $validator.focusInvalid();
                return false;
            } else {
                url = '../DataProject/SubmitUpdateProject';

                //Cek session terlebih dahulu
                $.ajax({
                    type: 'GET',
                    url: '../Login/CekSession',
                    success: function (hasil) {
                        if (hasil == true) {
                            $.ajax({
                                url: url,
                                type: 'POST',
                                data: { ""CatatanUser"": Catatan, ""ProjectStatusIdValue"": $(""#ProjectStatusIdValue"").val(), ""ProjectId"": ProjectId, ""ProjectOpenCloseId"": $(""#ProjectOpenCloseId"").val() },
                                success: function (d) {
                                    //Tampilkan alert status
                                    if (d == """") {
                                        BackDraft();
       ");
            WriteLiteral(@"                                 NotifikasiSukses(""Sukses"", ""Data berhasil disimpan!"");
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
                });
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
         ");
            WriteLiteral("           window.location.href = \"../Account/Login?a=true\";\r\n                }\r\n            }\r\n        });\r\n    }\r\n</script>");
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

#pragma checksum "C:\work\E-Library\E-Library\PortalPMO\Views\DataKategoriProject\_Script.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b1c26b964a7d8030dc450c98c2313254e2682c5e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_DataKategoriProject__Script), @"mvc.1.0.view", @"/Views/DataKategoriProject/_Script.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/DataKategoriProject/_Script.cshtml", typeof(AspNetCore.Views_DataKategoriProject__Script))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b1c26b964a7d8030dc450c98c2313254e2682c5e", @"/Views/DataKategoriProject/_Script.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6ba151424cb7e4ff7d935c55cfcaa12c04b783fe", @"/Views/_ViewImports.cshtml")]
    public class Views_DataKategoriProject__Script : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 15381, true);
            WriteLiteral(@"<script type=""text/javascript"">
    var State = '';
    var Table;
    $(document).ready(function () {
        LoadDataTable();
    });

    function LoadDataTable(data) {
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
                        responsive: true,
                        processing: true, // untuk menampilkan bar prosessing
                        serverSide: true, // untuk proses server side datatable harus diset true
                        orderMulti: false, // untuk disable multi order column
  ");
            WriteLiteral(@"                      retrieve: true,
                        //autoWidth: true,
                        dom: '<""top""i>rt<""bottom""lp><""clear"">', // untuk menghilangkan search global
                        //data: data,
                        ajax: {
                            ""url"": '../DataKategoriProject/LoadData',
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
  ");
            WriteLiteral(@"                              last: ""Halaman Akhir"",
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
                                ""data"": ""Id"", ""name"": ""Id"", ""orderable"": false, ""visible"": true, ""render"": function (data, type, full, meta) {
                                    return '<div class=""btn-group""><a href=""javascript:void(0)"" class=""btn btn-small btn-facebook"" id=""button-edit"" ");
            WriteLiteral(@"onclick=""Edit(' + full.Id + ')"" style=""margin-right:5px;"" data-toggle=""tooltip"" title=""Ubah Data""><i class=""fa fa-edit btnEdit""></i></a>'
                                        + '<a href=""javascript:void(0)"" class=""btn btn-small btn-facebook"" id=""button-view"" onclick=""View(' + full.Id + ')"" style=""margin-right:5px;"" data-toggle=""tooltip"" title=""View Data""><i class=""fa fa-eye btnView""></i></a>'
                                        + '<a href=""javascript:void(0)"" class=""btn btn-small btn-facebook"" id=""button-delete"" onclick=""Delete(' + full.Id + ')"" style=""margin-right:5px;"" data-toggle=""tooltip"" title=""Delete Data""><i class=""fa fa-trash btnDelete""></i></a></div>'
                                }
                            },
                            { responsivePriority: 3, ""data"": ""Kode"", ""name"": ""Kode"" },
                            { responsivePriority: 4, ""data"": ""Nama"", ""name"": ""Nama"" },
                            { responsivePriority: 5, ""data"": ""Keterangan"", ""name"": ""Keterangan"" },
  ");
            WriteLiteral(@"                          { responsivePriority: 6, ""data"": ""Order_By"", ""name"": ""Order_By"" },
                            {
                                responsivePriority: 7, ""data"": ""Status"", ""name"": ""Status"",
                                ""render"": function (data, type, full, meta) {
                                    if (data == 'Aktif') {
                                        return '<p class=""mt-2 btn btn-primary disabled""><b>' + data + '</b></p>'
                                    }
                                    else {
                                        return '<p class=""mt-2 btn btn-danger disabled""><b>' + data + '</b></p>'
                                    }
                                }
                            },
                            { responsivePriority: 8, ""data"": ""CreatedTime"", ""name"": ""CreatedTime"", ""visible"": false },
                            { responsivePriority: 9, ""data"": ""CreatedBy"", ""name"": ""CreatedBy"", ""visible"": false },
              ");
            WriteLiteral(@"              { responsivePriority: 10, ""data"": ""UpdatedTime"", ""name"": ""UpdatedTime"", ""visible"": false },
                            { responsivePriority: 11, ""data"": ""UpdatedBy"", ""name"": ""UpdatedBy"", ""visible"": false }
                        ],
                        ""order"": [[1, ""desc""]]
                    });

                    //$.fn.dataTable.ext.errMode = 'throw';

                    //--------------------------Function untuk melempar parameter search ---------------------//
                    //Untuk melempar parameter search
                    //oTable = $('#Table').DataTable();
                    $('#BtnSearch').click(function () {
                        Table.columns(2).search($('#txtKodeSearchParam').val().trim());
                        Table.columns(3).search($('#txtNamaSearchParam').val().trim());


                        //hit search ke server
                        Table.draw();
                    });


                    //---------------------Function un");
            WriteLiteral(@"tuk reset data pencarian----------------//
                    $('#BtnClearSearch').click(function () {
                        $('#txtKodeSearchParam').val("""");
                        $('#txtNamaSearchParam').val("""");

                        Table.columns(2).search($('#txtKodeSearchParam').val().trim());
                        Table.columns(3).search($('#txtNamaSearchParam').val().trim());
                        //hit search ke server
                        Table.draw();
                    });

                    Table.columns.adjust().responsive.recalc();
                }
                else {
                    window.location.href = ""../Login/Login?a=true"";
                }
            }
        });
    }


    function Create() {
        ShowModal('bd-example-modal-lg');
        State = ""create"";
        UrlPartialView = '../DataKategoriProject/Create';
        //Cek session masih aktif atau tidak
        $.ajax({
            type: 'GET',
            url: '../Login/");
            WriteLiteral(@"CekSession',
            success: function (hasil) {
                if (hasil == true) {
                    LoadPartialViewData(UrlPartialView, function (data) {
                        //ShowModal('bd-example-modal-lg');
                        $('#DivContentBody').hide();
                        document.getElementById(""DivFormBody"").innerHTML = data;

                        ValidationForm();
                        $(""input[name=IsActive][value=True]"").prop(""checked"", true);

                    });
                }
                else {
                    window.location.href = ""../Login/Login?a=true"";
                }
            }
        });
    }

    //--***************************************START EDIT ******************************************//
    function Edit(Id) {
        ShowModal('bd-example-modal-lg');

        $.ajax({
            type: 'GET',
            url: '../Login/CekSession',
            success: function (hasil) {
                if (hasil == true");
            WriteLiteral(@") {
                    //ShowModal('bd-example-modal-lg');

                    State = ""edit"";
                    SearchData = { ""Id"": Id }
                    UrlPartialView = '../DataKategoriProject/Edit';
                    LoadPartialViewData(UrlPartialView, function (data) {

                        $('#DivContentBody').hide();
                        document.getElementById(""DivFormBody"").innerHTML = data;

                       
                        ValidationForm();
                    }, SearchData);
                }
                else {
                    window.location.href = ""../Login/Login?a=true"";
                }
            }
        });

    }
    //--***************************************END EDIT ******************************************//

    //--***************************************START VIEW ******************************************//
    function View(Id) {
        ShowModal('bd-example-modal-lg');

        $.ajax({
            type: 'GET'");
            WriteLiteral(@",
            url: '../Login/CekSession',
            success: function (hasil) {
                if (hasil == true) {
                    //ShowModal('bd-example-modal-lg');

                    State = ""view"";
                    SearchData = { ""Id"": Id }
                    UrlPartialView = '../DataKategoriProject/View';
                    LoadPartialViewData(UrlPartialView, function (data) {
                        $('#DivContentBody').hide();
                        document.getElementById(""DivFormBody"").innerHTML = data;

                        $(""#FormData :input"").prop(""disabled"", true);

                    }, SearchData);
                }
                else {
                    window.location.href = ""../Login/Login?a=true"";
                }
            }
        });
    }
    //--***************************************END VIEW ******************************************//


    //Untuk Validasi Form data Pinjaman
    function ValidationForm() {
        //Untuk vali");
            WriteLiteral(@"dasi form
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
                    error.insert");
            WriteLiteral(@"After(element.parent());
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
        $('#BtnSubmit').click(function () {
            var url = '';
            var $valid = $(""#FormData"").valid();
            if (!$valid) {
                //alert(""False"");
                $validator.focusInvalid();
                return false;
            } else {
                if (State == ""edit"") {
                    url = '../DataKategoriProject/SubmitEdit';
                ");
            WriteLiteral(@"}
                else {
                    url = '../DataKategoriProject/SubmitCreate';
                }
                //Cek session terlebih dahulu
                $.ajax({
                    type: 'GET',
                    url: '../Login/CekSession',
                    success: function (hasil) {
                        if (hasil == true) {
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
                                        Noti");
            WriteLiteral(@"fikasiError(""Error"", d)
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

   
 
    //--***************************************START DELETE ******************************************//
    function Delete(Id) {
        $.ajax({
            type: 'GET',
            url: '../Login/CekSession',
            success: function (hasil) {
                if (hasil == true) {

                }
                else {
                    window.location.href = ""../Login/Login?a=true"";
                }
            }
        });


        listSelected = [];
        State = ""delete"";
           swal({
                title: ""Konfirmasi"",
                text: ""Apakah Anda yakin ingin menghapus data ini?");
            WriteLiteral(@""",
                icon: ""warning"",
                buttons: true,
                dangerMode: true,
            })
                .then((willDelete) => {
                    if (willDelete) {
                        $.ajax({
                            url: '../DataKategoriProject/Delete',
                            type: 'POST',
                            data: { ""Ids"": Id },
                            success: function (d) {
                                if (d == """") {
                                    NotifikasiSukses(""Sukses"", ""Data berhasil dihapus!"");
                                } else {
                                    NotifikasiError(""Error"", ""Data gagal dihapus!"")
                                }
                                Table.draw();
                            }
                        });
                    }
                });
        return false;
    }
    //--***************************************END DELETE ************************************");
            WriteLiteral("******//\r\n\r\n</script>");
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

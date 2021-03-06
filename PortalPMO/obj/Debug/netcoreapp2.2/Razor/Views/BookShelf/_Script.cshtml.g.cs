#pragma checksum "C:\work\E-Library\E-Library\PortalPMO\Views\BookShelf\_Script.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b4ae4c7bfac4370e21705d2f4d2d92c17db36023"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_BookShelf__Script), @"mvc.1.0.view", @"/Views/BookShelf/_Script.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/BookShelf/_Script.cshtml", typeof(AspNetCore.Views_BookShelf__Script))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b4ae4c7bfac4370e21705d2f4d2d92c17db36023", @"/Views/BookShelf/_Script.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6ba151424cb7e4ff7d935c55cfcaa12c04b783fe", @"/Views/_ViewImports.cshtml")]
    public class Views_BookShelf__Script : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 19122, true);
            WriteLiteral(@"<script type=""text/javascript"">

    var State = '';
    var Table;
    $(document).ready(function () {
        LoadDataTable();
    });

    function LoadDataTable(data) {
        debugger;
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
                        orderMulti: false, // untuk disable m");
            WriteLiteral(@"ulti order column
                        retrieve: true,
                        //autoWidth: true,
                        dom: '<""top""i>rt<""bottom""lp><""clear"">', // untuk menghilangkan search global
                        //data: data,
                        ajax: {
                            ""url"": '../BookShelf/LoadData',
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
                                first: ""Halaman");
            WriteLiteral(@" Awal"",
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
                            { responsivePriority: 1, ""data"": ""Number"", ""name"": ""Number"" , ""className"": ""text-center""},
                            {
                                responsivePriority: 2, ""data"": ""Id"", ""name"": ""Id"", ""className"": ""text-center"",
                                ""render"": function (data, type, full, meta) {
                                    return '<div class=""dropdown d-inline-block"">'
                                  ");
            WriteLiteral(@"      + '<button type=""button"" aria-haspopup=""true"" aria-expanded=""false"" data-toggle=""dropdown"" class=""mb-2 mr-2 dropdown-toggle btn btn-outline-primary"">Action</button>'
                                        + '<div class=""dropdown-menu"" style=""position: absolute; will-change: transform; top: 0px; left: 0px; transform: translate3d(-8px, 33px, 0px);"">'
                                        + '<button type=""button"" class=""dropdown-item"" onclick=""Edit(' + full.Id + ')"">Borrow Book</button>'
                                        + '<button type=""button"" class=""dropdown-item"" onclick=""View(' + full.Id + ')"">Book Detail</button>'
                                        //+ '<button type=""button"" class=""dropdown-item"" onclick=""Delete(' + full.Id + ')"">Hapus Data</button>'
                                        + '</div>'
                                        + '</div>'
                                }
                            },
                            { responsivePriority: 3, ""data"": ""Na");
            WriteLiteral(@"me"", ""name"": ""Name"" },
                            { responsivePriority: 4, ""data"": ""Author"", ""name"": ""Author"", ""className"": ""text-center"" },
                            { responsivePriority: 7, ""data"": ""RealeaseDate"", ""name"": ""RealeaseDate"", ""orderable"": false, ""className"": ""text-center""},
                            {
                                responsivePriority: 8, ""data"": ""Picture"", ""name"": ""Picture"", ""className"": ""text-center"",
                                ""render"": function (data, type, full, meta) {
                                    debugger;
                                    return '<img src=""'+full.Picture+'"" width=""200"" height=""300"">'
                                }
                            },
                            { responsivePriority: 9, ""data"": ""Description"", ""name"": ""Description"", ""className"": ""text-center""},
                            {
                                responsivePriority: 10, ""data"": ""IsBestSeller"", ""name"": ""IsBestSeller"", ""className"": ""text-");
            WriteLiteral(@"center"",
                                ""render"": function (data, type, full, meta) {
                                    debugger;
                                    if (data == true) {
                                        return '<center><p class=""mt-2 btn btn-block btn-primary disabled""><b> Yes </b></p></center>'
                                    }
                                    else {
                                        return '<center><p class=""mt-2 btn btn-block btn-danger disabled""><b> No </b></p></center>'
                                    }
                                }
                            },
                            {
                                responsivePriority: 11, ""data"": ""IsBorrowed"", ""name"": ""IsBorrowed"", ""className"": ""text-center"",
                                ""render"": function (data, type, full, meta) {
                                    debugger;
                                    if (data == true) {
                               ");
            WriteLiteral(@"         return '<center><p class=""mt-2 btn btn-block btn-primary disabled""><b> Yes </b></p></center>'
                                    }
                                    else {
                                        return '<center><p class=""mt-2 btn btn-block btn-danger disabled""><b> No </b></p></center>'
                                    }
                                }
                            }
                        ],
                        ""order"": [[1, ""desc""]]
                    });

                    //$.fn.dataTable.ext.errMode = 'throw';

                    //--------------------------Function untuk melempar parameter search ---------------------//
                    //Untuk melempar parameter search
                    //oTable = $('#Table').DataTable();
                    $('#BtnSearch').click(function () {
                        Table.columns(2).search($('#txtNamaSearchParam').val().trim());
                        Table.columns(3).search($('#txtKodeSe");
            WriteLiteral(@"archParam').val().trim());


                        //hit search ke server
                        Table.draw();
                    });


                    //---------------------Function untuk reset data pencarian----------------//
                    $('#BtnClearSearch').click(function () {
                        $('#txtNamaSearchParam').val("""");
                        $('#txtKodeSearchParam').val("""");

                        Table.columns(2).search($('#txtNamaSearchParam').val().trim());
                        Table.columns(3).search($('#txtKodeSearchParam').val().trim());
                        //hit search ke server
                        Table.draw();
                    });


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
        UrlPartialView = '../BookShel");
            WriteLiteral(@"f/Create';
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
            ty");
            WriteLiteral(@"pe: 'GET',
            url: '../Login/CekSession',
            success: function (hasil) {
                if (hasil == true) {
                    //ShowModal('bd-example-modal-lg');

                    State = ""edit"";
                    SearchData = { ""Id"": Id }
                    UrlPartialView = '../BookShelf/Edit';
                    LoadPartialViewData(UrlPartialView, function (data) {

                        $('#DivContentBody').hide();
                        document.getElementById(""DivFormBody"").innerHTML = data;

                        var today = new Date();
                        var dd = today.getDate();
                        var mm = today.getMonth() + 1; //January is 0 so need to add 1 to make it 1!
                        var yyyy = today.getFullYear();
                        if (dd < 10) {
                            dd = '0' + dd
                        }
                        if (mm < 10) {
                            mm = '0' + mm
                       ");
            WriteLiteral(@" }

                        today = yyyy + '-' + mm + '-' + dd;
                        document.getElementById(""BorrowDate"").setAttribute(""min"", today);
                        document.getElementById(""FinishDate"").setAttribute(""min"", today);
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
            type: 'GET',
            url: '../Login/CekSession',
            success: function (hasil) {
                if (hasil == true) {
                    //ShowModal('bd-example-modal-lg');

                    State ");
            WriteLiteral(@"= ""view"";
                    SearchData = { ""Id"": Id }
                    UrlPartialView = '../BookShelf/View';
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
        //Untuk validasi form
        var $validator = $(""#FormData"").validate({
            ignore: [],
            rules: {
                Name: {
                    required: true
                },
                Author: {");
            WriteLiteral(@"
                    required: true
                },
                ReleaseDate: {
                    required: true
                },
                Description: {
                    required: true
                },
                IsBestseller: {
                    required: true
                },
                BorrowDate: {
                    required: true
                },
                FinishDate: {
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
         ");
            WriteLiteral(@"       if (element.is('textarea')) {
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
        $('#BtnSubmit').click(function () {
            var url = '';         
            var Form = $('#FormData')[0];
            var Formfix = new FormData(Form);
            Formfix.append('BorrowDate', $(""#BorrowDate"").val());
            Formfix.append");
            WriteLiteral(@"('FinishDate', $(""#FinishDate"").val());

            var $valid = $(""#FormData"").valid();
            if (!$valid) {
                //alert(""False"");
                $validator.focusInvalid();
                return false;
            } else {
                if (State == ""edit"") {
                    url = '../BookShelf/SubmitBorrowed';
                }
                else {
                    url = '../BookShelf/SubmitCreate';
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
                                data: Formfix,
                                processData: false,
                                contentType: false,
                      ");
            WriteLiteral(@"          success: function (d) {
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
                            return false;
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
      ");
            WriteLiteral(@"      type: 'GET',
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
                text: ""Apakah Anda yakin ingin menghapus data ini?"",
                icon: ""warning"",
                buttons: true,
                dangerMode: true,
            })
                .then((willDelete) => {
                    if (willDelete) {
                        $.ajax({
                            url: '../BookShelf/Delete',
                            type: 'POST',
                            data: { ""Ids"": Id },
                            success: function (d) {
                                if (d == """") {
                                    NotifikasiSukses(""S");
            WriteLiteral(@"ukses"", ""Data berhasil dihapus!"");
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
    //--***************************************END DELETE ******************************************//

    function inputGroupFileBook() {
        debugger;
        if ($('#InputDokumenBook').val() != '' && $('#InputDokumenBook').val() != 'Choose file') {
            var filename = $('#InputDokumenBook').val();
            filename = filename.replace('C:\\fakepath\\', '')
            var error = false;
            $('#fileLabelBook').text(filename);

            if (filename.endsWith('.PNG') || filename.endsWith('.JPG') || filename.endsWith('.jpg') || filename.endsWith('.png')) {
                $('#warning').hide();
       ");
            WriteLiteral(@"         error = false;
            } else {
                $('#error-file-text').text('File bukan PNG/JPG.');
                $('#warning').show();
                error = true;
            }

            if (error) {
                //$('#inputFileUpload').css('color', 'gainsboro');
                $('#inputFileUploadBook').prop('disabled', true);
            } else {
                //$('#inputFileUpload').css('color', 'inherit');
                $('#inputFileUploadBook').prop('disabled', false);
            }
        }
    }

    function checkDate() {
        document.getElementById(""FinishDate"").setAttribute(""min"", $('#BorrowDate').val());
    }
</script>");
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

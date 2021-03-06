#pragma checksum "C:\work\E-Library\E-Library\PortalPMO\Views\MappingKewenangan\_Script.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "42c19cbe23ee356005fbcd65cdc2843d35b92564"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_MappingKewenangan__Script), @"mvc.1.0.view", @"/Views/MappingKewenangan/_Script.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/MappingKewenangan/_Script.cshtml", typeof(AspNetCore.Views_MappingKewenangan__Script))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"42c19cbe23ee356005fbcd65cdc2843d35b92564", @"/Views/MappingKewenangan/_Script.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6ba151424cb7e4ff7d935c55cfcaa12c04b783fe", @"/Views/_ViewImports.cshtml")]
    public class Views_MappingKewenangan__Script : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 19071, true);
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
                        responsive: true,
                        processing: true, // untuk menampilkan bar prosessing
                        serverSide: true, // untuk proses server side datatable harus diset true
                        orderMulti: false, // untuk disable multi order column
                        retrieve: true,
       ");
            WriteLiteral(@"                 //autoWidth: true,
                        dom: '<""top""i>rt<""bottom""lp><""clear"">', // untuk menghilangkan search global
                        //data: data,
                        ajax: {
                            ""url"": '../MappingKewenangan/LoadData',
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
                                last: ""Halaman Akh");
            WriteLiteral(@"ir"",
                                previous: ""Halaman Sebelumnya"",
                                next: ""Halaman Selanjutnya"",
                            },
                            lengthMenu: ""Menampilkan _MENU_ Data"",
                            emptyTable: ""Data tidak ditemukan"",
                            infoEmpty: ""Menampilkan 0 Sampai 0 Dari 0 Data"",
                            zeroRecords: ""Data tidak ditemukan""
                        },
                        columns: [
                            { responsivePriority: 1, ""data"": ""Number"", ""name"": ""Number"", ""className"": ""text-center"" },
                            {
                                responsivePriority: 2,
                                ""data"": ""Id"", ""name"": ""Id"", ""className"": ""text-center"", ""orderable"": false, ""visible"": true, ""render"": function (data, type, full, meta) {
                                    return '<div class=""dropdown d-inline-block"">'
                                        + '<button type=");
            WriteLiteral(@"""button"" aria-haspopup=""true"" aria-expanded=""false"" data-toggle=""dropdown"" class=""mb-2 mr-2 dropdown-toggle btn btn-outline-primary"">Action</button>'
                                        + '<div class=""dropdown-menu"" style=""position: absolute; will-change: transform; top: 0px; left: 0px; transform: translate3d(-8px, 33px, 0px);"">'
                                        + '<button type=""button"" class=""dropdown-item"" onclick=""Edit(' + full.Id + ',' + full.statusJabatanId + ',' + full.Unit_Id + ')"">Ubah Data</button>'
                                        + '<button type=""button"" class=""dropdown-item"" onclick=""View(' + full.Id + ',' + full.statusJabatanId + ',' + full.Unit_Id + ')"">Detail Data</button>'
                                        + '<button type=""button"" class=""dropdown-item"" onclick=""Delete(' + full.Id + ',' + full.StatusJabatanId + ',' + full.Unit_Id + ')"">Hapus Data</button>'

                                        + '</div>'
                                        + '</div>'
     ");
            WriteLiteral(@"                           }
                            },
                            { responsivePriority: 3, ""data"": ""JabatanName"", ""name"": ""JabatanName"", ""className"": ""text-center"" },
                            { responsivePriority: 4, ""data"": ""RoleName"", ""name"": ""RoleName"", ""className"": ""text-center"" },
                            //{ responsivePriority: 5, ""data"": ""UnitName"", ""name"": ""UnitName"", ""className"": ""text-center"" },
                            {
                                responsivePriority: 6, ""data"": ""IsActive"", ""name"": ""IsActive"", ""className"": ""text-center"",
                                ""render"": function (data, type, full, meta) {
                                    if (data == true) {
                                        return '<center><p class=""mt-2 btn btn-block btn-primary disabled""><b> Aktif </b></p></center>'
                                    }
                                    else {
                                        return '<center><p class=""mt-2 ");
            WriteLiteral(@"btn btn-block btn-danger disabled""><b>Tidak Aktif </b></p></center>'
                                    }
                                }
                            },
                            { responsivePriority: 7, ""data"": ""CreatedTime"", ""name"": ""CreatedTime"", ""visible"": false },
                            { responsivePriority: 8, ""data"": ""CreatedById"", ""name"": ""CreatedById"", ""visible"": false },
                            { responsivePriority: 10, ""data"": ""UpdatedTime"", ""name"": ""UpdatedTime"", ""visible"": false },
                            { responsivePriority: 11, ""data"": ""UpdatedById"", ""name"": ""UpdatedById"", ""visible"": false }
                        ],
                        ""order"": [[1, ""desc""]]
                    });

                    //$.fn.dataTable.ext.errMode = 'throw';

                    //--------------------------Function untuk melempar parameter search ---------------------//
                    //Untuk melempar parameter search
                    //oTable = $");
            WriteLiteral(@"('#Table').DataTable();
                    $('#BtnSearch').click(function () {
                        Table.columns(1).search($('#txtJabatanSearchParam').val().trim());
                        Table.columns(2).search($('#txtRoleSearchParam').val().trim());

                        //hit search ke server
                        Table.draw();
                    });


                    //---------------------Function untuk reset data pencarian----------------//
                    $('#BtnClearSearch').click(function () {
                        $('#txtJabatanSearchParam').val("""");
                        $('#txtRoleSearchParam').val("""");

                        Table.columns(1).search($('#txtJabatanSearchParam').val().trim());
                        Table.columns(2).search($('#txtRoleSearchParam').val().trim());
                        //hit search ke server
                        Table.draw();
                    });

                    Table.columns.adjust().responsive.recalc();
");
            WriteLiteral(@"                }
                else {
                    window.location.href = ""../Login/Login?a=true"";
                }
            }
        });
    }


    function Create() {
        ShowModal('bd-example-modal-lg');
        State = ""create"";
        UrlPartialView = '../MappingKewenangan/Create';
        //Cek session masih aktif atau tidak
        $.ajax({
            type: 'GET',
            url: '../Login/CekSession',
            success: function (hasil) {
                if (hasil == true) {
                    LoadPartialViewData(UrlPartialView, function (data) {
                        $('#DivContentBody').hide();
                        document.getElementById(""DivFormBody"").innerHTML = data;

                        $(""#Jabatan_Id"").select2({
                        });
                        $(""#Roles"").select2({
                            tags: true,
                            placeholder: '-Silahkan pilih-',
                            escapeMarkup: functi");
            WriteLiteral(@"on (markup) { return markup; }, // let our custom formatter work
                            minimumInputLength: 0
                        });
                        DropdownJabatanServerSide('Jabatan_Id', '100', false);
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
    function Edit(Jabatan_Id, statusJabatanId, unit_Id) {
        ShowModal('bd-example-modal-lg');

        $.ajax({
            type: 'GET',
            url: '../Login/CekSession',
            success: function (hasil) {
                if (hasil == true) {
                    //ShowModal('bd-example-modal-lg');

                    State = ""edit"";
      ");
            WriteLiteral(@"              SearchData = { ""id"": Jabatan_Id, ""statusJabatanId"": statusJabatanId, ""unit_Id"": unit_Id }
                    UrlPartialView = '../MappingKewenangan/Edit';
                    LoadPartialViewData(UrlPartialView, function (data) {
                        $('#DivContentBody').hide();
                        document.getElementById(""DivFormBody"").innerHTML = data;
                        debugger;
                        var dataRoles = $('#RolesValue').val();
                        SearchData = { ""Roles"": dataRoles }
                        UrlPartialView = ""../Utility/GetDropdownRolesByMenuId"";
                        LoadPartialViewData(UrlPartialView, function (data) {
                            debugger;
                            $('#Roles').val(null).trigger('change');
                            $.each(data, function (index, value) {
                                $('#Roles').append('<option value=""' + value.id + '"" selected>' + value.text + '</option>');
                 ");
            WriteLiteral(@"           });
                        }, SearchData);

                        //$(""#JabatanId"").select2({
                        //    tags: true
                        //});
                        $(""#Roles"").select2({
                            tags: true,
                            placeholder: '-Silahkan pilih-',
                            escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
                            minimumInputLength: 0
                        });
                        //DropdownJabatanServerSide('Jabatan_Id', '100', false)
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

    //--***************************************START VIEW ****");
            WriteLiteral(@"**************************************//
    function View(Jabatan_Id, statusJabatanId, unit_Id) {
        ShowModal('bd-example-modal-lg');

        $.ajax({
            type: 'GET',
            url: '../Login/CekSession',
            success: function (hasil) {
                if (hasil == true) {
                    //ShowModal('bd-example-modal-lg');

                    State = ""view"";
                    SearchData = { ""id"": Jabatan_Id }
                    UrlPartialView = '../MappingKewenangan/View';
                    LoadPartialViewData(UrlPartialView, function (data) {
                        debugger;
                        $('#DivContentBody').hide();
                        document.getElementById(""DivFormBody"").innerHTML = data;
                        var dataRoles = $('#RolesValue').val();
                        SearchData = { ""Roles"": dataRoles }
                        UrlPartialView = ""../Utility/GetDropdownRolesByMenuId"";
                        LoadPartialViewData");
            WriteLiteral(@"(UrlPartialView, function (data) {
                            $.each(data, function (index, value) {
                                $('#Roles').append('<option value=""' + value.id + '"" selected>' + value.text + '</option>');
                            });
                        }, SearchData);

                        $(""#JabatanId"").select2({
                            tags: true
                        });
                        $(""#Roles"").select2({
                            tags: true
                        });

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
        //Untuk validasi");
            WriteLiteral(@" form
        var $validator = $(""#FormData"").validate({
            ignore: [],
            rules: {
                JabatanId: {
                    required: true
                },
                RoleId: {
                    required: true
                },
                Keterangan: {
                    required: true
                },
                IsActive: {
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
            ");
            WriteLiteral(@"        element.next().css('border', '1px solid red');
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
            var $valid = $(""#FormData"").valid();
            if (!$valid) {
                //alert(""False"");
                $validator.focusInvalid();
                return false;
            } else {
                debugger;
     ");
            WriteLiteral(@"           var RoleId = $(""#Roles"").val().join();
                if (State == ""edit"") {
                    url = '../MappingKewenangan/SubmitEdit';
                }
                else {
                    url = '../MappingKewenangan/SubmitCreate';
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
                                data: $('#FormData').serialize() + ""&RolesId="" + RoleId,
                                //data: $('#FormData').serialize(),
                                success: function (d) {
                                    //Tampilkan alert status
                                    if (d == """") {
                                    ");
            WriteLiteral(@"    BackDraft();
                                        NotifikasiSukses(""Sukses"", ""Data berhasil disimpan!"");
                                        Table.draw();
                                    }
                                    else if (d == ""data already exist"") {
                                        NotifikasiError(""Error"", ""Data Sudah Pernah diinput sebelumnya, silahkan edit Role data tersebut"");
                                    }
                                    else {
                                        NotifikasiError(""Error"", d);
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



    //--***************************************ST");
            WriteLiteral(@"ART DELETE ******************************************//
    function Delete(Jabatan_Id, statusJabatanId, unit_Id) {
        debugger;
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
            text: ""Apakah Anda yakin ingin menghapus data ini?"",
            icon: ""warning"",
            buttons: true,
            dangerMode: true,
        })
            .then((willDelete) => {
                if (willDelete) {
                    $.ajax({
                        url: '../MappingKewenangan/Delete',
                        type: 'POST',
                        data: { ""Jabatan_Id"": Jabatan_Id, ""statusJabatanId"": statusJ");
            WriteLiteral(@"abatanId, ""unit_Id"": unit_Id },
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
    //--***************************************END DELETE ******************************************//

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

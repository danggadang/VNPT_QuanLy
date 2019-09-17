$(document).ready(function () {
    $.fn.modal.prototype.constructor.Constructor.DEFAULTS.backdrop = 'static';
    $.fn.modal.prototype.constructor.Constructor.DEFAULTS.keyboard = false;
    var table = $("#dtable").DataTable({
        ajax: {
            dataType: 'json',
            type: 'get',//server side only works well with type "POST" !!!
            url: '/HoaDon/LoadHoaDon',
            dataSrc: function (json) {
                for (var i = 0; i < json.data.length; i++) {
                    for (var j = 0; j < json.data1.length; j++) {
                        if (json.data1[j]["ID"] == json.data[i]["IDKhachHang"]) {
                            json.data[i]["khachHang"] = json.data1[j]["TenKH"];
                        }
                    }
                    for (var j = 0; j < json.data2.length; j++) {
                        if (json.data2[j]["ID"] == json.data[i]["IDDichVu"]) {
                            json.data[i]["dichVu"] = json.data2[j]["TenDV"];
                        }
                    }
                    for (var j = 0; j < json.data3.length; j++) {
                        if (json.data3[j]["ID"] == json.data[i]["IDNhanVien"]) {
                            json.data[i]["nhanVien"] = json.data3[j]["TenNV"];
                        }
                    }
                }
                return json.data;
            },
        },
        columns: [
            { data: 'khachHang' },
            { data: 'nhanVien' },
            { data: 'dichVu' },
            { data: 'SoLuong' },
            {
                data: 'TongTien', render: function (data) {
                    return `<p>` + formatNumber(data, '.', ',') + ` VNĐ</p>`;
                }
            },
            {
                'sortable': false,
                'searchable': false,
                'width':'22%',
                data: 'ID', render: function (data) {
                    return `
                                    <button class="btn btn-sm btn-warning" onclick="fnShowModal(${data})"> Sửa</button>
                                    <button class="btn btn-sm btn-danger" onclick="Delete(${data})"> Xóa</button>
                                    <button class="btn btn-sm btn-primary" onclick="ChiTiet(${data})"> Chi tiết</button> 
                                    <button class="btn btn-sm btn-primary" onclick="printData(${data})"> Print</button> `;
                }
            }

        ]
    });
})
function ChiTiet(id) {
    $.ajax({
        url: '/HoaDon/ChiTiet',
        data: { id: id },
        type: 'get',
        success: function (data) {
            console.log(data);
            $('#infoKhachHang').html(data.data["TenKH"]);
            $('#infoSoDienThoai').html(data.data["SDT"]);
            $('#infoDiaChi').html(data.data["DiaChi"]);
            $('#infoMail').html(data.data["Mail"]);
            $('#infoDichVu').html(data.data["TenDV"]);
            $('#infoSoLuong').html(data.data["SoLuong"]);
            $('#infoTongTien').html(data.data["TongTien"]);
            $('#infoNhanVien').html(data.data1);
            $('#ChiTietHoaDonModal').modal('show');
        }
    });
}
function formatNumber(nStr, decSeperate, groupSeperate) {
    nStr += '';
    x = nStr.split(decSeperate);
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + groupSeperate + '$2');
    }
    return x1 + x2;
}

function fnShowModal(id) {
    $.ajax({
        url: '/HoaDon/AddandEditHoaDonModal',
        data: { id: id },
        type: 'POST',
        success: function (data) {
            $('#containershow1').html(data);
            $('#AddandEditHoaDonModal').modal('show');
            var id = $('#iID').val();
            LoadThongTin(id);
        }
    });
}
function LoadThongTin(id) {
    $.ajax({
        url: '/HoaDon/LoadThongTin',
        type: 'get',
        data: { id: id },
        success: function (data) {
            html1 = "";
            html2 = "";
            idKhachHang = data.data["IDKhachHang"];
            idDichVu = data.data["IDDichVu"];
            idNhanVien = data.data["IDNhanVien"];
            $.each(data.data1, function (i, row) {
                if (row.ID == idKhachHang) {
                    html1 += `<option value="` + row.ID + `" class="oKhachHang" selected>` + row.TenKH + `</option>`;
                }
                else {
                    html1 += `<option value="` + row.ID + `" class="oKhachHang">` + row.TenKH + `</option>`;
                }
            })
            $.each(data.data2, function (i, row) {
                if (row.ID == idDichVu) {
                    html2 += `<option value="` + row.ID + `" class="oDichVu" id="`+row.SoTien+`" selected>` + row.TenDV + `</option>`;
                }
                else {
                    html2 += `<option value="` + row.ID + `" class="oDichVu">` + row.TenDV + `</option>`;
                }
            })
            $('#sKhachHang').html(html1);
            $('#sDichVu').html(html2);
        }
    });
}
function Save(id) {
    if (id == 0) {
        Create();
    }
    else {
        Edit();
    }
}

function Create() {
    idKhachHang = $("#sKhachHang option:selected").val();
    idDichVu = $("#sDichVu option:selected").val();
    var data = new FormData();
    data.append("SoLuong", $('#iSoLuong').val());
    data.append("idKhachHang", idKhachHang);
    data.append("idDichVu", idDichVu);
    $.ajax({
        url: '/HoaDon/AddHoaDon',
        type: 'POST',
        data: data,
        dataType: false,
        contentType: false,
        processData: false,
        success: function (data) {
            swal("Thêm khách hàng thành công!", "success");
            $('#dtable').DataTable().ajax.reload();
            $('#AddandEditHoaDonModal').modal('hide');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal("Oh no!!", thrownError, "error");
        }
    });
}
function Edit() {
    idKhachHang = $("#sKhachHang option:selected").val();
    idDichVu = $("#sDichVu option:selected").val();
    var data = new FormData();
    data.append("ID", $('#iID').val());
    data.append("SoLuong", $('#iSoLuong').val());
    data.append("idKhachHang", idKhachHang);
    data.append("idDichVu", idDichVu);
    $.ajax({
        url: '/HoaDon/EditHoaDon',
        type: 'POST',
        data: data,
        dataType: false,
        contentType: false,
        processData: false,
        success: function (data) {
            swal("Sửa khách hàng thành công!", "success");
            $('#dtable').DataTable().ajax.reload();
            $('#AddandEditHoaDonModal').modal('hide');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal("Oh no!!", thrownError, "error");
        }
    });
}
function Delete(id) {
    $.ajax({
        url: '/HoaDon/DeleteHoaDon',
        dataType: 'json',
        data: { id: id },
        type: 'POST',
        success: function (data) {
            swal("Yes!!", "Xóa thành công!", "success")
            $('#dtable').DataTable().ajax.reload();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal("Oh no!!", thrownError);
        }
    });
}

function SaveTrangThai() {
    idTrangThai = $("#select option:selected").val();
    $.ajax({
        url: '/HoaDon/LoadNhanVienGroup',
        type: 'GET',
        dataType: 'json',
        data: { idTrangThai: idTrangThai },
        success: function (data) {

            html = "";
            $.each(data.data, function (i, row) {
                html += "<option value=" + row.ID + ">" + row.Name + "</option>";
            });
            $('#iCartegory').html(html);
        }
    });

}
function LoadNhanVienGroup() {

    $.ajax({
        url: '/HoaDon/LoadNhanVienGroup',
        type: 'GET',
        success: function (data) {

            html = "";
            $.each(data.data, function (i, row) {
                html += "<option value=" + row.ID + ">" + row.Name + "</option>";
            });
            $('#iCartegory').html(html);
        }
    });
}
function printData(id) {
    $.ajax({
        url: '/HoaDon/ChiTiet',
        type: 'GET',
        data: {id:id},
        success: function (data) {
            $("#table").css("border", "1px solid black");
            $('.name').html(data.data["TenKH"]);
            $('#address').html(data.data["DiaChi"]);
            $('#phone').html(data.data["SDT"]);
            $('#mail').html(data.data["Mail"]);
            $('#service').html(data.data["TenDV"]);
            $('#cost').html(data.data["TongTien"]);
            $("#testPrint").css("visibility", "visible");
            var divToPrint = document.getElementById("testPrint");
            newWin = window.open("");
            newWin.document.write(divToPrint.outerHTML);
            newWin.print();
            newWin.close();
            $("#testPrint").css("visibility", "hidden");
        }
    });
}

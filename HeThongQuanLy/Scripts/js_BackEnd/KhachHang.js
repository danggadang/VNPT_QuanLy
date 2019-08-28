$(document).ready(function () {
    $.fn.modal.prototype.constructor.Constructor.DEFAULTS.backdrop = 'static';
    $.fn.modal.prototype.constructor.Constructor.DEFAULTS.keyboard = false;
        $("#dtable").DataTable({
            ajax: {
                dataType: 'json',
                type: 'get',//server side only works well with type "POST" !!!
                url: '/KhachHang/LoadKhachHang',
                dataSrc: function (json) {
                    for (var i = 0; i < json.data.length; i++) {
                        for (var j = 0; j < json.data1.length; j++) {
                            if (json.data1[j]["ID"] == json.data[i]["IDNhanVien"]) {
                                json.data[i]["nhanvien"] = json.data1[j]["TenNV"];
                            }
                        }
                    }
                    return json.data;
                },
            },
            columns: [
                { data: 'TenKH'},
                { data: 'SoDienThoai' },
                { data: 'Mail' },
                { data: 'DiaChi' },
                {data:'nhanvien'},
                //{
                    
                //    data:0,render: function (data) {
                //        return data.ID;
                //    }
                //},
                {
                    data: 'ID', render: function (data) {
                        return `
                                    <button class="btn btn-sm btn-primary" onclick="EditTrangThaiModal(${data})"> Trạng thái</button>
                                    <button class="btn btn-sm btn-warning" onclick="fnShowModal(${data})"> Sửa</button>
                                    <button class="btn btn-sm btn-danger" onclick="Delete(${data})"> Xóa</button>

                                `;
                    }
                }

            ]
        });
    //$.ajax({
    //    url: '/KhachHang/LoadKhachHang',
    //    type: 'get',
    //    success: function (data) {
    //        console.log(data.data1[0]["ID"]);
    //    }
    //});
})

function EditTrangThaiModal(id) {
    $.ajax({
        url: '/KhachHang/EditTrangThaiModal',
        dataType: 'json',
        data: { id: id },
        type: 'get',
        success: function (data) {
            console.log(data);
            var iddong = data.data[1];
            var idtrangthai = data.data[2];
            console.log(data);
            $('#iIDTrangThai').val(data.data[1]);
            var html = "";
            $.each(data.data[0], function (i, row) {
                if (row.ID == idtrangthai) {
                    html += `<option value="` + row.ID + `" class="oTrangThai" selected>` + row.TenTrangThai + `</option>`;
                }
                else {
                    html += `<option value="`+ row.ID + `" class="oTrangThai">` + row.TenTrangThai + `</option>`;
                }
                
            })
            $("#select").html(html);
            $('#EditTrangThaiModal').modal('show');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal("Oh no!!", thrownError, "error");
        }
    });
}

function fnShowModal(id) {
    $.ajax({
        url: '/KhachHang/AddandEditKhachHangModal',
        data: { id: id },
        type: 'POST',
        success: function (data) {
            $('#containershow1').html(data);
            $('#AddandEditKhachHangModal').modal('show');
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
    var data = new FormData();
    data.append("TenKhachHang", $('#iName').val());
    data.append("DiaChi", $('#iDiaChi').val());
    data.append("SoDienThoai", $('#iSoDienThoai').val());
    data.append("Mail", $('#iMail').val());
    debugger;
    $.ajax({
        url: '/KhachHang/AddKhachHang',
        type: 'POST',
        data: data,
        dataType: false,
        contentType: false,
        processData: false,
        success: function (data) {
            swal("Thêm khách hàng thành công!", "success");
            $('#dtable').DataTable().ajax.reload();
            $('#AddandEditKhachHangModal').modal('hide');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal("Oh no!!", thrownError, "error");
        }
    });
}
function Edit() {
    var data = new FormData();
    data.append("ID", $('#iID').val());
    data.append("TenKhachHang", $('#iName').val());
    data.append("DiaChi", $('#iDiaChi').val());
    data.append("SoDienThoai", $('#iSoDienThoai').val());
    data.append("Mail", $('#iMail').val());
    $.ajax({
        url: '/KhachHang/EditKhachHang',
        type: 'POST',
        data: data,
        dataType: false,
        contentType: false,
        processData: false,
        success: function (data) {
            swal("Sửa khách hàng thành công!", "success");
            $('#dtable').DataTable().ajax.reload();
            $('#AddandEditKhachHangModal').modal('hide');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal("Oh no!!", thrownError, "error");
        }
    });
}
function Delete(id) {
    $.ajax({
        url: '/KhachHang/DeleteKhachHang',
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
        url: '/KhachHang/LoadNhanVienGroup',
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
        url: '/KhachHang/LoadNhanVienGroup',
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


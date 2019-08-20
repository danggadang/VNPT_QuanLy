$(document).ready(function () {
    $.fn.modal.prototype.constructor.Constructor.DEFAULTS.backdrop = 'static';
    $.fn.modal.prototype.constructor.Constructor.DEFAULTS.keyboard = false;
    $("#dtable").DataTable({
        ajax: {
            dataType: 'json',
            type: 'get',//server side only works well with type "POST" !!!
            url: '/KhachHang/LoadKhachHang',
        },
        columns: [
            { data: 'TenKH'},

            { data: 'SoDienThoai' },
            { data: 'Mail' },
            { data: 'DiaChi' },
            { data: 'IDNhanVien' },
            {
                data: 'ID', render: function (data) {
                    return `

                                <button class="btn btn-sm btn-warning" onclick="fnShowModal(${data})"> Sửa</button>
                                <button class="btn btn-sm btn-danger" onclick="Delete(${data})"> Xóa</button>

                            `;
                }
            }

        ]
    });
});
function Save(id) {
    if (id == 0) {
        Create();
    }
    else {
        Edit();
    }
}
function Create() {
    var gioiTinh = $('input[name=rdGioiTinh]:checked', '#frmAddEdit').val();
    console.log(gioiTinh);
    var data = new FormData();
    var files = $("#iImage").get(0).files;
    data.append("Anh", files[0]);
    data.append("TenDangNhap", $('#iUserName').val());
    data.append("MatKhau", $('#iPassword').val());
    data.append("Ten", $('#iName').val());
    data.append("Mail", $('#iMail').val());
    data.append("GioiTinh", gioiTinh);
    debugger;
    $.ajax({
        url: '/KhachHang/AddKhachHang',
        type: 'POST',
        data: data,
        dataType: false,
        contentType: false,
        processData: false,
        success: function (data) {
            swal("Thêm quản lý thành công!", "success");
            $('#dtable').DataTable().ajax.reload();
            $('#AddandEditKhachHangModal').modal('hide');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal("Oh no!!", thrownError, "error");
        }
    });
}
function Edit() {
    var gioiTinh = $('input[name=rdGioiTinh]:checked', '#frmAddEdit').val();
    var data = new FormData();
    var files = $("#iImage").get(0).files;
    data.append("Anh", files[0]);
    data.append("ID", $('#iID').val());
    data.append("TenDangNhap", $('#iUserName').val());
    data.append("MatKhau", $('#iPassword').val());
    data.append("Ten", $('#iName').val());
    data.append("Mail", $('#iMail').val());
    data.append("GioiTinh", gioiTinh);
    debugger;
    $.ajax({
        url: '/KhachHang/EditKhachHang',
        type: 'POST',
        data: data,
        dataType: false,
        contentType: false,
        processData: false,
        success: function (data) {
            swal("Sửa quản lý thành công!", "success");
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

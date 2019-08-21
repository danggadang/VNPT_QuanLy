$(document).ready(function () {
    $.fn.modal.prototype.constructor.Constructor.DEFAULTS.backdrop = 'static';
    $.fn.modal.prototype.constructor.Constructor.DEFAULTS.keyboard = false;
    $("#dtable").DataTable({
        ajax: {
            dataType: 'json',
            type: 'post',//server side only works well with type "POST" !!!
            url: '/KhachHang/LoadKhachHang',
        },
        columns: [
            { data: 'TenKH'},
            { data: 'SoDienThoai' },
            { data: 'Mail' },
            { data: 'DiaChi' },
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

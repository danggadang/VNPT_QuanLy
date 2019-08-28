$(document).ready(function () {
    $.fn.modal.prototype.constructor.Constructor.DEFAULTS.backdrop = 'static';
    $.fn.modal.prototype.constructor.Constructor.DEFAULTS.keyboard = false;
    $("#dtable").DataTable({
        ajax: {
            dataType: 'json',
            type: 'get',//server side only works well with type "POST" !!!
            url: '/QuanLy/LoadQuanLy',
        },
        columns: [
            { data: 'TenNV', name: 'TenNV' },
            
            { data: 'Mail', name: 'Mail' },
            { data: 'GioiTinh', name: 'GioiTinh' },
            {
                data: 'Anh', render: function (data) {
                    return ` <img src="../../Images/${data}" style="width:100px; height:100px"/>`;
                }
            },
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
    $.ajax({
        url: '/QuanLy/AddQuanLy',
        type: 'POST',
        data: data,
        dataType: false,
        contentType: false,
        processData: false,
        success: function (data) {
            swal("Thêm quản lý thành công!", "success");
            $('#dtable').DataTable().ajax.reload();
            $('#AddandEditQuanLyModal').modal('hide');
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
    $.ajax({
        url: '/QuanLy/EditQuanLy',
        type: 'POST',
        data: data,
        dataType: false,
        contentType: false,
        processData: false,
        success: function (data) {
            swal("Sửa quản lý thành công!", "success");
            $('#dtable').DataTable().ajax.reload();
            $('#AddandEditQuanLyModal').modal('hide');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal("Oh no!!", thrownError, "error");
        }
    });
}
function Delete(id) {
    $.ajax({
        url: '/QuanLy/DeleteQuanLy',
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
function ReadImage(input) {
    console.log('dadoc');
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#image').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}
function fnShowModal(id) {
    $.ajax({
        url: '/QuanLy/AddandEditQuanLyModal',
        data: { id: id },
        type: 'POST',
        success: function (data) {
            $('#containershow1').html(data);
            $('#AddandEditQuanLyModal').modal('show');
        }
    });
}

$(document).ready(function () {
    $.fn.modal.prototype.constructor.Constructor.DEFAULTS.backdrop = 'static';
    $.fn.modal.prototype.constructor.Constructor.DEFAULTS.keyboard = false;
    $("#dtable").DataTable({

        ajax: {
            dataType: 'json',
            type: 'get',
            url: '/DichVuGroup/LoadDichVuGroup',
        },
        columns: [
            {
                data: 'TenNhom'              
            },
            {
                sortable: false,
                searchable:false,
                data: 'ID', render: function (data) {
                    return `

                                <button class="btn btn-sm btn-warning" onclick="fnShowModal(${data})"> Sửa</button>
                                <button class="btn btn-sm btn-danger" onclick="Delete(${data})"> Xóa</button>

                            `;
                }
            }

        ],
    });
});
function Status(id) {
    $.ajax({
        url: "/DichVuGroupGroup/Status",
        type: "post",
        data: { id: id },
        success: function (data) {
            $('#dtable').DataTable().ajax.reload();
        }
    })
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
    data.append("TenDichVuGroup", $('#iTenDichVuGroup').val());
    $.ajax({
        url: '/DichVuGroup/AddDichVuGroup',
        type: 'POST',
        data: data,
        dataType: false,
        contentType: false,
        processData: false,
        success: function (data) {
            swal("Thêm thành công!", "success");
            $('#dtable').DataTable().ajax.reload();
            $('#AddandEditDichVuGroupModal').modal('hide');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal("Oh no!!", thrownError, "error");
        }
    });
}
function Edit() {
    var trangThai;
    if ($('#tTrangThai').is(":checked")) {
        trangThai = 1;
    }
    else {
        trangThai = 0;
    }
    var data = new FormData();
    data.append("ID", $('#iID').val());
    data.append("TenDichVuGroup", $('#iTenDichVuGroup').val());
    data.append("TrangThai", trangThai);
    $.ajax({
        url: '/DichVuGroup/EditDichVuGroup',
        type: 'POST',
        data: data,
        dataType: false,
        contentType: false,
        processData: false,
        success: function (data) {
            swal("Thêm thành công!", "success");
            $('#dtable').DataTable().ajax.reload();
            $('#AddandEditDichVuGroupModal').modal('hide');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal("Oh no!!", thrownError, "error");
        }
    });
}
function Delete(id) {
    $.ajax({
        url: '/DichVuGroup/DeleteDichVuGroup',
        dataType: 'json',
        data: { id: id },
        type: 'POST',
        success: function (data) {
            swal("Yes!!", "Xóa   thành công!", "success")
            $('#dtable').DataTable().ajax.reload();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal("Oh no!!", thrownError);
        }
    });
}

function ReadImage(input) {
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
        url: '/DichVuGroup/AddandEditDichVuGroupModal',
        data: { id: id },
        type: 'POST',
        success: function (data) {
            $('#containershow1').html(data);
            $('#AddandEditDichVuGroupModal').modal('show');
        }
    });
}

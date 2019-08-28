$(document).ready(function () {
    $.fn.modal.prototype.constructor.Constructor.DEFAULTS.backdrop = 'static';
    $.fn.modal.prototype.constructor.Constructor.DEFAULTS.keyboard = false;
    $("#dtable").DataTable({
        
        ajax: {
            dataType: 'json',
            type: 'get',
            url: '/DichVu/LoadDichVu',
        },
        columnDefs: [
            { targets: [0], visible: true },
        ],
        columns: [
            { data: 'TenDV', name: 'TenDV' 
                //data: function (data, type, dataToSet)
                //{
                //    return `${data.TenDV}+${data.ID}`;
                //} 
            },
            {
                'sortable': false,
                'searchable': false,
                data: function (data, type, dataToSet) {
                    var check = data.TrangThai ? "checked" : "";
                    return `<label>
                            <input name="switch-field-1" onchange="Status(`+data.ID+`)" class="ace ace-switch ace-switch-4 btn-empty cb" type="checkbox" `+check+`>
							<span class="lbl"></span>
						    </label>`;
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

        ],
    });
});
function Status(id) {
    $.ajax({
        url: "/DichVu/Status",
        type: "post",
        data: {id:id},
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
    data.append("TenDichVu", $('#iTenDichVu').val());
    $.ajax({
        url: '/DichVu/AddDichVu',
        type: 'POST',
        data: data,
        dataType: false,
        contentType: false,
        processData: false,
        success: function (data) {
            swal("Thêm thành công!", "success");
            $('#dtable').DataTable().ajax.reload();
            $('#AddandEditDichVuModal').modal('hide');
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
    data.append("TenDichVu", $('#iTenDichVu').val());
    data.append("TrangThai", trangThai);
    $.ajax({
        url: '/DichVu/EditDichVu',
        type: 'POST',
        data: data,
        dataType: false,
        contentType: false,
        processData: false,
        success: function (data) {
            swal("Thêm thành công!", "success");
            $('#dtable').DataTable().ajax.reload();
            $('#AddandEditDichVuModal').modal('hide');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal("Oh no!!", thrownError, "error");
        }
    });
}
function Delete(id) {
    $.ajax({
        url: '/DichVu/DeleteDichVu',
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
        url: '/DichVu/AddandEditDichVuModal',
        data: { id: id },
        type: 'POST',
        success: function (data) {
            $('#containershow1').html(data);
            $('#AddandEditDichVuModal').modal('show');
        }
    });
}

function ShowEditModal(id) {
    $.ajax({
        url: '/User/EditModal',
        data: { id: id },
        type: 'get',
        success: function (data) {
            $('#containershow1').html(data);
            $('#EditModal').modal('show');
        }
    });
}
function Edit() {
    var gioiTinh = $('input[name=rdGioiTinh]:checked', '#frmEdit').val();
    var data = new FormData();
    data.append("ID", $('#iID').val());
    data.append("Ten", $('#iName').val());
    data.append("Mail", $('#iMail').val());
    data.append("GioiTinh", gioiTinh);
    $.ajax({
        url: '/User/Edit',
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
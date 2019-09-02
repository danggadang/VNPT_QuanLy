$(document).ready(function () {
    $.fn.modal.prototype.constructor.Constructor.DEFAULTS.backdrop = 'static';
    $.fn.modal.prototype.constructor.Constructor.DEFAULTS.keyboard = false;
    $("#dtable").DataTable({
        
        ajax: {
            dataType: 'json',
            type: 'get',
            url: '/DichVu/LoadDichVu',
            dataSrc: function (json) {
                for (var i = 0; i < json.data.length; i++) {
                    for (var j = 0; j < json.data1.length; j++) {
                        if (json.data1[j]["ID"] == json.data[i]["IDNhomDV"]) {
                            json.data[i]["dichVu"] = json.data1[j]["TenNhom"];
                        }                    
                    }
                }
                return json.data;
            }
        },
        columns: [
            { data: 'TenDV' },
            {
                data: 'SoTien',
                render: function (data) {
                    return `<p>` + formatNumber(data, '.', ',') + ` VNĐ</p>`;
                }
            },
            {data:'dichVu'},
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
                'sortable': false,
                'searchable': false,
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
function LoadNhomDichVu(id) {
    $.ajax({
        url: '/DichVu/LoadNhomDichVu',
        type: 'get',
        data: { id: id },
        success: function (data) {
            html = "";
            idNhomDichVu = data.data1;
            $.each(data.data, function (i, row) {
                if (row.ID == idNhomDichVu) {
                    html += `<option value="` + row.ID + `" class="oNhanVien" selected>` + row.TenNhom + `</option>`;
                }
                else {
                    html += `<option value="` + row.ID + `" class="oNhanVien">` + row.TenNhom + `</option>`;
                }
            })

            $('#sNhomDichVu').html(html);
        }
    });
}
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
    idNhanVien = $("#sNhomDichVu option:selected").val();
    var data = new FormData();  
    data.append("TenDichVu", $('#iTenDichVu').val());
    data.append("SoTien", $('#iSoTien').val());
    data.append("idNhomDichVu", idNhanVien);

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
    idNhanVien = $("#sNhomDichVu option:selected").val();
    var data = new FormData();
    data.append("ID", $('#iID').val());
    data.append("TenDichVu", $('#iTenDichVu').val());
    data.append("SoTien", $('#iSoTien').val());
    data.append("idNhomDichVu", idNhanVien);

    $.ajax({
        url: '/DichVu/EditDichVu',
        type: 'POST',
        data: data,
        dataType: false,
        contentType: false,
        processData: false,
        success: function (data) {
            swal("Sửa thành công!", "success");
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
            var id = $('#iID').val();
            LoadNhomDichVu(id);
        }
    });
}

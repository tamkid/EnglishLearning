$(document).ready(function () {
    func_load_word_table();
});

function func_load_word_table() {
    $.ajax({
        type: 'GET',
        url: 'Word/GetWordTable',
        success: function (data) {
            if (data != null) {
                $("#word-table").html(data);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}

function func_confirm_delete(id, word) {
    $("#wordDeleteId").val(id);
    $("#confirmContent").html(`Do you want to remove '${word}' ?`);
    $("#deleteConfirmModal").modal('show');
}

function func_delete() {
    var id = $("#wordDeleteId").val();
    $.ajax({
        type: 'DELETE',
        url: `Word/DeleteWord/${id}`,
        success: function (data) {
            func_load_word_table();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        },
        complete: function () {
            $("#deleteConfirmModal").modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            
        }
    });    
}
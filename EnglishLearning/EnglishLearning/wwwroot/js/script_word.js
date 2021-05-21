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
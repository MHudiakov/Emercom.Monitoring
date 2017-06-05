$(document).ready(function() {
    $('#tree tr').on('click', function(e) {
        var node = e.target;
        var id = $(node).parent().data('id');
        $.get('/EquipmentGroup/Equipment/' + id, function(response) {
            $('#list-partial').html(response);
        });
    });
    $('#tree tr td')[0].click();
})
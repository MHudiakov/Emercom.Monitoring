﻿$(document).ready(updateTree);

function updateTree() {
    if ($("#tree").length) {
        $("#tree").treegrid();
        $("#tree").treegrid("collapseAll");

        $('#tree tr').on('click',
            function (e) {
                var node = e.target;
                var id = $(node).parent().data('id');
                if (id) {
                    $.get('/EquipmentGroup/KEquipmentList/' + id,
                        function (response) {
                            $('#list-partial').html(response);
                        });
                }
            });

        $('#tree tr td')[0].click();
    }
    spinner.stop();
}
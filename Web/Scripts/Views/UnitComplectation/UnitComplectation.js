$(document).ready(function () {
    applyTreeGreed();
    setInterval(updateTable, 60 * 1000);
});

function applyTreeGreed() {
    $("#tree").treegrid({
        'initialState': 'collapsed',
        'saveState': true
    });
    //$("#tree").treegrid("collapseAll");
}

function updateTable() {
    $.get("/UnitComplectation/UnitComplectationTablePartial", { unitId: $('#UnitId').val() }, function (result) {
        if (result) {
            $("#unitComplectation").html(result);
            applyTreeGreed();
        } else {
            alert("Ошибка при обновлении таблицы");
        }
    });
}
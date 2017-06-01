$(document).ready(function () {
    //var id = $("#UnitId").val();
    var timer = $.timer(UpdateUnitTables);
    timer.set({ time: 15000, autostart: true });
});

function UpdateUnitTables() {
    var id = $("#UnitId").val();
    $.get("/Unit/UnitInfoPartial/" + id, function (response) {
        $("equipment").html(response);
        console.log("UnitInfoPartial updated");
    });
    $.get("/Unit/UnitMovementPartial/" + id, function (response) {
        $("movement").html(response);
        console.log("UnitMovementPartial updated");
    });
}
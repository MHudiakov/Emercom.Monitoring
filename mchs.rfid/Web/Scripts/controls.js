$(function () {
    if ($(".multiselect").length > 0) {
        $(".multiselect").multiselect({ sortable: false, searchable: false });
    }
    if ($(".datepicker").length > 0) {
        $(".datepicker").datepicker({ format: "dd.mm.yyyy", language: "ru-RU", autoclose: true });
    }
    if ($(".timepicker").length > 0) {
        $(".timepicker").timepicker({ minuteStep: 1, secondStep: 1, showSeconds: true, showMeridian: false });
    }
});

$.validator.methods.date = function (value, element) {
    return this.optional(element) || Globalize.parseDate(value, "dd.mm.yyyy") != null;
};


var spinner = (function () {
    var spinnerHtml =
        $('<div class="modal fade modal-spinner" id="spinner" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">' +
            '<div class="modal-dialog modal-dialog-spinner">' +
            '<div class="modal-content modal-content-spinner">' +
            '<div class="modal-body">' +
            '<h5 align="center"><img src="Content/images/ajax-loader.gif" style="margin-left: -10px; margin-right: 10px"/>Пожалуйста, подождите...</h5>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>');

    return {
        start: function start() {
            spinnerHtml.modal('show');
        },
        stop: function stop() {
            spinnerHtml.modal('hide');
        }
    };    
})();
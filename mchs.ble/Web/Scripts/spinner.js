var spinner = (function () {
    var spinnerHtml =
        $('<div class="modal fade" id="spinner" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">' +
            '<div class="modal-dialog">' +
            '<div class="modal-content">' +
            '<div class="modal-body">' +
            '<h5 align="center"><img src="..Content/images/ajax-loader.gif" style="margin-left: -10px; margin-right: 10px"/>Пожалуйста, подождите...</h5>' +
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




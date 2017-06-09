function OnEdit(id) {
    $.post("/User/EditUser", { id: id }, function (data) {
        var title = "Добавление пользователя";
        if (id != 0)
            title = "Редактирование пользователя";
        bootbox.dialog({
            title: title,
            message: data,
            buttons: {
                cancel: {
                    label: "Закрыть",
                    className: "btn-default",
                    callback: function () {
                        bootbox.hideAll();
                    }
                },
                ok: {
                    label: "Сохранить",
                    className: "btn-primary",
                    callback: function () {
                        if ($("#EditWorker").validate().form()) {
                            var worker = $("#EditWorker").serialize();
                            updateTable(worker);
                            return true;
                        }
                        else
                            return false;
                    }
                }
            }
        });

        $('select').selectpicker();
        $(".modal-dialog").css("z-index", 10000);

        //SetFormValidation($("#EditWorker"), {
        //    rules: {
        //        Login: {
        //            required: true,
        //            maxlength: 50
        //        },
        //        Password: {
        //            required: true,
        //            maxlength: 100
        //        },
        //        Surname: {
        //            required: true,
        //            maxlength: 50
        //        },
        //        Name: {
        //            required: true,
        //            maxlength: 50
        //        },
        //        SecondName: {
        //            required: true,
        //            maxlength: 50
        //        },
        //        PersonNumber: {
        //            required: true,
        //            maxlength: 100
        //        },
        //        Profession: {
        //            required: true,
        //            maxlength: 500
        //        },
        //        Description: {
        //            maxlength: 500
        //        },
        //    },
        //    messages: {
        //        Login: {
        //            required: "Пожалуйста, введите логин",
        //            maxlength: "Поле должно быть не длиннее 50 символов"
        //        },
        //        Password: {
        //            required: "Пожалуйста, введите пароль",
        //            maxlength: "Поле должно быть не длиннее 100 символов"
        //        },
        //        Surname: {
        //            required: "Пожалуйста, введите фамилию",
        //            maxlength: "Поле должно быть не длиннее 50 символов"
        //        },
        //        Name: {
        //            required: "Пожалуйста, введите имя",
        //            maxlength: "Поле должно быть не длиннее 50 символов"
        //        },
        //        SecondName: {
        //            required: "Пожалуйста, введите отчество",
        //            maxlength: "Поле должно быть не длиннее 50 символов"
        //        },
        //        PersonNumber: {
        //            required: "Пожалуйста, введите табельный номер",
        //            maxlength: "Поле должно быть не длиннее 100 символов"
        //        },
        //        Profession: {
        //            required: "Пожалуйста, введите профессию",
        //            maxlength: "Поле должно быть не длиннее 500 символов"
        //        },
        //        Description: {
        //            maxlength: "Поле должно быть не длиннее 500 символов"
        //        },
        //    }

   //     });
    });
}
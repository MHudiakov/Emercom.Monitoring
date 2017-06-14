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
                            var user = $("#EditUser").serialize();
                            updateTable(user);
                            return true;
                    }
                }
            }
        });

        $(".modal-dialog").css("z-index", 10000);

    });
}

function updateTable(user) {
    // Обновляем форму
    $.post("/User/AddOrEditUser", user, function (table) {
        $("#users").html(table);
    });
}
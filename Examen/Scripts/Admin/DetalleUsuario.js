$(document).ready(function () {

    $("#formDetalleUsuario").on("submit",
        function (e) {
            e.preventDefault();
            var form = $(this);
            form.parsley().validate();
            if (!form.parsley().isValid()) {
                return false;
            }
            var jsonForm = form.FormAsJSON();
            var request = jsonForm;
            GetResponse(CrearUsuario,
                request,
                function (out) {
                    if (out.Success) {
                        location.href = Index;

                    } else {

                    }
                });
        });

    $("#btnReturn").on("click",
        function () {
            location.href = "Usuarios";
        });

    $("#btnSaveUsuario").on("click",
        function () {
            var request = $("#formDetalleUsuario").FormAsJSON();
            GetResponse(CrearUsuario, request,
                function (out) {
                    if (out.Success) {
                        location.href = "Usuarios";
                    } else {

                    }
                });
        });
});

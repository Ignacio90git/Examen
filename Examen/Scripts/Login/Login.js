$(document).ready(function () {
    $("#loginForm").on("submit",
        function (e) {
            e.preventDefault();
            var button = $("#btnLogin");
            var form = $(this);
            form.parsley().validate();
            if (!form.parsley().isValid()) {
                return false;
            }
            button.html(
                "<span class=\"spinner-border spinner-border-sm mr-2\" role=\"status\" aria-hidden=\"true\"></span>Iniciando sesión ...");
            button.attr("disabled", true);
            var request = form.FormAsJSON();
            GetResponse(UrlLogin,
                request,
                function (out) {
                    if (out.Success) {
                        location.href = "Home/Index";

                    } else if ((typeof out === "string" || out instanceof String) && out.startsWith("<!DOCTYPE")) {
                        document.open();
                        document.write(out);
                        document.close();
                    } else {
                        button.html("Iniciar sesión");
                        button.removeAttr("disabled");
                    }
                });
        });
});
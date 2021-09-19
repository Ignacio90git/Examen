
$(document).ready(function () {

    $(function () {
        $("#FechaNacimiento").datepicker();
    });

    $("#formDetalleEmpleado").on("submit",
        function (e) {
            e.preventDefault();
            var form = $(this);
            form.parsley().validate();
            if (!form.parsley().isValid()) {
                return false;
            }
            var jsonForm = form.FormAsJSON();

            var request = jsonForm;

            GetResponse(CrearEmpleado,
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
            location.href = "Empleados";
        });

    $("#IdEmpresa").on("change",
        function () {
            var IdDepartamento = $("#IdDepartamento");
            var IdEmpresa = this.value;
            if (IdEmpresa > 0) {
                var NewDepartamentoList = modelJson.DepartamentoList.find(item => item.IdEmpresa == IdEmpresa);
                if (NewDepartamentoList != null) {
                    IdDepartamento.empty();
                    $.each(modelJson.DepartamentoList, function (index, item) {
                        if (item.IdEmpresa == IdEmpresa) {
                            IdDepartamento.append(
                                $("<option/>",
                                    {
                                        value: item.Id,
                                        text: item.Nombre
                                    })
                            );
                        }
                    });
                    IdDepartamento.trigger("chosen:updated");
                    IdDepartamento.trigger("change");
                }
            } else {
                IdDepartamento.empty();
                $.each(modelJson.DepartamentoList, function (index, item) {
                    IdDepartamento.append(
                        $("<option/>",
                            {
                                value: item.Id,
                                text: item.Nombre
                            })
                    );
                });
                IdDepartamento.trigger("chosen:updated");
                IdDepartamento.trigger("change");
            }
        });

    $("#btnSaveEmpleado").on("click",
        function () {
            var request = $("#formDetalleEmpleado").FormAsJSON();
            GetResponse(CrearEmpleado, request,
                function (out) {
                    if (out.Success) {
                        location.href = "Empleados";
                    } else {

                    }
                });
        });

});

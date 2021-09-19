var table = null;

$(document).ready(function () {
    $("#formBuscadorEmpleados").on("submit",
        function (e) {
            e.preventDefault();
            EjecutarBusqueda();
        });

    $(".body-content").on("click",
        ".btnEliminar",
        function () {
            var $this = $(this);
            var tr = $this.parents("tr");
            var data = table.row(tr).data();
            var request = { Id: data.Id };

            GetResponse(EliminarEmpleado,
                request,
                function (out) {
                    if (out.Success) {
                        EjecutarBusqueda();
                    } else {
                    }
                });
        });

    $(".body-content").on("click",
        ".btnEditar",
        function () {
            var $this = $(this);
            var tr = $this.parents("tr");
            var data = table.row(tr).data();
            LoadViewWithModel(DetalleEmpleado, data);
        });

    $("#btnRegistrar").on("click",
        function () {
            LoadViewWithModel(DetalleEmpleado, { Id: 0 });
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

    EjecutarBusqueda();
});

function EjecutarBusqueda() {
    var request = $("#formBuscadorEmpleados").FormAsJSON();
    GetResponse(BuscarEmpleados,
        request,
        function (out) {
            if (out.Success) {
                if (out.Data != undefined) {
                    $(".containerGridBuscador").show();
                    var columnsGrid =
                        [
                            { data: "Nombre" },
                            { data: "FechaIngreso" },
                            { data: "Correo", "width": "5%" },
                            { data: "Genero", "width": "5%" },
                            { data: "Telefono", "width": "5%" },
                            { data: "Celular" },
                            { data: "NombreDepartamento" },
                            { data: "NombreEmpresa" },
                            { data: "Id", "bSortable": false, "width": "15%" }
                        ];
                    table = $("#gridBuscador").DataTable({
                        'paging': true, // Table pagination
                        'ordering': true, // Column ordering
                        'info': true, // Bottom left status text
                        responsive: false,
                        scrollX: true,
                        lengthChange: false,
                        dom: "Bfrtip",
                        "aaSorting": [],

                        destroy: true,
                        columns: columnsGrid,
                        // Footer de la tabla de lista de cliente
                        language: {
                            "lengthMenu": "Mostrando _MENU_ registros por página",
                            "zeroRecords": "Sin información",
                            "info": "Mostrando registros _START_ a _END_ de _TOTAL_",
                            "infoEmpty": "Sin información",
                            "search": "<em class='fas fa-search'></em>",
                            "searchPlaceholder": "Buscar...",
                            "paginate": {
                                "first": "Primero",
                                "last": "Último",
                                "next": "Siguiente",
                                "previous": "Anterior"
                            },
                        },
                        createdRow: function (row, data, index) {
                            var htmlBtn =
                                '<div class="form-group text-center"><button type="button" class="btn btn-success btn-sm btnEditar" data-toggle="tooltip" data-placement="left" title="Editar/Detalles">Editar</button>';
                            htmlBtn =
                                `${htmlBtn
                                } <button type="button" class="btn btn-danger btn-sm btnEliminar" data-toggle="tooltip" data-placement="left" title="Eliminar">Eliminar</button>`;
                            htmlBtn = `${htmlBtn}</div>`;
                            $("td", row).eq(8).html(htmlBtn);
                        }
                    });
                    table.clear();
                    table.rows.add(out.Data).draw();
                    table.responsive.recalc();
                    table.responsive.rebuild();
                } else {
                    table = null;
                    $("#gridBuscador tbody").empty();
                    $(".containerGridBuscador").hide();
                }

            } else {
                table = null;
                $("#gridBuscador tbody").empty();
                $(".containerGridBuscador").hide();

            }
        });
}
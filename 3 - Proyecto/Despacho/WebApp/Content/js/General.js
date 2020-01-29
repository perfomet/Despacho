let General = function () {

    let Init = function () {
        InitElementos();
    };

    let InitElementos = function () {
        window.confirmar = function (titulo, pregunta, textoConfirmar, textoCancelar, _callback) {
            swal({
                title: titulo,
                text: pregunta,
                type: "warning",
                confirmButtonText: textoConfirmar,
                showCancelButton: textoCancelar != null && textoCancelar != this.undefined,
                cancelButtonText: textoCancelar
            }).then(function (e) {
                if (_callback) _callback(e.value);
            });
        };

        window.mensaje = function (titulo, mensaje, tipo, funcion) {
            switch (tipo) {
                case "error":
                    tipo = "error";
                    break;
                case "exito":
                    tipo = "success";
                    break;
                case "informacion":
                    tipo = "info";
                    break;
            }

            swal({
                title: titulo,
                text: mensaje,
                type: tipo,
                confirmButtonText: "Ok"
            }).then(function (e) {
                if (funcion) funcion();
            });
        };

        window.cargarTabla = function (identificador, controlador, parametros, idTabla, idBuscador, columnas, edita, desactiva) {

            if (edita || desactiva) {
                columnas.push({
                    field: "Acciones", title: "Acciones", responsive: { visible: "lg" }, template: function (e, a, i) {
                        let div = $('<div></div>');

                        let editar = $('<a href="/' + controlador + '/Edit/' + e[identificador] + '" class="m-portlet__nav-link btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only m-btn--pill" title="View "><i class="la la-edit"></i></a>');
                        let activarDesactivar = $('<a href="#" data-activo="' + e.EstaActivo + '" data-id="' + e[identificador] + '" class="activarDesactivar m-portlet__nav-link btn m-btn m-btn--hover-' + (e.EstaActivo == true ? "danger" : "success") + ' m-btn--icon m-btn--icon-only m-btn--pill" title="View "><i class="la la-power-off"></i></a>');

                        if (edita) div.append(editar);
                        if (desactiva) div.append(activarDesactivar);

                        return div.html();
                    }
                });
            }

            $.post("/" + controlador + '/Listar', parametros, function (datos) {
                $(idTabla).mDatatable({
                    data: {
                        type: "local",
                        source: datos,
                        pageSize: 10
                    },
                    layout: {
                        theme: "default",
                        class: "",
                        scroll: !1,
                        footer: !1
                    },
                    sortable: !0,
                    pagination: !0,
                    search: {
                        input: $(idBuscador)
                    },
                    columns: columnas
                });
            });

            $(document).on('click', '.activarDesactivar', function () {
                let estaActivo = $(this).data('activo');
                let id = $(this).data('id');
                if (estaActivo == true) {
                    confirmar("Desactivar", "¿Está seguro que desea desactivarlo?", "Si", "No", function (result) {
                        if (result) {
                            $.post('/' + controlador + '/EstaActivo', { id: id }, function (data) {
                                if (data.exito) {
                                    mensaje("Exito", "Desactivado correctamente", "exito", function () { location.reload(); });
                                } else {
                                    mensaje("Error", "No se pudo desactivar", "error");
                                }
                            });
                        }
                    });
                } else {
                    $.post('/' + controlador + '/EstaActivo', { id: id }, function (data) {
                        if (data.exito) {
                            mensaje("Exito", "Desactivado correctamente", "exito", function () { location.reload(); });
                        } else {
                            mensaje("Error", "No se pudo desactivar", "error");
                        }
                    });
                }
            });
        };
    };

    return {
        init: function () {
            Init();
        }
    };
}();

$(() => {
    General.init();
});
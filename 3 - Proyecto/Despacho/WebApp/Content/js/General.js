let General = function () {
  let _ValidaRut = function (rutCompleto) {
    if (!/^[0-9]+[-|‐]{1}[0-9kK]{1}$/.test(rutCompleto))
      return false;
    var tmp = rutCompleto.split('-');
    var digv = tmp[1];
    var rut = tmp[0];
    if (digv == 'K') digv = 'k';
    return (_ObtenerDV(rut) == digv);
  };

  let _ObtenerDV = function (T) {
    var M = 0, S = 1;
    for (; T; T = Math.floor(T / 10))
      S = (S + T % 10 * (9 - M++ % 6)) % 11;
    return S ? S - 1 : 'k';
  };
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

                        let editar = $('<a href="/' + controlador + '/Edit/' + e[identificador] + '" class="m-portlet__nav-link btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only m-btn--pill" title="Editar"><i class="la la-edit"></i></a>');
                        let activarDesactivar = $('<a href="#" data-activo="' + e.EstaActivo + '" data-id="' + e[identificador] + '" class="activarDesactivar m-portlet__nav-link btn m-btn m-btn--hover-' + (e.EstaActivo == true ? "danger" : "success") + ' m-btn--icon m-btn--icon-only m-btn--pill" title="' + (e.EstaActivo == true ? "Desactivar" : "Activar") + '"><i class="la la-power-off"></i></a>');

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
                    columns: columnas,
                    translate: {
                        records: {
                            processing: "Cargando...",
                            noRecords: "No se encontrarón registros"
                        },
                        toolbar: {
                            pagination: {
                                items: {
                                    default: {
                                        first: "Primero",
                                        prev: "Anterior",
                                        next: "Siguiente",
                                        last: "Último",
                                        more: "Más páginas",
                                        input: "Número de página",
                                        select: "Seleccionar tamaño de página"
                                    },
                                    info: "Viendo {{start}} - {{end}} de {{total}} registros"
                                }
                            }
                        }
                    }
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
                                    mensaje("Éxito", "Desactivado correctamente", "exito", function () { location.reload(); });
                                } else {
                                    mensaje("Error", "No se pudo desactivar", "error");
                                }
                            });
                        }
                    });
                } else {
                    $.post('/' + controlador + '/EstaActivo', { id: id }, function (data) {
                        if (data.exito) {
                            mensaje("Éxito", "Activado correctamente", "exito", function () { location.reload(); });
                        } else {
                            mensaje("Error", "No se pudo desactivar", "error");
                        }
                    });
                }
            });
        };

        window.crearSelectorFecha = function (idSelector, inicio, fin) {
            $(idSelector + " .form-control").val(inicio.format("DD/MM/YYYY") + " - " + fin.format("DD/MM/YYYY"))

            $(idSelector).daterangepicker({
              buttonClasses: "m-btn btn",
              applyLabel: "Aplicar",
              applyClass: "btn-primary",
              cancelClass: "btn-secondary",
              cancelLabel: "Cerrar",
              customRangeLabel: "Rango Personalizado",
              startDate: inicio,
              endDate: fin,
              ranges: {
                 Todo: [moment('01/01/1900', 'DD/MM/YYYY'), moment()],
                  Hoy: [moment(), moment()],
                  Ayer: [moment().subtract(1, "days"), moment().subtract(1, "days")],
                  "Últimos 7 Días": [moment().subtract(6, "days"), moment()],
                  "Últimos 30 Días": [moment().subtract(29, "days"), moment()],
                  "Este Mes": [moment().startOf("month"), moment().endOf("month")],
                "El Mes Pasado": [moment().subtract(1, "month").startOf("month"), moment().subtract(1, "month").endOf("month")],

                  }
            }, function (a, t, n) {
                $(idSelector + " .form-control").val(a.format("DD/MM/YYYY") + " - " + t.format("DD/MM/YYYY"))
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
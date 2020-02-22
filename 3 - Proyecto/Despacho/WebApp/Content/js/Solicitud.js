let Solicitud = function () {

    let solicitudes;

    let _Init = function () {
        _InitElementos();
    };

    let _InitElementos = function () {
        if ($(".m-select2").length > 0) $('.m-select2').select2();

        if ($("#lista-solicitudes").length > 0) {
            window.crearSelectorFecha("#filtro-fecha-solicitud", moment().subtract(6, 'days'), moment());

            $.post("/Solicitud/ObtenerSolicitudes", { solicitudId: 0 }, function (data) {
                solicitudes = data;

                _CargarLista();
            });

            $('#filtro-filtrar').click(function () {
                _Filtrar();
            });

            $(document).on('click', '#lista-solicitudes tbody tr', function () {
                let id = $(this).find('.solicitud-despacho-id').attr('data-id');
                location.href = "/Solicitud/Solicitud/" + id;
            });
        }
    };

    let _CargarLista = function () {
        $('#lista-solicitudes').mDatatable({
            data: {
                type: "local",
                source: solicitudes,
                pageSize: 10
            },
            layout: {
                theme: "default",
                class: "",
                scroll: false,
                footer: false
            },
            sortable: true,
            pagination: true,
            search: {
                input: $('#buscarExistencia')
            },
            columns: [
                {
                    field: "SolicitudDespachoId", title: "Numero Solicitud", responsive: { visible: "lg" }, template: function (e, a, i) {
                        return '<label class="solicitud-despacho-id" data-id="' + e.SolicitudDespachoId + '">' + e.SolicitudDespachoId + '</label>';
                    }
                },
                { field: "FechaSolicitud", title: "Fecha/Hora", responsive: { visible: "lg" }, type: "date", format: "DD/MM/YYYY" },
                {
                    field: "EstadoSolicitud", title: "Estado", responsive: { visible: "lg" }, template: function (e, a, i) {
                        return '<span class="m-badge m-badge--info m-badge--wide" style="font-size: 1rem;"><b>' + e.EstadoSolicitud + '</b></span>';
                    }
                },
                { field: "TipoSolicitud", title: "Tipo Solicitud", responsive: { visible: "lg" } },
                { field: "Prioridad", title: "Prioridad", responsive: { visible: "lg" } },
                { field: "Cliente", title: "Cliente", responsive: { visible: "lg" } },
                { field: "Solicitante", title: "Solicitante", responsive: { visible: "lg" } }
            ],
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
    };

    let _Filtrar = function () {
        let tabla = $('#lista-solicitudes').mDatatable();
        let picker = $('#filtro-fecha-solicitud').data('daterangepicker');

        let filtrado = solicitudes.filter((s) => {
            let valido = true;
            debugger;
            if ($('#filtro-patente').val() && s.PatenteCamion) valido = valido && s.PatenteCamion == $('#filtro-patente').val();
            //if ($('#filtro-denominacion').val()) valido = valido && s.CodBodega == $('#filtro-denominacion').val();
            if ($('#filtro-tipo-solicitud').val()) valido = valido && s.TipoSolicitudId == $('#filtro-tipo-solicitud').val();
            //if ($('#filtro-material').val()) valido = valido && s.CodBodega == $('#filtro-material').val();
            //if ($('#filtro-estado-equipos').val()) valido = valido && s.CodBodega == $('#filtro-estado-equipos').val();
            if ($('#filtro-estado-solicitud').val()) valido = valido && s.EstadoSolicitudId == $('#filtro-estado-solicitud').val();
            if (picker) valido = valido && moment(s.FechaSolicitud, 'DD/MM/YYYY HH:mm:ss').isBetween(picker.startDate, picker.endDate);

            return valido;
        });

        tabla.originalDataSet = filtrado;
        tabla.load();
    };

    return {
        init: function () {
            _Init();
        }
    };
}();

let DetalleSolicitud = function () {
    let _Init = function () {
        _InitElementos();
    };

    let _InitElementos = function () {
        if ($('#formulario-solicitud').length > 0) {
            new mWizard("formulario-solicitud", { startStep: 1 });

            $("#fechaRecepcion").datepicker({ todayHighlight: !0, autoclose: !0, format: "dd/mm/yyyy" });
            $("#fechaDespacho").datepicker({ todayHighlight: !0, autoclose: !0, format: "dd/mm/yyyy" });
            $("#fechaEntregaDoc").datepicker({ todayHighlight: !0, autoclose: !0, format: "dd/mm/yyyy" });
            $("#fechaRecepcionDoc").datepicker({ todayHighlight: !0, autoclose: !0, format: "dd/mm/yyyy" });
        }

        if ($('#btnGuardar').length > 0) {
            $('#btnGuardar').click(function () {
                _Guardar();
            });
        }

        if ($('#regionClienteId').length > 0) {
            $('#regionClienteId').change(function () {
                _CargarListaComunas($(this).val());
            });
        }
    };

    let _Guardar = function () {
        let partesRutCliente = $('#rutCliente').val().replace(/\./g, '').split('-');

        let solicitudId = $('#solicitudId').val();
        let estadoId = $('#estadoId').val();
        let tipoSolicitudId = $('#tipoSolicitudId').val();
        let fechaRecepcion = $('#fechaRecepcion').val();
        let bodegaOrigen = $('#bodegaOrigen').val();
        let numeroCliente = $('#numeroCliente').val();
        let nombreCliente = $('#nombreCliente').val();
        let direccionCliente = $('#direccionCliente').val();
        let comunaClienteId = $('#comunaClienteId').val();
        let telefonoContacto = $('#telefonoContacto').val();
        let rutCliente = partesRutCliente[0];
        let vrutCliente = partesRutCliente[1];
        let proyecto = $('#proyecto').val();
        let prioridadId = $('#prioridadId').val();
        let unidadNegocioId = $('#unidadNegocioId').val();
        let gerenciaId = $('#gerenciaId').val();
        let observaciones = $('#observaciones').val();

        let fechaDespacho = $('#fechaDespacho').val();
        let patenteCamion = $('#patenteCamion').val();
        let llamada = $('#llamada').val();
        let comentarios = $('#comentarios').val();
        let enlaceId = $('#enlaceId').val();

        let numeroDocumento = $('#numeroDocumento').val();
        let numeroEntrega = $('#numeroEntrega').val();
        let fechaEntregaDoc = $('#fechaEntregaDoc').val();
        let fechaRecepcionDoc = $('#fechaRecepcionDoc').val();
        let folio = $('#folio').val();
        let tipoDocumentoId = $('#tipoDocumentoId').val();

        let equiposSolicitados = EquiposSolicitados.getEquipos();

        $.post("/Solicitud/" + (solicitudId > 0 ? "Edit" : "Create"), {
            solicitud: {
                SolicitudDespachoId: solicitudId,
                TipoSolicitudId: tipoSolicitudId,
                EstadoSolicitudId: _ProximoEstado(estadoId),
                FechaSolicitud: moment().format("DD/MM/YYYY HH:mm"),
                FechaRecepcion: fechaRecepcion,
                BodegaOrigen: bodegaOrigen,
                NumeroCliente: numeroCliente,
                NombreCliente: nombreCliente,
                DireccionCliente: direccionCliente,
                ComunaClienteId: comunaClienteId,
                NumeroTelefonoContacto: telefonoContacto,
                RutCliente: rutCliente,
                VRutCliente: vrutCliente,
                Proyecto: proyecto,
                PrioridadId: prioridadId,
                UnidadNegocioId: unidadNegocioId,
                GerenciaId: gerenciaId,
                ObservacionAof: observaciones,

                FechaDespacho: fechaDespacho,
                PatenteCamion: patenteCamion,
                LlamadaDiaAnterior: llamada,
                ComentariosLlamada: comentarios,
                EnlaceId: enlaceId,

                NumeroDocumento: numeroDocumento,
                NumeroEntrega: numeroEntrega,
                FechaEntregaDocumento: fechaEntregaDoc,
                FechaRecepcionDocumento: fechaRecepcionDoc,
                Folio: folio,
                TipoDocumentoId: tipoDocumentoId
            },
            equiposSolicitados: equiposSolicitados
        }, function (data) {
            if (data.exito) {
                mensaje("Éxito", "Información guardada correctamente", "exito", function () { location.href = "/Solicitud/Index"; });
            } else {
                mensaje("Error", "No se pudo guardar la información", "error");
            }
        });
    };

    let _CargarListaComunas = function (regionId) {
        $.post('/Solicitud/ObtenerComunas', { regionId: regionId }, function (comunas) {
            $('#comunaClienteId').html('<option value="0" selected disabled>Seleccione una comuna</option>');

            if (comunas.length > 0) {
                comunas.forEach((comuna) => {
                    $('#comunaClienteId').append('<option value="' + comuna.ComunaId + '">' + comuna.Nombre + '</option>');
                });

                $('#comunaClienteId').removeAttr('disabled');
            } else {
                $('#comunaClienteId').attr('disabled', 'disabled');
            }

            $('#comunaClienteId').select2({ placeholder: "Seleccione una comuna" });
        });
    };

    let _ProximoEstado = function (estadoActual) {
        return (estadoId == 0 ? 1 : (estadoId == 1 ? 3 : parseInt(estadoId) + 1));
    };

    return {
        init: function () {
            _Init();
        }
    };
}();

let EquiposSolicitados = function () {
    let equipos = [];

    let _Init = function () {
        _InitElementos();
    };

    let _InitElementos = function () {
        if ($("#lista-equipos-solicitados").length > 0) {
            $.post("/Solicitud/ObtenerEquiposSolicitados", { solicitudId: $('#solicitudId').val() }, function (data) {
                equipos = data;
                _CargarLista();
            });
        }

        if ($('#solicitarEquipos').length > 0) {
            $('#solicitarEquipos').click(function () {
                _AgregarEquipos();
            });
        }
    };

    let _CargarLista = function () {
        $('#lista-equipos-solicitados').mDatatable({
            data: {
                type: "local",
                source: equipos,
                pageSize: 10
            },
            layout: {
                theme: "default",
                class: "",
                scroll: false,
                footer: false
            },
            sortable: true,
            pagination: false,
            //search: {
            //    input: $('#buscarExistencia')
            //},
            columns: [
                {
                    field: "NumeroPlaca", title: "Placa", responsive: { visible: "lg" }, template: function (e, a, i) {
                        let id = '<input type="hidden" class="form-control id" value="' + (e.EquipoSolicitadoId || '0') + '" />';
                        let placa = e.NumeroPlaca ? '<input type="hidden" class="form-control numero-placa" value="' + e.NumeroPlaca + '" />' + e.NumeroPlaca : '<input type="text" class="form-control numero-placa" value="' + (e.NumeroPlaca || '') + '" />';
                        return id + placa
                    }
                },
                { field: "Modelo", title: "Modelo", responsive: { visible: "lg" }, template: function (e, a, i) { return '<input type="hidden" class="form-control modelo" value="' + (e.Modelo || '') + '" />' + (e.Modelo || '') } },
                { field: "EstadoEquipo", title: "Estado", responsive: { visible: "lg" }, template: function (e, a, i) { return '<input type="hidden" class="form-control estado" value="' + (e.EstadoEquipo || '') + '" />' + (e.EstadoEquipo || '') } },
                {
                    field: "Eliminar", title: "Eliminar", responsive: { visible: "lg" }, template: function (e, a, i) {
                        return e.NumeroPlaca ? '' : '<a href="#lista-equipos-solicitados" onclick="EquiposSolicitados.Eliminar(' + e.EquipoSolicitadoId + ')" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title="Eliminar"><i class="la la-trash"></i></a>';
                    }
                }
            ],
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
    };

    let _AgregarEquipos = function () {
        let tabla = $('#lista-equipos-solicitados').mDatatable();

        let solicitudId = $('#solicitudId').val();
        let estadoId = $('#estadoEquipo').val();
        let estado = $('#estadoEquipo').find('option:selected').html();
        let modelo = $('#modeloEquipo').val();
        let cantidad = $('#cantidadEquipos').val();

        if (!estadoId) {
            mensaje('Error', 'Debe seleccionar un estado', 'error');
            return;
        }

        if (!modelo) {
            mensaje('Error', 'Debe seleccionar un modelo', 'error');
            return;
        }

        if (!cantidad || cantidad <= 0) {
            mensaje('Error', 'Debe ingresar una cantidad mayor a 0', 'error');
            return;
        }

        for (let i = 0; i < cantidad; i++) {
            equipos.push({
                EquipoSolicitadoId: 0,
                NumeroPlaca: null,
                Modelo: modelo,
                EstadoEquipoId: estadoId,
                EstadoEquipo: estado,
                SolicitudDespachoId: solicitudId
            });
        }

        tabla.originalDataSet = equipos;
        tabla.load();

        $('#modalEquipoSolicitado').modal('hide');

        _Limpiar();
    };

    let _Validar = function () {
        return equipos.length > 0;
    };

    let _Limpiar = function () {
        $('#estadoEquipo').val('0');
        $('#estadoEquipo').trigger('change');

        $('#modeloEquipo').val('0');
        $('#modeloEquipo').trigger('change');

        $('#cantidadEquipos').val(1);
    };

    let _Actualizar = function () {
        equipos.forEach((equipo) => {
            equipo.NumeroPlaca = $('#lista-equipos-solicitados tbody tr .id[value="' + equipo.EquipoSolicitadoId + '"]').parent().find('.numero-placa').val();
        });
    };

    let _Eliminar = function (id) {
        let tabla = $('#lista-equipos-solicitados').mDatatable();

        let indice = equipos.indexOf(equipos.find((e) => e.EquipoSolicitadoId == id));

        equipos.splice(indice, 1);

        tabla.originalDataSet = equipos;
        tabla.load();
    };

    return {
        init: function () {
            _Init();
        },
        getEquipos: function () {
            _Actualizar();
            return equipos;
        },
        Validar: _Validar,
        Limpiar: _Limpiar,
        Eliminar: _Eliminar
    };
}();

let PersonalAsignado = function () {
    let personal = [];

    let _Init = function () {
        _InitElementos();
    };

    let _InitElementos = function () {
        if ($("#lista-personal-asignado").length > 0) {
            $.post("/Solicitud/ObtenerPersonalAsignado", { solicitudId: $('#solicitudId').val() }, function (data) {
                personal = data;
                _CargarLista();
            });
        }

        if ($('#solicitarEquipos').length > 0) {
            $('#solicitarEquipos').click(function () {
                _AgregarEquipos();
            });
        }
    };

    let _CargarLista = function () {
        $('#lista-personal-asignado').mDatatable({
            data: {
                type: "local",
                source: equipos,
                pageSize: 10
            },
            layout: {
                theme: "default",
                class: "",
                scroll: false,
                footer: false
            },
            sortable: true,
            pagination: false,
            //search: {
            //    input: $('#buscarExistencia')
            //},
            columns: [
                {
                    field: "NumeroPlaca", title: "Placa", responsive: { visible: "lg" }, template: function (e, a, i) {
                        let id = '<input type="hidden" class="form-control id" value="' + (e.EquipoSolicitadoId || '0') + '" />';
                        let placa = e.NumeroPlaca ? '<input type="hidden" class="form-control numero-placa" value="' + e.NumeroPlaca + '" />' + e.NumeroPlaca : '<input type="text" class="form-control numero-placa" value="' + (e.NumeroPlaca || '') + '" />';
                        return id + placa
                    }
                },
                { field: "Marca", title: "Marca", responsive: { visible: "lg" }, template: function (e, a, i) { return '<input type="hidden" class="form-control marca" value="' + (e.Marca || '') + '" />' + (e.Marca || '') } },
                { field: "Modelo", title: "Modelo", responsive: { visible: "lg" }, template: function (e, a, i) { return '<input type="hidden" class="form-control modelo" value="' + (e.Modelo || '') + '" />' + (e.Modelo || '') } },
                { field: "EstadoEquipo", title: "Estado", responsive: { visible: "lg" }, template: function (e, a, i) { return '<input type="hidden" class="form-control estado" value="' + (e.EstadoEquipo || '') + '" />' + (e.EstadoEquipo || '') } },
                {
                    field: "Eliminar", title: "Eliminar", responsive: { visible: "lg" }, template: function (e, a, i) {
                        return /*e.NumeroPlaca ? '' : */'<a href="#lista-equipos-solicitados" onclick="EquiposSolicitados.Eliminar(' + e.EquipoSolicitadoId + ')" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title="Eliminar"><i class="la la-trash"></i></a>';
                    }
                }
            ],
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
    };

    let _Eliminar = function (id) {
        let tabla = $('#lista-personal-asignado').mDatatable();

        let indice = equipos.indexOf(equipos.find((e) => e.EquipoSolicitadoId == id));

        equipos.splice(indice, 1);

        tabla.originalDataSet = equipos;
        tabla.load();
    };

    return {
        init: function () {
            _Init();
        },
        getPersonal: function () {
            return personal;
        },
        Eliminar: _Eliminar
    };
}();

let EquiposRetirados = function () {
    let _Init = function () {
        _InitElementos();
    };

    let _InitElementos = function () {

    };

    return {
        init: function () {
            _Init();
        }
    };
}();

$(() => {
    Solicitud.init();
    DetalleSolicitud.init();
    EquiposSolicitados.init();
    PersonalAsignado.init();
    EquiposRetirados.init();
});
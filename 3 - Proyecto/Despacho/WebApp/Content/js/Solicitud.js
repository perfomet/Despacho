let Solicitud = function () {

    let solicitudes;

    let Init = function () {
        InitElementos();
    };

    let InitElementos = function () {
        $('.m-select2').select2();

        if ($("#lista-solicitudes").length > 0) {
            window.crearSelectorFecha("#filtro-fecha-solicitud", moment().subtract(6, 'days'), moment());

            $.post("/Solicitud/ObtenerSolicitudes", { solicitudId: 0 }, function (data) {
                solicitudes = data;

                CargarLista();
            });

            $('#filtro-filtrar').click(function () {
                Filtrar();
            });

            $(document).on('click', '#lista-solicitudes tbody tr', function () {
                let id = $(this).find('.solicitud-despacho-id').attr('data-id');
                location.href = "/Solicitud/Solicitud/" + id;
            });
        }

        if ($('#formulario-solicitud').length > 0) {
            new mWizard("formulario-solicitud", { startStep: 1 });

            $("#fechaRecepcion").datetimepicker({ todayHighlight: !0, autoclose: !0, format: "dd/mm/yyyy hh:ii" });
        }

        $('#btnGuardar').click(function () {
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
            let rutCliente = $('#rutCliente').val();
            let vrutCliente = $('#vrutCliente').val();
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

            $.post("/Solicitud/" + (solicitudId > 0 ? "Edit" : "Create"), {
                SolicitudDespachoId: solicitudId,
                TipoSolicitudId: tipoSolicitudId,
                EstadoSolicitudId: (estadoId == 0 ? 1 : (estadoId == 1 ? 3 : estadoId + 1)),
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
            }, function (data) {
                if (data.exito) {
                    mensaje("Éxito", "Información guardada correctamente", "exito", function () { location.href = "/Solicitud/Index"; });
                } else {
                    mensaje("Error", "No se pudo guardar la información", "error");
                }
            });
        });
    };

    let CargarLista = function () {
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

    let Filtrar = function () {
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
            Init();
        }
    };
}();

$(() => {
    Solicitud.init();
});
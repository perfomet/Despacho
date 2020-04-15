let Solicitud = function () {

    let solicitudes;

    let _Init = function () {
        _InitElementos();
    };

    let _InitElementos = function () {
        if ($(".m-select2").length > 0) $('.m-select2').select2();

        if ($("#lista-solicitudes").length > 0) {
            window.crearSelectorFecha("#filtro-fecha-solicitud", moment().subtract(6, 'days'), moment());

            let perfil = $('#perfil-usuario').val();

            $.post(webroot + "/Solicitud/ObtenerSolicitudes", { solicitudId: 0 }, function (data) {
                solicitudes = data.filter((s) => {
                    let cuenta = false;

                    cuenta = cuenta || ((perfil == 1 || perfil == 4) && s.EstadoSolicitudId > 0);
                    cuenta = cuenta || (perfil == 2 && (s.EstadoSolicitudId == 2 || s.EstadoSolicitud.SeccionFormularioId == 4));
                    cuenta = cuenta || (perfil == 3 && s.EstadoSolicitud.SeccionFormularioId == 2);

                    return cuenta;
                });

                _CargarLista();
            });

            $('#filtro-filtrar').click(function () {
                _Filtrar();
            });

            $(document).on('click', '#lista-solicitudes tbody tr', function () {
                let id = $(this).find('.solicitud-despacho-id').html();
                location.href = "~/Solicitud/Solicitud/" + id;
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
                { field: "SolicitudDespachoId", title: "#", responsive: { visible: "lg" }, template: function (e) { return '<span class="solicitud-despacho-id">' + e.SolicitudDespachoId + '</span>' } },
                { field: "NumeroSolicitud", title: "Numero Solicitud", responsive: { visible: "lg" } },
                { field: "FechaSolicitud", title: "Fecha/Hora", responsive: { visible: "lg" }, type: "date", format: "DD/MM/YYYY" },
                {
                    field: "EstadoSolicitud", title: "Estado", responsive: { visible: "lg" }, template: function (e, a, i) {
                        return '<span class="m-badge m-badge--info m-badge--wide" style="font-size: 1rem;"><b>' + e.EstadoSolicitud.Descripcion + '</b></span>';
                    }
                },
                { field: "TipoSolicitud.Descripcion", title: "Tipo Solicitud", responsive: { visible: "lg" } },
                { field: "Prioridad.descripcion", title: "Prioridad", responsive: { visible: "lg" } },
                { field: "Solicitante.Cliente.Nombre", title: "Cliente", responsive: { visible: "lg" } },
                { field: "Solicitante.NombreCompleto", title: "Solicitante", responsive: { visible: "lg" } }
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
        $("#rutCliente").inputmask({
            mask: "99999999-*", definitions: {
                '*': {
                    validator: "[0-9Kk]",
                    casing: "upper"
                }
            }
        });

        $("#telefonoContacto").inputmask({ mask: "+(56) 999999999" });
        $("#telefonoContactoAdicional").inputmask({ mask: "+(56) 999999999" });

        if ($('#formulario-solicitud').length > 0) {
            new mWizard("formulario-solicitud", {
                startStep: $('#seccionActual').val()
            });

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
        if (!_Validar()) return false;

        let partesRutCliente = $('#rutCliente').val().replace(/\./g, '').split('-');

        let solicitudId = $('#solicitudId').val();
        let numeroSolicitud = $('#numeroSolicitud').val();
        let estadoId = _ProximoEstado($('#estadoId').val());
        let tipoSolicitudId = $('#tipoSolicitudId').val();
        let fechaRecepcion = $('#fechaRecepcion').val();
        let numeroCliente = $('#numeroCliente').val();
        let nombreCliente = $('#nombreCliente').val();
        let calleDireccionCliente = $('#calleDireccionCliente').val();
        let numeroDireccionCliente = $('#numeroDireccionCliente').val();
        let regionClienteId = $('#regionClienteId').val();
        let comunaClienteId = $('#comunaClienteId').val();
        let telefonoContacto = $('#telefonoContacto').val();
        let telefonoContactoAdicional = $('#telefonoContactoAdicional').val();
        let rutCliente = partesRutCliente[0];
        let vrutCliente = partesRutCliente[1];
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

        $.post(webroot + "/Solicitud/" + (solicitudId > 0 ? "Edit" : "Create"), {
            solicitud: {
                SolicitudDespachoId: solicitudId,
                NumeroSolicitud: numeroSolicitud,
                TipoSolicitudId: tipoSolicitudId,
                EstadoSolicitudId: estadoId,
                FechaSolicitud: moment().format("DD/MM/YYYY HH:mm"),
                FechaRecepcion: fechaRecepcion,
                NumeroCliente: numeroCliente,
                NombreCliente: nombreCliente,
                CalleDireccionCliente: calleDireccionCliente,
                NumeroDireccionCliente: numeroDireccionCliente,
                RegionClienteId: regionClienteId,
                ComunaClienteId: comunaClienteId,
                NumeroTelefonoContacto: telefonoContacto,
                NumeroTelefonoContactoAdicional: telefonoContactoAdicional,
                RutCliente: rutCliente,
                VRutCliente: vrutCliente,
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
                mensaje("Éxito", "Información guardada correctamente", "exito", function () { location.href = "~/Solicitud/Index"; });
            } else {
                mensaje("Error", "No se pudo guardar la información", "error");
            }
        });
    };

    let _CargarListaComunas = function (regionId) {
        $.post(webroot + '/Solicitud/ObtenerComunas', { regionId: regionId }, function (comunas) {
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
        let proximoEstado = parseInt(estadoActual);

        if (estadoActual == 0) {
            proximoEstado = EquiposSolicitados.Validar() ? 1 : 2;
        } else if (estadoActual == 1) {
            proximoEstado = 3;
        } else if (estadoActual == 2) {
            proximoEstado = EquiposSolicitados.Validar() ? 1 : 2;
        } else {
            proximoEstado += 1;
        }

        return proximoEstado;
    };

    let _Validar = function () {
        let errores = [];

        errores = errores.concat(_ValidarSolicitud());
        errores = errores.concat(_ValidarPlanificacion());
        errores = errores.concat(_ValidarDocumentacion());
        errores = errores.concat(_ValidarConcrecion());
        errores = errores.concat(_ValidarAprobacion());

        console.log(errores);

        if (errores.length > 0) {
            alert("Hay errores");
        }

        return errores.length == 0;
    };

    let _ValidarSolicitud = function () {
        let errores = [];

        if ($('#numeroSolicitud').length > 0 && !$('#numeroSolicitud').val()) errores.push("Debe ingresar el número de solicitud.");
        if ($('#tipoSolicitudId').length > 0 && !$('#tipoSolicitudId').val()) errores.push("Debe seleccionar el tipo de solicitud.");
        if ($('#fechaRecepcion').length > 0 && !$('#fechaRecepcion').val()) errores.push("Debe ingresar la fecha de recepción.");
        if ($('#numeroCliente').length > 0 && !$('#numeroCliente').val()) errores.push("Debe ingresar el número del cliente.");
        if ($('#nombreCliente').length > 0 && !$('#nombreCliente').val()) errores.push("Debe ingresar el nombre del cliente.");
        if ($('#calleDireccionCliente').length > 0 && !$('#calleDireccionCliente').val()) errores.push("Debe ingresar la calle de la dirección del cliente.");
        if ($('#numeroDireccionCliente').length > 0 && !$('#numeroDireccionCliente').val()) errores.push("Debe ingresar el número de la dirección del cliente.");
        if ($('#regionClienteId').length > 0 && !$('#regionClienteId').val()) errores.push("Debe seleccionar la región del cliente.");
        if ($('#comunaClienteId').length > 0 && !$('#comunaClienteId').val()) errores.push("Debe seleccionar la comuna del cliente.");
        if ($('#telefonoContacto').length > 0 && !$('#telefonoContacto').val()) errores.push("Debe ingresar el teléfono de contacto.");
        if ($('#rutCliente').length > 0 && !General.ValidaRut($('#rutCliente').val())) errores.push("El RUT del cliente es inválido.");
        if ($('#prioridadId').length > 0 && !$('#prioridadId').val()) errores.push("Debe seleccionar una prioridad.");
        if ($('#unidadNegocioId').length > 0 && !$('#unidadNegocioId').val()) errores.push("Debe seleccionar una unidad de negocio.");
        if ($('#gerenciaId').length > 0 && !$('#gerenciaId').val()) errores.push("Debe seleccionar una gerencia.");
        if (EquiposSolicitados.getEquipos().length == 0) errores.push("Debe solicitar al menos un equipo.");
        if ($('#estadoId').val() == '2' && !EquiposSolicitados.Validar()) errores.push("Debe ingresar las placas de todos los equipos.");

        return errores;
    };

    let _ValidarPlanificacion = function () {
        let errores = [];
        return errores;
    };

    let _ValidarDocumentacion = function () {
        let errores = [];
        return errores;
    };

    let _ValidarConcrecion = function () {
        let errores = [];
        return errores;
    };

    let _ValidarAprobacion = function () {
        let errores = [];
        return errores;
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
            $.post(webroot + "/Solicitud/ObtenerEquiposSolicitados", { solicitudId: $('#solicitudId').val() }, function (data) {
                equipos = data;
                _CargarLista();
            });
        }

        if ($('#solicitarEquipos').length > 0) {
            $('#solicitarEquipos').click(function () {
                _AgregarEquipos();
            });
        }

        let input;
        let modelo;

        $(document).on('click', '.numero-placa', function (e) {
            input = $(e.target);
            modelo = $(e.target).parents('tr').find('.modelo').val();

            //Bodega.Filtrar(null, null, null, modelo);
            //Bodega.DibujarBodegas();

            $('#modalSeleccionPlaca').modal('show');
        });

        $(document).on('click', '#modalSeleccionPlaca .producto', function () {
            let prod = $(this);

            input.val(prod.find('.placa').text());

            $('#modalSeleccionPlaca').modal('hide');
        });

        $('#modalSeleccionPlaca').on('hidden.bs.modal', function () {
            Bodega.DibujarBodegas();
        });
    };

    let _CargarLista = function () {
        let columnas = [
            {
                field: "NumeroPlaca", title: "Placa", responsive: { visible: "lg" }, template: function (e) {
                    let id = '<input type="hidden" class="form-control id" value="' + (e.EquipoSolicitadoId || '0') + '" />';
                    let placa = (e.NumeroPlaca || $('#seccionActual').val() > 1) ? '<input type="hidden" class="form-control numero-placa" value="' + e.NumeroPlaca + '" />' + (e.NumeroPlaca || 'Sin Placa') : '<input type="text" class="form-control numero-placa" value="' + (e.NumeroPlaca || '') + '" />';
                    return id + placa
                }
            },
            { field: "Modelo", title: "Modelo", responsive: { visible: "lg" }, template: function (e) { return '<input type="hidden" class="form-control modelo" value="' + (e.Modelo || '') + '" />' + (e.Modelo || '') } },
            { field: "EstadoEquipo", title: "Estado", responsive: { visible: "lg" }, template: function (e) { return '<input type="hidden" class="form-control estado" value="' + (e.EstadoEquipo.Descripcion || '') + '" />' + (e.EstadoEquipo.Descripcion || '') } }
        ];

        // SI LA SECCIÓN ACTUAL ES SOLICITUD Y El USUARIO ES CLIENTE SE MUESTRA LA COLUMNA ELIMINAR
        if ($('#perfilUsuario').val() == 4 && $('#seccionActual').val() == 1) {
            columnas.push({
                field: "Eliminar", title: "Eliminar", responsive: { visible: "lg" }, template: function (e) {
                    return e.NumeroPlaca ? '' : '<a href="#lista-equipos-solicitados" onclick="EquiposSolicitados.Eliminar(' + e.EquipoSolicitadoId + ')" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title="Eliminar"><i class="la la-trash"></i></a>';
                }
            });
        }

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
                EstadoEquipo: { Descripcion: estado },
                SolicitudDespachoId: solicitudId
            });
        }

        tabla.originalDataSet = equipos;
        tabla.load();

        $('#modalEquipoSolicitado').modal('hide');

        _Limpiar();
    };

    let _Validar = function () {
        return equipos.filter((equipo) => {
            return !equipo.NumeroPlaca;
        }).length == 0;
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
            $.post(webroot + "/Solicitud/ObtenerPersonalAsignado", { solicitudId: $('#solicitudId').val() }, function (data) {
                personal = data;
                _CargarLista();
            });
        }

        if ($('#solicitarPersonal').length > 0) {
            $('#solicitarPersonal').click(function () {
                _AgregarEquipos();
            });
        }
    };

    let _CargarLista = function () {
        $('#lista-personal-asignado').mDatatable({
            data: {
                type: "local",
                source: personal,
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
                { field: "Personal.Nombre", title: "Nombre", responsive: { visible: "lg" }, template: function (e, a, i) { return '<input type="hidden" class="form-control personalid" value="' + (e.PersonalId || '') + '" />' + (e.Personal.NombreCompleto || '') } },
                { field: "Personal.TipoPersonal", title: "Tipo de Personal", responsive: { visible: "lg" }, template: function (e, a, i) { return '<input type="hidden" class="form-control tipopersonalid" value="' + (e.Tipopersonalid || '') + '" />' + (e.Personal.TipoPersonal || '') } }
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
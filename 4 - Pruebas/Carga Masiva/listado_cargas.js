let CargaMasiva = function () {

    let tablaAmbulatorio;
    let tablaDetalleAmbulatorio;

    let Init = function () {
        CapaDatos.init(() => {
            InitElementos();

            _CargarTablaAmbulatorio();
            _CargarResponsablesAmbulatorio();
        });
    };

    let InitElementos = function () {
        let detalleActual;

        tablaAmbulatorio = $('#listadoCargasMasivasAmbulatorio').DataTable({
            pageLength: 5,
            lengthChange: false,
            order: [[1, 'desc']],
            language: {
                decimal: ",",
                emptyTable: "No hay información",
                info: "Mostrando _START_ a _END_ de _TOTAL_ Elementos",
                infoEmpty: "Mostrando 0 to 0 of 0 Entradas",
                infoFiltered: "(Filtrado de _MAX_ total entradas)",
                thousands: ".",
                lengthMenu: "Ver _MENU_",
                loadingRecords: "Cargando...",
                processing: "Procesando...",
                search: "Buscar:",
                zeroRecords: "Sin resultados encontrados",
                paginate: {
                    first: "Primero",
                    last: "Ultimo",
                    next: "Siguiente",
                    previous: "Anterior"
                }
            },
            columnDefs: [
                {
                    targets: [0],
                    visible: false
                },
                {
                    targets: -1,
                    orderable: false,
                    render: function (e, a, t, n) {
                        let div = $('<div></div>');
                        div.append('<button type="button" class="btn btn-info revisar-carga" data-id="' + t[0] + '" data-toggle="modal" data-target="#modalDetalleCargaAmbulatorio"><i class="fas fa-search"></i></button>');

                        return div.html();
                    }
                }
            ]
        });

        tablaDetalleAmbulatorio = $('#listadoDetalleCargaAmbulatorio').DataTable({
            pageLength: 5,
            lengthChange: false,
            language: {
                decimal: ",",
                emptyTable: "No hay información",
                info: "Mostrando _START_ a _END_ de _TOTAL_ Elementos",
                infoEmpty: "Mostrando 0 to 0 of 0 Entradas",
                infoFiltered: "(Filtrado de _MAX_ total entradas)",
                thousands: ".",
                lengthMenu: "Ver _MENU_",
                loadingRecords: "Cargando...",
                processing: "Procesando...",
                search: "Buscar:",
                zeroRecords: "Sin resultados encontrados",
                paginate: {
                    first: "Primero",
                    last: "Ultimo",
                    next: "Siguiente",
                    previous: "Anterior"
                }
            },
            columnDefs: [
                { targets: 0, visible: false },
                {
                    targets: 1,
                    orderable: false,
                    render: function (e, a, t, n) {
                        let error = '<i class="fa fa-times px-3" style="color: #DC3C41;font-size: 2rem;"></i>';
                        let correcto = '<i class="fa fa-check px-3" style="color: #34BFA3;font-size: 2rem;"></i>';

                        return t[2].filter((e) => { return e.tipo == 'danger'; }).length > 0 ? error : correcto;
                    }
                },
                {
                    targets: 2,
                    orderable: false,
                    render: function (e, a, t, n) {
                        let div = $('<div></div>');
                        let boton = $('<button class="btn btn-sm btn-primary detalle-estados w-100" data-toggle="tooltip" data-placement="right" data-trigger="click" data-html="true" title="Tooltip on <b>right</b>"></button>');
                        let estados = t[2];
                        let actions = t[0];

                        let tooltip = $('<div></div>');

                        if (estados.length > 0) {
                            if (estados.length > 1) {
                                boton.html('Se encontraron <b>' + estados.length + '</b> errors <b>ver aquí</b>');
                            } else {
                                boton.html('Se encontró <b>' + estados.length + '</b> error <b>ver aquí</b>');
                            }

                            estados.forEach((e) => {
                                tooltip.append('<li>' + e.title + '</li>');
                            });
                        } else {
                            if (actions.length > 1) {
                                boton.html('Se realizaron <b>' + actions.length + '</b> procesos <b>ver aquí</b>');
                            } else {
                                boton.html('Se realizó <b>' + actions.length + '</b> proceso <b>ver aquí</b>');
                            }

                            actions.forEach((a) => {
                                if (a == 1) {
                                    tooltip.append('<li>Se creó el paciente</li>');
                                }

                                if (a == 2) {
                                    tooltip.append('<li>Se agregó el dispositivo al paciente</li>');
                                }

                                if (a == 3) {
                                    tooltip.append('<li>Se agregaron los procedimientos al dispositivo</li>');
                                }
                            });
                        }

                        boton.attr('title', tooltip.html());
                        div.html(boton);
                        return div.html();
                    }
                }
            ]
        });

        $('.boton-carga-masiva:not(.disabled)').click(function () {
            let target = $(this).data('target');

            $('.contenido-carga-masiva').hide();
            $(target).show();

            $('.boton-carga-masiva').removeClass('activo');
            $(this).addClass('activo');
        });

        $('#modalDetalleCargaAmbulatorio').on('show.bs.modal', function (e) {
            let idCarga = $(e.relatedTarget).attr('data-id');
            detalleActual = CapaDatos.ObtenerCargasMasivas(idCarga);

            _CargarDetalleAmbulatorio(detalleActual);

            tablaDetalleAmbulatorio.columns.adjust();
        });

        $('#modalDetalleCargaAmbulatorio').on('shown.bs.modal', function (e) {
            $.fn.dataTable.tables({ visible: true, api: true }).columns.adjust();
        });

        $(document).on('click', '.page-link', function () {
            $('.detalle-estados').tooltip();
        });

        $('#txtBuscarAmbulatorio').keyup(function () {
            tablaAmbulatorio.search(this.value).draw();
        });

        $('#txtBuscarDetalleAmbulatorio').keyup(function () {
            tablaDetalleAmbulatorio.search(this.value).draw();
        });

        $('input[name="resutadoRegistroAmbulatorio"]').click(function () {
            _CargarDetalleAmbulatorio(detalleActual);
        });

        $('#responsableCargaAmbulatorio').change(function () {
            _CargarTablaAmbulatorio();
        });

        $('#fechaDesdeCargaAmbulatorio').change(function () {
            _CargarTablaAmbulatorio();
        });

        $('#fechaHastaCargaAmbulatorio').change(function () {
            _CargarTablaAmbulatorio();
        });
    };

    let _CargarTablaAmbulatorio = function () {
        let responsableSeleccionado = $('#responsableCargaAmbulatorio').val();
        let fechaDesde = moment($('#fechaDesdeCargaAmbulatorio').val(), 'DD/MM/YYYY');
        let fechaHasta = moment($('#fechaHastaCargaAmbulatorio').val(), 'DD/MM/YYYY');

        let cargas = CapaDatos.ObtenerCargasMasivas().filter((c) => {
            return c.tipo.id == ModuloVigilancia.TiposCargaMasiva.ambulatorio.id;
        });

        tablaAmbulatorio.clear();
        let data = [];

        cargas.forEach((carga) => {
            let responsable = CapaDatos.ObtenerUsuario(carga.responsable);

            if (responsableSeleccionado != '0' && responsableSeleccionado != carga.responsable) {
                return;
            }

            let fechaCarga = moment(carga.fechaHora, 'DD/MM/YYYY');

            if (fechaDesde.isValid() && fechaHasta.isValid() && fechaDesde <= fechaHasta && (fechaDesde > fechaCarga || fechaHasta < fechaCarga)) {
                return;
            }

            let buenos = carga.registros.filter((r) => { return r.estados.length == 0; });
            let malos = carga.registros.filter((r) => { return r.estados.length > 0; });

            let valores = [
                carga.idCargaMasiva,
                carga.fechaHora,
                responsable.nombre,
                carga.nombreArchivo || '',
                buenos.length,
                malos.length,
                null
            ];

            data.push(valores);
        });

        tablaAmbulatorio.rows.add(data);
        tablaAmbulatorio.draw();
    };

    let _CargarDetalleAmbulatorio = function (carga) {
        $('#fechaHoraDetalle').html(carga.fechaHora);

        let responsable = CapaDatos.ObtenerUsuario(carga.responsable);
        $('#responsableDetalle').html(responsable.nombre);
        $('#archivoDetalle').html(carga.nombreArchivo || '');

        let buenos = carga.registros.filter((r) => { return r.estados.length == 0; }).length;
        let malos = carga.registros.filter((r) => { return r.estados.length > 0; }).length;

        $('#cantidadBuenosDetalle').html(buenos + ' Buenos');
        $('#cantidadMalosDetalle').html(malos + ' Malos');


        tablaDetalleAmbulatorio.clear();
        let data = [];

        carga.registros.forEach((registro) => {
            if ($('input[name="resutadoRegistroAmbulatorio"][value="1"]:checked').val() == undefined && registro.estados.length > 0) {
                return;
            }

            if ($('input[name="resutadoRegistroAmbulatorio"][value="2"]:checked').val() == undefined && registro.estados.length == 0) {
                return;
            }

            let mes = ModuloVigilancia.Meses().obtener({ numero: registro.mes });

            let valores = [
                registro.acciones || [],
                null,
                registro.estados || [],
                registro.tipoIdentificador || '',
                registro.identificador || '',
                registro.dv || '',
                registro.nombres || '',
                registro.apat || '',
                registro.amat || '',
                registro.fechaNacimiento || '',
                registro.dispositivo || '',
                registro.fechaInstalacion || '',
                mes ? mes.nombre : (registro.mes || ''),
                registro.anno || '',
                registro.cantidad || ''
            ];

            data.push(valores);
        });

        tablaDetalleAmbulatorio.rows.add(data);
        tablaDetalleAmbulatorio.draw();

        $('.detalle-estados').tooltip();
    };

    let _CargarResponsablesAmbulatorio = function () {
        let cargas = CapaDatos.ObtenerCargasMasivas().filter((c) => {
            return c.tipo.id == ModuloVigilancia.TiposCargaMasiva.ambulatorio.id;
        });

        $('#responsableCargaAmbulatorio').html('<option value="0">Todos</option>');
        let responsables = [];

        cargas.forEach((carga) => {
            let responsable = CapaDatos.ObtenerUsuario(carga.responsable);

            if (!responsables.contiene(responsable.usuario)) {
                $('#responsableCargaAmbulatorio').append('<option value="' + responsable.usuario + '">' + responsable.nombre + '</option>');
                responsables.push(responsable.usuario);
            }
        });
    };

    return {
        init: function () {
            Init();
        }
    }

}();

$(document).ready(function () {
    CargaMasiva.init();
});
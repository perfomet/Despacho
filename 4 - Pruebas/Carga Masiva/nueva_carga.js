let estados = {
    tipoIdentificadorIncorrecto: { id: 1, title: "Tipo de identificador incorrecto", class: "m-badge--danger", tipo: 'danger' },
    faltaIdentificador: { id: 2, title: "Falta el campo 'Identificador'", class: "m-badge--danger", tipo: 'danger' },
    faltaDv: { id: 3, title: "Falta el campo 'Dígito Verificador(DV)'", class: "m-badge--danger", tipo: 'danger' },
    rutInvalido: { id: 4, title: "El Rut ingresado no es válido", class: "m-badge--danger", tipo: 'danger' },
    faltaNombre: { id: 5, title: "Falta el campo 'Nombres'", class: "m-badge--danger", tipo: 'danger' },
    fechaNacimientoNoValida: { id: 6, title: "La Fecha de Nacimiento debe estar en formato DD/MM/AAAA", class: "m-badge--danger", tipo: 'danger' },
    dispositivoIncorrecto: { id: 7, title: "El Dispositivo seleccionado no es válido (QMT o CHD)", class: "m-badge--danger", tipo: 'danger' },
    fechaInstalacionNoValida: { id: 8, title: "La Fecha de Instalacion debe estar en formato DD/MM/AAAA", class: "m-badge--danger", tipo: 'danger' },
    procedimientoAnterior: { id: 9, title: "El período del procedimiento es anterior a la fecha de instalación", class: "m-badge--danger", tipo: 'danger' },
    mesNoValido: { id: 10, title: "El Mes debe ser un número entre 1 y 12", class: "m-badge--danger", tipo: 'danger' },
    annoNoValido: { id: 11, title: "El Año debe ser un número de 4 dígitos no mayor al año actual", class: "m-badge--danger", tipo: 'danger' },
    cantidadNoValida: { id: 12, title: "La Cantidad debe ser un número entre 1 y 30", class: "m-badge--danger", tipo: 'danger' },
    pacienteCreado: { id: 13, title: "Paciente creado", class: "m-badge--success", tipo: 'success' },
    disitivoAgregado: { id: 14, title: "Dispositivo agregado", class: "m-badge--success", tipo: 'success' },
    procedimientosCargados: { id: 15, title: "Procedimientos cargados", class: "m-badge--success", tipo: 'success' },
    fechaNacimientoPosterior: { id: 16, title: "La Fecha de Nacimiento ingresada es posterior a la fecha actual", class: "m-badge--danger", tipo: 'danger' },
    fechaInstalacionPosterior: { id: 17, title: "La Fecha de Instalación ingresada es posterior a la fecha actual", class: "m-badge--danger", tipo: 'danger' },
    periodoPosterior: { id: 18, title: "La Período es posterior al mes y/o año actual", class: "m-badge--danger", tipo: 'danger' },
    chdSoloAdulto: { id: 19, title: "El dispositivo CHD solo se puede instalar en pacientes mayores de 15 Años", class: "m-badge--danger", tipo: 'danger' },
    qmtAdultoPediatrico: { id: 20, title: "El dispositivo QMT solo se puede instalar en pacientes mayores de 29 Días", class: "m-badge--danger", tipo: 'danger' },
    faltaDispositivo: { id: 21, title: "Falta el campo 'Dispositivo'", class: "m-badge--danger", tipo: 'danger' },
    faltaMes: { id: 22, title: "Falta el campo 'Mes'", class: "m-badge--danger", tipo: 'danger' },
    faltaAnno: { id: 23, title: "Falta el campo 'Año'", class: "m-badge--danger", tipo: 'danger' },
    faltaCantidad: { id: 24, title: "Falta el campo 'Cantidad'", class: "m-badge--danger", tipo: 'danger' }
};

let acciones = {
    crearPaciente: 1,
    ingresarDispositivo: 2,
    agregarProcedimientos: 3
};

let CargaMasivaAmbulatorio = function () {
    let tabla;
    let registros = [];

    let InitCargaMasivaAmbulatorio = function () {
        CapaDatos.init(() => {
            InitElementos();
        });
    };

    let InitElementos = function () {
        tabla = $("#tabla").DataTable({
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
                                if (a == acciones.crearPaciente) {
                                    tooltip.append('<li>Se creó el paciente</li>');
                                }

                                if (a == acciones.ingresarDispositivo) {
                                    tooltip.append('<li>Se agregó el dispositivo al paciente</li>');
                                }

                                if (a == acciones.agregarProcedimientos) {
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

        let nombreArchivo;

        $('#archivoCarga').change((oEvent) => {
            // Get The File From The Input
            let archivo = oEvent.target.files[0];

            if (archivo) {
                nombreArchivo = archivo.name;

                $('label[for="archivoCarga"]').html(nombreArchivo);

                let reader = new FileReader();

                reader.onload = function (e) {
                    ModuloVigilancia.MostrarCargando();

                    try {
                        let data = e.target.result;
                        let workbook = XLSX.read(data, {
                            type: 'binary'
                        });

                        let filas = XLSX.utils.sheet_to_row_object_array(workbook.Sheets[workbook.SheetNames[0]]);
                        let columnas = _ObtenerColumnas(workbook.Sheets[workbook.SheetNames[0]]);

                        if (!_VerificarFormato(columnas)) {
                            ModuloVigilancia.OcultarCargando();
                            ModuloVigilancia.AlertaError('El formato del archivo seleccionado no es correcto, verifique el archivo.', '¡Atención!');
                            return;
                        }

                        if (filas && filas.length == 0) {
                            ModuloVigilancia.OcultarCargando();
                            ModuloVigilancia.AlertaError('El archivo cargado está vacío, ingrese información y cárguelo nuevamente.', '¡Atención!');
                            return;
                        }

                        registros = [];

                        filas.forEach((objeto) => {
                            registros.push({
                                tipoIdentificador: objeto['TipoIdentificador'],
                                identificador: objeto['Identificador'],
                                dv: objeto['DV'],
                                nombres: objeto['Nombres'],
                                apat: objeto['ApellidoPaterno'],
                                amat: objeto['ApellidoMaterno'],
                                fechaNacimiento: objeto['FechaNacimiento'],
                                dispositivo: objeto['Dispositivo'],
                                fechaInstalacion: objeto['FechaInstalacion'],
                                mes: objeto['Mes'],
                                anno: objeto['Año'],
                                cantidad: objeto['Cantidad']
                            });
                        });

                        $('#btnProcesarRegistros').addClass('pulso-guardar');
                        $('#alertaProcesar').show();
                    } catch (ex) {
                        console.log(ex);
                        ModuloVigilancia.AlertaError('No se pudo obtener la información del archivo seleccionado, verifique el archivo.', '¡Atención!');
                    }

                    $('#archivoCarga').val(null);
                    ModuloVigilancia.OcultarCargando();
                };

                reader.onerror = function (ex) {
                    console.log(ex);
                    ModuloVigilancia.AlertaError('No se pudo obtener la información del archivo seleccionado, verifique el archivo.', '¡Atención!');
                    ModuloVigilancia.OcultarCargando();
                    $('#archivoCarga').val(null);
                };

                reader.readAsBinaryString(archivo);
            }
        });

        $('#btnProcesarRegistros').click(() => {
            _ProcesarRegistros(nombreArchivo);

            $('#archivoCarga').val(null);
            $('label[for="archivoCarga"]').html('Seleccione un archivo...');
            $('#alertaProcesar').hide();
        });

        $(document).on('click', '.page-link', function () {
            $('.detalle-estados').tooltip();
        });

        $('#txtBuscar').keyup(function () {
            tabla.search(this.value).draw();
        });

        $('input[name="resutadoRegistro"]').click(function () {
            _CargarTabla();
        });
    };

    let _ProcesarRegistros = function (nombreArchivo) {
        try {
            if (registros.length == 0) {
                ModuloVigilancia.AlertaInfo('Debe cargar un archivo primero', '¡Atención!');
                return;
            }

            registros.forEach((registro) => {
                // VACÍA LAS LISTAS DE ESTADOS Y ACCIONES
                registro.estados = [];
                registro.acciones = [];

                // VALIDA QUE EL TIPO DE IDENTIFICADOR EXISTA Y SEA VÁLIDO
                if (
                    registro.tipoIdentificador && registro.tipoIdentificador.toUpperCase() != 'RUT' &&
                    registro.tipoIdentificador.toUpperCase() != 'PASAPORTE' &&
                    registro.tipoIdentificador.toUpperCase() != 'PROVISORIA'
                ) {
                    registro.estados.push(estados.tipoIdentificadorIncorrecto);
                }

                // VALIDA QUE EL IDENTIFICADOR EXISTA
                if (!registro.identificador) {
                    registro.estados.push(estados.faltaIdentificador);
                }

                // SI EL TIPO DE IDENTIFICADOR ES 'RUT', VALIDA QUE EL DÍGITO VERIFICADOR EXISTA
                if (registro.tipoIdentificador.toUpperCase() == 'RUT' && !registro.dv) {
                    registro.estados.push(estados.faltaDv);
                }

                // VALIDA QUE EL DISPOSITIVO EXISTA
                if (!registro.dispositivo) {
                    registro.estados.push(estados.faltaDispositivo);
                }

                // VALIDA QUE EL MES EXISTA
                if (!registro.mes) {
                    registro.estados.push(estados.faltaMes);
                }

                // VALIDA QUE EL AÑO EXISTA
                if (!registro.anno) {
                    registro.estados.push(estados.faltaAnno);
                }

                // VALIDA QUE LA CANTIDAD EXISTA
                if (!registro.cantidad) {
                    registro.estados.push(estados.faltaCantidad);
                }

                // SI EL TIPO DE IDENTIFICADOR ES 'RUT', Y EXISTEN LOS DATOS DE IDENTIFICACIÓN, VALIDA QUE EL RUT SEA VÁLIDO
                if (
                    !registro.estados.contiene(estados.tipoIdentificadorIncorrecto) &&
                    !registro.estados.contiene(estados.faltaIdentificador) &&
                    !registro.estados.contiene(estados.faltaDv) &&
                    registro.tipoIdentificador.toUpperCase() == 'RUT'
                ) {
                    let rut = registro.identificador + '-' + registro.dv;
                    if (!ModuloVigilancia.ValidaRut(rut)) {
                        registro.estados.push(estados.rutInvalido);
                    }
                }

                // SI NO HAY ERRORES DE VALIDACIÓN, PROSIGUE CON LA VALIDACIÓN DEL PACIENTE
                if (
                    !registro.estados.contiene(estados.tipoIdentificadorIncorrecto) &&
                    !registro.estados.contiene(estados.faltaIdentificador) &&
                    !registro.estados.contiene(estados.faltaDv) &&
                    !registro.estados.contiene(estados.rutInvalido) &&
                    !registro.estados.contiene(estados.faltaDispositivo) &&
                    !registro.estados.contiene(estados.faltaMes) &&
                    !registro.estados.contiene(estados.faltaAnno) &&
                    !registro.estados.contiene(estados.faltaCantidad)
                ) {
                    // INTENTA OBTENER AL PACIENTE
                    let identificador = registro.identificador;
                    if (registro.tipoIdentificador.toUpperCase() == 'RUT') identificador += '-' + registro.dv;
                    let paciente = CapaDatos.ObtenerPacientes(identificador);

                    // SI NO EXISTE EL PACIENTE, VALIDAMOS LOS DATOS PARA CREARLO
                    if (!paciente) {
                        // VALIDA SI EXISTE EL NOMBRE DEL PACIENTE
                        if (!registro.nombres) {
                            registro.estados.push(estados.faltaNombre);
                        }

                        // OBTIENE LA FECHA DE NACIMIENTO
                        let fechaNacimiento = moment(registro.fechaNacimiento, 'DD/MM/YYYY', true);

                        // VALIDA QUE LA FECHA DE NACIMIENTO SEA UNA FECHA VÁLIDA EN FORMATO 'DD/MM/YYYY'
                        if (!fechaNacimiento.isValid()) {
                            registro.estados.push(estados.fechaNacimientoNoValida);
                        }

                        // SI LA FECHA DE NACIMIENTO EN VÁLIDA, VALIDA QUE ESTA NO SEA POSTERIOR A LA FECHA ACTUAL
                        if (!registro.estados.contiene(estados.fechaNacimientoNoValida) && fechaNacimiento > moment(moment().format('DD/MM/YYYY'), 'DD/MM/YYYY')) {
                            registro.estados.push(estados.fechaNacimientoPosterior);
                        }

                        // SI NO HAY ERRORES DE VALIDACIÓN, AGREGA LA ACCIÓN DE CREAR PACIENTE AL REGISTRO
                        if (
                            !registro.estados.contiene(estados.faltaNombre) &&
                            !registro.estados.contiene(estados.fechaNacimientoNoValida) &&
                            !registro.estados.contiene(estados.fechaNacimientoPosterior)
                        ) {
                            registro.acciones.push(acciones.crearPaciente);
                        }
                    }

                    // SI EXISTE EL PACIENTE O SE CREARÁ, VALIDAMOS LOS DATOS PARA EL DISPOSITVO Y SUS PROCEDIMIENTOS
                    if (paciente || registro.acciones.contiene(acciones.crearPaciente)) {
                        // VALIDA SI EL DISPOSITIVO ES 'CHD' O 'QMT'
                        if (registro.dispositivo != 'CHD' && registro.dispositivo != 'QMT') {
                            registro.estados.push(estados.dispositivoIncorrecto);
                        }

                        // SI DISPOSITVO INGRESADO ES VÁLIDO
                        if (!registro.estados.contiene(estados.dispositivoIncorrecto)) {
                            // OBTIENE EL DISPOSITVO DEL PACIENTE
                            let dispositivo = _ObtenerDispositivoPaciente(registro, paciente);

                            //SI EL PACIENTE NO TIENE ESE DISPOSITIVO INSTALADO
                            if (!dispositivo) {
                                // OBTENEMOS LA FECHA DE INSTALACIÓN DEL REGISTRO
                                let fechaInstalacion = moment(registro.fechaInstalacion, 'DD/MM/YYYY', true);

                                // VALIDA QUE LA FECHA DE INSTALACIÓN SEA VÁLIDA EN FORMATO 'DD/MM/YYYY'
                                if (!fechaInstalacion.isValid()) {
                                    registro.estados.push(estados.fechaInstalacionNoValida);
                                }

                                // SI LA FECHA ES VÁLIDA, VALIDA QUE ESTA NO SEA POSTERIOR A LA FECHA ACTUAL
                                if (!registro.estados.contiene(estados.fechaInstalacionNoValida) && fechaInstalacion > moment(moment().format('DD/MM/YYYY'), 'DD/MM/YYYY')) {
                                    registro.estados.push(estados.fechaInstalacionPosterior);
                                }

                                let edadEnDias = moment().diff(moment(registro.fechaNacimiento, 'DD/MM/YYYY'), 'days');
                                let rangoEtario = CapaDatos.ObtenerRangoEtario({ edadEnDias: edadEnDias }).idRangoEtario;

                                if (registro.dispositivo == 'CHD' && rangoEtario != 'RE04') {
                                    registro.estados.push(estados.chdSoloAdulto);
                                }

                                if (registro.dispositivo == 'QMT' && rangoEtario != 'RE04' && rangoEtario != 'RE03' && rangoEtario != 'RE02') {
                                    registro.estados.push(estados.qmtAdultoPediatrico);
                                }

                                // SI NO HAY ERRORES DE VALIDACIÓN, AGREGA LA ACCIÓN DE INGRESAR DISPOSITIVO AL REGISTRO
                                if (
                                    !registro.estados.contiene(estados.fechaInstalacionNoValida) &&
                                    !registro.estados.contiene(estados.fechaInstalacionPosterior) &&
                                    !registro.estados.contiene(estados.chdSoloAdulto) &&
                                    !registro.estados.contiene(estados.qmtAdultoPediatrico)
                                ) {
                                    registro.acciones.push(acciones.ingresarDispositivo);
                                }
                            }

                            // SI EXISTE EL DISPOSITIVO O SE CREARÁ, VALIDAMOS LOS DATOS PARA AGREGAR LOS PROCEDIMIENTOS
                            if (dispositivo || registro.acciones.contiene(acciones.ingresarDispositivo)) {
                                // VALIDA QUE EL MES INGRESADO SEA UN NÚMERO ENTRE 0 Y 12
                                if (isNaN(registro.mes) || registro.mes > 12 || registro.mes < 0) {
                                    registro.estados.push(estados.mesNoValido);
                                }

                                // VALIDA QUE EL AÑO INGRESADO SEA UN NÚMERO Y NO SEA MENOR A 1850 NI MAYOR QUE EL AÑO ACTUAL 
                                if (isNaN(registro.anno) || registro.anno < 1850 || registro.anno > moment().get('year')) {
                                    registro.estados.push(estados.annoNoValido);
                                }

                                // VALIDA QUE EL PERÍODO INGRESADO NO SEA POSTERIOR AL PERÍODO ACTUAL
                                if (moment(registro.mes + '/' + registro.anno, 'MM/YYYY') > moment(moment().format('MM/YYYY'), 'MM/YYYY')) {
                                    registro.estados.push(estados.periodoPosterior);
                                }

                                // VALIDA QUE LA CANTIDAD DE PROCEDIMIENTOS SEA UN NÚMERO ENTRE 1 Y 30
                                if (isNaN(registro.cantidad) || registro.cantidad < 1 || registro.cantidad > 30) {
                                    registro.estados.push(estados.cantidadNoValida);
                                }

                                // OBTIENE EL MES Y AÑO DE INSTALACIÓN DEL DISPOSITIVO
                                let fechaInstalacion = moment(registro.fechaInstalacion, 'DD/MM/YYYY');
                                let mesInst = fechaInstalacion.get('month') + 1;
                                let annoInst = fechaInstalacion.get('year');

                                // VALIDA QUE EL PERÍODO DEL PROCEDIMIENTO NO SEA ANTERIOR A LA INSTALACIÓN DEL DISPOSITIVO
                                if (annoInst > registro.anno || (annoInst == registro.anno && mesInst > registro.mes)) {
                                    registro.estados.push(estados.procedimientoAnterior);
                                }

                                // SI NO HAY ERRORES DE VALIDACIÓN, AGREGA LA ACCIÓN DE AGREGAR PROCEDIMIENTOS AL REGISTRO
                                if (
                                    !registro.estados.contiene(estados.mesNoValido) &&
                                    !registro.estados.contiene(estados.annoNoValido) &&
                                    !registro.estados.contiene(estados.periodoPosterior) &&
                                    !registro.estados.contiene(estados.cantidadNoValida)
                                ) {
                                    registro.acciones.push(acciones.agregarProcedimientos);
                                }
                            }
                        }
                    }
                }

                _GuardarDatos(registro);
            });

            let cargaMasiva = {
                idCargaMasiva: CapaDatos.ObtenerMaxSecuencia(),
                fechaHora: moment().format('DD/MM/YYYY HH:mm'),
                responsable: sessionStorage["usuarioLogueado"],
                registros: registros,
                tipo: ModuloVigilancia.TiposCargaMasiva.ambulatorio,
                nombreArchivo: nombreArchivo
            };

            CapaDatos.InsertarCargaMasiva(cargaMasiva);

            _CargarTabla();

            $('.detalle-estados').tooltip();

            $('#divTablaResultado').show();

            ModuloVigilancia.AlertaExito('Archivo procesado, para ver detalle ir a histórico de cargas', '¡Atención!');
        } catch (ex) {
            console.log(ex);
            ModuloVigilancia.AlertaError('No se pudo procesar la información, verifique el archivo.', '¡Atención!');
        }

        $('#btnProcesarRegistros').removeClass('pulso-guardar');
    };

    let _GuardarDatos = function (registro) {
        if (_ObtenerEstadosMalos(registro).length == 0) {
            let paciente;

            if (registro.acciones.contiene(acciones.crearPaciente)) {
                let identificador = registro.identificador;
                if (registro.tipoIdentificador.toUpperCase() == 'RUT') identificador += '-' + registro.dv;
                paciente = CapaDatos.ObtenerPacientes(identificador);

                if (!paciente) {
                    paciente = _CrearPaciente(registro);
                }
            }

            let dispositivo;

            if (paciente && registro.acciones.contiene(acciones.ingresarDispositivo)) {
                dispositivo = _ObtenerDispositivoPaciente(registro, paciente);

                if (!dispositivo) {
                    dispositivo = _AgregarDispositivo(registro, paciente);
                }
            }

            if (dispositivo && registro.acciones.contiene(acciones.agregarProcedimientos)) {
                _AgregarProcedimientos(registro, paciente);
            }
        }
    };

    let _CrearPaciente = function (registro) {
        let tipoIdentificador = registro.tipoIdentificador;
        if (tipoIdentificador.toUpperCase() == 'RUT') tipoIdentificador = 'rut';
        if (tipoIdentificador.toUpperCase() == 'PASAPORTE') tipoIdentificador = 'pasaporte';
        if (tipoIdentificador.toUpperCase() == 'PROVISORIA') tipoIdentificador = 'IdentificaciónProvisoria';

        let identificador = registro.identificador;
        if (tipoIdentificador == 'rut') identificador += '-' + registro.dv;

        let edadEnDias = moment().diff(moment(registro.fechaNacimiento, 'DD/MM/YYYY'), 'days');
        let rangoEtario = CapaDatos.ObtenerRangoEtario({ edadEnDias: edadEnDias });

        let registroAntiguo = _ObtenerRegistroMasAntiguo(registro);

        let fechaAdmision = '01/' + (registroAntiguo.mes > 10 ? registroAntiguo.mes : ('0' + registroAntiguo.mes)) + '/' + registroAntiguo.anno;

        CapaDatos.InsertarPaciente({
            idPaciente: identificador,
            tipoIdentificador: tipoIdentificador,
            nombre: registro.nombres,
            apellidoPaterno: registro.apat,
            apellidoMaterno: registro.amat,
            fechaNacimiento: moment(registro.fechaNacimiento, 'DD/MM/YYYY').format('DD/MM/YYYY'),
            rangoEtario: rangoEtario.idRangoEtario,
            esAmbulatorio: true,
            fechaAdmision: fechaAdmision,
            dispositivos: [],
            procedimientos: [],
            sindromes: [],
            servicios: [],
            vigilancias: []
        });

        return CapaDatos.ObtenerPacientes(identificador);
    };

    let _AgregarDispositivo = function (registro, paciente) {
        let dispositivo;

        let sigla;
        if (registro.dispositivo == 'CHD') sigla = 'CHD';
        if (registro.dispositivo == 'QMT') sigla = 'CVCqmt-a.';
        let dis = CapaDatos.ObtenerDispositivos().obtener({ sigla: sigla });

        if (dis) {
            let ubicacion;
            if (dis.sigla == 'CVCqmt-a.') {
                let idServicio = paciente.rangoEtario == 'RE04' ? 53 : (paciente.rangoEtario == 'RE02' || paciente.rangoEtario == 'RE03') ? 54 : 0;
                ubicacion = CapaDatos.ObtenerUbicacion().obtener({ idServicio: idServicio });
            }

            if (dis.sigla == 'CHD') {
                let idServicio = paciente.rangoEtario == 'RE04' ? 52 : 0;
                ubicacion = CapaDatos.ObtenerUbicacion().obtener({ idServicio: idServicio });
            }

            if (ubicacion) {
                let fechaInstalacion;
                let cambiarFechaInstalacion = false;

                if (registro.fechaInstalacion) {
                    fechaInstalacion = moment(registro.fechaInstalacion, 'DD/MM/YYYY');
                } else {
                    let registroAntiguo = _ObtenerRegistroMasAntiguo(registro);

                    fechaInstalacion = moment('01/' + (registro.mes > 10 ? registro.mes : ('0' + registro.mes)) + '/' + registro.anno, 'DD/MM/YYYY');
                    cambiarFechaInstalacion = true;
                }

                dispositivo = {
                    idSecuencia: CapaDatos.ObtenerMaxSecuencia(),
                    idDispositivo: dis.idDispositivo,
                    esActual: true,
                    acciones: [{
                        idAccion: "1",
                        nombreAccion: "Instalacion",
                        fecha: fechaInstalacion.format('DD/MM/YYYY'),
                        fechaRegistro: moment().format('DD/MM/YYYY')
                    }],
                    ubicacionOrigen: ubicacion.idUbicacion,
                    procedimientosAmbulatorios: [],
                    cambiarFechaInstalacion: cambiarFechaInstalacion
                };

                paciente.dispositivos.push(dispositivo);

                paciente.servicios.push({
                    idUbicacion: ubicacion.idUbicacion,
                    fechaIngresoServicio: fechaInstalacion.format('DD/MM/YYYY'),
                    esActual: true
                });

                CapaDatos.ActulizarPaciente(paciente, paciente.idPaciente);
            }
        }

        return dispositivo;
    };

    let _AgregarProcedimientos = function (registro, paciente) {
        let dispositivo = _ObtenerDispositivoPaciente(registro, paciente);

        if (dispositivo) {
            let mes = ModuloVigilancia.Meses().obtener({ numero: registro.mes });

            let procedimiento = dispositivo.procedimientosAmbulatorios.obtener({ numeroMes: registro.mes, anno: registro.anno });

            if (procedimiento) {
                procedimiento.cantidadProcedimientos += parseInt(registro.cantidad);
            } else {
                dispositivo.procedimientosAmbulatorios.push({
                    cantidadProcedimientos: parseInt(registro.cantidad),
                    mes: mes.nombre,
                    numeroMes: mes.numero,
                    anno: registro.anno
                });
            }
        }

        CapaDatos.ActulizarPaciente(paciente, paciente.idPaciente);
    };

    let _ObtenerRegistroMasAntiguo = function (registro) {
        return registros.filter((r) => {
            return r.identificador == registro.identificador;
        }).sort((a, b) => {
            if (a.mes > b.mes && a.anno > b.anno) {
                return 1;
            }

            if (a.mes < b.mes && a.anno < b.anno) {
                return -1;
            }

            return 0;
        })[0];
    };

    let _ObtenerDispositivoPaciente = function (registro, paciente) {
        if (paciente && paciente.dispositivos) {
            return paciente.dispositivos.filter((d) => {
                let dis = CapaDatos.ObtenerDispositivos(d.idDispositivo);
                let valido = false;

                valido = valido || (d.esActual && registro.dispositivo == 'QMT' && dis.sigla == 'CVCqmt-a.');
                valido = valido || (d.esActual && registro.dispositivo == 'CHD' && dis.sigla == 'CHD');

                return valido;
            })[0];
        }

        return null;
    };

    let _ObtenerEstadosMalos = function (registro) {
        return registro.estados.filter((e) => { return e.tipo == 'danger'; });
    };

    let _CargarTabla = function () {
        tabla.clear();
        let data = [];

        registros.forEach((registro) => {
            if ($('input[name="resutadoRegistro"][value="1"]:checked').val() == undefined && registro.estados.length > 0) {
                return;
            }

            if ($('input[name="resutadoRegistro"][value="2"]:checked').val() == undefined && registro.estados.length == 0) {
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

        tabla.rows.add(data);
        tabla.draw();

        $('.detalle-estados').tooltip();
    };

    let _ObtenerColumnas = function (hoja) {
        let headers = [];

        if (!hoja['!ref']) {
            return [];
        }

        let range = XLSX.utils.decode_range(hoja['!ref']);
        let C, R = range.s.r;

        for (C = range.s.c; C <= range.e.c; ++C) {
            let cell = hoja[XLSX.utils.encode_cell({ c: C, r: R })];

            if (cell && cell.t) headers.push(XLSX.utils.format_cell(cell));
        }

        return headers;
    };

    let _VerificarFormato = function (columnas) {
        if (!columnas || columnas.length != 12) {
            return false;
        }

        if (!columnas.contiene('TipoIdentificador')) {
            return false;
        }

        if (!columnas.contiene('Identificador')) {
            return false;
        }

        if (!columnas.contiene('DV')) {
            return false;
        }

        if (!columnas.contiene('Nombres')) {
            return false;
        }

        if (!columnas.contiene('ApellidoPaterno')) {
            return false;
        }

        if (!columnas.contiene('ApellidoMaterno')) {
            return false;
        }

        if (!columnas.contiene('FechaNacimiento')) {
            return false;
        }

        if (!columnas.contiene('Dispositivo')) {
            return false;
        }

        if (!columnas.contiene('FechaInstalacion')) {
            return false;
        }

        if (!columnas.contiene('Mes')) {
            return false;
        }

        if (!columnas.contiene('Año')) {
            return false;
        }

        if (!columnas.contiene('Cantidad')) {
            return false;
        }

        return true;
    };

    return {
        init: function () {
            InitCargaMasivaAmbulatorio();
        }
    };
}();

$(() => {
    CargaMasivaAmbulatorio.init();
});
let estados = {
  faltaTipoSolicitud: { id: 1, title: "Falta el campo 'TipoSolicitud'", class: "m-badge--danger", tipo: 'danger' },
  faltaFechaSolicitud: { id: 2, title: "Falta el campo 'FechaSolicitud'", class: "m-badge--danger", tipo: 'danger' },
  faltaFechaRecepcion: { id: 3, title: "Falta el campo 'FechaRecepcion'", class: "m-badge--danger", tipo: 'danger' },
  faltaBodegaOrigen: { id: 4, title: "Falta el campo 'BodegaOrigen'", class: "m-badge--danger", tipo: 'danger' },
  faltaNumeroCliente: { id: 5, title: "Falta el campo 'NumeroCliente'", class: "m-badge--danger", tipo: 'danger' },
  faltaNombreCliente: { id: 6, title: "Falta el campo 'NombreCliente'", class: "m-badge--danger", tipo: 'danger' },
  faltaDireccionCliente: { id: 7, title: "Falta el campo 'DireccionCliente'", class: "m-badge--danger", tipo: 'danger' },
  faltaComuna: { id: 8, title: "Falta el campo 'Comuna'", class: "m-badge--danger", tipo: 'danger' },
  faltaTelefonoContacto: { id: 9, title: "Falta el campo 'TelefonoContacto'", class: "m-badge--danger", tipo: 'danger' },
  faltaRUT: { id: 10, title: "Falta el campo 'RUT'", class: "m-badge--danger", tipo: 'danger' },
  rutInvalido: { id: 11, title: "El RUT ingresado no es válido", class: "m-badge--danger", tipo: 'danger' },
  faltaProyecto: { id: 12, title: "Falta el campo 'Proyecto'", class: "m-badge--danger", tipo: 'danger' },
  faltaUnidadNegocio: { id: 13, title: "Falta el campo 'UnidadNegocio'", class: "m-badge--danger", tipo: 'danger' },
  faltaGerencia: { id: 14, title: "Falta el campo 'Gerencia'", class: "m-badge--danger", tipo: 'danger' },
  faltaObservacionAof: { id: 15, title: "Falta el campo 'ObservacionAof'", class: "m-badge--danger", tipo: 'danger' },
  faltaPrioridad: { id: 16, title: "Falta el campo 'Prioridad'", class: "m-badge--danger", tipo: 'danger' },
  faltaPlaca: { id: 17, title: "Falta el campo 'Placa'", class: "m-badge--danger", tipo: 'danger' },
  tipoSolicitud: { id: 18, title: "Tipo de Solicitud incorrecta", class: "m-badge--danger", tipo: 'danger' },
  tipoFechaSolicitud: { id: 19, title: "Fecha de Solicitud incorrecta", class: "m-badge--danger", tipo: 'danger' },
  tipoFechaRecepcion: { id: 20, title: "Fecha de Recepción incorrecta", class: "m-badge--danger", tipo: 'danger' },
  tipoBodegaOrigen: { id: 21, title: "Bodega Origen incorrecta", class: "m-badge--danger", tipo: 'danger' },
  tipoNumeroCliente: { id: 22, title: "Numero Cliente incorrecto", class: "m-badge--danger", tipo: 'danger' },
  tipoNombreCliente: { id: 23, title: "Nombre Cliente incorrecto", class: "m-badge--danger", tipo: 'danger' },
  tipoComuna: { id: 24, title: "Comuna incorrecta", class: "m-badge--danger", tipo: 'danger' },
  tipoGerencia: { id: 25, title: "Gerencia incorrecta", class: "m-badge--danger", tipo: 'danger' },
  tipoPrioridad: { id: 26, title: "Prioridad incorrecta", class: "m-badge--danger", tipo: 'danger' },
  tipoPlaca: { id: 27, title: "Placa incorrecta", class: "m-badge--danger", tipo: 'danger' }
   };



let acciones = {
  crearPaciente: 1,
  ingresarDispositivo: 2,
  agregarProcedimientos: 3
};

let CargaMasiva = function () {
  let tabla;
  let registros = [];

  let InitCargaMasiva = function () {
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

              /*
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
              */
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
                tiposolicitud: objeto['TipoSolicitud'],
                fechasolicitud: objecto['FechaSolicitud'],
                fecharecepcion: objecto['FechaRecepcion'],
                bodegaorigen: objeto['BodegaOrigen'],
                numerocliente: objeto['NumeroCliente'],
                nombrecliente: objeto['NombreCliente'],
                direccioncliente: objeto['DireccionCliente'],
                comuna: objeto['Comuna'],
                telefonocontacto: objeto['TelefonoContacto'],
                rut: objeto['RUT'],
                proyecto: objeto['Proyecto'],
                unidadnegocion: objeto['UnidadNegocio'],
                gerencia: objeto['Gerencia'],
                observacionaof: objeto['ObservacionAof'],
                prioridad: objeto['Prioridad'],
                placa: objecto]['Placa']
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

        // VALIDA QUE EL TIPO DE SOLICITUD EXISTA Y SEA VÁLIDO
        {
          registro.estados.push(estados.tipoSolicitud);
        }
        // VALIDA QUE LA FECHA DE SOLICITUD EXISTA Y SEA VÁLIDO
        {
          registro.estados.push(estados.FechaSolicitud);
        }
        // VALIDA QUE LA FECHA DE RECEPCIÓN EXISTA Y SEA VÁLIDO
        {
          registro.estados.push(estados.FechaRecepcion);
        }
        // VALIDA QUE EL TIPO DE BODEGA DE ORIGEN EXISTA Y SEA VÁLIDO
        {
          registro.estados.push(estados.tipoBodegaOrigen);
        }

        // VALIDA QUE EL tipoBodegaOrigen EXISTA
        if (!registro.tipoBodegaOrigen) {
          registro.estados.push(estados.tipoBodegaOrigen);
        }

        // SI EL TIPO DE NUMERO DE CLIENTE ES VALIDO 
       {
          registro.estados.push(estados.tipoNumeroCliente);
        }

        // VALIDA QUE EL NUMERO DE CLIENTE EXISTA
        if (!registro.tipoNumeroCliente) {
          registro.estados.push(estados.tipoNumeroCliente);
        }

        // SI EL TIPO DE NOMBRE DE CLIENTE ES VALIDO 
        {
          registro.estados.push(estados.tipoNombreCliente);
        }

        // VALIDA QUE EL NOMBRE DE CLIENTE EXISTA
        if (!registro.tipoNombreCliente) {
          registro.estados.push(estados.tipoNombreCliente);
        }

        // SI EL TIPO DE COMUNA ES VALIDO 
        {
          registro.estados.push(estados.tipoComuna);
        }

        // VALIDA QUE LA COMUNA EXISTA
        if (!registro.tipoComuna) {
          registro.estados.push(estados.tipoComuna);
        }

        // SI EL TIPO DE GERENCIA ES VALIDO 
        {
          registro.estados.push(estados.tipoGerencia);
        }

        // VALIDA QUE LA GERENCIA EXISTA
        if (!registro.tipoGerencia) {
          registro.estados.push(estados.tipoGerencia);
        }

        // SI EL TIPO DE PRIORIDAD ES VALIDO 
        {
          registro.estados.push(estados.tipoPrioridad);
        }

        // VALIDA QUE LA PRIORIDAD EXISTA
        if (!registro.tipoPrioridad) {
          registro.estados.push(estados.tipoPrioridad);
        }

        // SI EL TIPO DE PLACA ES VALIDO 
        {
          registro.estados.push(estados.tipoPlaca);
        }

        // VALIDA QUE LA PLACA EXISTA
        if (!registro.tipoPlaca) {
          registro.estados.push(estados.tipoPlaca);
        }
        
       
        *******



        

        // SI EL TIPO DE IDENTIFICADOR ES 'RUT', Y EXISTEN LOS DATOS DE IDENTIFICACIÓN, VALIDA QUE EL RUT SEA VÁLIDO
        if (
          !registro.estados.contiene(estados.tipoSolicitud) &&
          !registro.estados.contiene(estados.tipoBodegaOrigen) &&
          !registro.estados.contiene(estados.tipoNumeroCliente) &&
          !registro.estados.contiene(estados.tipoNombreCliente) &&
          !registro.estados.contiene(estados.tipoComuna) &&
          !registro.estados.contiene(estados.tipoNumeroCliente) &&
          !registro.estados.contiene(estados.tipoGerencia) &&
          !registro.estados.contiene(estados.tipoPrioridad) &&
          !registro.estados.contiene(estados.tipoPlaca) &&
          !registro.estados.contiene(estados.faltaTipoSolicitud) &&
          !registro.estados.contiene(estados.faltaFechaSolicitud) &&
          !registro.estados.contiene(estados.faltaFechaRecepcion) &&
          !registro.estados.contiene(estados.faltaBodegaOrigen) &&
          !registro.estados.contiene(estados.faltaNumeroCliente) &&
          !registro.estados.contiene(estados.faltaNombreCliente) &&
          !registro.estados.contiene(estados.faltaRUT) &&
          !registro.estados.contiene(estados.faltaDireccionCliente) &&
          !registro.estados.contiene(estados.faltaComuna) &&
          !registro.estados.contiene(estados.faltaDireccionCliente) &&
          !registro.estados.contiene(estados.faltaGerencia) &&
          !registro.estados.contiene(estados.faltaPrioridad) &&
          !registro.estados.contiene(estados.faltaProyecto) &&
          !registro.estados.contiene(estados.faltaObservacionAof) &&
          !registro.estados.contiene(estados.faltaPlaca)
        )
        {
          let rut = registro.rut;
          if (!ModuloVigilancia.ValidaRut(rut))
          {
            registro.estados.push(estados.rutInvalido);
          }
        }
        // SI NO HAY ERRORES DE VALIDACIÓN, PROSIGUE CON LA VALIDACIÓN DE LA PLACA
        if (
          !registro.estados.contiene(estados.tipoBodegaOrigen) &&
          !registro.estados.contiene(estados.tipoNumeroCliente) &&
          !registro.estados.contiene(estados.tipoNombreCliente) &&
          !registro.estados.contiene(estados.tipoComuna) &&
          !registro.estados.contiene(estados.tipoNumeroCliente) &&
          !registro.estados.contiene(estados.tipoGerencia) &&
          !registro.estados.contiene(estados.tipoPrioridad) &&
          !registro.estados.contiene(estados.tipoPlaca) &&
          !registro.estados.contiene(estados.faltaBodegaOrigen) &&
          !registro.estados.contiene(estados.faltaNumeroCliente) &&
          !registro.estados.contiene(estados.faltaNombreCliente) &&
          !registro.estados.contiene(estados.faltaRUT) &&
          !registro.estados.contiene(estados.faltaDireccionCliente) &&
          !registro.estados.contiene(estados.faltaComuna) &&
          !registro.estados.contiene(estados.faltaDireccionCliente) &&
          !registro.estados.contiene(estados.faltaGerencia) &&
          !registro.estados.contiene(estados.faltaPrioridad) &&
          !registro.estados.contiene(estados.faltaProyecto) &&
          !registro.estados.contiene(estados.faltaObservacionAof) &&
          !registro.estados.contiene(estados.faltaPlaca)
        )
          {
            // INTENTA OBTENER AL PACIENTE
            let identificador = registro.identificador;
            if (registro.tipoIdentificador.toUpperCase() == 'RUT') identificador += '-' + registro.dv;
            let paciente = CapaDatos.ObtenerPacientes(identificador);

          
            // SI NO HAY ERRORES DE VALIDACIÓN, AGREGA LA ACCIÓN DE CREAR PACIENTE AL REGISTRO
          if (!registro.estados.contiene(estados.faltaNombreCliente))
            {
              registro.acciones.push(acciones.crearPaciente);
            }
          }
          // SI LA FECHA ES VÁLIDA, VALIDA QUE ESTA NO SEA POSTERIOR A LA FECHA ACTUAL
          if (!registro.estados.contiene(estados.fechaInstalacionNoValida) && fechaInstalacion > moment(moment().format('DD/MM/YYYY'), 'DD/MM/YYYY'))
        {
          registro.estados.push(estados.fechaInstalacionPosterior);
        }
         _GuardarDatos(registro);
        });

      let cargaMasiva = {
        idCargaMasiva: CapaDatos.ObtenerMaxSecuencia(),
        fechaHora: moment().format('DD/MM/YYYY HH:mm'),
        responsable: sessionStorage["usuarioLogueado"],
        archivo:nombreArchivo,
        registros: registros,
        tipo: ModuloVigilancia.CargaMasiva
        
      };

      CapaDatos.InsertarCargaMasiva(cargaMasiva);

      _CargarTabla();

      $('.detalle-estados').tooltip();

      $('#divTablaResultado').show();

      ModuloVigilancia.AlertaExito('Archivo procesado, para ver detalle ir a histórico de cargas', '¡Atención!');
    } catch (ex)
    {
      console.log(ex);
      ModuloVigilancia.AlertaError('No se pudo procesar la información, verifique el archivo.', '¡Atención!');
    }

    $('#btnProcesarRegistros').removeClass('pulso-guardar');
  };

  let _GuardarDatos = function (registro) {
    if (_ObtenerEstadosMalos(registro).length == 0) {
      let cargamasiva;

      if (registro.acciones.contiene(acciones.CrearCargaMasiva)) {
        let identificador = registro.identificador;
        if (registro.tipoIdentificador.toUpperCase() == 'RUT') identificador += '-' + registro.dv;
        paciente = CapaDatos.ObtenerPacientes(identificador);

        if (!CargaMasivaId) {
          cargamasiva = _CrearCargaMasiva(registro);
        }
      }

    }
  };

  let _CrearCargaMasiva = function (registro) {
    let cargamasivaid = registro.CargaMasivaId;
    
    let identificador = registro.identificador;
    if (tipoIdentificador == 'rut') identificador += '-' + registro.dv;

    
    let registroAntiguo = _ObtenerRegistroMasAntiguo(registro);

    let fechaAdmision = '01/' + (registroAntiguo.mes > 10 ? registroAntiguo.mes : ('0' + registroAntiguo.mes)) + '/' + registroAntiguo.anno;

    CapaDatos.InsertarPaciente({
      idPaciente: identificador,
      tipoIdentificador: tipoIdentificador,
      nombre: registro.nombres,
      apellidoPaterno: registro.apat,
      apellidoMaterno: registro.amat,
      
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

  

  let _ObtenerEstadosMalos = function (registro)
  {
    return registro.estados.filter((e) => { return e.tipo == 'danger'; });
  };

  let _CargarTabla = function ()
  {
    tabla.clear();
    let data = [];

    registros.forEach((registro) => {
      if ($('input[name="resutadoRegistro"][value="1"]:checked').val() == undefined && registro.estados.length > 0) {
        return;
      }

      if ($('input[name="resutadoRegistro"][value="2"]:checked').val() == undefined && registro.estados.length == 0) {
        return;
      }
      
      let valores = [
        registro.acciones || [],
        null,
        registro.estados || [],
        registro.tipoSolicitud || '',
        registro.tipoBodegaOrigen || '',
        registro.tipoNumeroCliente || '',
        registro.tipoNombreCliente || '',
        registro.tipoComuna || '',
        registro.tipoGerencia || '',
        registro.tipoPrioridad || '',
        registro.tipoPlaca || '',
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

    if (!columnas.contiene('TipoSolicitud')) {
      return false;
    }

    if (!columnas.contiene('FechaSolicitud')) {
      return false;
    }

    if (!columnas.contiene('FechaRecepcion')) {
      return false;
    }

    if (!columnas.contiene('BodegaOrigen')) {
      return false;
    }

    if (!columnas.contiene('NumeroCliente')) {
      return false;
    }

    if (!columnas.contiene('NombreCliente')) {
      return false;
    }

    if (!columnas.contiene('DireccionCliente')) {
      return false;
    }

    if (!columnas.contiene('Comuna')) {
      return false;
    }

    if (!columnas.contiene('TelefonoContacto')) {
      return false;
    }

    if (!columnas.contiene('Rut')) {
      return false;
    }

    if (!columnas.contiene('Proyecto')) {
      return false;
    }

    if (!columnas.contiene('UnidadNegocio')) {
      return false;
    }
    if (!columnas.contiene('Gerencia')) {
      return false;
    }
    if (!columnas.contiene('ObservacionAof')) {
      return false;
    }
    if (!columnas.contiene('Prioridad')) {
      return false;
    }
    if (!columnas.contiene('Placa')) {
      return false;
    }
    return true;
  };

  return {
    init: function () {
      InitCargaMasiva();
    }
  };
}();

//Antigua versión
let CargaMasiva = function () {

  let cargasmasivas;

    let Init = function () {
        InitElementos();
    };

  let InitElementos = function () {
    $('.m-select2').select2();

    f($("#lista-cargasmasivas").length > 0)
    {
      window.crearSelectorFecha("#filtro-fechacarga", moment().subtract(6, 'days'), moment());

      $.post("/CargaMasiva/ObtenerCargasMasivas", { cargamasivaId: 0 }, function (data) {
        cargasmasivas = data;

        CargarLista();
      });

      $('#filtro-filtrar').click(function () {
        Filtrar();
      });

      $(document).on('click', '#lista-cargasmasivas tbody tr', function () {
        let id = $(this).find('.cargamasiva-usuario-id').attr('data-id');
        location.href = "/CargaMasiva/CargaMasiva/" + id;
      });
    }

    let CargarLista = function () {
      let picker = $('#filtro-fechacarga').data('daterangepicker');
      $('#lista-cargasmasivas').mDatatable({
        data: {
          type: "local",
          source: cargasmasivas.filter((e) => { return moment(e.CargaMasiva, 'DD/MM/YYYY').isBetween(picker.startDate, picker.endDate); }),
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
          input: $('#buscarCargaMasiva')
        },
        columns: [
          { field: "CargaMasivaId", title: "#", width: 50, selector: !1, textAlign: "center" }, 
          { field: "NombreUsuario", title: "Usuario", responsive: { visible: "lg" }, 
          { field: "FechaHora", title: "Realizada", responsive: { visible: "lg" }, type: "date", format: "DD/MM/YYYY"},
          { field: "Archivo", title: "Archivo", responsive: { visible: "lg" } }
          ],
        translate: {
          records: {
                      processing: "Cargando...",
                      noRecords: "No se encontraron registros"
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
      $.post("/CargaMasiva/Listar", { usuarioId: 0 }, function (cargas) {
          $(".m_datatable").mDatatable({
              data: {
                  type: "local",
                  source: cargas,
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
                  input: $("#buscarCargaMasiva")
              },
              columns: [
                { field: "CargaMasivaId", title: "#", width: 50, selector: !1, textAlign: "center" },
                { field: "NombreUsuario", title: "Usuario", responsive: { visible: "lg" } },
                { field: "FechaHora", title: "Realizada", responsive: { visible: "lg" }, type: "date", format: "DD/MM/YYYY" },
                { field: "Archivo", title: "Archivo", responsive: { visible: "lg" } }
                ], true, true);
              
          });
      });
  };

    return {
            init: function () {
              Init();
              }
            };
  }();

$(() => {
    CargaMasiva.init();
});
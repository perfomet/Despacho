let estados = {
  faltaNumeroSolicitud: { id: 1, title: "Falta el campo 'NumeroSolicitud'", clase: "m-badge--danger", tipo: 'danger' },
  faltaTipoSolicitud: { id: 2, title: "Falta el campo 'TipoSolicitud'", clase: "m-badge--danger", tipo: 'danger' },
  faltaFechaSolicitud: { id: 3, title: "Falta el campo 'FechaSolicitud'", clase: "m-badge--danger", tipo: 'danger' },
  faltaFechaRecepcion: { id: 4, title: "Falta el campo 'FechaRecepcion'", clase: "m-badge--danger", tipo: 'danger' },
  faltaNumeroCliente: { id: 5, title: "Falta el campo 'NumeroCliente'", clase: "m-badge--danger", tipo: 'danger' },
  faltaNombreCliente: { id: 6, title: "Falta el campo 'NombreCliente'", clase: "m-badge--danger", tipo: 'danger' },
  faltaCalleDireccionCliente: { id: 7, title: "Falta el campo 'CalleDireccionCliente'", clase: "m-badge--danger", tipo: 'danger' },
  faltaNumeroDireccionCliente: { id: 8, title: "Falta el campo 'NumeroDireccionCliente'", clase: "m-badge--danger", tipo: 'danger' },
  faltaRegionCliente: { id: 9, title: "Falta el campo 'RegionCliente'", clase: "m-badge--danger", tipo: 'danger' },
  faltaComunaCliente: { id: 10, title: "Falta el campo 'ComunaCliente'", clase: "m-badge--danger", tipo: 'danger' },
  faltaNumeroTelefonoContacto: { id: 11, title: "Falta el campo 'NumeroTelefonoContacto'", clase: "m-badge--danger", tipo: 'danger' },
  faltaNumeroTelefonoContactoAdicional: { id: 12, title: "Falta el campo 'NumeroTelefonoContactoAdicional'", clase: "m-badge--danger", tipo: 'danger' },
  faltaRUTCliente: { id: 13, title: "Falta el campo 'RUTCliente'", clase: "m-badge--danger", tipo: 'danger' },
  faltaUnidadNegocio: { id: 14, title: "Falta el campo 'UnidadNegocio'", clase: "m-badge--danger", tipo: 'danger' },
  faltaGerencia: { id: 15, title: "Falta el campo 'Gerencia'", clase: "m-badge--danger", tipo: 'danger' },
  faltaObservacionAof: { id: 16, title: "Falta el campo 'ObservacionAof'", clase: "m-badge--danger", tipo: 'danger' },
  faltaPrioridad: { id: 17, title: "Falta el campo 'Prioridad'", clase: "m-badge--danger", tipo: 'danger' },
  faltaPlaca: { id: 18, title: "Falta el campo 'Placa'", clase: "m-badge--danger", tipo: 'danger' },
  tipoNumeroSolicitud: { id: 19, title: "Número de Solicitud incorrecta (debe ser numérica)", clase: "m-badge--danger", tipo: 'danger' },
  tipoSolicitud: { id: 20, title: "Tipo de Solicitud incorrecta", clase: "m-badge--danger", tipo: 'danger' },
  tipoFechaSolicitud: { id: 21, title: "Fecha de Solicitud incorrecta", clase: "m-badge--danger", tipo: 'danger' },
  tipoFechaRecepcion: { id: 22, title: "Fecha de Recepción incorrecta", clase: "m-badge--danger", tipo: 'danger' },
  tipoNumeroCliente: { id: 23, title: "Numero Cliente incorrecto", clase: "m-badge--danger", tipo: 'danger' },
  tipoNombreCliente: { id: 24, title: "Nombre Cliente incorrecto", clase: "m-badge--danger", tipo: 'danger' },
  tipoRegionCliente: { id: 25, title: "Nombre Región incorrecto", clase: "m-badge--danger", tipo: 'danger' },
  tipoComunaCliente: { id: 26, title: "ComunaCliente incorrecta", clase: "m-badge--danger", tipo: 'danger' },
  tipoNumeroTelefonoContacto: { id: 27, title: "NumeroTelefonoContacto incorrecto", clase: "m-badge--danger", tipo: 'danger' },
  tipoNumeroTelefonoContactoAdicional: { id: 28, title: "NumeroTelefonoContactoAdicional incorrecto", clase: "m-badge--danger", tipo: 'danger' },
  rutInvalido: { id: 29, title: "RUT inválido", clase: "m-badge--danger", tipo: 'danger' },
  tipoUnidadNegocio: { id: 30, title: "Unidad de Negocio incorrecta", clase: "m-badge--danger", tipo: 'danger' },
  tipoGerencia: { id: 31, title: "Gerencia incorrecta", clase: "m-badge--danger", tipo: 'danger' },
  tipoPrioridad: { id: 32, title: "Prioridad incorrecta", clase: "m-badge--danger", tipo: 'danger' },
  tipoPlaca: { id: 33, title: "Placa incorrecta", clase: "m-badge--danger", tipo: 'danger' },
  fechaRecepcionmenorFechaSolicitud: { id: 34, title: "La Fecha de Solicitud es mayor que la Fecha de Recepción", clase: "m-badge--danger", tipo: 'danger' }
};

let acciones = {
  crearSolicitud: 1,
  agregarPlaca: 2
};

const snumero = 0;
const stexto = 1;


let CargaMasiva = function () {
  let cargasmasivas = [];

  let InitCargaMasiva = function () {
    InitElementos();
  };

  let InitElementos = function () {

    $('.m-select2').select2();

    if ($("#listacargasmasivas").length > 0) {
      window.crearSelectorFecha("#filtro-fechacarga", moment().subtract(6, 'days'), moment());

      $.post("/CargaMasiva/Listar", { clienteId: 0 }, function (data) {
        cargasmasivas = data;

        CargarLista();
      });

      $('#filtro-filtrar').click(function () {
        let picker = $('#filtro-fecha-solicitud').data('daterangepicker');
        //Filtrar();
      });
    }
  }

  let CargarLista = function () {
    let picker = $('#filtro-fechacarga').data('daterangepicker');

    $('#listacargasmasivas').mDatatable({
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
        { field: "NombreUsuario", title: "Usuario", responsive: { visible: "lg" } },
        { field: "FechaHora", title: "Realizada", responsive: { visible: "lg" }, type: "date", format: "DD/MM/YYYY" },
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

  return {
    init: function () {
      InitCargaMasiva();
    }
  };
}();

let CargaMasivaDetalle = function () {
  let tabla;
  let registros = [];

  let _Init = function () {
    _InitElementos();
  }

  let _InitElementos = function () {
    if ($('#tabla').length > 0) {
      tabla = $('#tabla').mDatatable({
        data: {
          type: "local",
          source: [],
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
          {
            field: "", title: "", width: 50, selector: !1, textAlign: "center", template: function (e, a, i) {
              let error = '<i class="fa fa-times px-3" style="color: #DC3C41;font-size: 2rem;"></i>';
              let correcto = '<i class="fa fa-check px-3" style="color: #34BFA3;font-size: 2rem;"></i>';

              return e[2].filter((x) => { return x.tipo == 'danger'; }).length > 0 ? error : correcto;
            }
          },
          {
            field: "", title: "Resultados", width: 50, selector: !1, textAlign: "center", template: function (e, a, i) {
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
          },
          { field: "NumeroSolicitud", title: "#", width: 50, selector: !1, textAlign: "center" },
          { field: "TipoSolicitud", title: "#", width: 50, selector: !1, textAlign: "center" },
          { field: "FechaSolicitud", title: "#", width: 50, selector: !1, textAlign: "center" },
          { field: "FechaRecepcion", title: "#", width: 50, selector: !1, textAlign: "center" },
          { field: "NumeroCliente", title: "#", width: 50, selector: !1, textAlign: "center" },
          { field: "NombreCliente", title: "#", width: 50, selector: !1, textAlign: "center" },
          { field: "CalleDireccionCliente", title: "#", width: 50, selector: !1, textAlign: "center" },
          { field: "NumeroDireccionCliente", title: "#", width: 50, selector: !1, textAlign: "center" },
          { field: "RegionCliente", title: "#", width: 50, selector: !1, textAlign: "center" },
          { field: "ComunaCliente", title: "#", width: 50, selector: !1, textAlign: "center" },
          { field: "NumeroTelefonoContacto", title: "#", width: 50, selector: !1, textAlign: "center" },
          { field: "NumeroTelefonoContactoAdicional", title: "#", width: 50, selector: !1, textAlign: "center" },
          { field: "RutCliente", title: "#", width: 50, selector: !1, textAlign: "center" },
          { field: "UnidadNegocio", title: "#", width: 50, selector: !1, textAlign: "center" },
          { field: "Gerencia", title: "#", width: 50, selector: !1, textAlign: "center" },
          { field: "ObservacionAof", title: "#", width: 50, selector: !1, textAlign: "center" },
          { field: "Prioridad", title: "#", width: 50, selector: !1, textAlign: "center" },
          { field: "Placa", title: "#", width: 50, selector: !1, textAlign: "center" }
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
    }

    let nombreArchivo;

    $('#archivoCarga').change((oEvent) => {
      // Get The File From The Input
      let archivo = oEvent.target.files[0];

      if (archivo) {
        nombreArchivo = archivo.name;

        $('label[for="archivoCarga"]').html(nombreArchivo);

        let reader = new FileReader();

        reader.onload = function (e) {
          //MOSTRAR CARGANDO

          try {
            let data = e.target.result;
            let workbook = XLSX.read(data, {
              type: 'binary'
            });

            let filas = XLSX.utils.sheet_to_row_object_array(workbook.Sheets[workbook.SheetNames[0]]);
            let columnas = _ObtenerColumnas(workbook.Sheets[workbook.SheetNames[0]]);

            if (!_VerificarFormato(columnas)) {
              //OCULTAR CARGANDO
              alert('El formato del archivo seleccionado no es correcto, verifique el archivo.');
              return;
            }

            if (filas && filas.length == 0) {
              //OCULTAR CARGANDO
              alert('El archivo cargado está vacío, ingrese información y cárguelo nuevamente.');
              return;
            }

            registros = [];

            filas.forEach((objeto) => {
              registros.push({
                NumeroSolicitud: objeto['NumeroSolicitud'],
                TipoSolicitud: objeto['TipoSolicitud'],
                FechaSolicitud: objeto['FechaSolicitud'],
                FechaRecepcion: objeto['FechaRecepcion'],
                NumeroCliente: objeto['NumeroCliente'],
                NombreCliente: objeto['NombreCliente'],
                CalleDireccionCliente: objeto['CalleDireccionCliente'],
                NumeroDireccionCliente: objeto['NumeroDireccionCliente'],
                RegionCliente: objeto['RegionCliente'],
                ComunaCliente: objeto['ComunaCliente'],
                NumeroTelefonoContacto: objeto['NumeroTelefonoContacto'],
                NumeroTelefonoContactoAdicional: objeto['NumeroTelefonoContactoAdicional'],
                RutCliente: objeto['RutCliente'],
                UnidadNegocio: objeto['UnidadNegocio'],
                Gerencia: objeto['Gerencia'],
                ObservacionAof: objeto['ObservacionAof'],
                Prioridad: objeto['Prioridad'],
                Placa: objeto['Placa']
              });
            });

            $('#btnProcesarRegistros').addClass('pulso-guardar');
            $('#alertaProcesar').show();
          } catch (ex) {
            console.log(ex);
            alert('No se pudo obtener la información del archivo seleccionado, verifique el archivo.');
          }

          $('#archivoCarga').val(null);
          //OCULTAR CARGANDO
        };

        reader.onerror = function (ex) {
          console.log(ex);
          alert('No se pudo obtener la información del archivo seleccionado, verifique el archivo.');
          //OCULTAR CARGANDO
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

    $(document).on('click', '#listacargasmasivas tbody tr', function () {
      let id = $(this).find('.cargamasiva-usuario-id').attr('data-id');
      location.href = "/CargaMasiva/CargaMasiva/" + id;
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
        alert('Debe cargar un archivo primero');
        _ValidarNumeroSolicitud(registros);
        return;
      }
      else {
        $.post('/CargaMasiva/Create', {
          cargamasiva: {
            FechaHora: new Date(),
            Archivo: nombreArchivo
          },
          detallecargamasiva: registros
        }, function (data) {
          cargasmasivas = data;
          return;
        });
      }
      let numeroSolicitudActual = 0;


      registros.forEach((registro) => {
        // VACÍA LAS LISTAS DE ESTADOS Y ACCIONES
        registro.estados = [];
        registro.acciones = [];

        //VALIDA NUMERO DE SOLICITUD


        // VALIDA TIPO DE SOLICITUD
        if (!registro.TipoSolicitud) {
          registro.estados.push(estados.faltaTipoSolicitud);
        }
        else {

        }

        // VALIDA LA FECHA DE SOLICITUD 
        if (!registro.FechaSolicitud) {
          registro.estados.push(estados.faltaFechaSolicitud);
        }
        else {
          var RegExPattern = /^\d{1,2}\/\d{1,2}\/\d{2,4}$/;
          if ((registro.FechaSolicitud.match(RegExPattern)) && (registro.FechaSolicitud != '')) {

          } else {
            registro.estados.push(estados.tipoFechaSolicitud);
          }
        }
        // VALIDA LA FECHA DE RECEPCIÓN 
        if (!registro.FechaRecepcion) {
          registro.estados.push(estados.faltaFechaRecepcion);
        }
        else {
          var RegExPattern = /^\d{1,2}\/\d{1,2}\/\d{2,4}$/;
          if ((registro.FechaRecepcion.match(RegExPattern)) && (registro.FechaRecepcion != '')) {

          }
          else {
            if (registro.FechaRecepcion >= registro.FechaSolicitud) {

            }
            else {
              registro.estados.push(estados.fechaRecepcionmenorFechaSolicitud);
            }

          }
        }
        // VALIDA EL NUMERO DE CLIENTE  
        if (!registro.NumeroCliente) {
          registro.estados.push(estados.faltaNumeroCliente);
        }
        else {

          if (isNaN(registro.NumeroCliente)) {
            registro.estados.push(estados.tipoNumeroCliente)
          }
          else {

          }

        }

        // VALIDA NOMBRE DE CLIENTE  
        if (!registro.NombreCliente) {
          registro.estados.push(estados.faltaNombreCliente);
        }
        else {

        }
        // VALIDA CalleDireccionCliente
        if (!registro.CalleDireccionCliente) {
          registro.estados.push(estados.faltaCalleDireccionCliente);
        }
        else {

        }
        // VALIDA NumeroDireccionCliente
        if (!registro.NumeroDireccionCliente) {
          registro.estados.push(estados.faltaNumeroDireccionCliente);
        }
        else {
          if (!isNaN(registro.NumeroDireccionCliente)) {
            registro.estados.push(estados.tipoNumeroDireccionCliente)
          }
          else {

          }

        }
        // VALIDA LA  REGION 
        if (!registro.RegionCliente) {
          registro.estados.push(estados.faltaRegionCliente);
        }
        else {

        }

        // VALIDA COMUNA 
        if (!registro.ComunaCliente) {
          registro.estados.push(estados.faltaComunaCliente);
        }
        else {

        }
        //VALIDA NUMERO TELEFONO CONTACTO
        if (!registro.NumeroTelefonoContacto) {
          registro.estados.push(estados.faltaNumeroTelefonoContacto)
        }
        else {
          if (!isNaN(registro.NumeroTelefonoContacto)) {
            registro.estados.push(estados.tipoNumeroTelefonoContacto)
          }
          else {

          }
        }
        //VALIDA NUMERO TELEFONO CONTACTO ADICIONAL
        if (!registro.NumeroTelefonoContactoAdicional) {
          registro.estados.push(estados.tipoNumeroTelefonoContactoAdicional)
        }
        else {
          if (!isNaN(registro.NumeroTelefonoContactoAdicional)) {
            registro.estados.push(estados.tipoNumeroTelefonoContactoAdicional)
          }
          else {

          }
        }
        //VALIDA RUTCLIENTE
        if (!registro.RutCliente) {
          registro.estados.push(estados.faltaRUTCliente);
        }
        else {
          if (!_ValidaRut(registro.rutcliente)) {
            registro.estados.push(estados.rutInvalido);
          }
          else {

          }

        }
        //VALIDA UNIDADNEGOCIO
        if (!registro.UnidadNegocio) {
          registro.estados.push(estados.faltaUnidadNegocio);
        }
        else {
          if (isNaN(registro.UnidadNegocio)) {
            registro.estados.push(estados.tipoUnidadNegocio)
          }
          else {

          }

        }
        // VALIDA GERENCIA 
        if (!registro.Gerencia) {
          registro.estados.push(estados.faltaGerencia);
        }
        else {
          if (isNaN(registro.Gerencia)) {
            registro.estados.push(estados.tipoGerencia)
          }
          else {

          }
        }
        // VALIDA OBSERVACIONAOF 
        if (!registro.ObservacionAof) {
          registro.estados.push(estados.faltaObservacionAof);
        }
        else {

        }

        // VALIDA PRIORIDAD 
        if (!registro.Prioridad) {
          registro.estados.push(estados.faltaPrioridad);
        }
        else {
          if (isNaN(registro.Prioridad)) {
            registro.estados.push(estados.tipoPrioridad)
          }
          else {

          }
        }
        //VALIDA PLACA
        if (!registro.Placa) {
          registro.estados.push(estados.faltaPlaca);
        }
        else {
          if (!isNaN(registro.Placa)) {
            registro.estados.push(estados.tipoPlaca)
          }
          else {

          }
        }

        // SI EL TIPO DE IDENTIFICADOR ES 'RUT', Y EXISTEN LOS DATOS DE IDENTIFICACIÓN, VALIDA QUE EL RUT SEA VÁLIDO
        if (
          (registro.estados.indexOf(estados.faltaNumeroSolicitud) < 0) &&
          (registro.estados.indexOf(estados.faltaTipoSolicitud) < 0) &&
          (registro.estados.indexOf(estados.faltaFechaSolicitud) < 0) &&
          (registro.estados.indexOf(estados.faltaFechaRecepcion) < 0) &&
          (registro.estados.indexOf(estados.faltaNumeroCliente) < 0) &&
          (registro.estados.indexOf(estados.faltaNombreCliente) < 0) &&
          (registro.estados.indexOf(estados.faltaCalleDireccionCliente) < 0) &&
          (registro.estados.indexOf(estados.faltaNumeroDireccionCliente) < 0) &&
          (registro.estados.indexOf(estados.faltaRegionCliente) < 0) &&
          (registro.estados.indexOf(estados.faltaComunaCliente) < 0) &&
          (registro.estados.indexOf(estados.faltaNumeroTelefonoContacto) < 0) &&
          (registro.estados.indexOf(estados.faltaNumeroTelefonoContactoAdicional) < 0) &&
          (registro.estados.indexOf(estados.faltaRUTCliente) < 0) &&
          (registro.estados.indexOf(estados.faltaUnidadNegocio) < 0) &&
          (registro.estados.indexOf(estados.faltaGerencia) < 0) &&
          (registro.estados.indexOf(estados.faltaObservacionAof) < 0) &&
          (registro.estados.indexOf(estados.faltaPrioridad) < 0) &&
          (registro.estados.indexOf(estados.faltaPlaca) < 0) &&
          (registro.estados.indexOf(estados.tipoNumeroSolicitud) < 0) &&
          (registro.estados.indexOf(estados.tipoSolicitud) < 0) &&
          (registro.estados.indexOf(estados.tipoFechaSolicitud) < 0) &&
          (registro.estados.indexOf(estados.tipoFechaRecepcion) < 0) &&
          (registro.estados.indexOf(estados.tipoNumeroCliente) < 0) &&
          (registro.estados.indexOf(estados.tipoNombreCliente) < 0) &&
          (registro.estados.indexOf(estados.tipoRegionCliente) < 0) &&
          (registro.estados.indexOf(estados.tipoComunaCliente) < 0) &&
          (registro.estados.indexOf(estados.tipoNumeroTelefonoContacto) < 0) &&
          (registro.estados.indexOf(estados.tipoNumeroTelefonoContactoAdicional) < 0) &&
          (registro.estados.indexOf(estados.rutInvalido) < 0) &&
          (registro.estados.indexOf(estados.tipoUnidadNegocio) < 0) &&
          (registro.estados.indexOf(estados.tipoGerencia) < 0) &&
          (registro.estados.indexOf(estados.tipoPrioridad) < 0) &&
          (registro.estados.indexOf(estados.tipoPlaca) < 0)

        ) {

        }

        // SI NO HAY ERRORES DE VALIDACIÓN, PROSIGUE CON LA VALIDACIÓN DE LA PLACA
        if (
          (registro.estados.indexOf(estados.faltaNumeroSolicitud) < 0) &&
          (registro.estados.indexOf(estados.faltaNumeroCliente) < 0) &&
          (registro.estados.indexOf(estados.faltaNombreCliente) < 0) &&
          (registro.estados.indexOf(estados.faltaRUT) < 0) &&
          (registro.estados.indexOf(estados.faltaDireccionCalleCliente) < 0) &&
          (registro.estados.indexOf(estados.faltaDireccionNumeroCliente) < 0) &&
          (registro.estados.indexOf(estados.faltaRegionCliente) < 0) &&
          (registro.estados.indexOf(estados.faltaComunaCliente) < 0) &&
          (registro.estados.indexOf(estados.faltaGerencia) < 0) &&
          (registro.estados.indexOf(estados.faltaPrioridad) < 0) &&
          (registro.estados.indexOf(estados.faltaObservacionAof) < 0) &&
          (registro.estados.indexOf(estados.faltaPlaca) < 0) &&
          (registro.estados.indexOf(estados.tipoNumeroSolicitud) < 0) &&
          (registro.estados.indexOf(estados.tipoSolicitud) < 0) &&
          (registro.estados.indexOf(estados.tipoFechaSolicitud) < 0) &&
          (registro.estados.indexOf(estados.tipoFechaRecepcion) < 0) &&
          (registro.estados.indexOf(estados.tipoNumeroCliente) < 0) &&
          (registro.estados.indexOf(estados.tipoNombreCliente) < 0) &&
          (registro.estados.indexOf(estados.tipoRegionCliente) < 0) &&
          (registro.estados.indexOf(estados.tipoComunaCliente) < 0) &&
          (registro.estados.indexOf(estados.tipoNumeroTelefonoContacto) < 0) &&
          (registro.estados.indexOf(estados.tipoNumeroTelefonoContactoAdicional) < 0) &&
          (registro.estados.indexOf(estados.rutInvalido) < 0) &&
          (registro.estados.indexOf(estados.tipoUnidadNegocio) < 0) &&
          (registro.estados.indexOf(estados.tipoGerencia) < 0) &&
          (registro.estados.indexOf(estados.tipoPrioridad) < 0) &&
          (registro.estados.indexOf(estados.tipoPlaca) < 0) &&
          numeroSolicitudActual != registro.numeroSolicitud
        ) {
          // SI NO HAY ERRORES DE VALIDACIÓN, AGREGA LA ACCIÓN DE CREAR SOLICITUD AL REGISTRO
          registro.acciones.push(acciones.crearSolicitud);
        } else if ((registro.estados.indexOf(estados.faltaPlaca) < 0) && registro.numeroSolicitud == numeroSolicitudActual) {
          // SI SOLO TENGO LA PLACA
          registro.acciones.push(acciones.agregarPlaca);
        }

        numeroSolicitudActual = registro.NumeroSolicitud;
      });
      //Tarea Guardar resultados del registro en Validación en el Usuario 
      console.log(registros);

      // VALIDACIÓN EN EL SERVIDOR
      $.post('/CargaMasiva/Validar', { detalles: registros }, function (data) {
        registros = data;
        
        _CargarTabla();

        $('.detalle-estados').tooltip();

        $('#divTablaResultado').show();

        alert('Archivo procesado, para ver detalle ir a histórico de cargas');
      });
    } catch (ex) {
      console.log(ex);
      alert('No se pudo procesar la información, verifique el archivo.');
    }

    $('#btnProcesarRegistros').removeClass('pulso-guardar');
  };

  let _Guardar = function () {
    let cargaMasiva = {
      idCargaMasiva: CapaDatos.init,
      fechaHora: moment().format('DD/MM/YYYY HH:mm'),
      responsable: sessionStorage["usuarioLogueado"],
      archivo: nombreArchivo
    };

    let detalle = [];
    let productosDetalle = [];

    registros.forEach((registro) => {
      if (registro.acciones.contiene(acciones.crearSolicitud)) {
        detalle.push({
          //propiedades del detalle
          numeroSolicitud: registro.numeroSolicitud
        });

        productosDetalle.push({
          numeroPlaca: registro.numeroPlaca,
          numeroSolicitud: registro.numeroSolicitud
        });
      } else if (registro.acciones.contiene(acciones.agregarPlaca)) {
        productosDetalle.push({
          numeroPlaca: registro.numeroPlaca,
          numeroSolicitud: registro.numeroSolicitud
        });
      }
    });

    $.post("/CargaMasiva/Create", {
      cargamasiva: cargaMasiva,
      detallecargamasiva: detalle,
      cargaMasivaDetalleProductos: productosDetalle
    }, function (data) {

    });
  }

  let _ObtenerEstadosMalos = function (registro) {
    return registro.estados.filter((e) => { return e.tipo == 'danger'; });
  };

  let _CargarTabla = function () {
    //tabla.clear();
    let tabla;
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
        registro.NumeroSolicitud || '',
        registro.tipoSolicitud || '',
        registro.tipoNumeroCliente || '',
        registro.tipoNombreCliente || '',
        registro.tipoRegionCliente || '',
        registro.tipoComunaCliente || '',
        registro.tipoGerencia || '',
        registro.tipoPrioridad || '',
        registro.tipoPlaca || '',
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
    if (!columnas || columnas.length != 18) {
      return false;
    }

    if (columnas.indexOf('NumeroSolicitud') < 0) {
      return false;
    }
    else {

    }
    if (columnas.indexOf('TipoSolicitud') < 0) {
      return false;
    }
    else {

    }
    if (columnas.indexOf('FechaSolicitud') < 0) {
      return false;
    }
    else {

    }
    if (columnas.indexOf('FechaRecepcion') < 0) {
      return false;
    }
    else {

    }
    if (columnas.indexOf('NumeroCliente') < 0) {
      return false;
    }
    else {

    }
    if (columnas.indexOf('NombreCliente') < 0) {
      return false;
    }
    else {

    }
    if (columnas.indexOf('CalleDireccionCliente') < 0) {
      return false;
    }
    else {

    }
    if (columnas.indexOf('NumeroDireccionCliente') < 0) {
      return false;
    }
    else {

    }
    if (columnas.indexOf('RegionCliente') < 0) {
      return false;
    }
    else {

    }
    if (columnas.indexOf('ComunaCliente') < 0) {
      return false;
    }
    else {

    }
    if (columnas.indexOf('NumeroTelefonoContacto') < 0) {
      return false;
    }
    else {

    }
    if (columnas.indexOf('NumeroTelefonoContactoAdicional') < 0) {
      return false;
    }
    else {

    }
    if (columnas.indexOf('RutCliente') < 0) {
      return false;
    }
    else {

    }
    if (columnas.indexOf('UnidadNegocio') < 0) {
      return false;
    }
    else {

    }
    if (columnas.indexOf('Gerencia') < 0) {
      return false;
    }
    else {

    }
    if (columnas.indexOf('ObservacionAof') < 0) {
      return false;
    }
    else {

    }
    if (columnas.indexOf('Prioridad') < 0) {
      return false;
    }
    else {

    }
    if (columnas.indexOf('Placa') < 0) {
      return false;
    }
    else {

    }
    return true;
  };

  //tarea
  let numerosolicitudescreadas = [];
 
  let _ValidarNumeroSolicitud = function (Registros) {
    Registros.forEach((registro) => {
      if (!numerosolicitudescreadas.include(registro.NumeroSolicitud)) {
        numerosolicitudescreadas.push(registro.NumeroSolicitud)
      }
      else {
      }
    };

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

  return {
    init: function () {
      _Init();
    },
    Registros: function () {
      return registros;
    }
  };
}();

$(() => {
  CargaMasiva.init();
  CargaMasivaDetalle.init();
});
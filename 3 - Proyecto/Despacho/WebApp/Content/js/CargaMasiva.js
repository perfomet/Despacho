let Errores = {
  faltaNumeroSolicitud: { id: 1, title: "Falta el campo 'NumeroSolicitud' o el mismo no es numérico", clase: "m-badge--danger", tipo: 'danger' },
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
  faltaPrioridad: { id: 16, title: "Falta el campo 'Prioridad'", clase: "m-badge--danger", tipo: 'danger' },
  faltaPlaca: { id: 17, title: "Falta el campo 'Placa'", clase: "m-badge--danger", tipo: 'danger' },
  tipoNumeroSolicitud: { id: 18, title: "Número de Solicitud incorrecta (debe ser numérico)", clase: "m-badge--danger", tipo: 'danger' },
  tipoFechaSolicitud: { id: 19, title: "Fecha de Solicitud incorrecta (Formato DD/MM/YYYY)", clase: "m-badge--danger", tipo: 'danger' },
  tipoFechaRecepcion: { id: 20, title: "Fecha de Recepción incorrecta (Formato DD/MM/YYYY)", clase: "m-badge--danger", tipo: 'danger' },
  tipoNumeroDireccionCliente: { id: 21, title: "Número de calle del cliente incorrecto (debe ser numérico)", clase: "m-badge--danger", tipo: 'danger' },
  rutInvalido: { id: 22, title: "El RUT ingresado es inválido", clase: "m-badge--danger", tipo: 'danger' },
  fechaRecepcionmenorFechaSolicitud: { id: 23, title: "La Fecha de Solicitud es mayor que la Fecha de Recepción", clase: "m-badge--danger", tipo: 'danger' }
};

let Acciones = {
  crearSolicitud: 1,
  agregarPlaca: 2
};

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
  };

  let CargarLista = function () {
    let picker = $('#filtro-fechacarga').data('daterangepicker');

    $('#listacargasmasivas').mDatatable({
      data: {
        type: "local",
        source: cargasmasivas.filter((e) => {
          return moment(e.CargaMasiva, 'DD/MM/YYYY').isBetween(picker.startDate, picker.endDate);
        }),
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
        {
          field: "FechaHora",
          title: "Realizada",
          responsive: { visible: "lg" },
          type: "date",
          format: "DD/MM/YYYY"
        },
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
  };

  let _InitElementos = function () {
    if ($('#lista-registros').length > 0) {
      _InitTabla();
    }

    let nombreArchivo;

    $('#archivoCarga').change((oEvent) => {
      // Get The File From The Input
      let archivo = oEvent.target.files[0];

      if (archivo) {
        nombreArchivo = archivo.name;

        $('label[for="archivocarga"]').html(nombreArchivo);

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
              let region = objeto['RegionCliente'];

              if (region) region = region.replace(/\_/g, ' ');

              registros.push({
                NumeroSolicitud: objeto['NumeroSolicitud'],
                TipoSolicitud: objeto['TipoSolicitud'],
                FechaSolicitud: objeto['FechaSolicitud'],
                FechaRecepcion: objeto['FechaRecepcion'],
                NumeroCliente: objeto['NumeroCliente'],
                NombreCliente: objeto['NombreCliente'],
                CalleDireccionCliente: objeto['CalleDireccionCliente'],
                NumeroDireccionCliente: objeto['NumeroDireccionCliente'],
                RegionCliente: region,
                ComunaCliente: objeto['ComunaCliente'],
                NumeroTelefonoContacto: objeto['NumeroTelefonoContacto'],
                NumeroTelefonoContactoAdicional: objeto['NumeroTelefonoContactoAdicional'],
                RutCliente: objeto['RutCliente'],
                UnidadNegocio: objeto['UnidadNegocio'],
                Gerencia: objeto['Gerencia'],
                ObservacionAof: objeto['ObservacionAof'],
                Prioridad: objeto['Prioridad'],
                NumeroPlaca: objeto['Placa'],
                Errores: [],
                Acciones: []
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

    $(document).on('click', '.page-link', function () {
      $('.detalle-estados').tooltip();
    });
  };

  let _InitTabla = function () {
    tabla = $('#lista-registros').mDatatable({
      data: {
        type: "local",
        source: registros,
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
        input: $('#txtBuscar')
      },
      columns: [
        {
          field: "acciones", title: " ", responsive: { visible: "lg" }, template: function (e) {
            let error = '<i class="fa fa-times px-3" style="color: #DC3C41;font-size: 2rem;"></i>';
            let correcto = '<i class="fa fa-check px-3" style="color: #34BFA3;font-size: 2rem;"></i>';

            return e.Errores.filter((err) => {
              return err.tipo == 'danger';
            }).length > 0 ? error : correcto;
          }
        },
        {
          field: "estados", title: "Resultado", responsive: { visible: "lg" }, width: '240px', template: function (e) {
            let div = $('<div></div>');
            let boton = $('<button class="btn btn-sm btn-primary detalle-estados w-100" data-toggle="tooltip" data-placement="right" data-trigger="click" data-html="true" title="Tooltip on <b>right</b>"></button>');
            let Errors = e.Errores;
            let Actions = e.Acciones;

            let tooltip = $('<div></div>');

            if (Errors.length > 0) {
              if (Errors.length > 1) {
                boton.html('Se encontraron <b>' + Errors.length + '</b> errors <b>ver aquí</b>');
              } else {
                boton.html('Se encontró <b>' + Errors.length + '</b> error <b>ver aquí</b>');
              }

              Errors.forEach((e) => {
                tooltip.append('<li>' + e.title + '</li>');
              });
            } else {
              if (Actions.length > 1) {
                boton.html('Se realizarán <b>' + Actions.length + '</b> procesos <b>ver aquí</b>');
              } else {
                boton.html('Se realizará <b>' + Actions.length + '</b> proceso <b>ver aquí</b>');
              }

              Actions.forEach((a) => {
                if (a == Acciones.crearSolicitud) {
                  tooltip.append('<li>Se creará la solicitud N° ' + e.NumeroSolicitud + '</li>');
                }

                if (a == Acciones.agregarPlaca) {
                  tooltip.append('<li>Se agregará la placa ' + e.Placa + ' a la solicitud ' + e.NumeroSolicitud + '</li>');
                }
              });
            }

            boton.attr('title', tooltip.html());
            div.html(boton);
            return div.html();
          }
        },
        { field: "NumeroSolicitud", title: "Numero Solicitud", responsive: { visible: "lg" } },
        { field: "TipoSolicitud", title: "Tipo Solicitud", responsive: { visible: "lg" } },
        { field: "FechaSolicitud", title: "Fecha Solicitud", responsive: { visible: "lg" } },
        { field: "FechaRecepcion", title: "Fecha Recepción", responsive: { visible: "lg" } },
        { field: "NumeroCliente", title: "N° Cliente", responsive: { visible: "lg" } },
        { field: "NombreCliente", title: "Nombre Cliente", responsive: { visible: "lg" } },
        { field: "CalleDireccionCliente", title: "Calle Dirección", responsive: { visible: "lg" } },
        { field: "NumeroDireccionCliente", title: "Número Dirección", responsive: { visible: "lg" } },
        { field: "RegionCliente", title: "Región", responsive: { visible: "lg" } },
        { field: "ComunaCliente", title: "Comuna", responsive: { visible: "lg" } },
        { field: "NumeroTelefonoContacto", title: "Teléfono", responsive: { visible: "lg" } },
        { field: "NumeroTelefonoContactoAdicional", title: "Teléfono Adicional", responsive: { visible: "lg" } },
        { field: "RutCliente", title: "Rut Cliente", responsive: { visible: "lg" } },
        { field: "UnidadNegocio", title: "Unidad Negocio", responsive: { visible: "lg" } },
        { field: "Gerencia", title: "Gerencia", responsive: { visible: "lg" } },
        { field: "ObservacionAof", title: "Observación", responsive: { visible: "lg" } },
        { field: "Prioridad", title: "Prioridad", responsive: { visible: "lg" } },
        { field: "NumeroPlaca", title: "Placa", responsive: { visible: "lg" } },
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

  let _ProcesarRegistros = function () {
    try {
      // VALIDA QUE SE CARGÓ UN ARCHIVO
      if (registros.length == 0) {
        alert('Debe cargar un archivo primero');
        return;
      }

      let numeroSolicitudActual = 0;

      // BORRA LOS ERRORES Y ACCIONES ANTES DE VALIDAR
      registros.forEach((registro) => {
        registro.Errores = [];
        registro.Acciones = [];
      });

      // VALIDACIÓN EN EL SERVIDOR
      $.post('/CargaMasiva/Validar', { detalles: registros }, function (data) {
        registros = data;

        registros.forEach((registro) => {
          //VALIDA NUMERO DE SOLICITUD

          if (!registro.NumeroSolicitud) {
            registro.Errores.push(Errores.faltaNumeroSolicitud);
          }

          // VALIDA TIPO DE SOLICITUD
          if (!registro.TipoSolicitud) {
            registro.Errores.push(Errores.faltaTipoSolicitud);
          }

          // VALIDA LA FECHA DE SOLICITUD VACÍA
          if (!registro.FechaSolicitud) {
            registro.Errores.push(Errores.faltaFechaSolicitud);
          } else if (!Validaciones.ValidarFecha(registro.FechaSolicitud)) {
            // VALIDA EL FORMATO DE LA FECHA DE SOLICITUD
            registro.Errores.push(Errores.tipoFechaSolicitud);
          }

          // VALIDA LA FECHA DE RECEPCIÓN VACÍA
          if (!registro.FechaRecepcion) {
            registro.Errores.push(Errores.faltaFechaRecepcion);
          } else if (!Validaciones.ValidarFecha(registro.FechaRecepcion)) { // VALIDA EL FORMATO DE LA FECHA DE RECEPCIÓN
            registro.Errores.push(Errores.tipoFechaRecepcion);
          } else if (moment(registro.FechaRecepcion, 'DD/MM/YYYY') < moment(registro.FechaSolicitud, 'DD/MM/YYYY')) {
            registro.Errores.push(Errores.fechaRecepcionmenorFechaSolicitud);
          }

          // VALIDA EL NUMERO DE CLIENTE  
          if (!registro.NumeroCliente) {
            registro.Errores.push(Errores.faltaNumeroCliente);
          }

          // VALIDA NOMBRE DE CLIENTE  
          if (!registro.NombreCliente) {
            registro.Errores.push(Errores.faltaNombreCliente);
          }

          // VALIDA CalleDireccionCliente
          if (!registro.CalleDireccionCliente) {
            registro.Errores.push(Errores.faltaCalleDireccionCliente);
          }

          // VALIDA NumeroDireccionCliente
          if (!registro.NumeroDireccionCliente) {
            registro.Errores.push(Errores.faltaNumeroDireccionCliente);
          } else if (!Validaciones.ValidaNumero(registro.NumeroDireccionCliente)) {
            registro.Errores.push(Errores.tipoNumeroDireccionCliente);
          }

          // VALIDA LA  REGION 
          if (!registro.RegionCliente) {
            registro.Errores.push(Errores.faltaRegionCliente);
          }

          // VALIDA COMUNA 
          if (!registro.ComunaCliente) {
            registro.Errores.push(Errores.faltaComunaCliente);
          }

          //VALIDA NUMERO TELEFONO CONTACTO
          if (!registro.NumeroTelefonoContacto) {
            registro.Errores.push(Errores.faltaNumeroTelefonoContacto)
          }

          //VALIDA RUTCLIENTE
          if (!registro.RutCliente) {
            registro.Errores.push(Errores.faltaRUTCliente);
          } else if (!Validaciones.ValidaRut(registro.RutCliente)) {
            registro.Errores.push(Errores.rutInvalido);
          }

          //VALIDA UNIDADNEGOCIO
          if (!registro.UnidadNegocio) {
            registro.Errores.push(Errores.faltaUnidadNegocio);
          }

          // VALIDA GERENCIA 
          if (!registro.Gerencia) {
            registro.Errores.push(Errores.faltaGerencia);
          }

          // VALIDA PRIORIDAD 
          if (!registro.Prioridad) {
            registro.Errores.push(Errores.faltaPrioridad);
          }

          //VALIDA PLACA
          if (!registro.NumeroPlaca) {
            registro.Errores.push(Errores.faltaPlaca);
          }

          if (numeroSolicitudActual != registro.numeroSolicitud && registro.Errores.length == 0) {
            // SI ESTE NÚMERO DE SOLICITUD SE CREARÁ, SE VALIDA QUE NO SE REPITA PARA CREAR EN EL ARCHIVO
            if (registros.filter((r) => {
              return r.NumeroSolicitud == registro.NumeroSolicitud && (!r.Acciones || (r.Acciones && r.Acciones.indexOf(Acciones.crearSolicitud) >= 0));
            }).length > 0) {
              registro.Acciones.push(Acciones.agregarPlaca);
            } else {
              registro.Acciones.push(Acciones.crearSolicitud);
            }
          } else if ((registro.Errores.indexOf(Errores.faltaPlaca) < 0) && registro.numeroSolicitud == numeroSolicitudActual) {
            registro.Acciones.push(Acciones.agregarPlaca);
            registro.Errores = [];
          }

          numeroSolicitudActual = registro.NumeroSolicitud;
        });

        console.log(registros);

        tabla.originalDataSet = registros;
        tabla.load();

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
      if (registro.Acciones.contiene(Acciones.crearSolicitud)) {
        detalle.push({
          //propiedades del detalle
          numeroSolicitud: registro.numeroSolicitud
        });

        productosDetalle.push({
          numeroPlaca: registro.numeroPlaca,
          numeroSolicitud: registro.numeroSolicitud
        });
      } else if (registro.Acciones.contiene(Acciones.agregarPlaca)) {
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
  };

  let _ObtenerEstadosMalos = function (registro) {
    return registro.Errores.filter((e) => {
      return e.tipo == 'danger';
    });
  };

  let _CargarTabla = function () {
    //tabla.clear();
    let tabla;
    let data = [];

    registros.forEach((registro) => {
      if ($('input[name="resutadoRegistro"][value="1"]:checked').val() == undefined && registro.Errores.length > 0) {
        return;
      }

      if ($('input[name="resutadoRegistro"][value="2"]:checked').val() == undefined && registro.Errores.length == 0) {
        return;
      }

      let valores = [
        registro.Acciones || [],
        null,
        registro.Errores || [],
        registro.NumeroSolicitud || '',
        registro.TipoSolicitud || '',
        registro.NumeroCliente || '',
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

    if (hoja['!ref']) {

      let range = XLSX.utils.decode_range(hoja['!ref']);
      let C, R = range.s.r;

      for (C = range.s.c; C <= range.e.c; ++C) {
        let cell = hoja[XLSX.utils.encode_cell({ c: C, r: R })];

        if (cell && cell.t) headers.push(XLSX.utils.format_cell(cell));
      }
    }

    return headers;
  };

  let _VerificarFormato = function (columnas) {
    if (!columnas || columnas.length != 18) return false;
    if (columnas.indexOf('NumeroSolicitud') < 0) return false;
    if (columnas.indexOf('TipoSolicitud') < 0) return false;
    if (columnas.indexOf('FechaSolicitud') < 0) return false;
    if (columnas.indexOf('FechaRecepcion') < 0) return false;
    if (columnas.indexOf('NumeroCliente') < 0) return false;
    if (columnas.indexOf('NombreCliente') < 0) return false;
    if (columnas.indexOf('CalleDireccionCliente') < 0) return false;
    if (columnas.indexOf('NumeroDireccionCliente') < 0) return false;
    if (columnas.indexOf('RegionCliente') < 0) return false;
    if (columnas.indexOf('ComunaCliente') < 0) return false;
    if (columnas.indexOf('NumeroTelefonoContacto') < 0) return false;
    if (columnas.indexOf('NumeroTelefonoContactoAdicional') < 0) return false;
    if (columnas.indexOf('RutCliente') < 0) return false;
    if (columnas.indexOf('UnidadNegocio') < 0) return false;
    if (columnas.indexOf('Gerencia') < 0) return false;
    if (columnas.indexOf('ObservacionAof') < 0) return false;
    if (columnas.indexOf('Prioridad') < 0) return false;
    if (columnas.indexOf('Placa') < 0) return false;

    return true;
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
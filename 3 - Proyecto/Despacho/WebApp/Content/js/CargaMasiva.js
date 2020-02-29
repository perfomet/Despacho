import { isNumber } from "util";

let estados = {
  faltaNumeroSolicitud: { id: 1, title: "Falta el campo 'NumeroSolicitud'", class: "m-badge--danger", tipo: 'danger' },
  faltaTipoSolicitud: { id: 2, title: "Falta el campo 'TipoSolicitud'", class: "m-badge--danger", tipo: 'danger' },
  faltaFechaSolicitud: { id: 3, title: "Falta el campo 'FechaSolicitud'", class: "m-badge--danger", tipo: 'danger' },
  faltaFechaRecepcion: { id: 4, title: "Falta el campo 'FechaRecepcion'", class: "m-badge--danger", tipo: 'danger' },
  faltaNumeroCliente: { id: 5, title: "Falta el campo 'NumeroCliente'", class: "m-badge--danger", tipo: 'danger' },
  faltaNombreCliente: { id: 6, title: "Falta el campo 'NombreCliente'", class: "m-badge--danger", tipo: 'danger' },
  faltaCalleDireccionCliente: { id: 7, title: "Falta el campo 'CalleDireccionCliente'", class: "m-badge--danger", tipo: 'danger' },
  faltaNumeroDireccionCliente: { id: 8, title: "Falta el campo 'NumeroDireccionCliente'", class: "m-badge--danger", tipo: 'danger' },
  faltaRegionCliente: { id: 9, title: "Falta el campo 'RegionCliente'", class: "m-badge--danger", tipo: 'danger' },
  faltaComunaCliente: { id: 10, title: "Falta el campo 'ComunaCliente'", class: "m-badge--danger", tipo: 'danger' },
  faltaNumeroTelefonoContacto: { id: 11, title: "Falta el campo 'NumeroTelefonoContacto'", class: "m-badge--danger", tipo: 'danger' },
  faltaNumeroTelefonoContactoAdicional: { id: 12, title: "Falta el campo 'NumeroTelefonoContactoAdicional'", class: "m-badge--danger", tipo: 'danger' },
  faltaRUTCliente: { id: 13, title: "Falta el campo 'RUTCliente'", class: "m-badge--danger", tipo: 'danger' },
  rutInvalido: { id: 14, title: "El RUT ingresado no es válido", class: "m-badge--danger", tipo: 'danger' },
  faltaUnidadNegocio: { id: 15, title: "Falta el campo 'UnidadNegocio'", class: "m-badge--danger", tipo: 'danger' },
  faltaGerencia: { id: 16, title: "Falta el campo 'Gerencia'", class: "m-badge--danger", tipo: 'danger' },
  faltaObservacionAof: { id: 17, title: "Falta el campo 'ObservacionAof'", class: "m-badge--danger", tipo: 'danger' },
  faltaPrioridad: { id: 18, title: "Falta el campo 'Prioridad'", class: "m-badge--danger", tipo: 'danger' },
  faltaPlaca: { id: 19, title: "Falta el campo 'Placa'", class: "m-badge--danger", tipo: 'danger' },
  tipoSolicitud: { id: 20, title: "Tipo de Solicitud incorrecta", class: "m-badge--danger", tipo: 'danger' },
  tipoFechaSolicitud: { id: 21, title: "Fecha de Solicitud incorrecta", class: "m-badge--danger", tipo: 'danger' },
  tipoFechaRecepcion: { id: 22, title: "Fecha de Recepción incorrecta", class: "m-badge--danger", tipo: 'danger' },
  tipoNumeroCliente: { id: 23, title: "Numero Cliente incorrecto", class: "m-badge--danger", tipo: 'danger' },
  tipoNombreCliente: { id: 24, title: "Nombre Cliente incorrecto", class: "m-badge--danger", tipo: 'danger' },
  tipoRegionCliente: { id: 25, title: "Nombre Región incorrecto", class: "m-badge--danger", tipo: 'danger' },
  tipoComunaCliente: { id: 26, title: "ComunaCliente incorrecta", class: "m-badge--danger", tipo: 'danger' },
  tipoGerencia: { id: 27, title: "Gerencia incorrecta", class: "m-badge--danger", tipo: 'danger' },
  tipoPrioridad: { id: 28, title: "Prioridad incorrecta", class: "m-badge--danger", tipo: 'danger' },
  tipoPlaca: { id: 29, title: "Placa incorrecta", class: "m-badge--danger", tipo: 'danger' }
};

let acciones = {
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
                /* tarea escrbir los nombres de los registros igual que cómo están en el objeto*/
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
        return;
      }

      let numeroSolicitudActual = 0;

      registros.forEach((registro) => {
        // VACÍA LAS LISTAS DE ESTADOS Y ACCIONES
        registro.estados = [];
        registro.acciones = [];

        // VALIDA QUE EL NUMERO DE SOLICITUD EXISTA Y SEA VÁLIDO
        if (!registro.NumeroSolicitud) {
          
        }
        else {
          registro.estados.push(estados.numeroSolicitud);
        }
        
        if (isNumber(!registro.NumeroSolicitud)) {

        }
        else {

        }
        
        if (!registro.TipoSolicitud) {
          
        }
        else {
          registro.estados.push(estados.faltaTipoSolicitud);
        }

        // VALIDA QUE LA FECHA DE SOLICITUD EXISTA Y SEA VÁLIDO
        if (!registro.FechaSolicitud) {
          registro.estados.push(estados.FechaSolicitud);
        }
        else {

        }
        // VALIDA QUE LA FECHA DE RECEPCIÓN EXISTA Y SEA VÁLIDO
        if (!registro.FechaRecepcion) {
          registro.estados.push(estados.FechaRecepcion);
        }
        else {

        }
        // SI EL TIPO DE NUMERO DE CLIENTE ES VALIDO 
        if (!registro.TipoNumeroCliente) {
          registro.estados.push(estados.tipoNumeroCliente);
        }
        else {

        }
        // VALIDA QUE EL NUMERO DE CLIENTE EXISTA
        if (!registro.TipoNumeroCliente) {
          registro.estados.push(estados.tipoNumeroCliente);
        }
        else {

        }
        // SI EL TIPO DE NOMBRE DE CLIENTE ES VALIDO 
        if (!registro.TipoNombreCliente) {
          registro.estados.push(estados.tipoNombreCliente);
        }
        else {

        }
        // VALIDA QUE EL NOMBRE DE CLIENTE EXISTA
        if (!registro.TipoNombreCliente) {
          registro.estados.push(estados.tipoNombreCliente);
        }
        else {

        }
        // SI EL TIPO DE REGION ES VALIDO 
        if (!registro.RegionCliente) {
          registro.estados.push(estados.tipoRegionCliente);
        }

        // VALIDA QUE LA REGION EXISTA
        if (!registro.RegionCliente) {
          registro.estados.push(estados.faltaRegionCliente);
        }
        else {

        }
        // SI EL TIPO DE COMUNA ES VALIDO 
        if (!registro.ComunaCliente) {
          registro.estados.push(estados.faltaComunaCliente);
        }
        else {

        }
        // VALIDA QUE LA COMUNA EXISTA
        if (!registro.ComunaCliente) {
          registro.estados.push(estados.tipoComunaCliente);
        }
        else {

        }
        // SI EL TIPO DE GERENCIA ES VALIDO 
        if (!registro.Gerencia) {
          registro.estados.push(estados.tipoGerencia);
        }
        else {

        }
        // VALIDA QUE LA GERENCIA EXISTA
        if (!registro.Gerencia) {
          registro.estados.push(estados.tipoGerencia);
        }
        else {

        }
        // SI EL TIPO DE PRIORIDAD ES VALIDO 
        if (!registro.Prioridad) {
          registro.estados.push(estados.tipoPrioridad);
        }
        else {

        }
        // VALIDA QUE LA PRIORIDAD EXISTA
        if (!registro.Prioridad) {
          registro.estados.push(estados.tipoPrioridad);
        }
        else {

        }
        // SI EL TIPO DE PLACA ES VALIDO 
        if (!registro.Placa) {
          registro.estados.push(estados.tipoPlaca);
        }
        else {

        }
        // VALIDA QUE LA PLACA EXISTA
        if (!registro.Placa) {
          registro.estados.push(estados.tipoPlaca);
        }
        else {

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
          (registro.estados.indexOf(estados.tipoSolicitud) < 0) &&
          (registro.estados.indexOf(estados.tipoRegionCliente) < 0) &&
          (registro.estados.indexOf(estados.tipoComunaCliente) < 0) &&
          (registro.estados.indexOf(estados.tipoUnidadNegocio) < 0) &&
          (registro.estados.indexOf(estados.tipoGerencia) < 0) &&
          (registro.estados.indexOf(estados.tipoPrioridad) < 0) &&
          (registro.estados.indexOf(estados.tipoPlaca) < 0)

        ) {
          let rutcliente = registro.RutCliente;
          if (!_ValidaRut(rutcliente)) {
            registro.estados.push(estados.rutInvalido);
          }
          else {

          }
        }

        // SI NO HAY ERRORES DE VALIDACIÓN, PROSIGUE CON LA VALIDACIÓN DE LA PLACA
        if (
          (registro.estados.indexOf(estados.tipoNumeroCliente) < 0) &&
          (registro.estados.indexOf(estados.tipoNombreCliente) < 0) &&
          (registro.estados.indexOf(estados.tipoRegionCliente) < 0) &&
          (registro.estados.indexOf(estados.tipoComunaCliente) < 0) &&
          (registro.estados.indexOf(estados.tipoGerencia) < 0) &&
          (registro.estados.indexOf(estados.tipoPrioridad) < 0) &&
          (registro.estados.indexOf(estados.tipoPlaca) < 0) &&
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

      _CargarTabla();

      $('.detalle-estados').tooltip();

      $('#divTablaResultado').show();

      alert('Archivo procesado, para ver detalle ir a histórico de cargas');
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
    if (columnas.indexOf('TipoSolicitud') < 0) {
      return false;
    }

    if (columnas.indexOf('FechaSolicitud') < 0) {
      return false;
    }

    if (columnas.indexOf('FechaRecepcion') < 0) {
      return false;
    }

    if (columnas.indexOf('NumeroCliente') < 0) {
      return false;
    }

    if (columnas.indexOf('NombreCliente') < 0) {
      return false;
    }

    if (columnas.indexOf('CalleDireccionCliente') < 0) {
      return false;
    }

    if (columnas.indexOf('NumeroDireccionCliente') < 0) {
      return false;
    }

    if (columnas.indexOf('RegionCliente') < 0) {
      return false;
    }

    if (columnas.indexOf('ComunaCliente') < 0) {
      return false;
    }

    if (columnas.indexOf('NumeroTelefonoContacto') < 0) {
      return false;
    }

    if (columnas.indexOf('NumeroTelefonoContactoAdicional') < 0) {
      return false;
    }

    if (columnas.indexOf('RutCliente') < 0) {
      return false;
    }

    if (columnas.indexOf('UnidadNegocio') < 0) {
      return false;
    }
    if (columnas.indexOf('Gerencia') < 0) {
      return false;
    }
    if (columnas.indexOf('ObservacionAof') < 0) {
      return false;
    }
    if (columnas.indexOf('Prioridad') < 0) {
      return false;
    }
    if (columnas.indexOf('Placa') < 0) {
      return false;
    }
    return true;
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

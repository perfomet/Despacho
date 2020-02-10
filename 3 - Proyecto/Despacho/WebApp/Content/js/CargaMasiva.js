let CargaMasiva = function () {

  let cargasmasivas;

    let Init = function () {
        InitElementos();
    };

  let InitElementos = function () {
    $('.m-select2').select2();

    f($("#lista-cargasmasivas").length > 0) {
      window.crearSelectorFecha("#filtro-fecha-solicitud", moment().subtract(6, 'days'), moment());

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
      $('#lista-cargasmasivas').mDatatable({
        data: {
          type: "local",
          source: cargas,
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
                { field: "Usuario", title: "Usuario", responsive: { visible: "lg" } },
                { field: "FechaHora", title: "Realizada", responsive: { visible: "lg" } },
                { field: "Archivo", title: "Archivo", responsive: { visible: "lg" } }
                ], true, true);
              ]
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
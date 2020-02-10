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
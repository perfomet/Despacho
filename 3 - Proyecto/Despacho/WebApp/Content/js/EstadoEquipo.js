let EstadoEquipo = function () {

  let Init = function () {
    InitElementos();
  };

  let InitElementos = function () {
    $.post("/EstadoEquipo/Listar", { patente: 0 }, function (estadosequipos) {
      $(".m_datatable").mDatatable({
        data: {
          type: "local",
          source: estadosequipos,
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
          input: $("#buscarEstadoEquipo")
        },
        columns:
          [
            { field: "EstadoEquipoId", title: "#", width: 50, selector: !1, textAlign: "center" },
            { field: "Descripcion", title: "Descripción", responsive: { visible: "lg" } }
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
  TipoDocumento.init();
});
let UnidadMedida = function () {

  let Init = function () {
    InitElementos();
  };

  let InitElementos = function () {
    $.post("/UnidadMedida/Listar", { UnidadMedidaId: 0 }, function (unidadesmedida) {
      $(".m_datatable").mDatatable({
        data: {
          type: "local",
          source: unidadesmedida,
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
          input: $("#buscarUnidadMedida")
        },
        columns: [
          { field: "UnidadMedidaId", title: "#", width: 50, selector: !1, textAlign: "center" },
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
  UnidadMedida.init();
});
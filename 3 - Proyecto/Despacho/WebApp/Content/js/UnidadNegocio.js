let UnidadNegocio = function () {

  let Init = function () {
    InitElementos();
  };

  let InitElementos = function () {
    $.post("/UnidadNegocio/Listar", { Id: 0 }, function (unidadnegocio) {
      $("#listaunidadesdenegocio").mDatatable({
        data: {
          type: "local",
          source: unidadnegocio,
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
          input: $("#buscarUnidadNegocio")
        },
        columns: [
          { field: "UnidadNegocioId", title: "#", width: 50, selector: !1, textAlign: "center" },
          { field: "Descripcion", title: "Descripción", responsive: { visible: "lg" } },
          { field: "ClienteId", title: "# Cliente", responsive: { visible: "lg" } }
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
  UnidadNegocio.init();
});
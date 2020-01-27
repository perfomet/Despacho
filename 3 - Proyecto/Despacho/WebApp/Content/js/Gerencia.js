let Gerencia = function () {

  let Init = function () {
    InitElementos();
  };

  let InitElementos = function () {
    $.post("/Gerencia/Listar", { gerenciaId: 0 }, function (gerencias) {
      $("#listagerencias").mDatatable({
        data: {
          type: "local",
          source: gerencias,
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
          input: $("#buscargerencia")
        },
        columns: [
          { field: "GerenciaId", title: "#", width: 50, selector: !1, textAlign: "center" },
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
  Gerencia.init();
});
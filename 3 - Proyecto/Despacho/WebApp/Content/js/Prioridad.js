let Prioridad = function () {

  let Init = function () {
    InitElementos();
  };

  let InitElementos = function () {
    $.post("/Prioridad/Listar", { id: 0 }, function (prioridades) {
      $("#listaprioridades").mDatatable({
        data: {
          type: "local",
          source: prioridades,
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
          input: $("#buscarprioridad")
        },
        columns: [
          { field: "PrioridadId", title: "#", width: 50, selector: !1, textAlign: "center" },
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
  Prioridad.init();
});
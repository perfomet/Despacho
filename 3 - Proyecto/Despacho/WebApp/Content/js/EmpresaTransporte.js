let EmpresaTransporte = function () {

  let Init = function () {
    InitElementos();
  };

  let InitElementos = function () {
    $.post("/EmpresaTransporte/Listar", { empresatransporteId: 0 }, function (empresasdetransportes) {
      $("#listaEmpresaTransporte").mDatatable({
        data: {
          type: "local",
          source: empresasdetransportes,
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
          input: $("#buscarEmpresaTransporte")
        },
        columns: [
          { field: "EmpresaTransporteId", title: "#", width: 50, selector: !1, textAlign: "center" },
          { field: "Nombre", title: "Nombre", responsive: { visible: "lg" } },
          { field: "EsPropia", title: "Es Propia", responsive: { visible: "lg" } }
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
  EmpresaTransporte.init();
});
let EmpresaTransporte = function () {

  let Init = function () {
    InitElementos();
  };

  let InitElementos = function () {
    if ($("#listaempresastransporte").length > 0) {
      cargarTabla("EmpresaTransporteId", "EmpresaTransporte", { Id: 0 }, "#listaempresatransporte", "#buscarempresatransporte", [
        { field: "EmpresaTransporteId", title: "#", width: 50, selector: !1, textAlign: "center" },
        { field: "Nombre", title: "Nombre", responsive: { visible: "lg" } },
        { field: "EsPropia", title: "Es Propia", responsive: { visible: "lg" }, template: function (e, a, i) { return e.EsPropia == true ? "Si" : "No"; }  },
        { field: "EstaActivo", title: "Activo", responsive: { visible: "lg" }, template: function (e, a, i) { return e.EstaActivo == true ? "Si" : "No"; } }
      ], true, true);
    }

    $('#btnGuardar').click(function () {
      let id = $('#id').val();
      let activo = $('#activo').val();
      let nombre = $('#nombre').val();
      let espropia = $('#espropia').val();
      
      $.post("/EmpresaTransporte/" + (id > 0 ? "Edit" : "Create"), {
        EmpresaTransporteId: id,
        Nombre: nombre,
        EsPropia: espropia,
        EstaActivo: activo
      }, function (data) {
        if (data.exito) {
          mensaje("Éxito", "Información guardada correctamente", "exito", function () { location.href = "/EmpresaTransporte/Index"; });
        } else {
          mensaje("Error", "No se pudo guardar la información", "error");
        }
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
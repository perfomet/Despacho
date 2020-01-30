let TipoDocumento = function () {

  let Init = function () {
    InitElementos();
  };

  let InitElementos = function () {
    if ($("#listaClientes").length > 0) {
      cargarTabla("TipoDocumentoId", "TipoDocumento", { Id: 0 }, "#listatiposdedocumentos", "#buscartipodocumento",
      [
        { field: "TipoDocumentoId", title: "#", width: 50, selector: !1, textAlign: "center" },
        { field: "Descripcion", title: "Descripción", responsive: { visible: "lg" } },
        { field: "EstaActivo", title: "Activo", responsive: { visible: "lg" }, template: function (e, a, i) { return e.EstaActivo == true ? "Si" : "No"; } }
       ], true, true);
    }
    $('#btnGuardar').click(function () {
      let id = $('#id').val();
      let activo = $('#activo').val();
      let descripcion = $('#descripcion').val();
      

      $.post("/TipoDocumento/" + (id > 0 ? "Edit" : "Create"), {
       TipoDocumentoId: id,
        Descripcion: descripcion,
        EstaActivo: activo
      }, function (data) {
        if (data.exito) {
          mensaje("Éxito", "Información guardada correctamente", "exito", function () { location.href = "/TipoDocumento/Index"; });
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
  TipoDocumento.init();
});
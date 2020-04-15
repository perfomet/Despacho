let TipoDocumento = function () {

  let Init = function () {
    InitElementos();
  };

  let InitElementos = function () {
    if ($("#listadocumentos").length > 0) {
      cargarTabla("Tipodocumentoid", "TipoDocumento", { tipodocumentoid: 0 }, "#listadocumentos", "#buscartipodocumento",
      [
        { field: "Tipodocumentoid", title: "#", width: 50, selector: !1, textAlign: "center" },
        { field: "Descripcion", title: "Descripción", responsive: { visible: "lg" } },
        { field: "EstaActivo", title: "Activo", responsive: { visible: "lg" }, template: function (e, a, i) { return e.EstaActivo == true ? "Si" : "No"; } }
       ], true, true);
    }

    $('#btnGuardar').click(function () {
      let id = parseInt($('#id').val());
      let activo = $('#activo').val();
      let descripcion = $('#descripcion').val();

      $.post(webroot + "/TipoDocumento/" + (id > 0 ? "Edit" : "Create"), {
        Tipodocumentoid: id,
        Descripcion: descripcion,
        EstaActivo: activo
      }, function (data) {
        if (data.exito) {
          mensaje("Éxito", "Información guardada correctamente", "exito", function () { location.href = webroot + "/TipoDocumento/Index"; });
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
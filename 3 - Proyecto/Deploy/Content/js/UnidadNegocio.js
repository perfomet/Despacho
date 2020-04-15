let UnidadNegocio = function () {

  let Init = function () {
    InitElementos();
  };

  let InitElementos = function () {
    $('.m-select2').select2();

    if ($("#listaunidadesdenegocio").length > 0) {
      cargarTabla("UnidadNegocioId", "UnidadNegocio", { Id: 0 }, "#listaunidadesdenegocio", "#buscarunidadnegocio", [
        { field: "UnidadNegocioId", title: "#", width: 50, selector: !1, textAlign: "center" },
        { field: "Descripcion", title: "Descripción", responsive: { visible: "lg" } },
        { field: "Clientenombre", title: "Cliente", responsive: { visible: "lg" } },
        { field: "EstaActivo", title: "Activo", responsive: { visible: "lg" }, template: function (e, a, i) { return e.EstaActivo == true ? "Si" : "No"; } }
      ], true, true);
    }

    $('#btnGuardar').click(function () {
      let id = $('#id').val();
      let activo = $('#activo').val();
      let descripcion = $('#descripcion').val();
      let cliente = $('#clienteid').val();
   

      $.post(webroot + "/UnidadNegocio/" + (id > 0 ? "Edit" : "Create"), {
        UnidadNegocioId: id,
        Descripcion: descripcion,
        ClienteId: cliente,
        EstaActivo: activo
      }, function (data) {
        if (data.exito) {
          mensaje("Éxito", "Información guardada correctamente", "exito", function () { location.href = webroot + "/UnidadNegocio/Index"; });
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
  UnidadNegocio.init();
});
let TipoSolicitud = function () {

  let Init = function () {
    InitElementos();
  };

  let InitElementos = function () {
    if ($("#listasolicitudes").length > 0) {
      cargarTabla("Tiposolicitudid", "TipoSolicitud", { Tiposolicitudid: 0 }, "#listasolicitudes", "#buscartiposolicitud",
      [
        { field: "Tiposolicitudid", title: "#", width: 50, selector: !1, textAlign: "center" },
        { field: "Descripcion", title: "Descripción", responsive: { visible: "lg" } },
        { field: "Observaciones", title: "Observaciones", responsive: { visible: "lg" } },
        { field: "EstaActivo", title: "Activo", responsive: { visible: "lg" }, template: function (e, a, i) { return e.EstaActivo == true ? "Si" : "No"; } }
        ], true, true);

      }

    $('#btnGuardar').click(function () {
      let id = $('#id').val();
      let activo = $('#activo').val();
      let descripcion = $('#descripcion').val();
      let observaciones = $('#observaciones').val();
      
      $.post("/TipoSolicitud/" + (id > 0 ? "Edit" : "Create"), {
        Tiposolicitudid: id,
        Descripcion: descripcion,
        Observaciones: observaciones,
        EstaActivo: activo
      }, function (data) {
        if (data.exito) {
          mensaje("Éxito", "Información guardada correctamente", "exito", function () { location.href = "/TipoSolicitud/Index"; });
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
  TipoSolicitud.init();
});
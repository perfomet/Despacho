let Prioridad = function () {

  let Init = function () {
    InitElementos();
  };

  let InitElementos = function () {
    if ($("#listaprioridades").length > 0) {
      cargarTabla("prioridadid", "Prioridad", { id: 0 }, "#listaprioridades", "#buscarprioridad", [
        { field: "prioridadid", title: "#", width: 50, selector: !1, textAlign: "center" },
        { field: "descripcion", title: "Descripción", responsive: { visible: "lg" } },
        { field: "EstaActivo", title: "Activo", responsive: { visible: "lg" }, template: function (e, a, i) { return e.EstaActivo == true ? "Si" : "No"; } }
      ], true, true);
    }

    $('#btnGuardar').click(function () {
      let id = $('#id').val();
      let activo = $('#activo').val();
      let descripcion = $('#descripcion').val();

      $.post(webroot + "/Prioridad/" + (id > 0 ? "Edit" : "Create"), {
        PrioridadId: id,
        Descripcion: descripcion,
        EstaActivo: activo
      }, function (data) {
        if (data.exito) {
          mensaje("Éxito", "Información guardada correctamente", "exito", function () { location.href = "~/Prioridad/Index"; });
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
  Prioridad.init();
});
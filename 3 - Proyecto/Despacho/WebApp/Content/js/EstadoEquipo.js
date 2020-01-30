let EstadoEquipo = function () {

  let Init = function () {
    InitElementos();
  };

  let InitElementos = function () {
    if ($("#listaestadosequipos").length > 0) {
      cargarTabla("EstadoEquipoId", "EstadoEquipo", { Id: 0 }, "#listaestadoequipos", "#buscarestadoequipo", [
        { field: "EstadoEquipoId", title: "#", width: 50, selector: !1, textAlign: "center" },
        { field: "Descripcion", title: "descripción", responsive: { visible: "lg" } },
        { field: "EstaActivo", title: "Activo", responsive: { visible: "lg" }, template: function (e, a, i) { return e.EstaActivo == true ? "Si" : "No"; } }
      ], true, true);
    }

    $('#btnGuardar').click(function () {
      let id = $('#id').val();
      let activo = $('#activo').val();
      let nombre = $('#descripcion').val();
      
      $.post("/EstadoEquipo/" + (id > 0 ? "Edit" : "Create"), {
        EstadoEquipoId: id,
        Descripcion: descripcion,
        EstaActivo: activo
      }, function (data) {
        if (data.exito) {
          mensaje("Exito", "Información guardada correctamente", "exito", function () { location.href = "/EstadoEquipo/Index"; });
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
  EstadoEquipo.init();
});
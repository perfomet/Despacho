let UnidadMedida = function () {

  let Init = function () {
    InitElementos();
  };

  let InitElementos = function () {
    if ($("#listaunidadesmedida").length > 0) {
      cargarTabla("UnidadMedidaId", "UnidadMedida", { Id: 0 }, "#listaunidadesmedida", "#buscarunidadmedida",
        [
          { field: "UnidadMedidaId", title: "#", width: 50, selector: !1, textAlign: "center" },
          { field: "Descripcion", title: "Descripción", responsive: { visible: "lg" } },
          { field: "EstaActivo", title: "Activo", responsive: { visible: "lg" }, template: function (e, a, i) { return e.EstaActivo == true ? "Si" : "No"; } }

        ], true, true);

    }
    $('#btnGuardar').click(function () {
      let id = $('#id').val();
      let activo = $('#activo').val();
      let descripcion = $('#descripcion').val();
      
      $.post(webroot + "/UnidadMedida/" + (id > 0 ? "Edit" : "Create"), {
        UnidadMedidaId: id,
        Descripcion: descripcion,
        EstaActivo: activo
      }, function (data) {
        if (data.exito) {
          mensaje("Éxito", "Información guardada correctamente", "exito", function () { location.href = webroot + "/UnidadMedida/Index"; });
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
  UnidadMedida.init();
});
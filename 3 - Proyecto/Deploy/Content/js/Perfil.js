let Perfil = function () {

  let Init = function () {
    InitElementos();
  };

  let InitElementos = function () {
    if ($("#listaperfil").length > 0) {
      cargarTabla("Perfilid", "Perfil", { Id: 0 }, "#listaperfil", "#buscarperfil", [
        { field: "Perfilid", title: "#", width: 50, selector: !1, textAlign: "center" },
        { field: "Descripcion", title: "Descripción", responsive: { visible: "lg" } },
        { field: "EstaActivo", title: "Activo", responsive: { visible: "lg" }, template: function (e, a, i) { return e.EstaActivo == true ? "Si" : "No"; } }
      ], true, true);
    }

    $('#btnGuardar').click(function () {
      let id = $('#id').val();
      let activo = $('#activo').val();
      let descripcion = $('#descripcion').val();
     
      $.post(webroot + "/Perfil/" + (id > 0 ? "Edit" : "Create"), {
        Perfilid: id,
        Descripcion: descripcion,
        EstaActivo: activo
      }, function (data) {
        if (data.exito) {
          mensaje("Éxito", "Información guardada correctamente", "exito", function () { location.href = "~/Perfil/Index"; });
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
  Perfil.init();
});
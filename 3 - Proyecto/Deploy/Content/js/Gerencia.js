let Gerencia = function () {

  let Init = function () {
    InitElementos();
  };

  let InitElementos = function () {
    $('.m-select2').select2();

    if ($("#listagerencias").length > 0) {
      cargarTabla("Gerenciaid", "Gerencia", { id: 0 }, "#listagerencias", "#buscargerencia", [
      { field: "Gerenciaid", title: "#", width: 50, selector: !1, textAlign: "center" },
      { field: "Descripcion", title: "Descripción", responsive: { visible: "lg" } },
      { field: "Clientenombre", title: "Cliente", responsive: { visible: "lg" } },
      { field: "EstaActivo", title: "Activo", responsive: { visible: "lg" }, template: function (e, a, i) { return e.EstaActivo == true ? "Si" : "No"; } }
      ], true, true);
    }
    $('#btnGuardar').click(function () {
      let id = $('#id').val();
      let activo = $('#activo').val();
      let descripcion = $('#descripcion').val();
      let clienteid = $('#clienteid').val();
      
      $.post(webroot + "/Gerencia/" + (id > 0 ? "Edit" : "Create"), {
        Gerenciaid: id,
        Descripcion: descripcion,
        Clienteid: clienteid,
        EstaActivo: activo
      }, function (data) {
        if (data.exito) {
          mensaje("Éxito", "Información guardada correctamente", "exito", function () { location.href = "~/Gerencia/Index"; });
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
  Gerencia.init();
});
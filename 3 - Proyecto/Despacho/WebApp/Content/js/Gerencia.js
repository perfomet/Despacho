let Gerencia = function () {

  let Init = function () {
    InitElementos();
  };

  let InitElementos = function ()
    {
    if ($("#listagerencias").length > 0)
    {
      cargarTabla("gerenciaid", "Gerencia", { Id: 0 }, "#listagerencias", "#buscargerencia",
        [
          { field: "gerenciaid", title: "#", width: 50, selector: !1, textAlign: "center" },
          { field: "descripcion", title: "Descripción", responsive: { visible: "lg" } },
          { field: "clienteid", title: "# Cliente", responsive: { visible: "lg" } },
          { field: "EstaActivo", title: "Activo", responsive: { visible: "lg" }, template: function (e, a, i) { return e.EstaActivo == true ? "Si" : "No"; } }
        ], true, true);

    }
    $('#btnGuardar').click(function () {
      let id = $('#id').val();
      let activo = $('#activo').val();
      let descripcion = $('#descripcion').val();
      let clienteid = $('#clienteid').val();
      
      $.post("/Gerencia/" + (id > 0 ? "Edit" : "Create"), {
        gerenciaid: id,
        descripcion: descripcion,
        clienteid: clienteid,
        EstaActivo: activo
      }, function (data) {
        if (data.exito) {
          mensaje("Éxito", "Información guardada correctamente", "exito", function () { location.href = "/Gerencia/Index"; });
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
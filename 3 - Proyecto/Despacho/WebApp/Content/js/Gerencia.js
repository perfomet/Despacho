let Gerencia = function () {

  let Init = function () {
    InitElementos();
  };

  let InitElementos = function ()
    {
    if ($("#listagerencia").length > 0)
    {
      cargarTabla("GerenciaId", "Descripcion", { Id: 0 }, "#listagerencias", "#buscargerencia",
        [
          { field: "GerenciaId", title: "#", width: 50, selector: !1, textAlign: "center" },
          { field: "Descripcion", title: "Descripción", responsive: { visible: "lg" } },
          { field: "ClienteId", title: "# Cliente", responsive: { visible: "lg" } },
          { field: "EstaActivo", title: "Activo", responsive: { visible: "lg" }, template: function (e, a, i) { return e.EstaActivo == true ? "Si" : "No"; } }
        ], true, true);

    }
    $('#btnGuardar').click(function () {
      let id = $('#id').val();
      let activo = $('#activo').val();
      let descripcion = $('#descripcion').val();
      let clienteid = $('#clienteid').val();
      
      $.post("/Gerencia/" + (id > 0 ? "Edit" : "Create"), {
        GerenciaId: id,
        Descripcion: descripcion,
        ClienteId: clienteid,
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
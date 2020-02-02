let Camion = function () {

    let Init = function () {
        InitElementos();
    };

    let InitElementos = function () {
        if ($("#listacamiones").length > 0) {
            cargarTabla("Patente", "Camion", { Patente: "" }, "#listacamiones", "#buscarcamion", [
                { field: "Patente", title: "Patente", width: 100, selector: !1, textAlign: "center" },
                { field: "Descripcion", title: "Descripción", responsive: { visible: "lg" } },
                { field: "Empresatransporteid", title: "Empresa Transporte", responsive: { visible: "lg" } },
                { field: "EstaActivo", title: "Activo", responsive: { visible: "lg" }, template: function (e, a, i) { return e.EstaActivo == true ? "Si" : "No"; } }
            ], true, true);
      }
      $('#btnGuardar').click(function () {
        let id = $('#patente').val();
        let activo = $('#activo').val();
        let descripcion = $('#descripcion').val();
        let empresatransporteid = $('#empresatransporteid').val();
            
        $.post("/Camion/" + (id > 0 ? "Edit" : "Create"), {
          patente: id,
          Descripcion: descripcion,
          EmpresaTransporteId: empresatransporteid,
          EstaActivo: activo
        }, function (data) {
          if (data.exito) {
            mensaje("Éxito", "Información guardada correctamente", "exito", function () { location.href = "/Camion/Index"; });
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
  Camion.init();
});
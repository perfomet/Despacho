let Camion = function () {

    let Init = function () {
        InitElementos();
    };

    let InitElementos = function () {
        if ($("#listaCamiones").length > 0) {
            cargarTabla("Patente", "Camion", { patentecamion: "" }, "#listaCamiones", "#buscarCamion", [
                { field: "Patente", title: "Patente", width: 100, selector: !1, textAlign: "center" },
                { field: "Descripcion", title: "Descripción", responsive: { visible: "lg" } },
                { field: "EmpresaTransporte", title: "Empresa Transporte", responsive: { visible: "lg" } },
                { field: "EsPropia", title: "Propia", responsive: { visible: "lg" }, template: function (e, a, i) { return e.espropia == true ? "Si" : "No"; } },
                { field: "EstaActivo", title: "Activo", responsive: { visible: "lg" }, template: function (e, a, i) { return e.EstaActivo == true ? "Si" : "No"; } }
            ], true, true);
      }
      $('#btnGuardar').click(function () {
        let id = $('#id').val();
        let activo = $('#activo').val();
        let nombre = $('#nombre').val();
        let rut = $('#rut').val();
        let vrut = $('#vrut').val();
        let sufijo = $('#sufijo').val();

        $.post("/Cliente/" + (id > 0 ? "Edit" : "Create"), {
          ClienteId: id,
          Nombre: nombre,
          Rut: rut,
          VRut: vrut,
          Sufijo: sufijo,
          EstaActivo: activo
        }, function (data) {
          if (data.exito) {
            mensaje("Exito", "Información guardada correctamente", "exito", function () { location.href = "/Cliente/Index"; });
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
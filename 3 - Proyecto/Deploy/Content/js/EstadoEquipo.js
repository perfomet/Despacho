let EstadoEquipo = function () {

  let Init = function () {
    InitElementos();
  };

  let InitElementos = function () {
    $('.m-select2').select2();

    if ($("#listaestadoequipos").length > 0) {
      cargarTabla("Estadoequipoid", "EstadoEquipo", { Id: 0 }, "#listaestadoequipos", "#buscarestadoequipo", [
        { field: "Estadoequipoid", title: "#", width: 50, selector: !1, textAlign: "center" },
        { field: "Descripcion", title: "Descripción", responsive: { visible: "lg" } },
        {
          field: "Bins", title: "Bin", responsive: { visible: "lg" }, template: function (e, a, i) {
            let bins = "";
            e.Bins.forEach((bin) => {
              bins += (bins != '' ? "<br/>" : "") + bin.Bin;
            });
            return bins;
          }
        },
        { field: "EstaActivo", title: "Activo", responsive: { visible: "lg" }, template: function (e, a, i) { return e.EstaActivo == true ? "Si" : "No"; } }
      ], true, true);
    }

    $('#btnGuardar').click(function () {
      let id = $('#id').val();
      let activo = $('#activo').val();
      let descripcion = $('#descripcion').val();
      let bins = [];

      $.each($('#bin option:selected'), function (indice, opcion) {
        bins.push({
          Estadoequipoid: id,
          Bin: $(opcion).html()
        });
      });

      $.post(webroot + "/EstadoEquipo/" + (id > 0 ? "Edit" : "Create"), {
        estadoequipo: {
          Estadoequipoid: id,
          Descripcion: descripcion,
          EstaActivo: activo
        },
        bins: bins
      }, function (data) {
        if (data.exito) {
          mensaje("Éxito", "Información guardada correctamente", "exito", function () { location.href = "~/EstadoEquipo/Index"; });
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
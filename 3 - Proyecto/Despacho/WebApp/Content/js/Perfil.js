let Perfil = function () {

  let Init = function () {
    InitElementos();
  };

  let InitElementos = function () {
    $.post("/Perfil/Listar", { id: 0 }, function (perfiles) {
      $("#listaperfiles").mDatatable({
        data: {
          type: "local",
          source: perfiles,
          pageSize: 10
        },
        layout: {
          theme: "default",
          class: "",
          scroll: !1,
          footer: !1
        },
        sortable: !0,
        pagination: !0,
        search: {
          input: $("#buscarperfil")
        },
        columns: [
          { field: "PerfilId", title: "#", width: 50, selector: !1, textAlign: "center" },
          { field: "Descripcion", title: "Descripción", responsive: { visible: "lg" } },
          { field: "EstaActivo", title: "Activo", responsive: { visible: "lg" }, template: function (e, a, i) { return e.EstaActivo == true ? "Si" : "No"; } }
        ], true, true);
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
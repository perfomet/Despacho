﻿let Usuario = function () {

  let Init = function () {
    InitElementos();
  };

  let InitElementos = function () {
    $.post("/Usuario/Listar", { id: 0 }, function (perfiles) {
      $("#listaPerfiles").mDatatable({
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
          input: $("#buscarPerfil")
        },
        columns: [
          { field: "PerfilId", title: "#", width: 50, selector: !1, textAlign: "center" },
          { field: "Descripcion", title: "Descripción", responsive: { visible: "lg" } }
          
        ]
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
﻿let TipoDocumento = function () {

  let Init = function () {
    InitElementos();
  };

  let InitElementos = function () {
    $.post("/TipoDocumento/Listar", { id: 0 }, function (tiposdocumentos) {
      $("#listatiposdocumentos").mDatatable({
        data: {
          type: "local",
          source: tiposdocumentos,
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
          input: $("#buscartipodocumento")
        },
        columns:
          [
            { field: "TipoDocumentoId", title: "#", width: 50, selector: !1, textAlign: "center" },
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
  TipoDocumento.init();
});
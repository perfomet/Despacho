let TipoSolicitud = function () {

  let Init = function () {
    InitElementos();
  };

  let InitElementos = function () {
    $.post("/TipoSolicitud/Listar", { id: 0 }, function (solicitudes) {
      $("#listatiposdesolicitudes").mDatatable({
        data: {
          type: "local",
          source: solicitudes,
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
          input: $("#buscartiposolicitud")
        },
        columns:
          [
            { field: "TipoSolicitudId", title: "#", width: 50, selector: !1, textAlign: "center" },
            { field: "Descripcion", title: "Descripción", responsive: { visible: "lg" } },
            { field: "Observaciones", title: "Observaciones", responsive: { visible: "lg" } }
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
  TipoSolicitud.init();
});
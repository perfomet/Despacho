let Usuario = function () {

  let Init = function () {
    InitElementos();
  };

  let InitElementos = function () {
    $.post("/Usuario/Listar", { id: 0 }, function (usuarios) {
      $("#listaUsuarios").mDatatable({
        data: {
          type: "local",
          source: usuarios,
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
          input: $("#buscarUsuario")
        },
        columns: [
          { field: "UsuarioId", title: "#", width: 50, selector: !1, textAlign: "center" },
          { field: "Username", title: "Usuario", responsive: { visible: "lg" } },
          { field: "Password", title: "Clave", responsive: { visible: "lg" } },
          { field: "Nombres", title: "Nombres", responsive: { visible: "lg" } },
          { field: "ApellidoPaterno", title: "Primer Apellido", responsive: { visible: "lg" } },
          { field: "ApellidoMaterno", title: "Segundo Apellido", responsive: { visible: "lg" } },
          { field: "Email", title: "Email", responsive: { visible: "lg" } },
          { field: "PerfilId", title: "Perfil", responsive: { visible: "lg" } },
          { field: "ClienteId", title: "Cliente", responsive: { visible: "lg" } }
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
  Usuario.init();
});
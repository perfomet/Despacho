let Usuario = function () {

  let Init = function () {
    InitElementos();
  };

  let InitElementos = function () {
    if ($("#listausuarios").length > 0) {
      cargarTabla("UsuarioId", "Usuario", { Id: 0 }, "#listausuarios", "#buscarusuario", [
        { field: "UsuarioId", title: "#", width: 50, selector: !1, textAlign: "center" },
        { field: "Username", title: "Usuario", responsive: { visible: "lg" } },
        { field: "Password", title: "Clave", responsive: { visible: "lg" } },
        { field: "Nombres", title: "Nombres", responsive: { visible: "lg" } },
        { field: "ApellidoPaterno", title: "Primer Apellido", responsive: { visible: "lg" } },
        { field: "ApellidoMaterno", title: "Segundo Apellido", responsive: { visible: "lg" } },
        { field: "Email", title: "Email", responsive: { visible: "lg" } },
        { field: "Perfil.Descripcion", title: "Rol", responsive: { visible: "lg" } },
        { field: "Cliente.Nombre", title: "Cliente", responsive: { visible: "lg" } },
        { field: "EstaActivo", title: "Activo", responsive: { visible: "lg" }, template: function (e, a, i) { return e.EstaActivo == true ? "Si" : "No"; } }
      ], true, true);
    }
  }

  $('#btnGuardar').click(function () {
    let id = $('#id').val();
    let activo = $('#activo').val();
    let username = $('#username').val();
    let password = $('#password').val();
    let nombres = $('#nombres').val();
    let apellidopaterno = $('#apellidopaterno').val();
    let apellidomaterno = $('#apellidomaterno').val();
    let email = $('#email').val();
    let perfilid = $('#perfilid').val();
    let clienteid = $('#clienteid').val();

    $.post("/Usuario/" + (id > 0 ? "Edit" : "Create"), {
      UsuarioId: id,
      Username: username,
      Password: password,
      Nombres: nombres,
      ApellidoPaterno: apellidopaterno,
      ApellidoMaterno: apellidomaterno,
      Email: email,
      Perfilid: perfilid,
      Clienteid: clienteid,
      EstaActivo: activo
    }, function (data) {
      if (data.exito) {
        mensaje("Éxito", "Información guardada correctamente", "exito", function () { location.href = "/Usuario/Index"; });
      } else {
        mensaje("Error", "No se pudo guardar la información", "error");
      }
    });
  });

  return {
    init: function () {
      Init();
    }
  };
}();

$(() => {
  Usuario.init();
});
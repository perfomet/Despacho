let Personal = function () {

  let Init = function () {
    InitElementos();
  };

  let InitElementos = function () {
    $('.m-select2').select2();

    $("#rut").inputmask({ mask: "99999999" })

    if ($("#listapersonal").length > 0) {
      cargarTabla("Personalid", "Personal", { Id: 0 }, "#listapersonal", "#buscarpersonal", [
        { field: "Personalid", title: "#", width: 50, selector: !1, textAlign: "center" },
        { field: "RUT", title: "RUT", responsive: { visible: "lg" } },
        { field: "DV", title: "DV", responsive: { visible: "lg" } },
        { field: "Nombre", title: "Nombre", responsive: { visible: "lg" } },
        { field: "Primerapellido", title: "Primer Apellido", responsive: { visible: "lg" } },
        { field: "Segundoapellido", title: "Segundo Apellido", responsive: { visible: "lg" } },
        { field: "Email", title: "email", responsive: { visible: "lg" } },
        { field: "TipoPersonal", title: "Tipo Personal", responsive: { visible: "lg" } },
        { field: "EstaActivo", title: "Activo", responsive: { visible: "lg" }, template: function (e, a, i) { return e.EstaActivo == true ? "Si" : "No"; } }
      ], true, true);
    }
    $('#btnGuardar').click(function () {
      let id = $('#id').val();
      let rut = $('#rut').val();
      let dv = $('#dv').val();
      let nombre = $('#nombre').val();
      let primerapellido = $('#primerapellido').val();
      let segundoapellido = $('#segundoapellido').val();
      let email = $('#email').val();
      let tipopersonalid = $('#tipopersonalid').val();
      let activo = $('#activo').val();


      $.post(webroot + "/Personal/" + (id > 0 ? "Edit" : "Create"), {
        Personalid: id,
        Rut: rut,
        Dv: dv,
        Nombre: nombre,
        Primerapellido: primerapellido,
        Segundoapellido: segundoapellido,
        Email: email,
        Tipopersonalid: tipopersonalid,
        EstaActivo: activo

      }, function (data) {
        if (data.exito) {
          mensaje("Éxito", "Información guardada correctamente", "exito", function () { location.href = "~/Personal/Index"; });
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
  Personal.init();
});
let Cliente = function () {

    let Init = function () {
        InitElementos();
    };

    let InitElementos = function () {
        if ($("#listaClientes").length > 0) {
            cargarTabla("ClienteId", "Cliente", { clienteId: 0 }, "#listaClientes", "#buscarCliente", [
                { field: "ClienteId", title: "#", width: 50, selector: !1, textAlign: "center" },
                { field: "Nombre", title: "Nombre", responsive: { visible: "lg" } },
                { field: "Rut", title: "Rut", responsive: { visible: "lg" } },
                { field: "VRut", title: "DV", responsive: { visible: "lg" } },
                { field: "Sufijo", title: "Sufijo", responsive: { visible: "lg" } },
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
    Cliente.init();
});
let Cliente = function () {

    let Init = function () {
        InitElementos();
    };

    let InitElementos = function () {
        if ($("#listaClientes").length > 0) {
            cargarTabla("ClienteId", "Cliente", { clienteId: 0 }, "#listaClientes", "#buscarCliente", [
                { field: "ClienteId", title: "#", width: 50, selector: !1, textAlign: "center" },
                { field: "Codigo", title: "Código", responsive: { visible: "lg" } },
                { field: "Nombre", title: "Nombre", responsive: { visible: "lg" } },
                { field: "Rut", title: "Rut", responsive: { visible: "lg" } },
                { field: "VRut", title: "DV", responsive: { visible: "lg" } },
                { field: "Prefijo", title: "Prefijo", responsive: { visible: "lg" } },
                { field: "EstaActivo", title: "Activo", responsive: { visible: "lg" }, template: function (e, a, i) { return e.EstaActivo == true ? "Si" : "No"; } }
            ], true, true);
        }

        $('#btnGuardar').click(function () {
            let id = $('#id').val();
            let activo = $('#activo').val();
            let codigo = $('#codigo').val();
            let nombre = $('#nombre').val();
            let rut = $('#rut').val();
            let vrut = $('#vrut').val();
            let prefijo = $('#prefijo').val();

            $.post(webroot + "/Cliente/" + (id > 0 ? "Edit" : "Create"), {
                ClienteId: id,
                Codigo: codigo,
                Nombre: nombre,
                Rut: rut,
                VRut: vrut,
                Prefijo: prefijo,
                EstaActivo: activo
            }, function (data) {
                if (data.exito) {
                    mensaje("Éxito", "Información guardada correctamente", "exito", function () { location.href = webroot + "/Cliente/Index"; });
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
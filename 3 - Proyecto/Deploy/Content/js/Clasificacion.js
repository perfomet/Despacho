let Clasificacion = function () {

    let Init = function () {
        InitElementos();
    };

    let InitElementos = function () {
        $('.m-select2').select2();

        if ($("#listaclasificacion").length > 0) {
            cargarTabla("Clasificacionid", "Clasificacion", { id: 0 }, "#listaclasificacion", "#buscarclasificacion", [
                { field: "Clasificacionid", title: "#", width: 50, selector: !1, textAlign: "center" },
                { field: "Cantidad", title: "Cantidad", responsive: { visible: "lg" } },
                { field: "UnidadMedida", title: "Unidad de Medida", responsive: { visible: "lg" } },
                { field: "EstaActivo", title: "Activo", responsive: { visible: "lg" }, template: function (e, a, i) { return e.EstaActivo == true ? "Si" : "No"; } }
            ], true, true);
        }

        $('#btnGuardar').click(function () {
            let id = $('#id').val();
            let activo = $('#activo').val();
            let cantidad = $('#cantidad').val();
            let unidadmedidaid = $('#unidadmedidaid').val();

            $.post(webroot + "/Clasificacion/" + (id > 0 ? "Edit" : "Create"), {
                Clasificacionid: id,
                Cantidad: cantidad,
                Unidadmedidaid: unidadmedidaid,
                EstaActivo: activo
            }, function (data) {
                if (data.exito) {
                    mensaje("Éxito", "Información guardada correctamente", "exito", function () { location.href = "~/Clasificacion/Index"; });
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
    Clasificacion.init();
});
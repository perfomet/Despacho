let Camion = function () {

    let Init = function () {
        InitElementos();
    };

    let InitElementos = function () {
        if ($("#listaCamiones").length > 0) {
            cargarTabla("Patente", "Camion", { patentecamion: "" }, "#listaCamiones", "#buscarCamion", [
                { field: "Patente", title: "Patente", width: 100, selector: !1, textAlign: "center" },
                { field: "Descripcion", title: "Descripción", responsive: { visible: "lg" } },
                { field: "EmpresaTransporte", title: "Empresa Transporte", responsive: { visible: "lg" } },
                { field: "EsPropia", title: "Propia", responsive: { visible: "lg" }, template: function (e, a, i) { return e.espropia == true ? "Si" : "No"; } },
                { field: "EstaActivo", title: "Activo", responsive: { visible: "lg" }, template: function (e, a, i) { return e.EstaActivo == true ? "Si" : "No"; } }
            ], true, true);
        }
    };

    return {
        init: function () {
            Init();
        }
    };
}();

$(() => {
    Camion.init();
});
let Camion = function () {

    let Init = function () {
        InitElementos();
    };

    let InitElementos = function () {
        if ($("#listaCamiones").length > 0) {
            cargarTabla("patente", "Camion", { patentecamion: "" }, "#listaCamiones", "#buscarCamion", [
                { field: "patente", title: "Patente", width: 100, selector: !1, textAlign: "center" },
                { field: "descripcion", title: "Descripción", responsive: { visible: "lg" } },
                { field: "empresatransporte", title: "Empresa Transporte", responsive: { visible: "lg" } },
                { field: "espropia", title: "Propia", responsive: { visible: "lg" }, template: function (e, a, i) { return e.espropia == true ? "Si" : "No"; } }
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
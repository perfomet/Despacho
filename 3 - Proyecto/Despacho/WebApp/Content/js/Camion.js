let Camion = function () {

    let Init = function () {
        InitElementos();
    };

    let InitElementos = function () {
        $.post("/Camion/Listar", { patentecamion: "" }, function (camiones) {

            camiones.forEach((camion) => {
                camion.espropia = camion.espropia == "True" ? "Si" : "No";
            });

            $("#listaCamiones").mDatatable({
                data: {
                    type: "local",
                    source: camiones,
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
                    input: $("#buscarCamion")
                },
                columns:
                    [
                        { field: "patente", title: "Patente", width: 100, selector: !1, textAlign: "center" },
                        { field: "descripcion", title: "Descripción", responsive: { visible: "lg" } },
                        { field: "empresatransporte", title: "Empresa Transporte", responsive: { visible: "lg" } },
                        { field: "espropia", title: "Propia", responsive: { visible: "lg" } }
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
    Camion.init();
});
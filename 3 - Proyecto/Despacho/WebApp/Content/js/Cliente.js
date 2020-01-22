let Cliente = function () {

    let Init = function () {
        InitElementos();
    };

    let InitElementos = function () {
        $.post("/Cliente/Listar", { clienteId: 0 }, function (clientes) {
            $(".m_datatable").mDatatable({
                data: {
                    type: "local",
                    source: clientes,
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
                    input: $("#buscarCliente")
                },
                columns: [
                    { field: "ClienteId", title: "#", width: 50, selector: !1, textAlign: "center" },
                    { field: "Nombre", title: "Nombre", responsive: { visible: "lg" } },
                    { field: "Rut", title: "Rut", responsive: { visible: "lg" } },
                    { field: "VRUT", title: "dígito", responsive: { visible: "lg" } }
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
    Cliente.init();
});
let Solicitud = function () {

    let Init = function () {
        InitElementos();
    };

    let InitElementos = function () {
        $('.m-select2').select2();

        if ($("#listaClientes").length > 0) {
            //CARGAR TABLA
        }

        $('#btnGuardar').click(function () {
            
        });
    };

    return {
        init: function () {
            Init();
        }
    };
}();

$(() => {
    Solicitud.init();
});
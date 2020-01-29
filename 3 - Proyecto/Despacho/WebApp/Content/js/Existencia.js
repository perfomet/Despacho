let Camion = function () {

    let Init = function () {
        InitElementos();
    };

    let InitElementos = function () {
        $.post("/Existencia/Listar", { serie: "" }, function (data) {
            console.log(data);
        });

        $.post("/Existencia/Listar", { serie: "1766670" }, function (data) {
            console.log(data);
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
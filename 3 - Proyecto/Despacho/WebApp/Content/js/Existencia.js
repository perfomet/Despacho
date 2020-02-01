let Existencia = function () {
    let existencias = [];
    let existenciasAgrupadas = {};

    let Init = function () {
        $('#m_portlet_tools_1 .m-portlet__body').toggle();

        $.post("/Existencia/Listar", { serie: "" }, function (data) {
            existencias = data;
            existenciasAgrupadas = AgruparExistencias(data);

            InitElementos();
        });
    };

    let InitElementos = function () {
        CargarFiltroBodegas();
        DibujarBodega(existenciasAgrupadas);
        CargarLista();
        $('.m-select2').select2();
        $('#filtro-filtrar').click(function () {
            Filtrar();
        });
    };

    let AgruparExistencias = function (lista) {
        let agrupado = {};

        lista.forEach((existencia) => {
            if (!agrupado[existencia.Bin]) agrupado[existencia.Bin] = {};
            if (!agrupado[existencia.Bin][existencia.Modelo]) agrupado[existencia.Bin][existencia.Modelo] = [];
            agrupado[existencia.Bin][existencia.Modelo].push(existencia);
        });

        return agrupado;
    };

    let DibujarBodega = function (existencias) {
        //console.log(existenciasAgrupadas);

        for (let a in existencias) {
            //console.log(a + ':');
            for (let e in existencias[a]) {
                //console.log(e, existenciasAgrupadas[a][e].length);
            }
        }
    };

    let CargarLista = function () {
        let tabla = $('#lista-existencia').mDatatable({
            data: {
                type: "local",
                source: existencias,
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
                input: $('#buscarExistencia')
            },
            columns: [
                { field: "Serie", title: "Serie", responsive: { visible: "lg" } },
                { field: "Placa", title: "Placa", responsive: { visible: "lg" } },
                { field: "Denominacion", title: "Descripción", responsive: { visible: "lg" } },
                { field: "Bin", title: "Bin", responsive: { visible: "lg" } },
                { field: "NomBodega", title: "Bodega", responsive: { visible: "lg" } },
                { field: "Propietario", title: "Cliente", responsive: { visible: "lg" } },
                { field: "FechaAlmacenaje", title: "Fecha Almacenaje", responsive: { visible: "lg" }, template: function (e, a, i) { return moment(parseInt(e.FechaAlmacenaje.split('(')[1].split(')')[0])).format("DD/MM/YYYY HH:mm"); } }
            ]
        });

        console.log(tabla.search.toString());
    };

    let CargarFiltroBodegas = function () {
        $.post("/Existencia/ObtenerBodegas", { codigo: "" }, function (bodegas) {
            $('#filtro-bodega').html('');

            bodegas.forEach((bodega) => {
                $('#filtro-bodega').append('<option value="' + bodega.Codigo + '">' + bodega.Nombre + '</option>');
            });

            $('#filtro-bodega').select2({ placeholder: "Seleccione una bodega" });

            CargarFiltroBins(bodegas[0].Codigo);

            $('#filtro-bodega').change(function () {
                CargarFiltroBins($(this).val());
            });
        });
    };

    let CargarFiltroBins = function (bodega) {
        $.post("/Existencia/ObtenerBins", { bodega: bodega }, function (bins) {
            $('#filtro-bin').html('');

            bins.forEach((bin) => {
                $('#filtro-bin').append('<option value="' + bin.Codigo + '">' + bin.Codigo + '</option>');
            });

            $('#filtro-bin').select2({ placeholder: "Seleccione una bin" });
        });
    };

    let Filtrar = function () {
        

        DibujarBodega(existenciasAgrupadas);
    };

    return {
        init: function () {
            Init();
        }
    };
}();

$(() => {
    Existencia.init();
});
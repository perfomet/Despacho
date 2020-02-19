let Existencia = function () {
    let existencias = [];
    let existenciasAgrupadas = {};

    let ancho = 2;
    let alto = 2;
    let profundidad = 5;

    let Init = function () {
        $.post("/Existencia/Listar", { serie: "" }, function (data) {
            console.log(data)
            existencias = data;
            //existenciasAgrupadas = AgruparExistencias(data);

            InitElementos();
        });
    };

    let InitElementos = function () {
        $('#filtro-bodega').select2();

        window.crearSelectorFecha("#filtro-fecha-almacenaje", moment().subtract(29, 'days'), moment());

        CargarLista();

        $('#filtro-bodega').change(function () {
            CargarFiltroBins();
        });

        $('#filtro-filtrar').click(function () {
            Filtrar();
        });
    };

    let AgruparExistencias = function (lista) {
        let agrupado = {};

        lista.forEach((existencia) => {
            if (!agrupado[existencia.Bodega]) agrupado[existencia.Bodega] = {};
            if (!agrupado[existencia.Bodega][existencia.Bin]) agrupado[existencia.Bodega][existencia.Bin] = {};
            if (!agrupado[existencia.Bodega][existencia.Bin][existencia.Modelo]) agrupado[existencia.Bodega][existencia.Bin][existencia.Modelo] = [];
            agrupado[existencia.Bodega][existencia.Bin][existencia.Modelo].push(existencia);
        });

        return agrupado;
    };

    let DibujarBodega = function (existencias) {
        $('#seccion-bodega').html('');

        let bodega = $('#filtro-bodega').val();

        $('#seccion-bodega').css('overflow-y', 'auto');

        for (let bin in (bodega ? existencias[bodega] : existencias[0])) {
            let divBin = $('<div class="seccion-bin" data-bin="' + bin + '"></div>');

            divBin.css('height', '500px');

            for (let modelo in existencias[bodega][bin]) {
                divBin.append(CrearSeccionModelo(modelo, existencias[bodega][bin][modelo]));
            }

            $('#seccion-bodega').append(divBin);
        }
    };

    let CrearSeccionModelo = function (modelo, productos) {
        let cantidad = productos.length;

        let agregados = 0;

        let divModelo = $('<div class="seccion-modelo" data-modelo="' + modelo + '"></div>');

        let fondoModelo = $('<div class="fondo-seccion"></div>');

        fondoModelo.css('width', (32 * 3 * ancho) + 'px');

        divModelo.css('margin-left', (32 * 2 * ancho) + 'px');

        divModelo.append(fondoModelo);

        //bim.css('background-color', 'gray');

        for (let al = 0; al < alto; al++) {
            for (let p = 0; p < profundidad; p++) {
                for (let an = 0; an < ancho; an++) {
                    if (cantidad > agregados) {
                        let x = an * 32;
                        let y = (p * 18) + (34 * al);

                        if (p > 0) x += 32 * p;

                        if (an > 0) y -= 18 * an;

                        let img = $('<img src="/Content/img/Caja.svg" data-x="' + x + '" data-y="' + y + '"data-z="' + p + '" style="left: ' + x + 'px;bottom: ' + y + 'px;z-index: -' + p + '"/>');

                        img.css('left', x + 'px');
                        img.css('bottom', y + 'px');
                        img.css('z-index', (p * -1));

                        divModelo.append(img);

                        agregados++;
                    }
                }
            }
        }

        return divModelo;
    };

    let CargarLista = function () {
        console.log(existencias)
        let picker = $('#filtro-fecha-almacenaje').data('daterangepicker');

        $('#lista-existencia').mDatatable({
            data: {
                type: "local",
                source: existencias.filter((e) => { return moment(e.FechaAlmacenaje, 'DD/MM/YYYY').isBetween(picker.startDate, picker.endDate); }),
                pageSize: 10
            },
            layout: {
                theme: "default",
                class: "",
                scroll: false,
                footer: false
            },
            sortable: true,
            pagination: true,
            search: {
                input: $('#buscarExistencia')
            },
            columns: [
                { field: "Serie", title: "Serie", responsive: { visible: "lg" } },
                { field: "Placa", title: "Placa", responsive: { visible: "lg" }, template: function (e, a, i) { return e.Placa ? e.Placa : e.Referencia } },
                { field: "Denominacion", title: "Descripción", responsive: { visible: "lg" } },
                { field: "Bin", title: "Bin", responsive: { visible: "lg" } },
                { field: "NomBodega", title: "Bodega", responsive: { visible: "lg" } },
                { field: "Cliente", title: "Cliente", responsive: { visible: "lg" } },
                { field: "FechaAlmacenaje", title: "Fecha Almacenaje", responsive: { visible: "lg" }, type: "date", format: "DD/MM/YYYY" }
            ],
            translate: {
                records: {
                    processing: "Cargando...",
                    noRecords: "No se encontrarón registros"
                },
                toolbar: {
                    pagination: {
                        items: {
                            default: {
                                first: "Primero",
                                prev: "Anterior",
                                next: "Siguiente",
                                last: "Último",
                                more: "Más páginas",
                                input: "Número de página",
                                select: "Seleccionar tamaño de página"
                            },
                            info: "Viendo {{start}} - {{end}} de {{total}} registros"
                        }
                    }
                }
            }
        });
    };

    let CargarFiltroBins = function () {
        $.post("/Existencia/ObtenerBins", { bodega: $('#filtro-bodega').val() }, function (bins) {
            $('#filtro-bin').html('<option value="0" selected>Todas</option>');

            if (bins.length > 0) {
                bins.forEach((bin) => {
                    $('#filtro-bin').append('<option value="' + bin.Codigo + '">' + bin.Codigo + '</option>');
                });

                $('#filtro-bin').removeAttr('disabled');
            } else {
                $('#filtro-bin').attr('disabled', 'disabled');
            }

            $('#filtro-bin').select2({ placeholder: "Seleccione una bin" });
        });
    };

    let Filtrar = function () {
        let tabla = $('#lista-existencia').mDatatable();
        let picker = $('#filtro-fecha-almacenaje').data('daterangepicker');

        let filtrado = existencias.filter((e) => {
            let valido = true;

            if ($('#filtro-bodega').val()) valido = valido && e.CodBodega == $('#filtro-bodega').val();
            if ($('#filtro-bin').val() && $('#filtro-bin').val() != 0) valido = valido && e.Bin == $('#filtro-bin').val();
            if (picker) valido = valido && moment(e.FechaAlmacenaje, 'DD/MM/YYYY').isBetween(picker.startDate, picker.endDate);

            return valido;
        });

        tabla.originalDataSet = filtrado;
        tabla.load();
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
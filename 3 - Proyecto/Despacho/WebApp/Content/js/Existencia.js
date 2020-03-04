let Existencia = function () {
    let existencias = [];

    let _Init = function () {
        $.post("/Existencia/Listar", { serie: "" }, function (data) {
            existencias = data;

            _InitElementos();
        });
    };

    let _InitElementos = function () {
        $('#filtro-bodega').select2();
        $('#filtro-bin').select2();
        $('select#filtro-cliente').select2();

        window.crearSelectorFecha("#filtro-fecha-almacenaje", moment().subtract(29, 'days'), moment());

        _CargarLista();

        $('#filtro-bodega').change(function () {
            _CargarFiltroBins();
        });

        $('#filtro-filtrar').click(function () {
            _Filtrar();
        });
    };

    let _CargarLista = function () {
        if ($('#filtro-fecha-almacenaje').length > 0) {
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
        }
    };

    let _CargarFiltroBins = function () {
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

    let _Filtrar = function () {
        let tabla = $('#lista-existencia').mDatatable();
        let picker = $('#filtro-fecha-almacenaje').data('daterangepicker');

        let filtrado = existencias.filter((e) => {
            let valido = true;

            if ($('#filtro-bodega').val()) valido = valido && e.CodBodega == $('#filtro-bodega').val();
            if ($('#filtro-bin').val() && $('#filtro-bin').val() != 0) valido = valido && e.Bin == $('#filtro-bin').val();
            if ($('#filtro-cliente').val() && $('#filtro-cliente').val() != 0) valido = valido && e.Cliente == $('#filtro-cliente').val();
            if (picker) valido = valido && moment(e.FechaAlmacenaje, 'DD/MM/YYYY').isBetween(picker.startDate, picker.endDate);

            return valido;
        });

        tabla.originalDataSet = filtrado;
        tabla.load();
    };

    return {
        init: function () {
            _Init();
        }
    };
}();

let Bodega = function () {
    let existenciasAgrupadas = {};
    let existencias = [];

    let Init = function () {
        $.post("/Existencia/Listar", { serie: "" }, function (data) {
            existencias = data;
            existenciasAgrupadas = _AgruparExistencias(data);

            InitElementos();
        });
    };

    let InitElementos = function () {
        _DibujarBodegas();
    };

    let _AgruparExistencias = function (lista) {
        let agrupado = {};

        lista.forEach((existencia) => {
            if (!agrupado[existencia.NomBodega]) agrupado[existencia.NomBodega] = {};
            if (!agrupado[existencia.NomBodega][existencia.Bin]) agrupado[existencia.NomBodega][existencia.Bin] = {};
            if (!agrupado[existencia.NomBodega][existencia.Bin][existencia.CodArt]) agrupado[existencia.NomBodega][existencia.Bin][existencia.CodArt] = [];
            agrupado[existencia.NomBodega][existencia.Bin][existencia.CodArt].push(existencia);
        });

        return agrupado;
    };

    let _DibujarBodegas = function () {
        let seccionDibujo = $('#seccion-dibujo-bodega');
        let contenedor = $('<div class="contenedor-bodegas"></div>');

        seccionDibujo.html('');

        for (let nombreBodega in existenciasAgrupadas) {
            let objBodega = existenciasAgrupadas[nombreBodega];

            let bodega = $('<div class="bodega"></div>');

            bodega.append('<span class="nombre-bodega">' + nombreBodega + '</span>');
            bodega.append('<span><span> Bins:</span><span class="cantidad-bins">' + _CantidadBins(objBodega) + '</span><span>');
            bodega.append('<span><span> Modelos:</span><span class="cantidad-modelos">' + _CantidadModelos(objBodega) + '</span><span>');
            bodega.append('<span><span> Productos:</span><span class="cantidad-productos">' + _CantidadProductos(objBodega) + '</span><span>');

            bodega.click(function () {
                _DibujarBins(objBodega);
            });

            contenedor.append(bodega);
        }

        seccionDibujo.append(contenedor);
    };

    let _DibujarBins = function (bodega) {
        let seccionDibujo = $('#seccion-dibujo-bodega');
        let contenedor = $('<div class="contenedor-bodegas"></div>');

        seccionDibujo.html('<button class="btn btn-primary ml-3 mb-4" id="volver-bodega">< BODEGAS</button>');

        seccionDibujo.find('#volver-bodega').click(function () {
            _DibujarBodegas();
        });

        for (let bin in bodega) {
            let divBin = $('<div class="bodega"></div>');

            divBin.append('<span class="nombre-bodega">' + bin + '</span>');
            divBin.append('<span><span> Modelos:</span><span class="cantidad-modelos">' + _CantidadModelos(bodega, bin) + '</span><span>');
            divBin.append('<span><span> Productos:</span><span class="cantidad-productos">' + _CantidadProductos(bodega, bin) + '</span><span>');

            divBin.click(function () {
                _DibujarProductos(bodega, bodega[bin]);
            });

            contenedor.append(divBin);
        }

        seccionDibujo.append(contenedor);
    };

    let _DibujarProductos = function (bodega, bin) {
        let seccionDibujo = $('#seccion-dibujo-bodega');

        seccionDibujo.html('<a href="#" class="btn btn-primary ml-3 mb-4" id="volver-bins">< BINS</a>');

        seccionDibujo.find('#volver-bins').click(function () {
            _DibujarBins(bodega);
        });

        let divSeccionDibujo = $('<div style="display: flex;align-items: center;"></div>');

        let btnAnterior = $('<a href="#" class="btn btn-primary mr-3 mb-4"><</a>');
        let btnSiguiente = $('<a href="#" class="btn btn-primary ml-3 mb-4">></a>');

        btnAnterior.hide();

        btnAnterior.click(function () {
            let actual = $('.contenedor-productos.activo');
            let previo = actual.prev('.contenedor-productos');

            if (previo.length > 0) {
                actual.removeClass('activo');
                previo.addClass('activo');
            }

            if (previo.prev('.contenedor-productos').length == 0) {
                btnAnterior.hide();
            }

            btnSiguiente.show();
        });

        btnSiguiente.click(function () {
            let actual = $('.contenedor-productos.activo');
            let siguiente = actual.next('.contenedor-productos');

            if (siguiente.length > 0) {
                actual.removeClass('activo');
                siguiente.addClass('activo');
            }

            if (siguiente.next('.contenedor-productos') == 0) {
                btnSiguiente.hide();
            }

            btnAnterior.show();
        });

        divSeccionDibujo.append(btnAnterior);

        let total = 0;

        for (let modelo in bin) {
            let objModelo = bin[modelo];
            let contenedor = $('<div class="contenedor-productos"></div>');

            contenedor.append('<div class="modelo-producto">' + modelo + '</div>');

            let divProductos = $('<div class="seccion-productos"></div>');

            total++;

            objModelo.forEach((producto) => {
                let divProducto = $('<div class="producto"></div>');

                divProducto.append('<span class="placa">' + producto.Serie + '</span>');
                divProducto.append('<span><span>Descripción:</span><span class="desc-producto">' + producto.Denominacion + '</span></span>');
                divProducto.append('<span><span>Serie:</span><span class="serie-producto">' + (producto.Placa || producto.Referencia) + '</span></span>');
                divProducto.append('<span><span>Fecha:</span><span class="fecha-producto">' + producto.FechaAlmacenaje + '</span></span>');

                divProductos.append(divProducto);
            });

            contenedor.append(divProductos);

            divSeccionDibujo.append(contenedor);
        }

        if (total > 1) divSeccionDibujo.append(btnSiguiente);

        divSeccionDibujo.find('.contenedor-productos').eq(0).addClass('activo');

        seccionDibujo.append(divSeccionDibujo);
    };

    let _CantidadBins = function (bodega) {
        return Object.keys(bodega).length;
    };

    let _CantidadModelos = function (bodega, bin) {
        let total = 0;
        if (!bin) {
            for (let nomBin in bodega) {
                total += Object.keys(bodega[nomBin]).length;
            }
        } else {
            total += Object.keys(bodega[bin]).length;
        }
        return total;
    };

    let _CantidadProductos = function (bodega, bin) {
        let total = 0;
        if (!bin) {
            for (let nomBin in bodega) {
                for (let modelo in bodega[nomBin]) {
                    total += bodega[nomBin][modelo].length;
                }
            }
        } else {
            for (let modelo in bodega[bin]) {
                total += bodega[bin][modelo].length;
            }
        }
        return total;
    };

    let _Filtrar = function (cliente, bodega, bin, modelo) {
        existenciasAgrupadas = _AgruparExistencias(existencias.filter((e) => {
            let valido = true;

            if (bodega) valido = valido && e.CodBodega == bodega;
            if (bin) valido = valido && e.Bin == bin;
            if (modelo) valido = valido && e.Modelo == modelo;
            if (cliente) valido = valido && e.Cliente == cliente;
            //if (picker) valido = valido && moment(e.FechaAlmacenaje, 'DD/MM/YYYY').isBetween(picker.startDate, picker.endDate);

            return valido;
        }));
    };

    return {
        init: function () {
            Init();
        },
        DibujarBodegas: _DibujarBodegas,
        Filtrar: _Filtrar
    };
}();

$(() => {
    Existencia.init();
    Bodega.init();
});
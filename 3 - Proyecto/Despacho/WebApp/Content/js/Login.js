let Login = function () {
    let usuario;

    let Init = function () {
        if (logueado == "True") location.href = '/Home/Index';

        usuario = localStorage.getItem('usuario');

        if (usuario) {
            usuario = JSON.parse(atob(usuario));

            $('#usuario').val(usuario.Username);
            let nombre = usuario.Nombres + ' ' + usuario.ApellidoPaterno;
            if (usuario.ApellidoMaterno) nombre += ' ' + usuario.ApellidoMaterno;

            $('.usuario-ok').html(nombre);
            $('#continuar').click();
        }

        InitElementos();
    };

    let InitElementos = function () {
        $('#continuar').click(() => {
            let user = $('#usuario').val();

            $.post('/Login/ObtenerUsuario', { usuario: user }, function (data) {
                if (data.Username == user) {
                    localStorage.setItem('usuario', btoa(JSON.stringify(data)));

                    let nombre = data.Nombres + ' ' + data.ApellidoPaterno;
                    if (data.ApellidoMaterno) nombre += ' ' + data.ApellidoMaterno;

                    $('.usuario-ok').html(nombre);

                    $('.div-clave').css('display', 'flex');
                    $('.div-ingresar').css('display', 'flex');
                    $('.div-usuario-ok').css('display', 'flex');
                    $('.div-continuar').hide();
                    $('.div-usuario').hide();
                } else {
                    alert('Usuario no encontrado');
                }
            });
        });

        $('#volver').click(() => {
            localStorage.removeItem('usuario');

            $('.div-clave').hide();
            $('.div-ingresar').hide();
            $('.div-usuario-ok').hide()
            $('.div-continuar').css('display', 'flex');
            $('.div-usuario').css('display', 'flex');
        });

        $('#ingresar').click(() => {
            usuario = localStorage.getItem('usuario');
            if (usuario) usuario = JSON.parse(atob(usuario));

            $.post('/Login/Ingresar', { usuario: usuario.Username, clave: $('#clave').val() }, function (data) {
                if (data.exito == true) {
                    localStorage.setItem('usuario', btoa(JSON.stringify(data.usuario)));

                    location.href = '/Home/Index';
                } else {
                    alert('Clave incorrecta.');
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
    Login.init();
});
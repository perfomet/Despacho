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


var game = new Phaser.Game(800, 400, Phaser.AUTO, 'test', null, true, false);

var BasicGame = function (game) { };

BasicGame.Boot = function (game) { };

var isoGroup, cursorPos, cursor;

BasicGame.Boot.prototype =
{
    preload: function () {
        game.load.image('tile', '/Content/img/Caja.png');

        game.time.advancedTiming = true;

        // Add and enable the plug-in.
        game.plugins.add(new Phaser.Plugin.Isometric(game));

        // This is used to set a game canvas-based offset for the 0, 0, 0 isometric coordinate - by default
        // this point would be at screen coordinates 0, 0 (top left) which is usually undesirable.
        game.iso.anchor.setTo(0.5, 0.5);
    },
    create: function () {
        // Create a group for our tiles.
        isoGroup = game.add.group();

        // Let's make a load of tiles on a grid.
        this.spawnTiles();

        // Provide a 3D position for the cursor
        cursorPos = new Phaser.Plugin.Isometric.Point3();
    },
    update: function () {
        // Update the cursor position.
        // It's important to understand that screen-to-isometric projection means you have to specify a z position manually, as this cannot be easily
        // determined from the 2D pointer position without extra trickery. By default, the z position is 0 if not set.
        game.iso.unproject(game.input.activePointer.position, cursorPos);

        if (game.input.activePointer.isDown == true) {
            let t;

            isoGroup.forEach(function (tile) {
                var inBounds = tile.isoBounds.containsXY(cursorPos.x, cursorPos.y);
                // If it does, do a little animation and tint change.
                if (inBounds) {
                    t = tile;
                }
            });
        }

        let t, inBounds;

        // Loop through all tiles and test to see if the 3D position from above intersects with the automatically generated IsoSprite tile bounds.
        isoGroup.forEach(function (tile) {
            inBounds = tile.isoBounds.containsXY(cursorPos.x, cursorPos.y);

            if (inBounds) {
                t = tile;
            }
        });

        // If it does, do a little animation and tint change.
        if (t && !t.selected) {
            t.selected = true;
            t.tint = 0x86bfda;
            game.add.tween(t);
        }

        isoGroup.forEach(function (tile) {
            inBounds = tile.isoBounds.containsXY(cursorPos.x, cursorPos.y);
            // If not, revert back to how it was.
            if (tile.selected && !inBounds) {
                tile.selected = false;
                tile.tint = 0xffffff;
                game.add.tween(t);
            }
        });

    },
    spawnTiles: function () {
        let ancho = 2;
        let profundidad = 14;
        let alto = 2;
        let cantidad = 21;
        let agregados = 0;

        var tile;

        for (let al = 0; al < alto; al++) {
            for (let p = 0; p < profundidad; p++) {
                for (let an = 0; an < ancho; an++) {
                    if (agregados < cantidad) {
                        tile = game.add.isoSprite(an * 40, p * 40, al * 40, 'tile', 0, isoGroup);
                        tile.anchor.set(0.1, 0);
                        tile.prueba = an + p + al;

                        agregados++;
                    }
                }
            }
        }
    }
};

game.state.add('Boot', BasicGame.Boot);
game.state.start('Boot');
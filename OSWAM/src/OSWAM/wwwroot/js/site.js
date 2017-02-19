﻿// Write your Javascript code.

//Tab3 Canvas
(function () {
    var canvas = document.getElementById('canvas'),
        context = canvas.getContext('2d');

    // resize the canvas to fill browser window dynamically
    window.addEventListener('resize', resizeCanvas, false);

    function resizeCanvas() {
        canvas.width = window.innerWidth - 20;
        canvas.height = window.innerHeight - 250;

                /**
                 * Your drawings need to be inside this function otherwise they will be reset when 
                    * you resize the browser window and the canvas goes will be cleared.
                 */
        drawStuff();
    }
    resizeCanvas();

    function drawStuff() {
        //grid width and height
        var bw = canvas.width;
        var bh = canvas.height;
        //padding around grid
        var p = 0;
        //size of canvas
        var cw = bw + (p * 2) + 1;
        var ch = bh + (p * 2) + 1;

        function drawBoard() {
            for (var x = 0; x <= bw; x += 40) {
                context.moveTo(0.5 + x + p, p);
                context.lineTo(0.5 + x + p, bh + p);
            }


            for (var x = 0; x <= bh; x += 40) {
                context.moveTo(p, 0.5 + x + p);
                context.lineTo(bw + p, 0.5 + x + p);
            }

            context.strokeStyle = "black";
            context.stroke();
        }

        drawBoard();
    }
})();


function ToggleDiv(Flag) {
    if (Flag == "first") {

        document.getElementById('dvFirstDiv').style.display = 'block';
        document.getElementById('dvSecondDiv').style.display = 'none';
    }
    else {
        document.getElementById('dvFirstDiv').style.display = 'none';
        document.getElementById('dvSecondDiv').style.display = 'block';
    }
}
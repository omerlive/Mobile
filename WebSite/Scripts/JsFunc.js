
/* age range slider*/
$(function () {

    $('#rangeslider').slider({
        range: true,
        min: 0,
        max: 100,
        values: [25, 35],
        slide: function (event, ui) {
            $('#rangeval').html(ui.values[0] + " - " + ui.values[1]);
        }
    });
});







function bigImg(x) {
    x.style.height = "90px";
    x.style.width = "90px";
}

function normalImg(x) {
    x.style.height = "70px";
    x.style.width = "70px";
}


function bigImgBorder(x) {
    x.style.border = '3px solid red';
    
}

function normalImgBorder(x) {

    x.style.border = '0px solid red';
}



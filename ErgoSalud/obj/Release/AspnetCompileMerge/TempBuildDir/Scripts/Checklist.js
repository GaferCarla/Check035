// For divs hide/active:

$(document).ready(function () {

    $('#next').click(function () {
        $('.current').removeClass('current').hide()
            .next().show().addClass('current');
        $("html, body").animate({ scrollTop: 0 });
        $('.current1').removeClass('current1').removeClass('active').hide()
            .next().css('display', 'block').addClass('active').addClass('current1');


        if ($('.current').hasClass('last')) {
            $('#next').css('display', 'none');
            $('#send').css('display', 'block');
            $('#avisoalert').css('display', 'block');
            $('.finalizar').css('display', 'block')

        }
        $('#prev').css('display', 'block');
    });

    $('#prev').click(function () {
        $('.current').removeClass('current').hide()
            .prev().show().addClass('current');
        $("html, body").animate({ scrollTop: 0 });

        $('.current1').removeClass('current1').removeClass('active').hide()
            .prev().css('display', 'block').addClass('active').addClass('current1');

        if ($('.current').hasClass('first')) {
            $('#prev').css('display', 'none');
        }
        $('#next').css('display', 'block');
        $('#send').css('display', 'none');
        $('#avisoalert').css('display', 'none');
        $('.finalizar').css('display', 'none');
    });

});


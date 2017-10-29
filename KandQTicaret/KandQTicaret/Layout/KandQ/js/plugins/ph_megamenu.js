jQuery.fn.ph_megamenu = function(options){
    var settings = {
        marker              : true,
        marker_content_off  : '[+]',
        marker_content_on   : '[-]',
        speed               : 100,
    }
    $.extend( settings, options );
    
    var menu = $(".ph_megamenu");
    var responsiveMenu = false;
    var currentScreen;
    
    $(menu).find('>li').has('ul').each(function() {
        $(this).addClass('has-submenu');
    });

    if(settings.marker == true){
        $(menu).find("a").each(function(){
            if($(this).siblings(".dropdown, .mega-menu").length > 0){
                $(this).append("<span class='marker marker-off'>" + settings.marker_content_off + "</span>");
                $(this).append("<span class='marker marker-on'>" + settings.marker_content_on + "</span>");
            }
        });
    }

    $(window).resize(function()
    {
        handleResize(false, currentScreen);
    });
    $(window).load(function()
    {
        handleResize(true, getCurrentScreen());
        resizeMegaMenu();    
    });

    // megamenu
    $('.ph_megamenu_mobile_toggle').on('click', 'a.show_megamenu', function(e) {
        switchMobile('show');
        e.preventDefault();
    });
    
    $('.ph_megamenu_mobile_toggle').on('click', 'a.hide_megamenu', function(e) {
        switchMobile('hide');
        e.preventDefault();
    });

    function getCurrentScreen()
    {
        compensante = scrollCompensate();
        if ($(document).width()+compensante >= 991 && $(document).width()+compensante <= 1200) 
        {
            currentScreen = 'medium';
        }
        else if ($(document).width()+compensante > 1200)  
        {
            currentScreen = 'big';
        }
        return currentScreen;
    }

    function scrollCompensate() 
    {
        var inner = document.createElement('p');
        inner.style.width = "100%";
        inner.style.height = "200px";

        var outer = document.createElement('div');
        outer.style.position = "absolute";
        outer.style.top = "0px";
        outer.style.left = "0px";
        outer.style.visibility = "hidden";
        outer.style.width = "200px";
        outer.style.height = "150px";
        outer.style.overflow = "hidden";
        outer.appendChild(inner);

        document.body.appendChild(outer);
        var w1 = inner.offsetWidth;
        outer.style.overflow = 'scroll';
        var w2 = inner.offsetWidth;
        if (w1 == w2) w2 = outer.clientWidth;

        document.body.removeChild(outer);

        return (w1 - w2);
    }

    function handleResize(firstTime, currentScreen)
    {
        compensante = scrollCompensate();

        if ($(document).width()+compensante <= 990 && responsiveMenu == false)
        {
            megaMobile('enable');
            responsiveMenu = true; 
            clickMenu(); 
        }
        else if ($(document).width()+compensante >= 991)
        {
            megaMobile('disable');
            responsiveMenu = false;
            bindHover();
            
            if(currentScreen == 'medium' && $(window).width()+compensante >= 1200 || currentScreen == 'big' && $(window).width()+compensante <= 1200 || firstTime === true)
            {
                currentScreen = getCurrentScreen();
                resizeMegaMenu(true);
            }
            else
            {
                resizeMegaMenu();
            }
        }
    }

    function switchMobile(action)
    {
        if(action == 'show')
        {
            $('#ph_megamenu_wrapper, a.hide_megamenu').show();
            $('a.show_megamenu').hide();
        }
        else
        {
            $('a.show_megamenu').show();
            $('#ph_megamenu_wrapper, a.hide_megamenu').hide();
        }
    }

    function megaMobile(action)
    {
        if(action == 'enable')
        {
            $('a.show_megamenu').show();
            $('#ph_megamenu_wrapper').hide();
			$('#ph_megamenu').find('.marker-off').show();
        }
        else
        {
            $('a.show_megamenu,a.hide_megamenu').hide();
            $('#ph_megamenu_wrapper').show();
			$('#ph_megamenu').find('.marker-on, .marker-off, .dropdown, .megamenu').hide();
        }
    }
    
    function bindHover(){
        $(menu).find("li").hoverIntent({
            over: showMenu,
            out: hideMenu,
            timeout: 200,
        });
    }
    
    function clickMenu() {
        $(menu).find(".has-submenu").on('click', '.marker-off', function(e) {
            $(this).parent().parent().find('.dropdown, .mega-menu').slideDown();
            e.preventDefault();
            $(this).hide();
            $(this).parent().find('.marker-on').css('display', 'inline-block');
        });
        
        $(menu).find(".has-submenu").on('click', '.marker-on', function(e) {
            $(this).parent().parent().find('.dropdown, .mega-menu').slideUp();
            e.preventDefault();
            $(this).hide();
            $(this).parent().find('.marker-off').css('display', 'inline-block');
        });
    }

    function showMenu()
    {
        if($('.rev_slider').length > 0)
        {
            $('.rev_slider').revpause();
        }
        
        $(this).children(".dropdown, .mega-menu").stop(true, true).fadeIn(settings.speed);
        $(this).addClass('open');
    }

    function hideMenu()
    {
        if($('.rev_slider').length > 0)
        {
            $('.rev_slider').revresume();
        }
        $(this).children(".dropdown, .mega-menu").stop(true, true).fadeOut(settings.speed);
        $(this).removeClass('open');
    }

    function resizeMegaMenu(recalc) {
        var megamenuwrapper = $('#ph_megamenu').outerWidth();
        $(menu).find('.has-submenu, .mega-menu').each(function (index) {
            var submenu = $(this).find('.mega-menu');
            if(recalc === true && submenu.length > 0)
            {
                var subwidth = 2; // 2 border
                $(this).find('.ph-col:not(.ph-hidden-desktop,.ph-hidden-mobile)').each(function()
                {
                    if($(this).hasClass('ph-new-row')) return false;
                    subwidth += $(this).width()+30; // + 30 = padding
                });
                submenu.css('width', subwidth);
            }
            else
            {
                var subwidth = submenu.width();
            }
            var position = $(this).position();
            var positionFromParent = position.left-$('#ph_megamenu').position().left;
            var elements_width = positionFromParent+subwidth;

            //  console.log(megamenuwrapper, "wrap");
            // console.log(submenu.width(), "sub");

            if(elements_width > $('.container').width())
            {
                submenu.css('left', ($('#ph_megamenu').width()-subwidth) + $('#ph_megamenu').position().left);
            }
            else if(elements_width < $('.container').width())
            {
                submenu.css('left', position.left);
            }
            else if(elements_width == $('.container').width())
            {
               submenu.css('left', position.left);
            }

            //submenu.css('left', ((megamenuwrapper + 30)- submenu.width()));

            // dropdown
            var menuwidth = $('#ph_megamenu').width();
            var menuel_position = $(this).position().left;
            var dropdown = $(this).find('.dropdown .dropdown');
            
            if(menuwidth - menuel_position < 440) {
                dropdown.css('left', '-220px');
            }
        });
    }
};
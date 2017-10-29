'use strict';

// Cache
var contentWrapper = $('div.content');
var searchBox = $('#search_block_top i');
var searchBoxInput = $('#search_block_top input[type="submit"]');
var sliderRange = $('#slider-range');
var gridViewContainer = $('#gridview');
var listViewContainer = $('#listview');
var qtyWantedInput = $('#quantity_wanted');
var defaultSlider = $('.slider');
var miniSliderDiv = $('.mini_slider');
var productListContainer = $(".product_list_ph")

function get_grid() {
    gridViewContainer.addClass("active");
    listViewContainer.removeClass("active");
    productListContainer.removeClass("list");
    productListContainer.addClass("grid");
}

function get_list() {
    listViewContainer.addClass("active");
    gridViewContainer.removeClass("active");
    productListContainer.removeClass("grid");
    productListContainer.addClass("list");
}

function hideDD() {
    $('.topbar .select-options').removeClass('active');
}

// switches selected class on buttons
function changeSelectedLink($elem) {
    // remove selected class on previous item
    $elem.parents('.portfolio-filter').find('.btn-primary').removeClass('btn-primary');
    // set selected class on new item
    $elem.addClass('btn-primary');
}

jQuery(document).ready(function ($) {

    $('body.cms div.rte li').wrapInner('<span/>');
    $('.top-sticky').affix({});

    // blur effect when megamenu is active
    $("#ph_megamenu > li").on('hover', function () {
        contentWrapper.addClass('blur');
    }, function () {
        contentWrapper.removeClass('blur');
    });

    // show language, currency options
    $('.topbar .select-options').on('click', function (event) {
        $('.topbar .select-options').not(this).removeClass('active');
        if ($(event.target).parent().parent().attr('class') == 'options') {
            hideDD();
        } else {
            if ($(this).hasClass('active') && $(event.target).is("p")) {
                hideDD();
            } else {
                $(this).toggleClass('active');
            }
        }
        event.stopPropagation();
    });

    $(document).on('click', function () { hideDD(); });
    $('.topbar .select-options ul').on('click', function () {
        var opt = $(this);
        var text = opt.text();
        $('.topbar .select-options.active p').text(text);
        hideDD();
    });

    // search block
    searchBox.on('click', function () {
        searchBoxInput.trigger('click');
    });

    searchBox.on('mouseenter', function () {
        searchBoxInput.addClass('hover');
    });

    searchBox.on('mouseleave', function () {
        searchBoxInput.removeClass('hover');
    });

    // cart
    $('.shopping_cart').on('hover', function () {
        $(this).find('a.cart-contents').addClass('active');
    }, function () {
        $(this).find('a.cart-contents').removeClass('active');
    });

    // categories
    $('#categories_block_left.simple ul li').on('mouseenter', function () {
        var submenuWidth = $(this).parent().width();
        $(this).addClass('active');
        if ($('.sidebar').hasClass('right-column')) {
            $(this).hover().find('ul').first().css({ 'width': submenuWidth, 'right': +submenuWidth }).show();
        } else {
            $(this).hover().find('ul').first().css({ 'width': submenuWidth, 'right': -submenuWidth }).show();
        }
    });

    $('#categories_block_left.simple ul li').mouseleave(function () {
        $(this).removeClass('active');
        $(this).find('ul').first().css({ 'right': '0' }).hide();
    });

    $('#categories_block_left.category ul li').on('click', 'i.plus', function () {
        el = $(this);
        el.parent().addClass('active');
        el.parent().find('ul').first().slideDown();
        el.hide();
        el.parent().find('i.minus').show();
    });

    $('#categories_block_left.category ul li').on('click', 'i.minus', function () {
        el = $(this);
        el.parent().removeClass('active');
        el.parent().find('ul').first().slideUp();
        el.hide();
        el.parent().find('i.plus').show();
    });

    $('#categories_block_left.category ul li i').on('hover', function () {
        $(this).parent().addClass('active2');
    }, function () {
        $(this).parent().removeClass('active2');
    });

    // slider range
    if (sliderRange.length > 0) {
        sliderRange.slider({
            range: true,
            min: 0,
            max: 20000,
            values: [1000, 7500],
            slide: function (event, ui) {
                $("#layered_price_range").val("₺" + ui.values[0] + " - ₺" + ui.values[1]);
            }
        });
        $("#layered_price_range").val("₺" + sliderRange.slider("values", 0) +
		" - ₺" + sliderRange.slider("values", 1));
    }

    // product lists
    if ($('.list-style-buttons').length > 0) {
        var default_view = 'grid',
			switcher = $('a.switcher'),
			theid = switcher.attr("id"),
			classNames = switcher.attr('class').split(' ');

        if ($.cookie('view') !== 'undefined') {
            $.cookie('view', default_view, { expires: 7, path: '/' });
        }

        if ($.cookie('view') == 'list') {
            get_list();
        }

        if ($.cookie('view') == 'grid') {
            get_grid();
        }

        listViewContainer.on('click', function (e) {
            $.cookie('view', 'list');
            get_list();
            e.preventDefault();
        })

        gridViewContainer.on('click', function (e) {
            $.cookie('view', 'grid');
            get_grid();
            e.preventDefault();
        });
    }

    // products image zoom and main image
    if ($('#product').length > 0) {
        $('#thumbs_list_frame a').on('hover', function () {
            var url = $(this).attr('href');
            $('#view_full_size a').attr('href', url);
            $('#bigpic').attr('src', url);
        });
        $('.image-zoom').magnificPopup({ type: 'image', mainClass: 'mfp-fade' });
        $('#thumbs_list_frame').each(function () {
            $(this).magnificPopup({
                delegate: 'a',
                type: 'image',
                mainClass: 'mfp-fade',
                gallery: {
                    enabled: true
                }
            });
        });
    }

    // tooltips
    if ($('.contact-form-box').length == 0) {
        $("[data-toggle='tooltip']").tooltip();
    }
    // use select style css
    if ($('#id_country').length > 0) {
        $('#id_country').selectBox({
            menuTransition: 'slide',
            menuSpeed: 'fast',
            keepInViewport: false
        });
    }

    if ($('#product select').length > 0) {
        $('select').selectBox({
            menuTransition: 'slide',
            menuSpeed: 'fast',
            keepInViewport: false
        });
    }

    if ($('form.std select').length > 0) {
        $('form.std select').selectBox({
            menuTransition: 'slide',
            menuSpeed: 'fast',
            keepInViewport: false
        });
    }

    // input file
    if ($('input[type="file"]').length > 0) {
        $(":file").filestyle({
            buttonName: "button",
            icon: false
        });
    }

    // animations
    if ($().appear) {

        if ($.browser.mobile == true) {
            // disable animation on mobile
            $("body").removeClass("cssAnimate");
        } else {
            $('.cssAnimate .animated').appear(function () {
                var $this = jQuery(this);

                $this.each(function () {
                    if ($this.data('time') != undefined) {
                        setTimeout(function () {
                            $this.addClass('activate');
                            $this.addClass($this.data('fx'));
                        }, $this.data('time'));
                    } else {
                        $this.addClass('activate');
                        $this.addClass($this.data('fx'));
                    }
                });
            }, { accX: 50, accY: -150 });

            clearTimeout();
        }
    }

    // quantity buttons
    $('#quantity_wanted_p a.more').on('click', function (e) {
        e.preventDefault();
        var currentVal = parseInt($("#quantity_wanted").val());
        if (!isNaN(currentVal)) {
            qtyWantedInput.val(currentVal + 1);
        } else {
            qtyWantedInput.val(1);
        }
    });

    $("#quantity_wanted_p a.less").on('click', function (e) {
        e.preventDefault();
        var currentVal = parseInt($("#quantity_wanted").val());
        if (!isNaN(currentVal) && currentVal > 1) {
            qtyWantedInput.val(currentVal - 1);
        } else {
            qtyWantedInput.val(1);
        }
    });

    // megamenu
    $('.ph_megamenu').ph_megamenu();

    // blog
    if (document.documentElement.clientWidth >= 991) {
        $('.recent_posts .post-content').equalHeights();
    }

    // scroll to top btn
    $('a.totop').on('click', function (e) {
        $('html, body').animate({ scrollTop: '0px' }, 1500);
        e.preventDefault();
    });

    // start carousels with products, manufacturers etc.
    installCarousels();

    $(window).on('load', function () {
        // slider
        if (defaultSlider.length > 0) {
            startSlider();
        }

        if (miniSliderDiv.length > 0) {
            miniSlider();
        }

        // portfolio
        if ($().isotope && $('.portfolio-container').length > 0) {

            var $container = $('.portfolio-container');
            $container.isotope({
                itemSelector: '.item',
                layoutMode: 'fitRows',
                resizable: false
            });

            $('.portfolio-filter').on('click', 'a', function (e) {
                var filterValue = $(this).attr('data-filter');
                var $this = $(this);
                $container.isotope({ filter: filterValue });
                changeSelectedLink($this);
                //$container.isotope('reLayout');
                e.preventDefault;
            });
        }

        if ($('.portfolio-container .item .portfolio-thumbnail').length > 0) {
            $('.portfolio-container .item .portfolio-thumbnail').each(function () {
                $(this).magnificPopup({
                    delegate: 'a',
                    type: 'image',
                    mainClass: 'mfp-fade'
                });
            });
        }
    });

    $(window).on('resize', function () {
        // blog
        if (document.documentElement.clientWidth >= 991) {
            $('.recent_posts .post-content').equalHeights();
        }

        // slider
        if (defaultSlider.length > 0) {
            startSlider();
        }

        if (miniSliderDiv.length > 0) {
            miniSlider();
        }
    });
});

function installCarousels() {

    $('.owl-carousel-ph').each(function () {

        /* Max items counting */
        var max_items = $(this).data('max-items');
        var md_items = max_items - 1;
        var sm_items = max_items - 1;

        $(this).owlCarousel({
            //stagePadding: 50,
            items: max_items,
            responsive: {
                0: {
                    items: 1
                },
                768: {
                    items: sm_items
                },
                991: {
                    items: md_items
                },
                1199: {
                    items: max_items
                }
            },
            pagination: false,
        });

        var owl = $(this).data('owlCarousel');

        /* Arrow next */
        $(this).parents('.carousel-style').find('.arrow-prev').on('click', function (e) {
            owl.prev();
            e.preventDefault();
        });

        /* Arrow previous */
        $(this).parents('.carousel-style').find('.arrow-next').on('click', function (e) {
            owl.next();
            e.preventDefault();
        });

    });

};

function startSlider() {
    defaultSlider.unslider({
        keys: true,
        dots: true,
        fluid: true
    });

    // add arrows prev, next to slider
    var unslider = defaultSlider.unslider();
    $('.unslider-arrow').on('click', function (e) {
        var fn = this.className.split(' ')[1];
        unslider.data('unslider')[fn]();
        e.preventDefault();
    });
}

function miniSlider() {
    miniSliderDiv.unslider({
        dots: true,
        fluid: true
    });
}
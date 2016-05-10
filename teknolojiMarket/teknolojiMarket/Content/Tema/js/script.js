
// ===============
// Slider function
// ===============
function slider(){

	//Main slider
	$('#flexcarousel').flexslider({
    animation: "slide",
    controlNav: false,
    animationLoop: false,
    slideshow: false,
    itemWidth: 188,
    //itemMargin: 5 ,
    asNavFor: '#flexslider'
  });
  
  	//Main slider
	$('#flexcarousel-details').flexslider({
    animation: "slide",
    controlNav: false,
    animationLoop: false,
    slideshow: false,
    itemWidth: 136,
    //itemMargin: 5 ,
    asNavFor: '#flexslider'
  });
   
  $('#flexslider').flexslider({
    animation: "slide",
    controlNav: true,
    animationLoop: true,
    slideshow: true,
	slideshowSpeed: 5000,
	animationSpeed: 600,
    sync: "#flexcarousel"
  });
  
  // Thubnail
  $('#flexcarousel-product').flexslider({
    animation: "slide",
    controlNav: false,
    animationLoop: false,
    slideshow: false,
    itemWidth: 115,
    asNavFor: "#flexslider-product"
  });
  
  $('#flexslider-product').flexslider({
    animation: "slide",
    controlNav: true,
    animationLoop: true,
    slideshow: false,
    sync: "#flexcarousel-product"
  });

  // Brands
  $('#flexcarousel-brands').flexslider({
    animation: "slide",
    controlNav: false,
    animationLoop: true,
    slideshow: false,
    itemWidth: 182
  });
}

// ===================
// Navigation function
// ===================

function navWidth(){
	var nav = $('.horizontal-nav ul li').not('.horizontal-nav ul li li'), 
	size = $('.horizontal-nav ul li').not('.horizontal-nav ul li li').size(),
	percent = 98.9/size;
	nav.css('width', percent+'%').parent().show();
}

$('.horizontal-nav ul li').mouseenter(function(){
	$('ul', this).stop().slideDown('fast');
}).mouseleave(function(){
	$('ul', this).stop().slideUp(150);
});

if ($.browser.msie) {
	//Back off
} else {
	selectnav('nav', {
		label: 'Menu'
	});	
}


// ======================
// Thumbnail Hover Effect
// ======================

function thumbHover(){

	if ($('html').hasClass('csstransforms3d')) {	
		
		$('.thumb').removeClass('scroll').addClass('flip');		
		$('.thumb.flip').hover(
			function () {
				$(this).find('.thumb-wrapper').addClass('flipIt');
			},
			function () {
				$(this).find('.thumb-wrapper').removeClass('flipIt');			
			}
		);
		
	} else {

		$('.thumb').hover(
			function () {
				$(this).find('.thumb-detail').stop().animate({bottom:0}, 500, 'easeOutCubic');
			},
			function () {
				$(this).find('.thumb-detail').stop().animate({bottom: ($(this).height() * -1) }, 500, 'easeOutCubic');			
			}
		);

	}
}

// ==========
// Google Map
// ==========

if ($('#map').hasClass('gmap')) {
	$('.gmap').mobileGmap();
}

// ============
// Initial load
// ============

$(function(){

	// Cart bubble
	$('.counter a').on('click', function(){
		$('.cartbubble').slideToggle();
	});
	$('#closeit').on('click',function(e){
		e.preventDefault;
		$('.cartbubble').slideUp();
	});
	
		// Cart bubble
	$('.absolutecontact a').on('click', function(){
		$('.absolutecontact').slideToggle();
	});
	$('.absolutecontact a').on('click',function(e){
		e.preventDefault;
		$('.absolutecontact').slideUp();
	});
	
	// Tab function
	$('#myTab a, #myTab button').click(function (e) {
		e.preventDefault();
		$(this).tab('show');
	});
	
	// Fancybox function
	$('#flexslider-product .slides a').fancybox();

	// Toggle function
	$('.product h6.subhead').on('click', function(){
		$('.query').slideToggle();
	});

    $(".collapse").collapse();
	
	slider();
	navWidth();
	thumbHover();

});


$(document).ready(function () {
// Turn dynamic animations for iOS devices (because it doesn't look right)

    var iOS = false,
        p = navigator.platform;

    if (p === 'iPad' || p === 'iPhone' || p === 'iPod') {
        iOS = true;
    }

    // Control Dynamic Content Sliding 

    if (iOS === false) {

        $('.flyIn').bind('inview', function (event, visible) {
            if (visible === true) {
                $(this).addClass('animated fadeInUp');
            }
        });

        $('.flyLeft').bind('inview', function (event, visible) {
            if (visible === true) {
                $(this).addClass('animated fadeInLeftBig');
            }
        });

        $('.flyRight').bind('inview', function (event, visible) {
            if (visible === true) {
                $(this).addClass('animated fadeInRightBig');
            }
        }); 
		$('.flyDown').bind('inview', function (event, visible) {
            if (visible === true) {
                $(this).addClass('animated fadeInDown');
            }
        });

    }

});

$(document).ready( function() {

$(".enditems").css("margin-left","-31px");

});
	
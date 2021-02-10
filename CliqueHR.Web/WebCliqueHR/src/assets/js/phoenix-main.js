$(document).ready(function () {
<<<<<<< HEAD
    buttonRipple();
    $('.carousel').carousel('pause');
    $('[data-toggle="tooltip"]').tooltip();
    $('[data-tooltip="tooltip"]').tooltip();
=======
    if (document.fullscreenEnabled) {
        var fullScreen = document.getElementById("fullScreen");
        fullScreen.addEventListener("click", function (event) {
            if (!document.fullscreenElement) {
                document.documentElement.requestFullscreen();
            } else {
                document.exitFullscreen();
            }
        }, false);
        // document.addEventListener("fullscreenchange", function (event) {
        //     console.log(event);
        //     if (!document.fullscreenElement) {
        //         fullScreen.innerText = "Activate fullscreen";
        //     } else {
        //         fullScreen.innerText = "Exit fullscreen";
        //     }
        // });
        document.addEventListener("fullscreenerror", function (event) {
            console.log(event);
        });
    }
    

    buttonRipple();
    $('.carousel').carousel('pause');
    $('[data-toggle="tooltip"]').tooltip();
    $('[data-tooltip="tooltip"]').tooltip()
    $('[data-toggle="popover"]').popover({
        trigger: 'focus'
    });
>>>>>>> change

    $(".image-list > .image-item").click(function(){
        $(this).parent().find(".selected-image").removeClass("selected-image");
        $(this).addClass("selected-image");
        var selectedImgUrl = $(".selected-image > img").attr('src');
        $(".body").css("background-image", "url(" + selectedImgUrl + ")");
    });

    $(".admin-image-list > .admin-image-item").click(function(){
        $(this).toggleClass("selected-image");
    });

    $('.theme-mode input[type=radio]').change(function() {
        // jQuery("body").prepend('<div class="custom-preloader">\
        //     <img src="images/logo.png" />\
        // </div>');
        if (this.value == 'light') {
            $(".body").removeClass("dark-theme");
        }
        else if (this.value == 'dark') {
            $(".body").addClass("dark-theme");
        }
    });

    $('.rounded-mode input[type=radio]').change(function() {
        if (this.value == "rounded") {
            $(".body").addClass("border-rounded");
        }
        else if(this.value == "flat"){
            $(".body").removeClass("border-rounded");
        }
    });

    /* For File Inut value print */
    $("input.custom-file-input").on("change",function(){
        //get the file name
        var fileName = $(this).val();
        str = fileName.replace(/C:\\fakepath\\/i,'');
        //replace the "Choose a file" label
        $(this).parent().parent().parent().find('.file-name').html(str);
    });

    // $('link[rel=stylesheet][href="css/phoenix-theme.css"]').remove();
    // $('head').append('<link rel="stylesheet" href="css/phoenix-dark-theme.css" type="text/css" />');

<<<<<<< HEAD
=======
    $(".multiple-select").multiselect({
        //buttonContainer: '<div class="mt-1"></div>',
        includeSelectAllOption: true,
        selectAllText: 'Select All',
        enableFiltering: true,
        buttonWidth: '100%',
        numberDisplayed: 2,
        enableCaseInsensitiveFiltering: true,
        // templates: { // Use the Awesome Bootstrap Checkbox structure
        //     li: '<li class="checkList"><a tabindex="0"><div class="aweCheckbox aweCheckbox-danger"><label for=""></label></div></a></li>'
        // }
    });
    $(".single-select").multiselect({
        //buttonContainer: '<div class="mt-1"></div>',
        includeSelectAllOption: true,
        selectAllText: 'Select All',
        enableFiltering: true,
        buttonWidth: '100%',
        numberDisplayed: 2,
        enableCaseInsensitiveFiltering: true
    });

    $('.multiselect-tree').multiselect({
        selectAllText: 'Select All',
        enableFiltering: true,
        numberDisplayed: 2,
        buttonWidth: '100%'
    });
    var optgroups = [
        {
            label: 'Group 1', children: [
                {label: 'Option 1.1', value: '1-1', selected: true},
                {label: 'Option 1.2', value: '1-2'},
                {label: 'Option 1.3', value: '1-3'}
            ]
        },
        {
            label: 'Group 2', children: [
                {label: 'Option 2.1', value: '1'},
                {label: 'Option 2.2', value: '2'},
                {label: 'Option 2.3', children: [
                    {label: 'Option 2.3.1', value: '2-1'},
                    {label: 'Option 2.3.2', value: '2-2'}
                ]}
            ]
        }
    ];
    $('.multiselect-tree').multiselect('dataprovider', optgroups);

>>>>>>> change
    /* menu open close wrapper screen click close menu */
    $('.menu-btn').on('click', function (e) {
        e.stopPropagation();
        if ($('body').hasClass('sidemenu-open') == true) {
            $('body').removeClass('sidemenu-open');
            setTimeout(function () {
                $('body').removeClass('menuactive');
            }, 500);
        } else {
            $('body').addClass('sidemenu-open menuactive');
        }
    });
    $('.outer-wrapper').on('click', function () {
        if ($('body').hasClass('sidemenu-open') == true) {
            $('body').removeClass('sidemenu-open');
            setTimeout(function () {
                $('body').removeClass('menuactive');
            }, 500);
        }
    });

    $(".setting-btn").click(function(){
        $(".setting-box").css("display","block");
        $(".main-menu").css("display","none");
    });
    $(".back-btn").click(function(){
        $(".setting-box").css("display","none");
        $(".main-menu").css("display","block");
    });
    
    /* Float label checking input is not empty */
    $('.float-label .form-control').on('blur', function () {
        if ($(this).val() || $(this).val().length != 0) {
            $(this).closest('.float-label').addClass('active');
        } else {
            $(this).closest('.float-label').removeClass('active');
        }
    })

    /* Full Menu */                
    $("#footer-menu .btn-bounce").on("click",function(){
        $(this).parents('.footer-menu').toggleClass("footer-expanded");
    });    

    /* Footer Search*/
    $(".footer-search").click(function(){
        $(".footer-search-open").css("display","block");
    });
    $(".footer-search-open .input-group-text").click(function(){
        $(this).parents().find(".footer-search-open").css("display","none");
    });
    /* End */

    /* always keep at least 1 open by preventing the current to close itself */
    $('[data-toggle="collapse"].slider-item-custom').on('click',function(e){
        if ( $(this).parents('.wrapper').find('.collapse.show') ){
            var idx = $(this).index('[data-toggle="collapse"].slider-item-custom');
            if (idx == $('.collapse.show').index('.collapse')) {
                // prevent collapse
                e.stopPropagation();
            }
        }
    });
<<<<<<< HEAD
    
});

=======

    /* Telphone Number JS */

    // var telInput = $(".phone-number");
    // // initialise plugin
    // telInput.intlTelInput({

    //     allowExtensions: true,
    //     formatOnDisplay: true,
    //     autoFormat: true,
    //     autoHideDialCode: true,
    //     autoPlaceholder: true,
    //     defaultCountry: "in",
    //     ipinfoToken: "yolo",

    //     nationalMode: false,
    //     numberType: "MOBILE",
    //     //onlyCountries: ['us', 'gb', 'ch', 'ca', 'do'],
    //     preferredCountries: ['in', 'ae', 'qa','om','bh','kw','ma'],
    //     preventInvalidNumbers: true,
    //     separateDialCode: true,
    //     initialCountry: "auto",
    //     geoIpLookup: function(callback) {
    //         $.get("http://ipinfo.io", function() {}, "jsonp").always(function(resp) {
    //             var countryCode = (resp && resp.country) ? resp.country : "";
    //             callback(countryCode);
    //         });
    //     },
    //     utilsScript: "https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/11.0.9/js/utils.js"
    // });
    // var reset = function() {
    //     telInput.removeClass("error");
    // };
    // // on keyup / change flag: reset
    // telInput.on("keyup change", reset);


    var telInput = $(".phone-number");
    // initialise plugin
    telInput.intlTelInput({
        separateDialCode: true,
        defaultCountry: "in",
        initialCountry: "in",
        preferredCountries: ['in','us', 'gb', 'ch', 'ca', 'do'],
        // Note that the callback must still be called in the event of an error, hence the use of always in this example. Tip: store the result in a cookie to avoid repeat lookups!
        // initialCountry: "auto", 
        // geoIpLookup: function(success, failure) {
        //     $.get("https://ipinfo.io", function() {}, "jsonp").always(function(resp) {
        //     var countryCode = (resp && resp.country) ? resp.country : "us";
        //     success(countryCode);
        //     });
        // },
        utilsScript:"https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/11.0.4/js/utils.js"
    });
        
    var reset = function() {
        telInput.removeClass("error");
    };
        
    // on keyup / change flag: reset
    telInput.on("keyup change", reset);
});


function colorPicker(){
    var colorList = ['000000', '1AAF42', '007B87', '001D4B', '57C158', 'F4C507', 'E2585A', 'E85FA3', '298FF4', '20c997', '17a2b8','118cf5'];
    var picker = $('.color-picker');

    for (var i = 0; i < colorList.length; i++) {
        picker.append('<li class="color-item" data-hex="' + '#' + colorList[i] + '" style="background-color:' + '#' +
            colorList[i] + ';"></li>');
    }
    // $('body').click(function () {
    //     picker.fadeOut();
    // });
    picker.children('li').click(function () {
        $(this).parent().find(".selected-color").removeClass("selected-color");
        var codeHex = $(this).data('hex');        
        $('.color-holder').css('background-color', codeHex);
        if($('head #primaryFill').length > 0){
            $('head #primaryFill').remove();            
        }
        $("<style id='primaryFill'>:root .body{ --theme-icon-color:"+ codeHex + "!important;}  </style>" ).appendTo( "head" ); // .navbar{ background: "+ codeHex + "!important; }
        $(this).addClass("selected-color");        
        document.querySelectorAll('object').forEach(function(e){
            e.contentDocument && e.contentDocument.querySelectorAll("[class='icon-fill']").forEach(function(f){f.style.fill= codeHex });
        });
    });
}

>>>>>>> change
/* Button Animation Effect Start */
function buttonRipple(e){
    $(".btn").click(function (e) {
        // Remove any old one
        $(".ripple").remove();
    
        // Setup
        var posX = $(this).offset().left,
            posY = $(this).offset().top,
            buttonWidth = $(this).width(),
            buttonHeight =  $(this).height();
        
        // Add the element
        $(this).prepend("<span class='ripple'></span>");
    
        
        // Make it round!
        if(buttonWidth >= buttonHeight) {
            buttonHeight = buttonWidth;
        } else {
            buttonWidth = buttonHeight; 
        }
        
        // Get the center of the element
        var x = e.pageX - posX - buttonWidth / 2;
        var y = e.pageY - posY - buttonHeight / 2;
        
        
        // Add the ripples CSS and start the animation
        $(".ripple").css({
            width: buttonWidth,
            height: buttonHeight,
            top: y + 'px',
            left: x + 'px'
        }).addClass("rippleEffect");
    });
}
/* Button Animation Effect End */

function matchStart(params, data) {
    // If there are no search terms, return all of the data
    if ($.trim(params.term) === '') {
        return data;
    }

    // Skip if there is no 'children' property
    if (typeof data.children === 'undefined') {
        return null;
    }

    // `data.children` contains the actual options that we are matching against
    var filteredChildren = [];
    $.each(data.children, function (idx, child) {
        if (child.text.toUpperCase().indexOf(params.term.toUpperCase()) == 0) {
        filteredChildren.push(child);
        }
    });

    // If we matched any of the timezone group's children, then set the matched children on the group
    // and return the group object
    if (filteredChildren.length) {
        var modifiedData = $.extend({}, data, true);
        modifiedData.children = filteredChildren;

        // You can return modified objects from here
        // This includes matching the `children` how you want in nested data sets
        return modifiedData;
    }

    // Return `null` if the term should not be displayed
    return null;
}
<<<<<<< HEAD
=======

$(function () {
    $('.custom_date_picker').datetimepicker(
        {
            format: 'DD MMM YYYY',
            icons: {
                up: "icon icon-up",
                down: "icon icon-down",
                previous: 'icon icon-back',
                next: 'icon icon-next',
            }
        }
    );
    $('.custom_month_picker').datetimepicker(
        {
            format: 'MMM YYYY',
            icons: {
                up: "icon icon-up",
                down: "icon icon-down",
                previous: 'icon icon-back',
                next: 'icon icon-next',
            }
        }
    );
    $('.custom_year_picker').datetimepicker(
        {
            format: 'YYYY',
            icons: {
                up: "icon icon-up",
                down: "icon icon-down",
                previous: 'icon icon-back',
                next: 'icon icon-next',
            }
        }
    );
    $('.custom_time_picker').datetimepicker(
        {
            format: 'LT',
            icons: {
                up: "icon icon-up",
                down: "icon icon-down",
                previous: 'icon icon-back',
                next: 'icon icon-next',
            }
        }
    );
});
>>>>>>> change

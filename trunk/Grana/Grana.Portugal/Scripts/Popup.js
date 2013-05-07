var popupStatus = 0;

Popup = {

    //loading popup with jQuery magic!
    loadPopup: function (idPopup) {
        //loads popup only if it is disabled
        if (popupStatus == 0) {
            $("#backgroundPopup").css({ "opacity": "0.7" });
            $("#backgroundPopup").fadeIn("slow");
            $('#' + idPopup).fadeIn("slow");
            popupStatus = 1;
        }
    },

    disablePopup: function () {
        //disables popup only if it is enabled
        if (popupStatus == 1) {
            $("#backgroundPopup").fadeOut("slow");
            $('.popup').fadeOut("slow");
            popupStatus = 0;
        }
    },
    centerPopup: function (idPopup) {
        //request data for centering
        var windowWidth = document.documentElement.clientWidth;
        var windowHeight = document.documentElement.clientHeight;
        var popupHeight = $('#' + idPopup).height();
        var popupWidth = $('#' + idPopup).width();
        //centering
        $('#' + idPopup).css({
                "position": "absolute",
                "top": 100,
                "left": windowWidth / 2 - popupWidth / 2
            });
        //only need force for IE6

        $("#backgroundPopup").css({
                "height": windowHeight
            });

    }

}
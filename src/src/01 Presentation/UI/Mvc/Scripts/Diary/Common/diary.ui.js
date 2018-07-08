var DRUI = (function () {


    $(function () {
        BindEvents();
    });

    function BindEvents() {
       
        $(document).on("ui.lock", function () { LockUI(); });
        $(document).on("ui.unlock", function () { UnLockUI(); });
        $(document).on("ui.startloader", function () { StartLoader(); });
        $(document).on("ui.stoploader", function () { StopLoader(); });
        BindDatePickerEvent();
    };

    //Custom Event Functions    

    function LockUI()
    {
        var $outerlayDiv = $('.dMenu').find('.outerlaydivui');
        if ($outerlayDiv.length == 0) {
            var outerLayDiv = '<div class="outerlaydivui"></div>';
            $('.dMenu').append($(outerLayDiv));
        }
        $outerlayDiv.fadeIn('slow', function () { });
       

    }

    function UnLockUI() {

        $('.outerlaydivui').fadeOut('slow', function () {

            $(this).remove();
        });

    }
        
    function BindDatePickerEvent() {
        $(".datepicker").datepicker({
            showAnim: "slideDown",
            duration: "slow",
            dateFormat: DREnum.DateFormats.dd_mm_yy,
            onSelect: function () {
                $(this).change();
            }
        });

        //$(".datepicker").datepicker({
        //    showOn: "button",
        //    buttonImage: "images/calendar.gif",
        //    buttonImageOnly: true
        //});
    }

    function StartLoader() {
        var $loaderDiv = $('.dMenu').find('.outerloader');
        if ($loaderDiv.length == 0) {
            var LoaderDiv = '<div class="outerloader">' +
                                      '<div class="loader"></div>' +
                                    '</div>';
            $('.dMenu').append($(LoaderDiv));
        }
        $loaderDiv.show();

    }

    function StopLoader() {
        $("div.outerloader").fadeOut('slow');
    }
    return {

    };
})();
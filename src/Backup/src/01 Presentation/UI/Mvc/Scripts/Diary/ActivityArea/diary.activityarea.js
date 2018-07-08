//JSModule to handle activityareas
var DRActivityArea = (function () {

    $(function () {
        bindEvents();
    });

    function bindEvents() {

        $(document).on("click", ".diary_activityArea_outerDiv_close", function (e) {
            //$(".diary_activityArea_outerDiv").hide(1000);
            hideAndFocusToParentDiv();
        });

    }

    function hideAndFocusToParentDiv(e,shouldClearAllInputs) {

        var $this = $('.diary_activityArea_outerDiv');

        if (shouldClearAllInputs) {
            DRCommon.ClearAllInputs($this); //clear all the inputs to intitialize
        }

        DRLayout.AdjustMainSection($this, false); //adjust the height of main div section
        $this.effect('blind', { mode: 'hide', direction: 'vertical', easing: 'swing' }, 400, function () {
            DRToolBox.Focus();
        });
    }

    function GetActivityDiv() {

        var activityDiv = '<div class="diary_activityArea_outerDiv" >' +
                         '<div class="diary_activityArea_outerDiv_close">X</div>' +
						 '</div>';
    }

    return {

        Hide: hideAndFocusToParentDiv
    };

})();
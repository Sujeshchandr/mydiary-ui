var DRCommon = (function () {

    var varvariable = 11;
    function clearAllInputs(control,e)
    {
        $Control = control;
        $Control.find(':input').each(function () {
            switch (this.type) {
                case 'password':
                case 'text':
                case 'textarea':
                case 'file':
                case 'select-one':
                case 'select-multiple':
                    if ($(this).hasClass('select2-input')) {
                        $(document).trigger('clearDropDowns');//Should define the logic in the controller
                    }
                    else {
                        $(this).val('');
                    }
                    break;
                case 'checkbox':
                case 'radio':
                    this.checked = false;
            }
           
         //   $(this).trigger('input'); //ngModel listens for "input" event, so to reflect the input changes to model, we need to trigger that event after setting the value:
        }
        
        );

    }
    return {
        ClearAllInputs :clearAllInputs
    }
})()
(function ($, undefined) {

    $.fn.DRValidate = (function () {       
       
        var result = true;
        this.each(function (index, item) {
        
            var value = $(item).val();
            if( $(item).has($('option'))) 
                  value = value == -1 ? "" : value;
            if (value == "" || value == "undefined") {

                $(item).bind("click", function () { $(this).RemoveDRValidation(); }); //  remove validation when user entered some value
                $(item).effect('pulsate', { times: 2, easing: 'easeOutBounce' }, 100, function () {
                    $(item).addClass("DRValidationError");
                });
                result = false;
            }
            //else {
            //    $(item).removeClass("DRValidationError");
            //}
        }
        );
        this.valid = result;
        return this;
      
    });

    $.fn.RemoveDRValidation = (function () {

        var value = this.val();
        if (this.has($('option')))
                value = value == -1 ? "" : value;
        if (value != "" || value != "undefined") {

            this.removeClass('DRValidationError');
        }
        return this;
       
    });

})(jQuery)
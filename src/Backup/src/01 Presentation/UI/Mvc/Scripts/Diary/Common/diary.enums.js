var DREnum = (function () {
    
    
    var socialServiceSites = (function() {

        return {
            Facebook: 1,
            Twitter: 2,
            LinkedIn: 3,
            GooglePlus:4
        };
    }());

    var uploadImages = (function () {

        return {
            Default: 0           
        };
    }());

    var dateFormats = (function () {
        return {
            
            dd_mm_yy: 'dd-mm-yy', //used for jquery-datepicker
            dd_MM_yy: 'dd-MM-yyyy' // Used for angular-ui.datepicker
        };
    })();

    return {
        SocialNWSites: socialServiceSites,
        UploadImages: uploadImages,
        DateFormats :dateFormats
    };
})();
var DRUserImage = (function () {
        
    var WcfUrl = "http://localhost:30491/";

    $(function () {
        bindEvents();
        init();
    });

    function bindEvents() {

        $(document).on('click', '#fileupload', function () {
            uploadImage();
        });
        
        $('.fileuploadSpan').on('click', function () {
            $('#fileupload').click();
        });
    }

    function init()
    {
        showUserPreviewImage('');
    }

    function uploadImage() {
     
        uploadImageByService().done(function (data) {
            if (data != typeof undefined && data.Type == "Error") {
                $(document).trigger('ui.unlock');               
                DRError({
                    Type: "Error",
                    Message: data.Message
                });

            }
            else {
                // wcf service with webHttpBinding [WCF + REST ],
                //so that we can call like the api by specifying the url
                var imageSrc = WcfUrl + "Services/ImageWcfService.svc/Get/" + data.result.UploadedImageId;
                $('.file_name').data("imagename", data.result.Name);
                showUserPreviewImage(imageSrc);
                $('.file_name').data("uploaded-imageId", data.result.UploadedImageId);
                new DRAlert("successfully uploaded image");
            }
        });
       
    }

    function saveAPIImage(url, siteId) {

        if (siteId == DREnum.SocialNWSites.Facebook) {

            saveFacebookImage(url)
                .done(function (data) {

                // wcf service with webHttpBinding [WCF + REST ],
                //so that we can call like the api by specifying the url
                var imageSrc = WcfUrl + "Services/ImageWcfService.svc/Get/" + data.UploadedImageId;
                $('.file_name').data("imagename", data.Name);
                showUserPreviewImage(imageSrc);
                $('.file_name').data("uploaded-imageId", data.UploadedImageId);
               
            })
                .always(function(data){
                    $(document).trigger('ui.unlock'); //lock started after getting response from facebook api in function GetLoginUserAPI()
                    $(document).trigger('ui.stoploader');//loader started after getting response from facebook api in function GetLoginUserAPI()
                });

        }
    }

    function showUserPreviewImage(imageSource) {

        var defaultSrc = '/../Content/Diary/UserImages/00000000-0000-0000-0000-000000000000/user-default-men-160.png';
        var source = imageSource == ("" | null | typeof undefined) ? defaultSrc : imageSource;
        var $DivPreviewImage = $('.previewImage');
        var $ImgeProfile = $DivPreviewImage.find('.user_profile_image');
        var imageName = $('.file_name').data("imagename");
        $DivPreviewImage.css('display', 'none');
        $ImgeProfile.attr('src', source);
        $ImgeProfile.attr('title', imageName);
        $ImgeProfile.aeImageResize({ height: 150, width: 150 });
        $DivPreviewImage.css('display', 'block');
    }
   
    function deleteUploadedUserImage() {
        var defered = $.Deferred();
        var userGuid = $('.file_name').data("userfolder");
        var jsonData = JSON.stringify({ userGuid: userGuid });

        $.ajax({
            url: 'People/DeleteUploadedImage',
            type: 'POST',
            datatype: 'json',
            data: jsonData,
            //contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (data) {

                defered.resolve(data);
            },
            error: function (status) {

                defered.reject();
                DRError({
                    Type: "Error",
                    Message:"Delete user image failed" 
                });                         
            }
        });
        return defered.promise();
    }

    function uploadImageByService() {
        //$(document).trigger('ui.lock');
        var  timer;
        hideLoader();
        jQuery.support.cors = true;
        var defered = $.Deferred();
        $('#fileupload').fileupload({
            dataType: 'json',
            crossDomain: true,
            type: 'POST',
            url: WcfUrl + 'Services/ImageWcfService.svc/Upload',
            autoUpload: true,
            contentType: 'multipart/form-data;',
            add: function (e, data) {
                var fileType = data.files[0].name.split('.').pop(), allowdtypes = 'jpeg,jpg,png,gif';
                if (allowdtypes.indexOf(fileType.toLocaleLowerCase()) < 0) {
                    var result = {
                        Type: "Error",
                        Message: "please upload images only"
                    };
                    defered.resolve(result);

                } else {
                    if (data.autoUpload || (data.autoUpload !== false &&
                    $(this).fileupload('option', 'autoUpload'))) {
                        data.process().done(function () {
                            data.submit();
                        });
                    }
                }
            },
            start: function (e, data) {
                timer && clearTimeout(timer);
                timer = setTimeout(function () {
                    showLoader();
                },
                1000);//only show loader ,if upload is taking more than 100ms
            },
            done: function (e, data) {             
                defered.resolve(data);
            },
            fail: function (e, XMLHttpRequest) {             
                defered.reject();
                DRError({
                    Type: "Error",
                    Message: "file upload failed.please try again"
                });                             
            },
            always: function (e, data) {               
                clearTimeout(timer);
                hideLoader();
               // $(document).trigger('ui.unlock');
            }
        });
        return defered.promise();
    }

    function uploadUserImageToDB() {
        
        var defered = $.Deferred();
        $('#fileupload').fileupload({
            dataType: 'json',          
            type:'POST',
            url: '/People/UploadImageToDB',        
            autoUpload: true,            
            done: function (e, data) {
                defered.resolve(data);
            },
            fail: function (e, data) {
                defered.reject();
                DRError({
                    Type: "Error",
                    Message: "file upload failed.please try again"
                });                        
            }

        })
        //.on('fileuploadprogressall', function (e, data) {
        //    var progress = parseInt(data.loaded / data.total * 100, 10);
        //    $('.progress .progress-bar').css('height','40%')
        //$('.progress .progress-bar').css('width', progress + '%');
        //})
        ;
        return defered.promise();
    }

    function saveFacebookImage(url) {
 
        jQuery.support.cors = true;
        var facebookJson = JSON.stringify({
            ImageUrl :url
        });
        var imageServiceUrl = WcfUrl + 'Services/FaceBookImageService.svc/Save';

        var defered = $.Deferred();
        $.ajax({
            url: imageServiceUrl,
            type: 'POST',
            datatype: 'json',
            crossDomain: true,
            data: facebookJson,
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (data) {

                defered.resolve(data);
            },
            error: function (status) {

                defered.reject();
                DRError({
                    Type: "Error",
                    Message: "Failed to save facebook image"
                });
                               
            }
     
                
        });
        return defered.promise();
    }

    function showLoader() {
        var $loaderDiv = $('.user_profile_image_loader');
        if ($loaderDiv.length > 0) {
            $loaderDiv.show();
        }
    }

    function hideLoader() {
        var $loaderDiv = $('.user_profile_image_loader');
        if ($loaderDiv.length > 0) {
            $loaderDiv.hide();
        }
    }

    
    return {
        ShowDefaultImage: showUserPreviewImage,
        SaveImage: saveAPIImage
    };
})();
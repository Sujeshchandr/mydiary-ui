var FaceBookAPI = (function()
{
    $(function() {
        BindEvents();
    });
    
    function BindEvents() {
        
        window.fbAsyncInit = function () {

            FB.init({
                appId: '1485666671663953',
                autoLogAppEvents: true,
                xfbml: true,
                cookie: true,  // enable cookies to allow the server to access
                version: 'v2.10'
            });
            ////FB.AppEvents.logPageView();

            // Now that we've initialized the JavaScript SDK, we call 
            // FB.getLoginStatus().  This function gets the state of the
            // person visiting this page and can return one of three states to
            // the callback you provide.  They can be:
            //
            // 1. Logged into your app ('connected')
            // 2. Logged into Facebook, but not your app ('not_authorized')
            // 3. Not logged into Facebook and can't tell if they are logged into
            //    your app or not.
            //
            // These three cases are handled in the callback function.

            //FB.getLoginStatus(function (response) {
            //    statusChangeCallback(response);
            //});

        };
        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) return;
            js = d.createElement(s); js.id = id;
            js.src = "//connect.facebook.net/en_US/sdk.js";
            fjs.parentNode.insertBefore(js, fjs);
        }(document, 'script', 'facebook-jssdk'));
    }
    // This is called with the results from from FB.getLoginStatus().
    function statusChangeCallback(response) {
       
        // The response object is returned with a status field that lets the
        // app know the current login status of the person.
        // Full docs on the response object can grbe found in the documentation
        // for FB.getLoginStatus().
        if (response.status === 'connected') {
            // Logged into your app and Facebook.
            GetLoginUserAPI();
        } else if (response.status === 'not_authorized') {
            // The person is logged into Facebook, but not your app.
            if (document.getElementById('status') != (typeof undefined && null))
            document.getElementById('status').innerHTML = 'Please log ' +
              'into this app.';
        } else {
            // The person is not logged into Facebook, so we're not sure if
            // they are logged into this app or not.
            if (document.getElementById('status') != (typeof undefined && null))
            document.getElementById('status').innerHTML = 'Please log ' +
              'into Facebook.';
        }
    }

    // This function is called when someone finishes with the Login
    // Button.  See the onlogin handler attached to it in the sample
    // code below.
    function checkLoginState() {      
        FB.getLoginStatus(function (response) {
            statusChangeCallback(response);
        });
    }
    function GetLoginUserAPI() {
        $(document).trigger('ui.lock');
        $(document).trigger('ui.startloader');
        FB.api('/me', { fields: 'email,last_name,first_name,middle_name,picture' }, function (response) {
           
            var loginResult = DRUser.LoginByOpenAPI(response.email, response.id, DREnum.SocialNWSites.Facebook);
            if(loginResult != null) {
                switch (loginResult.Status) {
                    case "Not Registered":
                        $(document).trigger('ui.lock');
                        $(document).trigger('ui.startloader');
                        var facebookUser = {
                            openUserId: response.id,
                            FirstName: response.first_name,
                            LastName: response.last_name,
                            EmailId: response.email,
                            FullName: response.name,
                            SiteId :DREnum.SocialNWSites.Facebook
                        };
                        DRUser.RegisterByOpenAPI(facebookUser);
                        break;
                    
                default:
                }
            }
        });
    }
    
    function GetLastNameAPI() {
        
        FB.api('/me', { fields: 'last_name' }, function (response) {
            console.log(response);
            document.getElementById('status').innerHTML =
            'Thanks for logging in, ' + response + '!';
        });
    }

    function GetUserPictureAPI() {
        //'https://graph.facebook.com/' + facebookUser.openUserId + '/picture?redirect=1&height=200&type=normal&width=200');
        // type : [square, small, normal, or large]
        FB.api('/me/picture',
        {
            "redirect": true,
            "height": "200",
            "type": "normal",
            "width": "200"
        }, function (response) {
            if ((response != null || typeof response != undefined) && (response.data != null || typeof response.data != undefined))
                DRUserImage.SaveImage(response.data.url, DREnum.SocialNWSites.Facebook);
        });
    }

    

    function getProfilePicture(faceBookUser) {
        var facebookUser = {
            openUserId :faceBookUser.openUserId
        };

        var picureUrl = 'https://graph.facebook.com/' + facebookUser.openUserId + '/picture?redirect=1&height=200&type=normal&width=200';
        var defered = $.Deferred();
        $.ajax({
            url: picureUrl,
            type: 'POST',
            datatype: 'json',          
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (data) {

                defered.resolve(data);
            },
            error: function (status) {

                defered.reject();
                DRError({
                    Type: "Error",
                    Message: "Failed to get facebook profile picture"
                });
            }
        });
        return defered.promise();
    }
   

    return {
        CheckLoginState: checkLoginState,
        GetPicture: GetUserPictureAPI
    };
})();

 

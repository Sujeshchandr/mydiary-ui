//module to handle user activities
var DRUser = (function () {

    $(function () {
        bindEvents();
         if(getUser().UserId == 0)
          logIn('div.menuloginData'); //To Do == > Remove this code after testing in development as this is a test login with dummy values
    });

    var u = {};

    function bindEvents() {

        $(document).on('click', 'div.register div.registerData input[type = "button"].registerbutton', function () {
            register();
        });

        $(document).on('click', 'div.login div.loginData input[type = "button"].loginbutton', function () {
            logIn('div.loginData');
        });

        $(document).on('click', 'div.menuLogin div.menuloginData input[type = "button"].loginbutton', function (e) {
            logIn('div.menuloginData');
        });

        $(document).on('keypress', 'div.menuLogin div.menuloginData input[type = "text"].emailid , div.menuLogin div.menuloginData input[type = "password"].password', function (e) { // trigger login  on pressing enter  key  
            var keycode = (event.keyCode ? event.keyCode : event.which);
            if (keycode == '13') {
                logIn('div.menuloginData');
                
            }

        });

        $(document).on("click", "div.drmenus a.logout", function () {
            logOut();
        });
    }

    
    function register() {

        var $registerDataDiv = $('div.registerData');
        $registerDataDiv.find('.errormessage').css('visibility', 'hidden');
    
        validateRegisterDetails().done(function (isValid) {
            if (isValid) {
                var password = $registerDataDiv.find('.password').val();
                var rePassword = $registerDataDiv.find('.repassword').val();

                if (password != rePassword) {
                    $registerDataDiv.find('.errormessage').html('<b>passwords not matching</b>').css('visibility', 'visible');
                    return; //if password not matches return
                }

                var userGuid = $('.file_name').data("userfolder");
                var imageName = $('.file_name').data("imagename");
                var $RegisterDataDiv = $('div.registerData');
                var firstName = $RegisterDataDiv.find('.firstname').val();
                var middleName = $RegisterDataDiv.find('.middlename').val();
                var lastName = $RegisterDataDiv.find('.lastname').val();
                var emailId = $RegisterDataDiv.find('.emailid').val();
                var password = $RegisterDataDiv.find('.password').val();
                var rePassword = $RegisterDataDiv.find('.repassword').val();
                var siteId = $RegisterDataDiv.data('siteId');
                var siteUserId = $RegisterDataDiv.data('openId');
                siteId = (siteId == undefined) ? 0 : siteId;
                siteUserId = (siteUserId == undefined) ? 0 : siteUserId;    
                var uploadedImageId =  $('.file_name').data("uploaded-imageId");

                var registerData = {

                    EmailId: emailId,
                    FirstName: firstName,
                    MiddleName: middleName,
                    LastName: lastName,
                    SiteId: siteId,
                    SiteUserId: siteUserId,
                    Password: password,
                    UserRoles: [{
                        RoleId: 1,
                        RoleName: 'Member'
                    }],
                    UserImages: [{

                        Name: imageName == undefined ? "user-default-men-160.png" : imageName,
                        UserGuid: userGuid == undefined ? "00000000-0000-0000-0000-000000000000" : userGuid,
                        UploadedImageId :uploadedImageId == undefined ? DREnum.UploadImages.Default: uploadedImageId //To Do ==> has to change the default imageid
                    }]
                };

              
                registerUser(registerData).done(function (data) {
                    if (data != null) {
                       
                        if (data) { //registration success
                            console.log(data);
                            var successData = {
                                IsRegistered: data,
                                EmailId: emailId,
                                OpenId: siteUserId,
                                SiteId: siteId,
                                IsOpenRegistration: (siteUserId != 0 && siteId != 0)
                            };
                            DRUserImage.ShowDefaultImage(''); //not working
                            if (successData.IsOpenRegistration) {
                                loginByOpenApi(successData.EmailId, successData.OpenId, successData.SiteId);
                            } else { 
                             //clear the datas entered
                                new DRAlert("Your registration was successfull.");
                            }
                        }
                        else //registration failed
                        {
                            DRError({
                                Type: "Error",
                                Message: "Your registration failed.please try again"
                            });
                        }
                    }

                });
            }
        });        
    }

    function logIn(loginDataControl) {
        
        var $loginDataDiv = $(loginDataControl);
        $('span.login-errormessage').css('visibility', 'hidden');

        $loginDataDiv.find('.emailid').val('sujeshchandr@gmail.com');//TO DO ==> Only for testing in development 
        $loginDataDiv.find('.password').val('sujesh');// Remove this code after testing in development

        validateLoginDetails(loginDataControl).done(function (isValidate) {

            if (isValidate) {
               
                var emailId = $loginDataDiv.find('.emailid').val();
                var password = $loginDataDiv.find('.password').val();

                var loginData = {

                    EmailId: emailId,
                    Password: password
                };
               
                logInUser(loginData).done(function (data) {
                    if (parseInt(data) == 0) {
                        $('span.login-errormessage').css('visibility', 'visible');
                    } else {
                        // new DRAlert("Your login was successfull.");
                        redirectToLandingPage();
                    }
                });

            }
        });

    }

    function logOut() {

        logOutUser().done(function (data) {
            if (data) {
                redirectToLogin();
            } else {
                //ToDO==>Show message that log out failed
            }
        });
    }

    function getUser() {
       
        if (u.UserId === undefined || u.UserId <= 0 )
        {
            getLoginUser().done(function (data) {
                u.UserId = data.UserId;
                u.LoginId = data.LoginId;
                u.EmailId = data.EmailId;
                u.FirstName = data.FirstName;
                u.MiddleName = data.MiddleName;
                u.LastName = data.LastName;
                u.ImageId = data.ImageId;
            });
        }
        return u;

    }
    
    function getLoginUser() {

        var defered = $.Deferred();       

        $.ajax({
            url: '/People/GetLoginUser',
            type: 'GET',   
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (data) {

                defered.resolve(data);
            },
            error: function (status) {

                defered.reject();
                DRError({
                    Type: "Error",
                    Message: "Failed to get login user"
                });
            }
        });
        return defered.promise();
    }

    function loginByOpenApi(emailId,openId,siteid) {
        var result = '';
        switch (siteid) {
            case 1:
                loginByFaceBook(emailId, openId,siteid).done(function (data) {
                    
                    if (parseInt(data) == 0) {
                         result = {
                            Status: "Not Registered"
                        };
                    }
                    else {
                        // new DRAlert("Your login was successfull.");
                        redirectToLandingPage();
                    }
                });
                break;
            
        default:
        }
        return result;

    }

    function registerByOpenApi(user) {
        
        var result = '';
        switch (user.SiteId) {
            case 1:
                registerByFacebook(user);
            default:
        }
        return result;
    }
    
    function registerByFacebook(facebookUser) {
        
        var $RegisterDataDiv = $('div.registerData');
        var $FirstNameDiv = $RegisterDataDiv.find('input.firstname');
        var $LastNameDiv = $RegisterDataDiv.find('input.lastname');
        var $EmailDiv = $RegisterDataDiv.find('input.emailid');
        
        if (facebookUser != null) {
            $FirstNameDiv.val(facebookUser.FirstName);
            $LastNameDiv.val(facebookUser.LastName);
            $EmailDiv.val(facebookUser.EmailId);
        }

        $RegisterDataDiv.data('openId', facebookUser.openUserId);
        $RegisterDataDiv.data('siteId', facebookUser.SiteId);
       // DRUserImage.ShowDefaultImage('https://graph.facebook.com/' + facebookUser.openUserId + '/picture?redirect=1&height=200&type=normal&width=200');// type : [square, small, normal, or large]
        FaceBookAPI.GetPicture(facebookUser);
       //ToDo ==> manually upload image to db
    }
    
   
    
    function validateRegisterDetails() {

        var defered = $.Deferred();
        var result = ($('div.register div.registerData').find('.DRvalidate').DRValidate().valid);
        if (result) {
            defered.resolve(result);
        }
        else {
            defered.reject();
        }
        return defered.promise();

    }

    function registerUser(registerData) {

        var defered = $.Deferred();        
        
        var userViewModel = JSON.stringify({

            EmailId: registerData.EmailId,
            FirstName: registerData.FirstName,
            MiddleName: registerData.MiddleName,
            LastName: registerData.LastName,
            SiteId: registerData.SiteId,
            SiteUserId: registerData.SiteUserId,
            Password: registerData.Password,
            UserRoles: [{
                RoleId: registerData.UserRoles[0].RoleId,
                RoleName:registerData.UserRoles[0].RoleName
            }],
            UserImages: [{
                
                Name: registerData.UserImages[0].Name,
                UserGuid: registerData.UserImages[0].UserGuid,
                ImageId: registerData.UserImages[0].UploadedImageId
            }]
        });

        $.ajax({
            url: 'People/AddUser',
            type: 'POST',
            datatype: 'json',
            data: userViewModel,
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (data) {               
                defered.resolve(data);
            },
            error: function (status) {

                defered.reject();
                DRError({
                    Type: "Error",
                    Message: "Failed to add user"
                });
            }
        });
        return defered.promise();
    }

    function validateLoginDetails(loginDataControl) {

        var defered = $.Deferred();
        var result = $(loginDataControl).find('.DRvalidate').DRValidate().valid;
        if (result) {
            defered.resolve(result);
        }
        else {
            defered.reject();
        }
        return defered.promise();
    }
   
    function logInUser(loginData) {

        var loginViewModel = JSON.stringify({

            EmailId: loginData.EmailId,
            Password: loginData.Password
        });
        var defered = $.Deferred();
        $.ajax({
            url: '/People/LogIn',
            type: 'POST',
            datatype: 'json',
            data: loginViewModel,
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (data) {

                defered.resolve(data);
            },
            error: function (status) {

                defered.reject();
                DRError({
                    Type: "Error",
                    Message: "login failed"
                });
            }
        });
        return defered.promise();
    }

    function logOutUser() {

        var defered = $.Deferred();

        var loginUser = getUser();
        var loginViewModel = JSON.stringify({

            //EmailId: loginUser.EmailId,
            //UserId: loginUser.UserId,
            LoginId: loginUser.LoginId
        });

        $.ajax({
            url: 'People/LogOut',
            type: 'POST',
            datatype: 'json',
            data: loginViewModel,
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (data) {

                defered.resolve(data);
            },
            error: function (status) {

                defered.reject();
                DRError({
                    Type: "Error",
                    Message: "logout failed"
                });
            }
        });
        return defered.promise();
    }

    function redirectToLogin() {
        window.location.replace("/People");
    }
    
    function redirectToLandingPage() {

         window.location.replace("/Home");       
        //window.location.replace ("/Expense");     
    }

    function loginByFaceBook(emailId, openId,siteId) {
        
        var defered = $.Deferred();
        var openLoginViewModel = JSON.stringify({

            OpenUserId: openId,
            SiteId: siteId
        });
        $.ajax({
            url: '/People/LogInByFacebook',
            type: 'POST',
            datatype: 'json',
            data: openLoginViewModel,
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (data) {

                defered.resolve(data);
            },
            error: function (status) {

                defered.reject();
                DRError({
                    Type: "Error",
                    Message: "login by facebook failed"
                });
            }
        });
        return defered.promise();
    }
  
    
    return {
        GetLoginUser: getUser,
        LoginByOpenAPI: loginByOpenApi,
        RegisterByOpenAPI :registerByOpenApi
    };

})();               
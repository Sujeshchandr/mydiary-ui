var DRLayout = (function () {

    $(function () {
        bindEvents();
        
    });

    var WcfUrl = "http://localhost:3049/";

    function bindEvents() {
       setMenuByLogin();
    }

    function setMenuByLogin() {

        var $drMenu =$('.drmenus');
        var loginUser = DRUser.GetLoginUser();
        var userId = loginUser.UserId;
        var firstName = loginUser.FirstName;
        if (userId == 0) {
           
            $drMenu.append(getRegisterLoginDiv());

        } else {
            if (firstName != null && firstName != '') {
              
                var userProfileImageUrl = WcfUrl + "Services/ImageWcfService.svc/Get/" + loginUser.ImageId;
                var $profileMenu = $('div.profilemenu');
                var $userImage = $profileMenu.html('<span> <img width="25" height="20" alt="user image" class="user-profile-image" src="../Images/Diary/user-default-men-160.png"></span>  ' + 'Welcome ' + firstName).addClass('userprofile');
                $('.user-profile-image').attr('src', userProfileImageUrl);
            }
            else {
                $('div.profilemenu').removeClass('userprofile').html('');
            }
            $drMenu.append(getUserNavigationDiv());
        }
    }
  
    function getRegisterLoginDiv() {

                var div  =  '<div class="float-left dMenu">' +
                            '<nav>' +
                            '<ul id="menu">' +
                            //'<li><a href="/People">Register</a></li>' +
                            //'<li><a href="/People">Login</a></li>' +
                            '</ul></nav>' +
                            '</div>';
        
         var loginMenuDiv = '<div class="float-right menuLogin">' +
                            '<div class="menuloginData">' +
                            '<text> EmailId </text>' +
                            '<input class="emailid DRvalidate" type="text" />' +
                            '<text> Password </text>' +
                            '<input class="password DRvalidate" type="password" />' +
                            '<text> </text>' +
                            '<input type="button" class="loginbutton" value="LOGIN">' +
                            '<span><fb:login-button scope="public_profile,email" onlogin="FaceBookAPI.CheckLoginState();"></fb:login-button></span>' +
                            //'<div id="status"></div>' +
                            //'</span>' +
                            '</div>' +
                            '<span class="login-errormessage" style="visibility:hidden">Please enter a valid email or password</span>' +
                            '</div>';
        
        return $(div + loginMenuDiv);
    }

    function getUserNavigationDiv()
    {
        var div ='<div class="float-right dMenu">'+                    
                    '<nav>'+
                        '<ul id="menu">'+
                            '<li><a href="/Home">Home</a></li>'+
                            '<li><a href="/Income">Incomes</a></li>'+
                            '<li><a href="/Expense">Expenses</a></li>' +
                             '<li><a href="/Test">Tests</a></li>' +
                            '<li><a href="#" class="logout" >Logout</a></li>' +
                        '</ul>'+
                    '</nav>'+
                '</div>';
        return $(div);
    }
    
    function setMinHeightForMainSection(control,isExpand)
    {
        var $Control = $(control);
        var l = parseInt($Control.css('height'));
        var s = parseInt($('div.mainsection').css('min-height'));
        var m = isExpand ? (s+l) : (s-l);
        $('div.mainsection').css('height', (m + 'px'));
    }

    function getProfileInfoDiv() {
        if (data.FirstName != null && data.FirstName != '') {
            $('div.userprofile').addClass('userprofile').html('Welcome' + data.FirstName);
        }
        else {
            $('div.userprofile').removeClass('userprofile').html('');
        }
    }

    return {
        AdjustMainSection: setMinHeightForMainSection
    };
})();
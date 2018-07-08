
//- See more at: http://istockphp.com/jquery/creating-popup-div-with-jquery/#sthash.1m32vBsB.dpuf

//JS Module to showPopup
var DRPopup = (function () {

    var popupStatus = 0; // set value
    var clickOnce = 0;
    $(function () {
        BindEvents();
    });

    function BindEvents() {

        $(document).on("popup.lock", function () { LockPopup(); });
        $(document).on("popup.unlock", function () { UnLockPopup(); });
        $(document).on("popup.close", function () { ClosePopup(); });      
        $(document).on("click", "#popup_data_button_cancel ,.close", function () { ClosePopup(); });  // function close pop up       
        $(document).on("click", "#popup_data_button_save", function () { $(document).trigger("popup.save.action");});       
        $(document).on("keyup", function (event) { ClosePopUpByESCKey(event) });        
        
    }

    //Public Functions
    function showPopup(data) {


        if (typeof data == 'object') {            
            $(document).trigger("ui.startloader");
            loadPopup(data)
        }
    }      

    function ClosePopup() {

        if (popupStatus == 1) { // if value is 1, close popup

            $("#popup_container").fadeOut("slow", function () {
                $('#backgroundPopup').fadeOut("slow", function () {
                    $('#backgroundPopup').remove();
                    $('#popup_container').remove();
                    // $('#outerloader').remove();
                    $(document).trigger("ui.stoploader");
                    popupStatus = 0;  // and set value to 0
                });//hide popup

            });

        }
    }

    function ClosePopUpByESCKey(event) {
        if (event.which == 27) { // 27 is 'Ecs' in the keyboard
            ClosePopup();
        }
    }

    function LockPopup() {

        $('#backgroundPopup').css('z-index', '99999');
    }

    function UnLockPopup() {
        $('#backgroundPopup').css('z-index', '0');
    }
   
    //Private Functions  
    function loadPopup(data) {

        if (popupStatus == 0) { // if value is 0, show popup

            InitializePopups(data);            
            $(document).trigger("ui.stoploader");
            $('#popup_container').show();
            $('#popup_container').fadeIn('slow', function () {

                $("#backgroundPopup").css("opacity", "0.8"); // css opacity, supports IE7, IE8
                $("#backgroundPopup").fadeIn('slow', function () {
                    popupStatus = 1; // and set value to 1
                });

            });
        }
    }

    function InitializePopups(data) {

        var msgDiv = GetPopupUI(data);
        $('.dMenu').append(msgDiv);
        $('#popup_container').hide().addClass('defaultPopup');

    }

    function GetPopupUI(data) {

        //data = GetDummyPopupData();

        var popupBackgroundDiv = '<div id="backgroundPopup">' +
                                 '</div>';

        var popupContainerDiv =  '<div id="popup_container">' +
                                    '<div id="popup_data">' +
                                         '<div id="popup_data_title">' +
                                            '<div class="close">X</div>' +
						                    '<div class="Title">' + data.Title + '</div>' +
                                         '</div>' +
                                         '<div id="popup_data_content" >' + data.Content + '</div>' +
                                             '<div id="popup_data_buttons" >'+
                                                    '<div id="popup_data_button_save">' +  data.ButtonEntity[0].Save + '</div>' +
                                                    '<div id="popup_data_button_cancel">' +  data.ButtonEntity[1].Cancel + '</div>' +
                                            '</div>' +
                                    '</div>' +
					             '</div>';

        var popupLoaderDiv   =   '<div class="outerloader">' +
				                   '<div class="loader"></div>' +
			                     '</div>';

        return  popupBackgroundDiv + popupContainerDiv + popupLoaderDiv;

    }
  

    //Test Functions
    function GetDummyPopupData() {

        var data = {

            Title: "Popup Title",
            Content:'<div class="editor-label">' +
                    '<label for="ExpenseTypeId">ExpenseTypeId</label>' +
                    '</div>' +
                    '<div class="editor-field">' +
                    '<input class="text-box single-line" data-val="true" data-val-number="The field ExpenseTypeId must be a number." data-val-required="The ExpenseTypeId field is required." id="ExpenseTypeId" name="ExpenseTypeId" type="number" value="" />' +
                    '<span class="field-validation-valid" data-valmsg-for="ExpenseTypeId" data-valmsg-replace="true"></span>' +
                    '</div>' +
                    '<div class="editor-label">' +
                    '<label for="ExpenseType">ExpenseType</label>' +
                    '</div>' +
                    '<div class="editor-field">' +
                    '<input class="text-box single-line" id="ExpenseType" name="ExpenseType" type="text" value="" />' +
                    '<span class="field-validation-valid" data-valmsg-for="ExpenseType" data-valmsg-replace="true"></span>' +
                    '</div>',
            ButtonEntity: 

               [ {
                   Save: '<button >Save</button>',
                   Action: 'popup.save'
                   
               },
               {
                  
                   Cancel: '<button>Cancel</button>'
               }
               ],
            Type:"ExpenseType"
            
        };

        return data;
    }

    

    return {
        show: function (data) {
            showPopup(data);
        }
    }


})();
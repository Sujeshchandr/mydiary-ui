function DRAlert(data) {

    $(function () {
        Bindevents();
    });

    function Bindevents() {
        //onclick event for add new expensetype.
        $(document).on('click', ".buttonclose", function (e) {
            HideMessage(e);
        });
        setTimeout(AutoClose,10000); //10 seconds
    }

   
    if (typeof data == "string") {

        var data={
            Type: "Success",
            Message:data
        };

    }
    else if (typeof data == "object") {
        data.Type = data.Type;
        data.Message = data.Message;

    }

    ShowMessage(data);//function to show all notification messages

    //bind events    
    function AutoClose()
    {
        $('.dMenu').find('.message').animate({ top: "-20" }, 350, "", function () {
            $(this).remove();
        });
        
    }

    function GetMessageDiv(message) {
        var msgDiv = '<div class="message"><span class="buttonclose" >X</span><p>' + message + '</p></div>';
      
        return msgDiv;
    }

    function HideMessage(e) {
      
        var CloseButton = e.currentTarget;
        var $thisDiv = $(CloseButton).parent('.message');
        $thisDiv.animate({ top: "-20" }, 350, "", function () {
            $thisDiv.remove();
        });
    }

    function ShowMessageNotification(msg, classname) {

        var top = 87;//default
        if ($('.dMenu').find('.message').length > 0) //if message already exist
        {
            var lastDiv = $('.dMenu').find('.message').last();            
            top = ($(lastDiv).offset().top + 20); //show new message below old message           
        }
        var className = classname;
        var msgDiv = GetMessageDiv(msg);
        msgDiv = $(msgDiv).addClass(className);
        $('.dMenu').append(msgDiv);
        $('.dMenu').find(msgDiv).animate({ top: top }, 300);        

    }

    function ShowMessage(msgData) {

        if (msgData.Type == 'undefined' && msgData.Message == 'undefined') return;
        $(document).trigger("ui.unlock"); // if lock is triggered from any area, after showing the message, unlock shuld trigger in all cases
        $(document).trigger("ui.stoploader"); //if startloader is triggered from any area, after showing the message, stoploader should trigger in all cases
        if (msgData.Type == "Error") {
            ShowMessageNotification(msgData.Message, 'error');
           
        }
        else if (msgData.Type == "Info") {
            ShowMessageNotification(msgData.Message, 'info');

        }
        else if (msgData.Type == "Warning") {
            ShowMessageNotification(msgData.Message, 'warning');
        }
        else if (msgData.Type == "Success") {
            ShowMessageNotification(msgData.Message, 'success');
        }
        else {

        }
        
    }

    
  
};
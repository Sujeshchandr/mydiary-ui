var DRLog = (function () {


    function LogError(data) {
        console.log('Message: ' + data.Message + 'Type: ' + data.Type);
        DRAlert(data);
    }

    return {
        error: function (data) {
            LogError(data);
        }
    };

})();
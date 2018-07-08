var Options = {};


$.extend({

    ajaxCrossDomain: function (options) {

        Options = options;

        var iFrame = null;
        var form = createForm(Options.url, iFrame, Options);

        if (!iFrame) {
            iFrame = createIframe();
        }

        form.setAttribute('target', iFrame.name);
        form.style.display = 'none';
        document.body.appendChild(form);

        var jsonObj = $.parseJSON(Options.data);

        //var params = Object.keys(jsonObj).map(function (key,value) { 

        //    return jsonObj[key];

        //});
        //var params = [];
        //if (params.length > 0)
            addFormParams(form, jsonObj);

        //var params1 = arr.map(function (key) {

        //    return arr[key];

        //});
        //if (arr && arr.length > 0) {

           
        //    var params = [];//FILL FORM PARMATERS { params['name'] = value }

        //    for (var i = 0; i < arr.length ; i++) {
        //        params[i] = arr[i];
        //    }

        //    if (params.length > 0)
        //        addFormParams(form, params);
        //}
        
      
        addEvent(iFrame, 'load', onIFrameLoaded);//bind load event of iframe
        
        form.submit();

        removeElement(form);
    }

});


function addEvent(element, type, handler) {
    if (element.addEventListener) {
        element.addEventListener(type, handler, false);
    } else if (element.attachEvent) {
        element.attachEvent("on" + type, handler);
    } else {
        element["on" + type] = handler;
    }
}

function onIFrameLoaded(e) {

    // when we remove iframe from dom
    // the request stops, but in IE load
    //// event fires
    //if (!iframe || !iframe.parentNode) {
    //    return;
    //}
   
    // iframe.contentWindow.document - for IE<7
    try {

       // var doc = iframe.contentDocument || iframe.contentWindow.document;
       // var innerHtml = doc.body.innerHTML;
      //  var jsonResponse = /(\{.*\})/.exec(innerHtml);//Coonverting to JSON
        var responeHtml = $(document.getElementById('xxxxxxxxxxxxxxxxxxxxxx_iframe_id').contentDocument.body).children();
        if (responeHtml.length == 0) return;
        var jsonResponse = JSON.parse(responeHtml.html());

        if (jsonResponse == undefined) {
            Options.error(); //error
        }
        else {
            Options.success(jsonResponse); //success
        }
    }
    catch (error) {

        //IE may throw an "access is denied" error when attempting to access contentDocument
        Options.error(); //error
    }
   
};

function createForm(target, iFrame, options) {

    var form = document.createElement('form');
    form.encoding = "multipart/form-data";
    form.method = "POST";
    form.setAttribute('action', target);
   
    return form;
}

function createIframe() {

    this.uniqueIdentifier = 'xxxxxxxxxxxxxxxxxxxxxx';
    var iFrame = (/MSIE (6|7|8)/).test(navigator.userAgent) ?
    document.createElement('<iframe name="' + this.uniqueIdentifier + '_iframe' + '">') :
    document.createElement('iframe');

    iFrame.setAttribute('id', this.uniqueIdentifier + '_iframe_id');
    iFrame.setAttribute('name', this.uniqueIdentifier + '_iframe');
    iFrame.style.display = 'none';
    document.body.appendChild(iFrame);
    return iFrame;
}

function addFormParams(form, params) {

    var input;
    $.each(params, function (key,value) {
        if (value && value.nodeType === 1) {
            input = value;
        } else {
            input = document.createElement('input');
            input.setAttribute('value', value);
        }
        input.setAttribute('name', key);
        form.appendChild(input);
    });
}

function removeElement(element) {
    element.parentNode.removeChild(element);
}

//sample ajax request 
function sendAjaxRequestAsFormData() {

    var defered = $.Deferred();

    var expenseType = new $scope.ExpenseTypeViewModel(0, 'New Type');

    var url = '/Expense/AddExpenseType';

    $.ajaxCrossDomain({
        url: url,
        dataType: 'JSON',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(expenseType),
        processData: false,
        success: function (data, textStatus, jQxhr) {
            defered.resolve(data);
            //$('#response pre').html(JSON.stringify(data));
        },
        error: function (jqXhr, textStatus, errorThrown) {
            alert("error occured");
            defered.reject();
        }

    });

    return defered.promise();
};
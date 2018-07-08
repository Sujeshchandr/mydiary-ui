//module to handle expenses
var DRExpenses = (function () {

    $(function () {
        bindEvents();
        init();
    });

   // var currentLoginUser = {};
    //bind events
    function bindEvents() {        
       
        $(document).on("click", '#AddExpenseType', DRExpenseTypes.Show);     
        $(document).on("popup.save.action", function (event) { saveAction(event);});   
        $(document).on("click", ".add_expense_div input:button", function (event) { addExpense(event); });        
        $(document).on("expenses.bind", function () { bindExpenses(); });       
        $(document).on("change", ".FilteredDate", function () {
            bindFilterdExpenses($(this).val(), $('.FilteredType').val());
        });
        $(document).on("change", ".FilteredType", function () {
            bindFilterdExpenses($('.FilteredDate').val(), $(this).val());
        });  
        $(document).on('resize orientationchange', function (e) {
            if ($.datepicker._datepickerShowing) {
                var datepicker = $.datepicker._curInst;
                dpInput = datepicker.input;
                dpElem = datepicker.dpDiv;
                dpElem.position({
                    my: 'left top',
                    of: dpInput,
                    at: 'left bottom'
                });
            }
        });
        
    }

    //public functions
    function init() {
        //currentLoginUser = DRUser.GetLoginUser(); //Intialize User Object before any action 
        DRExpenseTypes.Bind(true);
        bindToolBox(); 
        bindExpenses();
        
    }

    function addExpense(event) {
       
        validateExpense().done(function () {

           insertExpense(event).done(function (data) {

             if (data == true) {
                 
                 DRActivityArea.Hide(true);              
                 $(document).trigger("ui.lock");
                 bindExpenses();                                 
                 new DRAlert("successfully added expense");                 
             }
             else
                 new DRAlert({  Message: "failed to add expense", Type: "Error" });
           })

        });

    }

    function getFilteredExpenses(date,type) {

        var defered = $.Deferred(); //Deferreds don't make AJAX synchronous, instead they make it easier to work with callbacks and asynchronous methods.

        $.ajax({
            url: 'Expense/GetFilteredExpenses?date=' + date + '&type=' + type + '&userId=' + DRUser.GetLoginUser().UserId,
            type: 'GET',
            datatype: 'json',
            async: false, // make the call synchronous
            success: function (data) {
                // Notify listeners that the AJAX call completed successfully.
                defered.resolve(data);
            },
            error: function (status) {
                //notify listeners that Something went wrong - 
                defered.reject();
                DRError({
                    Type: "Error",
                    Message: "Failed to get expensesByDate"
                });
            }
        });

        return defered.promise();
    }

    function validateExpense() {        
        var defered = $.Deferred();
        var result = $('.add_expense_div #expensediv_data').find('.DRvalidate').DRValidate().valid;
        if (result) {
            defered.resolve(result);
        }
        else {
            defered.reject();
        }
        return defered.promise(); 
       
    }

    function insertExpense() {

        var defered = $.Deferred();
        var $ExpenseDataDiv = $('.add_expense_div #expensediv_data');
      
            var expenseViewModel = JSON.stringify({

                Type: { ExpenseTypeId: $ExpenseDataDiv.find('#ddlExpenseType').val() },
                Date: $ExpenseDataDiv.find('#expensediv_data_date').val(),
                Amount: $ExpenseDataDiv.find('#expensediv_data_amount').val(),
                Description: $ExpenseDataDiv.find('#expensediv_data_description').val(),
                Comments: $ExpenseDataDiv.find('#expensediv_data_comments').val(),
                UserId: DRUser.GetLoginUser().UserId

            });

            $.ajax({
                url: 'Expense/AddExpense',
                type: 'POST',
                datatype: 'json',
                data: expenseViewModel,
                contentType: 'application/json; charset=utf-8',
                async: false,
                success: function (data) {

                    defered.resolve(data);
                },
                error: function (status) {

                    defered.reject();
                    DRError({
                        Type: "Error",
                        Message: "Failed to add Expense"
                    });
                }
            });
        

        return defered.promise();
    }

    //sub module to handle expenseTypes
    var DRExpenseTypes = (function () {


        function showExpenseType() {

            getExpenseTypeTemplate().done(function (data) {

                var popupData = {
                    Title: "Add new expense type",
                    Content: data,
                    ButtonEntity:
                                  [
                                      {
                                          Save: '<button  data-Type="ExpenseType" >Save</button>',
                                          Action: 'popup.save'
                                      },
                                     {
                                         Cancel: '<button>Cancel</button>'
                                     }
                                  ],
                    Type:"ExpenseType"

                };
                DRPopup.show(popupData);
            });                
            
        }        
        function getExpenseTypeTemplate() {

            var defered = $.Deferred(); //Deferreds don't make AJAX synchronous, instead they make it easier to work with callbacks and asynchronous methods.

           $.ajax({
               url: 'Expense/GetExpenseTypeTemplate',
                type: 'GET',
                datatype: 'json',
                async: false, // make the call synchronous
                success: function (data) {
                    // Notify listeners that the AJAX call completed successfully.
                    defered.resolve(data);                  
                },
                error: function (status) {
                    //notify listeners that Something went wrong - 
                    defered.reject();                   
                    DRError({
                        Type: "Error",
                        Message: "Failed to get ExpenseTypeTemplate"
                    });
                }
            });

            return defered.promise();
        }

        
        function addExpenseType(data) {

            getExpenseType(data).done(function (data) {

                if(data == true)
                {  
                    DRExpenseTypes.Bind();                    
                    DRAlert("successfully added expense type");   
                }
                else
                {
                    DRAlert("failed to add expense type"); 
                }
            });
        }
        function getExpenseType(data) {

            var defered = $.Deferred(); 

            var postData = JSON.stringify({
                ExpenseType: data.ExpenseType,
                UserId: DRUser.GetLoginUser().UserId
            }); 
            
            $.ajax({
                url: 'Expense/AddExpenseType',
                type: 'POST',
                data: postData,
                datatype: 'json',
                contentType: 'application/json; charset=utf-8',
                async: true, // make the call synchronous
                success: function (data) {                    
                    defered.resolve(data);
                },
                error: function (status) {                   
                    defered.reject();
                    DRError({
                        Type: "Error",
                        Message: "Failed to AddExpenseType"
                    });
                }
            });

            return defered.promise();
        }

        function bindExpenseTypes(isOnLoad)  {

            getAllExpenseTypes().done(function (data) {

                $('#ddlExpenseType').empty(); // Clear all
                var selectOption = $('<option>');
                selectOption.attr('value', -1).text("Select One");
                $('#ddlExpenseType').append(selectOption);
                $.each(data, function (index, value) {
                    var newOption = $('<option>');
                    newOption.attr('value', value.ExpenseTypeId).text(value.ExpenseType);
                    $('#ddlExpenseType').append(newOption);
                })
                if (!isOnLoad) {
                    $('select#ddlExpenseType>option:last').prop('selected', true);//select the newly added one
                }
            }
            );

        }
        function getAllExpenseTypes() {

            var defered = $.Deferred(); //Deferreds don't make AJAX synchronous, instead they make it easier to work with callbacks and asynchronous methods.

            $.ajax({
                url: 'Expense/GetAllExpenseTypes?userId=' + DRUser.GetLoginUser().UserId,
                type: 'GET',
                datatype: 'json',
                async: false, // make the call synchronous
                success: function (data) {
                    // Notify listeners that the AJAX call completed successfully.
                    defered.resolve(data);
                },
                error: function (status) {
                    //notify listeners that Something went wrong - 
                    defered.reject();
                    DRError({
                        Type: "Error",
                        Message: "Failed to get GetExpenseTypePartial"
                    });
                }
            });

            return defered.promise();
        }

        function getDataForExpenseType() {
           
            var data = {
                ExpenseType: $('input[id="ExpenseType"]').val()
            };

            return data;
        }

        //Custom Events Functions
        function saveExpenseType()
        {
           
            validateInputs().done(function(data){
                $(document).trigger("popup.lock");
                $(document).trigger("popup.close");                
                $(document).trigger("ui.startloader");
                DRExpenseTypes.Add(getDataForExpenseType());
            });
        }

        function validateInputs()
        {

            var defered = $.Deferred();
            var inputs = [];
            inputs.push($('input[id="ExpenseType"]'));
            var result = $(inputs).DRValidate().valid;
            if (result) {
                defered.resolve(result);
            }
            else {
                defered.reject();
            }
            return defered.promise();            
                      
        }

        return {
            Show: showExpenseType,
            Add: addExpenseType,           
            Bind: bindExpenseTypes,
            Save: saveExpenseType,
            
           
        };
    })();   

    //custom events functions

    function bindFilterdExpenses(date, type) {

        if (date == "" && type == "") {
            $(document).trigger('expenses.bind');
        }
        else {
            getFilteredExpenses(date, type).done(function(data) {
                bindExpenseinActivityArea(data);                
            });

        }
    }
    function bindExpenses() {

        getExpenses().done(function(data) {
            bindExpenseinActivityArea(data);
        });
    }

    function getExpenses() {

        var defered = $.Deferred();        

        $.ajax({
            url: 'Expense/GetAllExpenses?userId=' + DRUser.GetLoginUser().UserId,
            type: 'GET',
            datatype: 'json',                   
            async: false,
            success: function (data) {

                defered.resolve(data);
            },
            error: function (status) {

                defered.reject();
                DRError({
                    Type: "Error",
                    Message: "Failed to get expenses"
                });
            }
        });


        return defered.promise();
    }
    function saveAction(event) {

       
        var type = $("#popup_data_button_save button").data('type');
        switch (type) {

            case "ExpenseType":
                DRExpenseTypes.Save(event);
                break;

        }

    }

    function bindToolBox() {
        
        DRToolBox.Bind('ExpenseToolBox');
    }    

    //TODo==> Get the template from code
    function bindExpenseinActivityArea(data) {

        $('.expenseDetails_Summary').html('');

        //Title section
        var titleDiv = '<div class="expenseDetails_Summary_Title">' + data.Title + ' ( Total Expenses: ' + data.TotalAmount + ' )' + '</div>';

        //Header section
        var expenseDateDiv = '<div class="expenseDetails_Date">' +
                                '<div class="expenseDetails_Summary_Header"><div >Date</div></div>' +
                                '<div class="expenseDetails_Summary_Data"></div>' +
                              '</div>';
        var expenseTypeDiv = '<div class="expenseDetails_ExpenseType">' +
                                '<div class="expenseDetails_Summary_Header"><div  id="expensetypeTitle">ExpenseType</div></div>' +
                                '<div class="expenseDetails_Summary_Data"></div>' +
                            '</div>';

        var expenseDescriptionDiv = '<div class="expenseDetails_ExpenseDescription"> ' +
                                       '<div class="expenseDetails_Summary_Header"><div id="descriptionTitle">Description</div></div> ' +
                                              ' <div class="expenseDetails_Summary_Data">  </div>' +
                                    '</div>';
        //var expenseCommentsDiv = '<div class="expenseDetails_ExpenseComments"> ' +
        //                               '<div class="expenseDetails_Summary_Header"><div  id="commentsTitle">Comments</div></div> ' +
        //                               '<div class="expenseDetails_Summary_Data"></div>' +
        //                        '</div>';
        var expenseAmountDiv = '<div class="expenseDetails_ExpenseAmount"> ' +
                                       '<div class="expenseDetails_Summary_Header"><div  id="amountTitle">Amount</div></div> ' +
                                       '<div class="expenseDetails_Summary_Data"></div>' +
                                '</div>';

        $('.expenseDetails_Summary').append(titleDiv);
        $('.expenseDetails_Summary').append(expenseDateDiv);
        $('.expenseDetails_Summary').append(expenseTypeDiv);
        $('.expenseDetails_Summary').append(expenseDescriptionDiv);
        //$('.expenseDetails_Summary').append(expenseCommentsDiv);
        $('.expenseDetails_Summary').append(expenseAmountDiv);


        if (data.expenses == null || data.expenses == 'undefined' || data.expenses == '' ) {

            //Title section
             titleDiv = '<div class="expenseDetails_Summary_Title">' + data.Title + ' (Total Amount: ' + data.TotalAmount + ' )' + '</div>';
             //$('.expenseDetails_Summary').append(titleDiv);
        }
        else
        {
            //Summary section
            $.each(data.expenses, function (i, expense) {            
               

                var $expenseDateDiv = $('.expenseDetails_Summary').find('.expenseDetails_Date');
                $expenseDateDiv.find('.expenseDetails_Summary_Data').append('<div>' + expense.Date + '</div');

                var $expenseTypeDiv = $('.expenseDetails_Summary').find('.expenseDetails_ExpenseType');
                $expenseTypeDiv.find('.expenseDetails_Summary_Data').append('<div>' + expense.Type.ExpenseType + '</div');

                var $expenseDescriptionDiv = $('.expenseDetails_Summary').find('.expenseDetails_ExpenseDescription');
                $expenseDescriptionDiv.find('.expenseDetails_Summary_Data').append('<div>' + expense.Description + '</div');

                //var $expenseCommentsDiv = $('.expenseDetails_Summary').find('.expenseDetails_ExpenseComments');
                //var $expenseCommentsDataDiv = $expenseCommentsDiv.find('.expenseDetails_Summary_Data').append('<div>' + data.expenses[i].Comments + '</div');

                var $expenseAmountDiv = $('.expenseDetails_Summary').find('.expenseDetails_ExpenseAmount');
                $expenseAmountDiv.find('.expenseDetails_Summary_Data').append('<div>' + expense.Amount + '</div');

            });
        }
       

                       
    }
    
     //test functions 
    function getDummyActivityArea() {

        var activityDiv = '<div class="diary_activityArea_outerDiv" >' +
                            '<div class="diary_activityArea_outerDiv_close">X</div>' +
                            '</div>';
        return activityDiv;
    }

    function BindActivityArea() {
        var $expenseDiv = $('.add_expense_div');
        $('.add_expense_div').remove();
        $('.diary_activityArea_outerDiv').append($expenseDiv);        
        $('.expenseDetails_Outerdiv').append(getDummyActivityArea());
    }
    

    return {
     
    };

})();
var DRToolBox = (function () {

    $(function () {
        bindEvents();
    });

    function bindEvents() {

        $(document).on("click", ".addexpenses_toolbox a", function () {
            
            var $this = $(this);

            InitExpenseAction().done(function(){

                var $ExpenseDataDiv = $('.add_expense_div #expensediv_data');
                setFocusToChildDiv($this);

                //if already shown then return
                if (DRExpenseController.scope().IsActivityAreaActive($ExpenseDataDiv)) {
                    return;
                }

                DRLayout.AdjustMainSection(".diary_activityArea_outerDiv", true);

                $('.DRValidationError').RemoveDRValidation();

                $(".diary_activityArea_outerDiv").effect('slide', { mode: 'show', direction: 'up', easing: 'swing' }, 800,
                     function () {

                         $(this).effect('bounce', { mode: 'effect', direction: 'up', distance: 30, times: 2, duration: 100, easing: 'swing' }, 500);
                     });
            });
            
        });

        $(document).on("click", ".addincomes_toolbox a", function () {

            var $this = $(this);

            InitIncomeAction().done(function () {

                var $IncomeDataDiv = $('.add_income_div #incomediv_data');
                setFocusToChildDiv($this);

                //if already shown then return
                if (DRIncomeController.scope().IsActivityAreaActive($IncomeDataDiv)) {
                    return;
                }

                DRLayout.AdjustMainSection(".diary_activityArea_outerDiv", true);

                $('.DRValidationError').RemoveDRValidation();

                $(".diary_activityArea_outerDiv").effect('slide', { mode: 'show', direction: 'up', easing: 'swing' }, 800,
                     function () {

                         $(this).effect('bounce', { mode: 'effect', direction: 'up', distance: 30, times: 2, duration: 100, easing: 'swing' }, 500);
                     });
            });

        });

    }

    //Public Functions
    function bindToolBox(type) {

        switch (type) {
            case "ExpenseToolBox":
                getToolBox("Expense").done(function (data) {
                    $('#expenseDetailsFullDiv').append(data);
                });
                break;
            case "IncomeToolBox":
                getToolBox("Income").done(function (data) {
                    $('#incomeDetailsFullDiv').append(data);
                });
        }

    }
    
    function getToolBox(type) {

        var defered = $.Deferred();
        var Jsondata = { };
        $.ajax({
            url: 'ToolBox/Get?type=' + type,
            type: 'GET',          
            datatype: 'html',
            async: false,
            success: function (data) {
                defered.resolve(data);
            },
            error: function (status) {
                defered.reject();
                DRError({
                    Type: "Error",
                    Message: "Failed to load ToolBox"
                });
            }
        });

        return defered.promise();
    }

    function isActivityAreaActive(childDataDiv) {

        var $ActivityAreaDiv = $(".diary_activityArea_outerDiv");
        var $ChildDataDiv = childDataDiv;
        var isActivityAreaChildExist = $ActivityAreaDiv.find($ChildDataDiv).css('visibility') == 'visible' ? true : false;
        var isActivityAreaVisible = $ActivityAreaDiv.css('display') == 'none' ? false : true;
        if (isActivityAreaChildExist && isActivityAreaVisible) {
            return true;;
        }
        return false;
    }

    function setFocusToChildDiv(control) {
         var $this = control;
         var childDivTop = 0;
         var $parentDiv = $this.parent();
       
         switch ($parentDiv.data('type')) {

             case "AddIncomes":
                 childDivTop = $this.offset().top + $('.incomeDetails_innerdiv').height();
                 break;
             case "AddExpenses":
                 childDivTop = $this.offset().top + $('.expenseDetails_innerdiv').height();
                 break;

             default:
         }
        
        $('html, body').animate({ 'scrollTop': childDivTop }, 1000, 'swing');
    }

    function setFocusToToolBox($parentDiv) {

        var $this = $('.diary_toolbox');       
        var offSet = 60;
        var toolBoxTop = $this.offset().top + offSet;
        $('html, body').animate({ 'scrollTop': toolBoxTop }, 100, 'swing');
    }

    //Test Functions
    function getDummyToolBox() {

        var toolBoxDiv = '<div class="diary_toolbox">' +
                         '<div class="addexpenses_toolbox" data-type="AddExpenses" >Add your expenses </div>' +
                         '<div class="addexpenses_toolbox1" ><div class="expenseDetails_filterdiv_date">' +
                         '<span>Select Date </span>' +
                         '<div><input type="text"  class="datepicker FilteredDate"/></div>' +
                         '</div>   </div>' +
                         '<div class="addexpenses_toolbox2">toolbox3</div>' +
                         '<div class="addexpenses_toolbox3">toolbox4 </div>' +
                         '</div>';

        return toolBoxDiv;
    }

    function showExpense(control) {
        var $this = control;
        //if already shown then return                  
        var $ExpenseDataDiv = $('.add_expense_div #expensediv_data');
        setFocusToChildDiv($this);

        if (isActivityAreaActive($ExpenseDataDiv)) return;

        DRLayout.AdjustMainSection(".diary_activityArea_outerDiv", true);

        $('.DRValidationError').RemoveDRValidation();

        $(".diary_activityArea_outerDiv").effect('slide', { mode: 'show', direction: 'up', easing: 'swing' }, 800,
             function () {

                 $(this).effect('bounce', { mode: 'effect', direction: 'up', distance: 30, times: 2, duration: 100, easing: 'swing' }, 500);
             });
    }

    function showIncome(control) {

        var $this = control;
        //if already shown then return                  
        var $IncomeDataDiv = $('.add_income_div #incomediv_data');
        setFocusToChildDiv($this);

        if (isActivityAreaActive($IncomeDataDiv)) return;

        DRLayout.AdjustMainSection(".diary_activityArea_outerDiv", true);

        $('.DRValidationError').RemoveDRValidation();

        $(".diary_activityArea_outerDiv").effect('slide', { mode: 'show', direction: 'up', easing: 'swing' }, 800,
             function () {

                 $(this).effect('bounce', { mode: 'effect', direction: 'up', distance: 30, times: 2, duration: 100, easing: 'swing' }, 500);
             });
    };

    function InitExpenseAction() {

        var defered = $.Deferred();

        if (DRExpenseController.scope().initExpenseAction())
        {
            defered.resolve();
        }
        else
        {
            defered.reject();
        }
        return defered.promise();
    }

    function InitIncomeAction() {

        var defered = $.Deferred();

        if (DRIncomeController.scope().initIncomeAction()) {
            defered.resolve();
        }
        else {
            defered.reject();
        }
        return defered.promise();
    }

    return {
        Bind: bindToolBox,
        Focus: setFocusToToolBox,
        ShowExpense: showExpense,
        ShowIncome:showIncome
    };

})();
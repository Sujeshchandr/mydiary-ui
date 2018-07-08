var DRExpenseController = (function () {

    DRExpenseAngularModule.controller('expenseController', ['$scope', '$document', '$http', '$modalService', '$templateCache', '$httpCors', '$timeout',

    function ($scope, $document, $http, $modalService, $templateCache, $httpCors,$timeout ) {

        $document.ready(function () {

            bindEvents();
            init();
            
        });



        $scope.isSelected = 'nope';
        $scope.onText = 'Date';
        $scope.offText = 'Month';
        $scope.isActive = true;
        $scope.size = 'mini';
        $scope.animate = true;
        $scope.radioOff = true;
        $scope.handleWidth = "auto";
        $scope.labelWidth = "auto";
        $scope.inverse = true;
        $scope.label = "";

        $scope.months = [

            {
                id: 1,
                name: 'January'
            },
            {
                id: 2,
                name: 'Febraury'
            },
            {
                 id: 3,
                 name: 'March'
             },
            {
                id: 4,
                name: 'April'

            },
            {
                 id: 5,
                 name: 'May'
             },
            {
                id: 6,
                name: 'June'
            },
            {
                 id: 7,
                 name: 'July'
             },
            {
                id: 8,
                name: 'August'

            },
            {
                 id: 9,
                 name: 'September'
             },
            {
                id: 10,
                name: 'October'
            },
            {
                 id: 11,
                 name: 'November'
             },
            {
                id: 12,
                name: 'December'

            }
        ];

        // Simulate async data update
        $timeout(function () {
            $scope.data = [
              [28, 48, 40, 19, 86, 27, 90],
              [65, 59, 80, 81, 56, 55, 40]
            ];
        }, 3000);

        $scope.$watch('isSelected', function () {
            //$log.info('Selection changed.');
        });

        $scope.toggle = function () {
            $scope.isSelected = $scope.isSelected === 'yep' ? 'nope' : 'yep';
        };

        $scope.setUndefined = function () {
            $scope.isSelected = undefined;
        };

        $scope.toggleActivation = function () {
            $scope.isActive = !$scope.isActive;
        }



		//private properties

		//public properties  
		$scope.expenseTypes = [];
		$scope.DropDownInitializer = {
			TypeId: 0,
			Type: 'Select One',
			UserId: $scope.loggedInUserId
		};
		$scope.loggedInUserId = 0;
		$scope.dateFormat = DREnum.DateFormats.dd_MM_yy;;
		$scope.disabled = undefined;

		//view models
		$scope.ExpenseTypeViewModel = function (typeId, type, userId) {

			this.TypeId = (typeId === undefined) ? 0 : typeId;
			this.Type = (type === undefined) ? 0 : type;
			this.UserId = (userId === undefined) ? $scope.loggedInUserId : userId;
		};

		$scope.ExpenseSummaryViewModel = function (expenseViewModels, totalAmount, paging) {

			this.ExpenseViewModels = expenseViewModels,
            this.TotalAmount = totalAmount,
            this.Paging = paging
		};

		$scope.ExpenseViewModel = function (expenseId,expenseType, amount, description, comments, expenseDate, userId) {

			this.ExpenseId = (expenseId == undefined) ? 0 : expenseId;
			this.ExpenseType = (expenseType === undefined) ? new $scope.ExpenseTypeViewModel() : expenseType;
			this.Amount = (amount === undefined) ? 0.0 : amount;
			this.Description = (description === undefined) ? 0.0 : description;
			this.Comments = (comments === undefined) ? 0.0 : comments;
			this.ExpenseDate = (expenseDate === undefined) ? Date.now : expenseDate;
			this.UserId = (userId === undefined) ? $scope.loggedInUserId : userId;
			this.CreatedBy = (userId === undefined) ? $scope.loggedInUserId : userId;
		};

		//Actions
		$scope.ExpenseTypeActions = (function () {

			this.IsAddSectionVisible = false,

            this._add = function () {
            	if ($scope.ExpenseTypeViewModel.Type == '') {
            		this.hide(true);
            		return;
            	}
            	var newExpenseType = new $scope.ExpenseTypeViewModel(0, $scope.ExpenseTypeViewModel.Type);
            	$http({
            		withCredentials: false,
            		method: 'post',
            		url: '/Expense/AddExpenseType',
            		headers: { 'Content-Type': 'application/json; charset=utf-8' ,'X-Requested-With': 'XMLHttpRequest'},
            		data: JSON.stringify(newExpenseType),
            		datatype: 'json',
            		async: true
            	})
                .success(function (expenseTypeId, status, headers, config) {
                	var newExpenseType = new $scope.ExpenseTypeViewModel(expenseTypeId, $scope.ExpenseTypeViewModel.Type);
                	$scope.expenseTypes.push(newExpenseType);
                	$scope.ExpenseViewModel.ExpenseType = newExpenseType;
                	$scope.ExpenseTypeActions.Hide(true);
                })
                .error(function (data, status, headers, config) {
                	DRError({
                		Type: "Error",
                		Message: "failed to add expensetype"
                	});
                });

            },

            this._show = function () {
            	$scope.ExpenseTypeViewModel.Type = '';
            	this.IsAddSectionVisible = true;
            },

            this._hide = function () {
            	this.IsAddSectionVisible = false;
            },

            this._cancel = function () {
            	this.Hide();
            	$scope.ExpenseTypeViewModel = new $scope.ExpenseTypeViewModel();
            }

			this._getAll = function () {

				if ($scope.loggedInUserId == 0) return;

			    $http.get("/Expense/GetAllExpenseTypes?userId=" + $scope.loggedInUserId, { headers: { 'X-Requested-With': 'XMLHttpRequest' }})
                     .success(function (expenseTypes, status, headers, config) {
                         //angular.forEach(obj, iterator, [context]);
                         $scope.expenseTypes.push($scope.DropDownInitializer);
                     	angular.forEach(expenseTypes, function (expenseType, key) {
                     		this.push(new $scope.ExpenseTypeViewModel(expenseType.TypeId, expenseType.Type));
                     	}, $scope.expenseTypes);
                     	// $scope.ExpenseViewModel.ExpenseType = $scope.expenseTypes[0]; //initialize 
                     })
                     .error(function (data, status, headers, config) {
                     	DRError({
                     		Type: "Error",
                     		Message: "failed to get all expensetypes"
                     	});
                     });
			}

			return {
				GetAll: this._getAll,
				Add: this._add,
				Show: this._show,
				Hide: this._hide,
				Cancel: this._cancel
			}
		})();

		$scope.ExpenseActions = (function () {


			this._add = function () {

				validateExpense()
                .done(function () {

                	if ($scope.ExpenseViewModel.ExpenseType === undefined || $scope.ExpenseViewModel.ExpenseType.TypeId <= 0) {
                		//var $ExpenseDropDown = $('#expensediv_data_ExpenseType').find('div:first');
                		//$ExpenseDropDown.addClass('DRValidationError');
                		return; //TODo ----> Needs to implement another way of validation              
                	}

                	var expenseViewModel = {
                		ExpenseId: $scope.ExpenseViewModel.ExpenseId,
                		ExpenseType: $scope.ExpenseViewModel.ExpenseType,
                		Amount: $scope.ExpenseViewModel.Amount,
                		Description: $scope.ExpenseViewModel.Description,
                		Comments: $scope.ExpenseViewModel.Comments,
                		ExpenseDate: $scope.ExpenseViewModel.ExpenseDate,
                		UserId: $scope.loggedInUserId,
                		CreatedBy: $scope.ExpenseViewModel.CreatedBy
                	};
                	$http({
                		withCredentials: false,
                		method: 'post',
                		url: '/Expense/AddExpense',
                		headers: { 'Content-Type': 'application/json; charset=utf-8' ,'X-Requested-With': 'XMLHttpRequest' },
                		data: JSON.stringify(expenseViewModel),
                		datatype: 'json',
                		async: true
                	})
                    .success(function (ExpenseId, status, headers, config) {
                    	$scope.ExpenseActions.GetAll(); //Refresh Expenses list after adding
                    	$scope.ExpenseActions.Hide();
                    	DRAlert("Expense added successfully.");
                    })
                    .error(function (data, status, headers, config) {
                    	DRError({
                    		Type: "Error",
                    		Message: "Failed to add Expense."
                    	});
                    });
                })
                .fail(function () {
                	if ($scope.ExpenseViewModel.ExpenseId === undefined || $scope.ExpenseViewModel.ExpenseId <= 0) {
                		//var $ExpenseDropDown = $('#expensediv_data_ExpenseType').find('div:first');
                		//$ExpenseDropDown.addClass('DRValidationError');
                		return; //TODo ----> Needs to implement another way of validation              
                	}
                });

			},

            this._update = function () {

                validateExpense()
                .done(function () {

                if ($scope.ExpenseViewModel.ExpenseType === undefined || $scope.ExpenseViewModel.ExpenseType.TypeId <= 0) {
                    //var $ExpenseDropDown = $('#expensediv_data_ExpenseType').find('div:first');
                    //$ExpenseDropDown.addClass('DRValidationError');
                    return; //TODo ----> Needs to implement another way of validation              
                }

                var expenseViewModel = {
                    ExpenseId: $scope.ExpenseViewModel.ExpenseId,
                    ExpenseType: $scope.ExpenseViewModel.ExpenseType,
                    Amount: $scope.ExpenseViewModel.Amount,
                    Description: $scope.ExpenseViewModel.Description,
                    Comments: $scope.ExpenseViewModel.Comments,
                    ExpenseDate: $scope.ExpenseViewModel.ExpenseDate,
                    UserId: $scope.loggedInUserId,
                    CreatedBy: $scope.ExpenseViewModel.CreatedBy
                };
                $http({
                    withCredentials: false,
                    method: 'PUT',
                    url: '/Expense/EditExpense',
                    headers: { 'Content-Type': 'application/json; charset=utf-8','X-Requested-With': 'XMLHttpRequest' },
                    data: JSON.stringify(expenseViewModel),
                    datatype: 'json',
                    async: false
                })
                .success(function (ExpenseId, status, headers, config) {
                    $scope.ExpenseActions.GetAll(); //Refresh Expenses list after adding
                    $scope.ExpenseActions.Hide();
                    DRAlert("Expense updated successfully.");
                })
                .error(function (data, status, headers, config) {
                    DRError({
                        Type: "Error",
                        Message: "Failed to update Expense."
                    });
                });
            })
                .fail(function () {
                if ($scope.ExpenseViewModel.ExpenseId === undefined || $scope.ExpenseViewModel.ExpenseId <= 0) {
                    //var $ExpenseDropDown = $('#expensediv_data_ExpenseType').find('div:first');
                    //$ExpenseDropDown.addClass('DRValidationError');
                    return; //TODo ----> Needs to implement another way of validation              
                }
            });
            },

            this._edit = function (rowItem, event) {

                var expense = rowItem.entity;

                $scope.ExpenseViewModel.ExpenseId = expense.ExpenseId;
                $scope.ExpenseViewModel.ExpenseType = expense.ExpenseType;
                $scope.ExpenseViewModel.Amount = expense.Amount;
                $scope.ExpenseViewModel.Description = expense.Description;
                $scope.ExpenseViewModel.Comments = expense.Comments;
                $scope.ExpenseViewModel.ExpenseDate = expense.ExpenseDate;
                $scope.ExpenseViewModel.CreatedBy = expense.CreatedBy;

                DRToolBox.ShowExpense($('.addexpenses_toolbox a'));
                $scope.ExpenseAction = '$scope.ExpenseActions.Update';

                //angular.element('#expensediv_data_amount').trigger('click');
            },

            this._delete = function (rowItem, event) {

                var expense = rowItem.entity;

                $scope.deleteConfirmationPopup = {

                    id: expense.ExpenseId,
                    title: 'Expense',
                    description: 'Are you sure you want to delete this expense ?',
                    ok:'Yes',
                    cancel: 'No',
                    okCallBack: function (expenseId) {
                        deleteExpense(expenseId);
                    },
                    cancelCallBack: function () {
                    }

                };

                $modalService.show($scope.deleteConfirmationPopup);

            },

            this._hide = function (shouldClearAllInputs) {

                DRActivityArea.Hide(shouldClearAllInputs);
            }

			this._getAll = function () {
				LoadExpenses();
			}

			return {
				Add: _add,
				Edit: _edit,
                Update :_update,
				Delete: _delete,
				Hide: _hide,
				GetAll: _getAll
			}
		})();

		$scope.ExpenseOperation = function () {
		    eval('(' + $scope.ExpenseAction + ')')();
		}

		//grid
		$scope.expenseData = [];

		$scope.gridOptions = {
		    data: 'expenseData',
		    columnDefs: [
                         { field: 'ExpenseDate', displayName: 'Date', width: '15%' }, // cellFilter: "date:'dd/MM/yyyy'" ToDo ==>Not working...Needs to check
                         { field: 'ExpenseType.Type', displayName: 'ExpenseType', width: '20%' },
                         { field: 'Description', displayName: 'Description', width: '47%', cellTemplate: $templateCache.get('template/popover/commentsDirectiveTemplate.html') },
                         { field: 'Amount', displayName: 'Amount', width: '10%' },
                         { field: '', cellTemplate: $templateCache.get('template/expense/actionTemplate.html'), width: '8%' }
		    ],
		    multiSelect: false, //Not Reguired if we set enableRowSelection to false
		    enableRowSelection: false,
		    enableHighlighting :true,
		    plugins: [new ngGridCsvExportPlugin()],//TESTING EXPORT TO EXCEL ( !!!!!!!!! BETA !!!!!!!! )
            
		};
		
		$scope.PagingViewModel = {

			MaximumNoOfPages: 10,
			TotalItems: 0,
			CurrentPage: 1

		};
		
		$scope.Filters = {
			ExpenseTypes: [],
			ExpenseDate: null,
            ExpenseMonth :null

		};
            
		//public Action methods

		function bindEvents() {

		    $scope.$watch("PagingViewModel.CurrentPage", function (oldvalue, newValue) {
		        if ((oldvalue == 'undedined' || newValue == 'undedined') || oldvalue != newValue) {
		            LoadExpenses();
		        }

		    });

		    $scope.$watch("Filters.ExpenseDate", function (oldvalue, newValue) {
		        if ((oldvalue == 'undedined' || newValue == 'undedined') || oldvalue != newValue) {
		            LoadExpenses();
		        }

		    });

		    $scope.$watch("Filters.ExpenseMonth", function (oldvalue, newValue) {
		        if ((oldvalue == 'undedined' || newValue == 'undedined') || oldvalue != newValue) {
		            LoadExpenses();
		        }

		    });

		    $scope.$watch("Filters.ExpenseTypes", function (oldvalue, newValue) {
		        if ((oldvalue == 'undedined' || newValue == 'undedined') || oldvalue != newValue) {
		            LoadExpenses();
		        }

		    });


		    $document.on('clearDropDowns', function () {
		        $scope.ExpenseViewModel.ExpenseType = $scope.DropDownInitializer;
		        //$scope.$apply();//TODO-->Commented for testing
		    });
		  
		    $document.on('click', function (e) {

		        if ($(e.target).closest(".popover").length > 0) {
		            return false;
		        };
		        if (!$(e.target).hasClass('expenseComments'))
		        {
		            var $$popups = document.querySelectorAll('.popover');
		            if ($$popups.length > 0) {
		                var $$popupElement = angular.element($$popups[0]);
		                if ($$popupElement.length > 0) {
		                    var $$parentPopupElement = $$popupElement.scope().$parent;

		                    if ($$parentPopupElement !== undefined) {
		                        $$popupElement.scope().$parent.isOpen = false;
		                        $$popupElement.remove();
		                    }
		                }
		            }
		        }
		       

		    });
		}

		function init() {

			$scope.loggedInUserId = DRUser.GetLoginUser().UserId;

			DRToolBox.Bind('ExpenseToolBox'); //bind toolbox

			initFilters(); //init and bind filters

			$scope.ExpenseActions.GetAll(); //bind all Expenses

			$scope.initExpenseAction();			
		};

		$scope.initExpenseAction =function() {
		
		    $scope.ExpenseViewModel = {};
		    $scope.ExpenseAction = '$scope.ExpenseActions.Add';
		    $scope.$digest();//To Update the view about the change of ExpenseAction
		    return true;
		}

		//Date picker methods

		$scope.today = function () {
			$scope.Filters.ExpenseDate = new Date().getFormattedDate($scope.dateFormat);
		};

		$scope.clear = function () {
			$scope.Filters.ExpenseDate = null;
		};

		// Disable weekend selection
		$scope.disabled = function (date, mode) {
			return (mode === 'day' && (date.getDay() === 0 || date.getDay() === 6));
		};

		$scope.toggleMin = function () {
			$scope.minDate = $scope.minDate ? null : new Date();
		};

		$scope.enable = function () {
			$scope.disabled = false;
		};

		$scope.disable = function () {
			$scope.disabled = true;
		};

		$scope.clear = function () {
			$scope.person.selected = undefined;
			$scope.address.selected = undefined;
			$scope.country.selected = undefined;
		};

		$scope.open = function ($event) {
			$event.preventDefault();
			$event.stopPropagation();

			$scope.opened = true;
		};

        ///
        /// Description : To check the expense activity area active
        ///
		$scope.IsActivityAreaActive = function isActivityAreaActive(childDataDiv) {

		    var $ChildDataDiv = childDataDiv;
		    var $ActivityAreaDiv = $(".diary_activityArea_outerDiv");
		    var isActivityAreaChildExist = $ActivityAreaDiv.find($ChildDataDiv).css('visibility') == 'visible' ? true : false;
		    var isActivityAreaVisible = $ActivityAreaDiv.css('display') == 'none' ? false : true;
		    if (isActivityAreaChildExist && isActivityAreaVisible) {
		        return true;;
		    }
		    return false;
		}


		function initFilters() {
			initDatePicker();
			initMultiSelectExpenseTypes();
		}

		function initDatePicker() {
			// $scope.today(); // ToDo==> Uncomment this after testing....
			$scope.toggleMin();
		}

		function initMultiSelectExpenseTypes() {

			$("#multipleselect-expensetypes").select2({

				placeholder: "Search by one or more expense types",
				selectedTagClass: 'label label-info', // label label-info are css classes that will be used for selected elements
				formatNoMatches: function () { return "No Expense types found"; },
				maximumSelectionSize: 3
			});
			$scope.ExpenseTypeActions.GetAll();  //LoadAll Expense Types

			var $Select2LiveRegion = $('.select2-hidden-accessible');//To remove the text shown of multiselect(Select2)... 
			$Select2LiveRegion.remove();//  TODO--> Find any configured property or function to remove this

		}

		function LoadExpenses() {

		    var expenseDate = $scope.Filters.ExpenseDate == undefined ? '' : $scope.Filters.ExpenseDate.toJSON();

		    var expenseMonth = $scope.Filters.ExpenseMonth == undefined ? '' : $scope.Filters.ExpenseMonth.name;

		    var expenseTypeQueryString = "&expenseTypes=" + null;

		    if ($scope.Filters.ExpenseTypes.length > 0) {

		        expenseTypeQueryString = '';
		        angular.forEach($scope.Filters.ExpenseTypes, function (expenseType, key) {

		            // To pass an array of simple values in MVC, you simply need to give the same name to multiple values 
		            expenseTypeQueryString += "&expenseTypes=" + expenseType;

		        });
		    }
		    
		    var url = '/Expense/GetAllExpenses?userId=' + $scope.loggedInUserId + "&expenseDate=" + expenseDate + "&expenseMonth=" + expenseMonth + expenseTypeQueryString + "&currentPage=" + $scope.PagingViewModel.CurrentPage;
		    

			$http({
				withCredentials: false,
				method: 'get',
				url: url,
				headers: { 'Content-Type': 'application/json; charset=utf-8' ,'X-Requested-With': 'XMLHttpRequest'},
				datatype: 'json',
				traditional: true,
				async: true
			})
            .success(function (expenseSummary, status, headers, config) {
            if (expenseSummary === undefined) {

              	DRError({
              		Type: "Error",
              		Message: "Failed to get Expenses."
              	});
            } else {
              	InitializeExpenseSummary(expenseSummary);
            }

            })

            .error(function (data, status, headers, config) {
              	DRError({
              		Type: "Error",
              		Message: "Failed to load Expenses."
              	});
              });

		}

		function InitializeExpenseSummary(expenseSummary) {

		    InitializeExpenseGrid(expenseSummary.ExpenseViewModels);

		   // exportToExcel(expenseSummary.ExpenseViewModels); TESTING EXPORT TO EXCEL

			$scope.ExpenseSummaryViewModel.TotalAmount = expenseSummary.TotalAmount;
			if (expenseSummary.PagingViewModel != null && expenseSummary.PagingViewModel != 'undefined') {

				$scope.PagingViewModel.TotalItems = expenseSummary.PagingViewModel.TotalItems;
				$scope.PagingViewModel.CurrentPage = expenseSummary.PagingViewModel.CurrentPage;
			}
			else {
				$scope.PagingViewModel.TotalItems = 0; //Initialize
				$scope.PagingViewModel.CurrentPage = 1; //Initialize
			}

		}

		function swapLastCommaForNewline(str) {
		    var newStr = str.substr(0, str.length - 1);
		    return newStr + "\n";
		}

		function csvStringify(str) {
		    if (str == null) { // we want to catch anything null-ish, hence just == not ===
		        return '';
		    }
		    if (typeof (str) === 'number') {
		        return '' + str;
		    }
		    if (typeof (str) === 'boolean') {
		        return (str ? 'TRUE' : 'FALSE');
		    }
		    if (typeof (str) === 'string') {
		        return str.replace(/"/g, '""');
		    }

		    return JSON.stringify(str).replace(/"/g, '""');
		}

		function InitializeExpenseGrid(expenses) {
			$scope.expenseData = [];
			if (expenses != null || expenses != undefined)
				$scope.expenseData = expenses;
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

		function exportToExcel(data) {

             if ($('.exportData').length > 0) {
		       $('.exportData').remove();
		    }

		    var toolBoxMenus = $('.expenseDetails_Pagination');
		    var exportAnchorLink = "<button class=\"exportData\"><a  href=\"data:text/csv;charset=UTF-8,";

            var excelJsonString ='';
		    

            angular.forEach(data, function (expenseObject, key) {

                for (property in expenseObject) {

                    switch (property) {

                        case "ExpenseType":
                            excelJsonString += '"' + csvStringify(expenseObject[property].Type) + '",';
                            break;

                        case "ExpenseDate":
                        case "Amount":
                        case "Comments":
                        case "Description":
                            excelJsonString += '"' + csvStringify(expenseObject[property]) + '",';
                            break;
                        
                    };
                }

                excelJsonString = swapLastCommaForNewline(excelJsonString);

		    });
		    exportAnchorLink += encodeURIComponent(excelJsonString);
		   // console.log(encodeURIComponent(excelJsonString));

		    exportAnchorLink += "\" download=\"Export.csv\">Export to excel</a></button>";
		    toolBoxMenus.append(exportAnchorLink);
		};

		function deleteExpense(expenseId) {

		    $http({
		        withCredentials: false,
		        method: 'DELETE',
		        url: '/Expense/DeleteExpense',
		        headers: { 'Content-Type': 'application/json; charset=utf-8', 'X-Requested-With': 'XMLHttpRequest' },
		        data: JSON.stringify({expenseId:expenseId}),
		        datatype: 'json',
		        async: false

		    })
            .success(function (response, status, headers, config) {

                $scope.ExpenseActions.GetAll(); //Refresh Expenses list after adding
                DRAlert("Expense deleted successfully.");

            })

            .error(function (data, status, headers, config) {
                DRError({
                    Type: "Error",
                    Message: "Failed to delete expense."
                });
            });
		}

	}])

    return {

		scope: function () {
			return angular.element(document.querySelector('#expenseDetailsFullDiv')).scope(); //returning the Expenseconroller scope by using id of the controller where it is using
		}
	};
})();
//angular js controller to handle incomes ,derived from myDiaryApp defined in myDiaryApp.js
var DRIncomeController = (function () {


    DRIncomeAngularModule.controller('incomeController', ['$scope', '$document', '$http', '$templateCache', '$modalService',

       function ($scope, $document, $http, $templateCache, $modalService) {

           var $this = $scope;

           //document.ready
           angular.element(document).ready(function () {

               bindEvents();
               init();

           });


           //private properties

           //public properties  
           $this.incomeTypes = [];
           $this.DropDownInitializer = {
               TypeId: 0,
               Type: 'Select One',
               UserId: $this.loggedInUserId
           };
           $this.loggedInUserId = 0;
           $this.dateFormat = DREnum.DateFormats.dd_MM_yy;
           $scope.disabled = undefined;


           //view models
           $this.IncomeTypeViewModel = function (typeId, type, userId) {

               this.TypeId = (typeId === undefined) ? 0 : typeId;
               this.Type = (type === undefined) ? 0 : type;
               this.UserId = (userId === undefined) ? $this.loggedInUserId : userId;
           };

           $this.IncomeSummaryViewModel = function (incomeViewModels, totalAmount, paging) {

               this.IncomeViewModels = incomeViewModels,
               this.TotalAmount = totalAmount,
               this.Paging = paging
           };

           $this.IncomeViewModel = function (incomeId, incomeType, amount, description, comments, incomeDate, userId) {

               this.IncomeId = (incomeId == undefined) ? 0 : incomeId;
               this.IncomeType = (incomeType === undefined) ? new $this.IncomeTypeViewModel() : incomeType;
               this.Amount = (amount === undefined) ? 0.0 : amount;
               this.Description = (description === undefined) ? 0.0 : description;
               this.Comments = (comments === undefined) ? 0.0 : comments;
               this.IncomeDate = (incomeDate === undefined) ? Date.now : incomeDate;
               this.UserId = (userId === undefined) ? $this.loggedInUserId : userId;
               this.CreatedBy = (userId === undefined) ? $this.loggedInUserId : userId;
           };


           //Actions
           $this.IncomeTypeActions = (function () {

               this.IsAddSectionVisible = false,

               this._add = function () {
                   if ($this.IncomeTypeViewModel.Type == '') {
                       this.hide();
                       return;
                   }
                   var newIncomeType = new $this.IncomeTypeViewModel(0, $this.IncomeTypeViewModel.Type);
                   $http({
                       withCredentials: false,
                       method: 'post',
                       url: '/Income/AddIncomeType',
                       headers: { 'Content-Type': 'application/json; charset=utf-8' },
                       data: JSON.stringify(newIncomeType),
                       datatype: 'json',
                       async: true
                   })
                   .success(function (incomeTypeId, status, headers, config) {
                       var newIncomeType = new $this.IncomeTypeViewModel(incomeTypeId, $this.IncomeTypeViewModel.Type);
                       $this.incomeTypes.push(newIncomeType);
                       $this.IncomeViewModel.IncomeType = newIncomeType;
                       $this.IncomeTypeActions.Hide();
                   })
                   .error(function (data, status, headers, config) {
                       DRError({
                           Type: "Error",
                           Message: "failed to add incometype"
                       });
                   });

               },

               this._show = function () {
                   $this.IncomeTypeViewModel.Type = '';
                   this.IsAddSectionVisible = true;
               },

               this._hide = function () {
                   this.IsAddSectionVisible = false;
               },

               this._cancel = function () {
                   this.Hide();
                   $this.IncomeTypeViewModel = new $this.IncomeTypeViewModel();
               }

               this._getAll = function () {

                   if ($this.loggedInUserId == 0) return;

                   $http.get("/Income/GetAllIncomeTypes?userId=" + $this.loggedInUserId)
                        .success(function (incomeTypes, status, headers, config) {
                            //angular.forEach(obj, iterator, [context]);
                            angular.forEach(incomeTypes, function (incomeType, key) {
                                this.push(new $this.IncomeTypeViewModel(incomeType.TypeId, incomeType.Type));
                            }, $this.incomeTypes);

                            // $this.IncomeViewModel.IncomeType = $this.incomeTypes[0]; //initialize 
                        })
                        .error(function (data, status, headers, config) {
                            DRError({
                                Type: "Error",
                                Message: "failed to get all incometypes"
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

           $this.IncomeActions = (function () {


               this._add = function () {

                   validateIncome()
                   .done(function () {

                       if ($this.IncomeViewModel.IncomeType === undefined || $this.IncomeViewModel.IncomeType.TypeId <= 0) {

                           return; //TODo ----> Needs to implement another way of validation              
                       }

                       var incomeViewModel = {
                           IncomeId: $this.IncomeViewModel.IncomeId,
                           IncomeType: $this.IncomeViewModel.IncomeType,
                           Amount: $this.IncomeViewModel.Amount,
                           Description: $this.IncomeViewModel.Description,
                           Comments: $this.IncomeViewModel.Comments,
                           IncomeDate: $this.IncomeViewModel.IncomeDate,
                           UserId: $this.loggedInUserId,
                           CreatedBy: $this.IncomeViewModel.CreatedBy
                       };
                       $http({
                           withCredentials: false,
                           method: 'post',
                           url: '/Income/AddIncome',
                           headers: { 'Content-Type': 'application/json; charset=utf-8' },
                           data: JSON.stringify(incomeViewModel),
                           datatype: 'json',
                           async: true
                       })
                       .success(function (incomeId, status, headers, config) {
                           $this.IncomeActions.GetAll(); //Refresh incomes list after adding
                           $this.IncomeActions.Hide();
                           DRAlert("Income added successfully.");
                       })
                       .error(function (data, status, headers, config) {
                           DRError({
                               Type: "Error",
                               Message: "Failed to add income."
                           });
                       });
                   })
                   .fail(function () {
                       if ($this.IncomeViewModel.IncomeId === undefined || $this.IncomeViewModel.IncomeId <= 0) {

                           return; //TODo ----> Needs to implement another way of validation              
                       }
                   });

               },

               this._edit = function (rowItem, event) {

                   var income = rowItem.entity;

                   $scope.IncomeViewModel.IncomeId = income.IncomeId;
                   $scope.IncomeViewModel.IncomeType = income.IncomeType;
                   $scope.IncomeViewModel.Amount = income.Amount;
                   $scope.IncomeViewModel.Description = income.Description;
                   $scope.IncomeViewModel.Comments = income.Comments;
                   $scope.IncomeViewModel.IncomeDate = income.IncomeDate;
                   $scope.IncomeViewModel.CreatedBy = income.CreatedBy;

                   DRToolBox.ShowIncome($('.addincomes_toolbox a'));
                   $scope.IncomeAction = '$scope.IncomeActions.Update';

               },

               this._update = function () {

                   validateIncome()
                  .done(function () {

                      if ($scope.IncomeViewModel.IncomeType === undefined || $scope.IncomeViewModel.IncomeType.TypeId <= 0) {

                          return; //TODo ----> Needs to implement another way of validation              
                      }

                      var incomeViewModel = {
                          IncomeId: $scope.IncomeViewModel.IncomeId,
                          IncomeType: $scope.IncomeViewModel.IncomeType,
                          Amount: $scope.IncomeViewModel.Amount,
                          Description: $scope.IncomeViewModel.Description,
                          Comments: $scope.IncomeViewModel.Comments,
                          IncomeDate: $scope.IncomeViewModel.IncomeDate,
                          UserId: $scope.loggedInUserId,
                          CreatedBy: $scope.IncomeViewModel.CreatedBy
                      };

                      $http({
                          withCredentials: false,
                          method: 'PUT',
                          url: '/Income/EditIncome',
                          headers: { 'Content-Type': 'application/json; charset=utf-8', 'X-Requested-With': 'XMLHttpRequest' },
                          data: JSON.stringify(incomeViewModel),
                          datatype: 'json',
                          async: false
                      })
                      .success(function (IncomeId, status, headers, config) {
                          $scope.IncomeActions.GetAll(); //Refresh Incomes list after adding
                          $scope.IncomeActions.Hide();
                          DRAlert("Income updated successfully.");
                      })
                      .error(function (data, status, headers, config) {
                          DRError({
                              Type: "Error",
                              Message: "Failed to update Income."
                          });
                      });
                  })
                  .fail(function () {
                      if ($scope.IncomeViewModel.IncomeId === undefined || $scope.IncomeViewModel.IncomeId <= 0) {

                          return; //TODo ----> Needs to implement another way of validation              
                      }
                  });
               },

               this._delete = function (rowItem, event) {

                   var income = rowItem.entity;

                   $scope.deleteConfirmationPopup = {

                       id: income.IncomeId,
                       title: 'Income',
                       description: 'Are you sure you want to delete this income ?',
                       ok: 'Yes',
                       cancel: 'No',
                       okCallBack: function (incomeId) {
                           deleteIncome(incomeId);
                       },
                       cancelCallBack: function () {
                       }

                   };

                   $modalService.show($scope.deleteConfirmationPopup);
               },

               this._hide = function () {

                   DRActivityArea.Hide(true);
               }

               this._getAll = function () {
                   LoadIncomes();
               }

               return {
                   Add: _add,
                   Edit: _edit,
                   Update: _update,
                   Delete: _delete,
                   Hide: _hide,
                   GetAll: _getAll
               }
           })();


           $scope.IncomeOperation = function () {
               eval('(' + $scope.IncomeAction + ')')();
           }

           //grid
           $scope.incomeData = [];

           $scope.gridOptions = {
               data: 'incomeData',
               columnDefs: [
                            { field: 'IncomeDate', displayName: 'Date', width: '15%' }, // cellFilter: "date:'dd/MM/yyyy'" ToDo ==>Not working...Needs to check
                            { field: 'IncomeType.Type', displayName: 'IncomeType', width: '20%' },
                            { field: 'Description', displayName: 'Description', width: '47%', cellTemplate: $templateCache.get('template/popover/commentsDirectiveTemplate.html') },
                            { field: 'Amount', displayName: 'Amount', width: '10%' },
                            { field: '', cellTemplate: $templateCache.get('template/income/actionTemplate.html'), width: '8%' }
               ],
               multiSelect: false,
               enableRowSelection: false,
               enableHighlighting: true,
           };

           $this.PagingViewModel = {

               MaximumNoOfPages: 10,
               TotalItems: 0,
               CurrentPage: 1

           };

           $this.Filters = {
               IncomeTypes: [],
               IncomeDate: null

           };


           //public Action methods

           function bindEvents() {

               $scope.$watch("PagingViewModel.CurrentPage", function (oldvalue, newValue) {
                   if ((oldvalue == 'undedined' || newValue == 'undedined') || oldvalue != newValue) {
                       LoadIncomes();
                   }

               });

               $scope.$watch("Filters.IncomeDate", function (oldvalue, newValue) {
                   if ((oldvalue == 'undedined' || newValue == 'undedined') || oldvalue != newValue) {
                       LoadIncomes();
                   }

               });

               $scope.$watch("Filters.IncomeTypes", function (oldvalue, newValue) {
                   if ((oldvalue == 'undedined' || newValue == 'undedined') || oldvalue != newValue) {
                       LoadIncomes();
                   }

               });

               $document.on('clearDropDowns', function () {
                   $scope.IncomeViewModel.IncomeType = $scope.DropDownInitializer;
                   $scope.$apply();
               });

               $document.on('click', function (e) {

                   if ($(e.target).closest(".popover").length > 0) {
                       return false;
                   };
                   if (!$(e.target).hasClass('incomeComments')) {
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

           };

           function init() {

               $this.loggedInUserId = DRUser.GetLoginUser().UserId;

               DRToolBox.Bind('IncomeToolBox'); //bind toolbox

               initFilters(); //init and bind filters


               $this.IncomeActions.GetAll(); //bind all incomes

               $this.initIncomeAction();

           };


           $scope.initIncomeAction = function () {

               $scope.IncomeViewModel = {};
               $scope.IncomeAction = '$scope.IncomeActions.Add';
               $scope.$digest();//To Update the view about the change of IncomeAction
               return true;
           }

           //Date picker methods

           $scope.today = function () {
               $scope.Filters.IncomeDate = new Date().getFormattedDate($this.dateFormat);
           };

           $scope.clear = function () {
               $scope.Filters.IncomeDate = null;
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
           /// Description : To check the income activity area active
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
               initMultiSelectIncomeTypes();
           }

           function initDatePicker() {
               // $scope.today(); // ToDo==> Uncomment this after testing....
               $scope.toggleMin();
           }

           function initMultiSelectIncomeTypes() {

               $("#multipleselect-incometypes").select2({

                   placeholder: "Search by one or more incometypes",
                   selectedTagClass: 'label label-info', // label label-info are css classes that will be used for selected elements
                   formatNoMatches: function () { return "No income types found"; },
                   maximumSelectionSize: 3
               });
               $this.IncomeTypeActions.GetAll();  //LoadAll Income Types

               var $Select2LiveRegion = $('.select2-hidden-accessible');//To remove the text shown of multiselect(Select2)... 
               $Select2LiveRegion.remove();//  TODO--> Find any configured property or function to remove this

           }

           function LoadIncomes() {

               //// checking incomedate default value works for null value instead of string.empty
               var incomeDate = $scope.Filters.IncomeDate == undefined ? null : $scope.Filters.IncomeDate.toJSON();

               var incomeTypeQueryString = "&incomeTypes=" + null;

               if ($scope.Filters.IncomeTypes.length > 0) {

                   incomeTypeQueryString = '';
                   angular.forEach($scope.Filters.IncomeTypes, function (incomeType, key) {

                       // To pass an array of simple values in MVC, you simply need to give the same name to multiple values 
                       incomeTypeQueryString += "&incomeTypes=" + incomeType;

                   });
               }

               //angular.forEach($scope.Filters.IncomeTypes, function (incomeTypeId, key) {
               //    this.push(new $this.IncomeTypeViewModel(incomeTypeId, ''));
               //    }, incomeFilters);


               var url = '/Income/GetAllIncomes?userId=' + $scope.loggedInUserId;

               if (incomeDate != null) {
                   url = url + "&incomeDate=" + incomeDate;
               }

               url = url + incomeTypeQueryString + "&currentPage=" + $scope.PagingViewModel.CurrentPage;

               $http({
                   withCredentials: false,
                   method: 'GET',
                   url: url,
                   headers: { 'Content-Type': 'application/json; charset=utf-8' },
                   datatype: 'json',
                   traditional: true,
                   async: true
               })
                 .success(function (incomeSummary, status, headers, config) {
                     if (incomeSummary === undefined) {

                         DRError({
                             Type: "Error",
                             Message: "Failed to get incomes."
                         });
                     } else {
                         InitializeIncomeSummary(incomeSummary);
                     }

                 })

                 .error(function (data, status, headers, config) {
                     DRError({
                         Type: "Error",
                         Message: "Failed to load incomes."
                     });
                 });

           }

           function InitializeIncomeSummary(incomeSummary) {

               InitializeIncomeGrid(incomeSummary.IncomeViewModels);
               $scope.IncomeSummaryViewModel.TotalAmount = incomeSummary.TotalAmount;
               if (incomeSummary.PagingViewModel != null && incomeSummary.PagingViewModel != 'undefined') {

                   $this.PagingViewModel.TotalItems = incomeSummary.PagingViewModel.TotalItems;
                   $this.PagingViewModel.CurrentPage = incomeSummary.PagingViewModel.CurrentPage;
               }
               else {
                   $this.PagingViewModel.TotalItems = 0; //Initialize
                   $this.PagingViewModel.CurrentPage = 1; //Initialize
               }

           }

           function InitializeIncomeGrid(incomes) {
               $scope.incomeData = [];
               if (incomes != null || incomes != undefined)
                   $scope.incomeData = incomes;
           }

           function validateIncome() {
               var defered = $.Deferred();
               var result = $('.add_income_div #incomediv_data').find('.DRvalidate').DRValidate().valid;
               if (result) {
                   defered.resolve(result);
               }
               else {
                   defered.reject();
               }
               return defered.promise();

           }

           function deleteIncome(incomeId) {

               $http({
                   withCredentials: false,
                   method: 'DELETE',
                   url: '/Income/DeleteIncome',
                   headers: { 'Content-Type': 'application/json; charset=utf-8', 'X-Requested-With': 'XMLHttpRequest' },
                   data: JSON.stringify({ incomeId: incomeId }),
                   datatype: 'json',
                   async: false

               })
               .success(function (response, status, headers, config) {

                   $scope.IncomeActions.GetAll(); //Refresh Incomes list after adding
                   DRAlert("Income deleted successfully.");

               })

               .error(function (data, status, headers, config) {
                   DRError({
                       Type: "Error",
                       Message: "Failed to delete income."
                   });
               });
           }

       }])

    return {

        scope: function () {
            return angular.element(document.querySelector('#incomeDetailsFullDiv')).scope(); //returning the incomeconroller scope by using id of the controller where it is using
        }
    };
})();


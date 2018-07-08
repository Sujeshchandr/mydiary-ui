//Expense Module
var DRExpenseAngularModule = angular.module('expenseModule', ['ngGrid', 'ui.bootstrap', 'ngSanitize',
                                                              'ui.select', 'jcs.angular-http-batch',
                                                              'ui.diary', 'diary.common',
                                                              'frapontillo.bootstrap-switch','chart.js']);

DRExpenseAngularModule.config(['$httpCorsProvider', function ($httpCorsProvider) //all providers are  appended with the suffix 'Provider' to the $providerCache
{
    $httpCorsProvider.defaults.withCredentials = true;// So all api call will go with this value.

}]);

DRExpenseAngularModule.run(["$templateCache", function ($templateCache) {

    //NOTES --> Templates can be get using $templateCache.get('{id}');

   $templateCache.put("template/popover/customPopOverContent.html", //TODO--> CLEAN THIS AFTER TESTING
     "<div data-popupID =\"{{popUpId}}\" class=\"popUp-error\">\n" +
      "<ul>\n" +
        "<li ng-repeat=\"opt in Errors\">\n" +
            "{{ opt.Description }}\n" +
        "</li>\n" +
      "</ul>\n" +
    "</div>\n" +
      "");

   $templateCache.put("template/popover/commentsPopOverContent.html",
     "<div data-popupID =\"{{popUpId}}\" class=\"popUp-comments\">\n" +
     "<ul>\n" +
        "<li >\n" +
            "{{ comments }}\n" +
        "</li>\n" +
      "</ul>\n" +
            
    "</div>\n" +
      "");

   $templateCache.put("template/popover/errorsPopOverTemplate.html",
     '<div class="ngCellText" >' +
     '<div   ng-if="row.entity.Errors.length > 0"  class="ngCellText" job="row" ng-custom-popover style="margin-top: -5px;" ></div >' +
     '<span  ng-if="row.entity.Errors.length == 0" style="color:green;" class="ng-cell-text" >{{row.getProperty(col.field)}}</span >' +
     '</div>'
     );


   $templateCache.put("template/popover/commentsDirectiveTemplate.html",
     '<div class="ngCellText" >' +
     '<span    class="ng-cell-text" style="margin-top: -5px;"   >{{row.getProperty(col.field)}}</span >' +
     '<span ng-if="row.entity.Comments"   class="ng-cell-text" style="margin-top: -5px;" job="row.entity"   ng-comments-popover >{{row.getProperty(col.field)}}</span >' +
     '</div>'
     );

   $templateCache.put("template/expense/actionTemplate.html",
      '<div class="ngCellText" style={} data-ng-model="row">' +
      '<span data-ng-click="ExpenseActions.Edit(row,$event)" class="glyphicon glyphicon-pencil" style="cursor: pointer;padding-right: 0px;padding-left: 0px;"></span>' +
      '<span data-ng-click="ExpenseActions.Delete(row,$event)" class="glyphicon glyphicon-remove" style="cursor: pointer;padding-right: 0px;padding-left:14px;"></span>' +
      '</div>'
     );


}]);

DRExpenseAngularModule.directive('ngCommentsPopover', ['$compile', '$templateCache', function ($compile, $templateCache) {

    return {
        restrict: 'A',
        scope: {
            Job: "=job"
        },
        template: '<i class="glyphicon glyphicon-comment  expenseComments"  data-expenseId ="{{popUpId}}" popover-template="\'{{template}}\'"  popover-trigger="click" popover-placement="right"  popover-append-to-body="true" popover-title="{{title}}"  popover-placement="right" ></i>',

        link: function ($scope, element, attrs) {

            initalizePopupScope();

            $scope.$watch('Job', function (newValue,oldValue) {// fix for popup value is not updating
               
                if ((oldValue == 'undedined' || newValue == 'undedined') || oldValue != newValue) {

                    initalizePopupScope();

                }
            });

            function initalizePopupScope()
            {
                $scope.popUpId = $scope.Job.ExpenseId;
                $scope.template = 'template/popover/commentsPopOverContent.html';
                $scope.title = 'Comments';
                $scope.status = $scope.Job.Description;
                $scope.comments = $scope.Job.Comments;

            }

            angular.element(element).on('click', function (e) {

                var currentExpenseId =  angular.element(this).scope().$parent.$parent.row.entity.ExpenseId;

                var $$popups = document.querySelectorAll('.popover');
                if ($$popups) {

                    angular.forEach($$popups, function (popupElement, index) {

                        var $$popupElement = angular.element(popupElement);

                        var commentsPopupDiv = $$popupElement.querySelectorAll('.popUp-comments');
                        if (commentsPopupDiv.length > 0) {

                            var popUpexpenseId = $$popupElement.scope().$parent.origScope.Job.ExpenseId;

                            if (parseInt(currentExpenseId) != parseInt(popUpexpenseId)) { // popUpexpenseId is not the clicked popup then remove that.

                                $$popupElement.scope().$parent.isOpen = false;
                                $$popupElement.remove();
                            }
                        }

                    });

                }

            });

        }
    };
}]); //this directive can be used as an attribute as 'ng-comments-popover';



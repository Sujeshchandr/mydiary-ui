//Income Module
var DRIncomeAngularModule = angular.module('incomeModule', ['ngGrid', 'ui.bootstrap', 'ngSanitize', 'ui.select', 'ui.diary', 'diary.common']);

DRIncomeAngularModule.run(["$templateCache", function ($templateCache) {


    $templateCache.put("template/popover/commentsPopOverContent.html",
      "<div data-popupID =\"{{popUpId}}\" class=\"popUp-comments\">\n" +
      "<ul>\n" +
         "<li >\n" +
             "{{ comments }}\n" +
         "</li>\n" +
       "</ul>\n" +

     "</div>\n" +
       "");

    $templateCache.put("template/popover/commentsDirectiveTemplate.html",
    '<div class="ngCellText" >' +
    '<span    class="ng-cell-text" style="margin-top: -5px;"   >{{row.getProperty(col.field)}}</span >' +
    '<span ng-if="row.entity.Comments"   class="ng-cell-text" style="margin-top: -5px;" job="row.entity"   ng-comments-popover >{{row.getProperty(col.field)}}</span >' +
    '</div>'
    );

    $templateCache.put("template/income/actionTemplate.html",
     '<div class="ngCellText" style={} data-ng-model="row">' +
     '<span data-ng-click="IncomeActions.Edit(row,$event)" class="glyphicon glyphicon-pencil" style="cursor: pointer;padding-right: 0px;padding-left: 0px;"></span>' +
     '<span data-ng-click="IncomeActions.Delete(row,$event)" class="glyphicon glyphicon-remove" style="cursor: pointer;padding-right: 0px;padding-left:14px;"></span>' +
     '</div>'
    );

    //$templateCache.put("template/income/actionTemplate.html",
    //   '<div class="ngCellText" style={} data-ng-model="row">' +
    //   '<span data-ng-click="IncomeActions.Delete(row,$event)" class="glyphicon glyphicon-remove" style="cursor: pointer;padding-right: 0px;padding-left:14px;"></span>' +
    //   '<span data-ng-click="IncomeActions.Edit(row,$event)" class="glyphicon glyphicon-pencil" style="cursor: pointer;padding-right: 0px;padding-left: 0px;"></span>' +
       
    //   '</div>'
    //  );


}]);

DRIncomeAngularModule.filter('propsFilter', function () { //TODo-->Remove as it is not using.

    return function (items, props) {
        var out = [];

        if (angular.isArray(items)) {
            items.forEach(function (item) {
                var itemMatches = false;

                var keys = Object.keys(props);
                for (var i = 0; i < keys.length; i++) {
                    var prop = keys[i];
                    var text = props[prop].toLowerCase();
                    if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                        itemMatches = true;
                        break;
                    }
                }

                if (itemMatches) {
                    out.push(item);
                }
            });
        } else {
            // Let the output be the input untouched
            out = items;
        }

        return out;
    }
});

DRIncomeAngularModule.directive('ngCommentsPopover', ['$compile', '$templateCache', function ($compile, $templateCache) {

    return {
        restrict: 'A',
        scope: {
            Job: "=job"
        },
        template: '<i class="glyphicon glyphicon-comment incomeComments"  data-incomeId ="{{popUpId}}" popover-template="\'{{template}}\'"  popover-trigger="click" popover-placement="right"  popover-append-to-body="true" popover-title="{{title}}"  popover-placement="right" ></i>',

        link: function ($scope, element, attrs) {

            initalizePopupScope();

            $scope.$watch('Job', function (newValue, oldValue) {// fix for popup value is not updating

                if ((oldValue == 'undedined' || newValue == 'undedined') || oldValue != newValue) {

                    initalizePopupScope();

                }
            });

            function initalizePopupScope() {
                $scope.popUpId = $scope.Job.IncomeId;
                $scope.template = 'template/popover/commentsPopOverContent.html';
                $scope.title = 'Comments';
                $scope.status = $scope.Job.Description;
                $scope.comments = $scope.Job.Comments;

            }

            angular.element(element).on('click', function (e) {

                var currentIncomeId = angular.element(this).scope().$parent.$parent.row.entity.IncomeId;

                var $$popups = document.querySelectorAll('.popover');
                if ($$popups) {

                    angular.forEach($$popups, function (popupElement, index) {

                        var $$popupElement = angular.element(popupElement);

                        var commentsPopupDiv = $$popupElement.querySelectorAll('.popUp-comments');
                        if (commentsPopupDiv.length > 0) {

                            var popUpincomeId = $$popupElement.scope().$parent.origScope.Job.IncomeId;

                            if (parseInt(currentIncomeId) != parseInt(popUpincomeId)) { // if popUpincomeId is not the clicked popup then remove that.

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


//Angular module overrided
angular.module('ui.bootstrap.popover', ['ui.bootstrap.tooltip'])

.directive('popoverTemplatePopup', function () {
    return {
        restrict: 'EA',
        replace: true,
        scope: {
            title: '@', contentExp: '&', placement: '@', popupClass: '@', animation: '&', isOpen: '&',
            originScope: '&'
        },
        templateUrl: 'template/popover/popover-template.html',

        link: function ($scope, element, attrs) {

            element.find('.closeErrorLog').on('click', function (e) {

                var $$popups = document.querySelectorAll('.popover');
                if ($$popups) {
                    var $$popupElement = angular.element($$popups[0]);
                    if ($$popupElement) {
                        $$popupElement.scope().$parent.isOpen = false;
                        $$popupElement.remove();
                    }
                }
            });
            
        }
    };
})

.directive('popoverTemplate', ['$tooltip', function ($tooltip) {
    return $tooltip('popoverTemplate', 'popover', 'click', {
        useContentExp: true
    });
}])

.directive('popoverPopup', function () {
    return {
        restrict: 'EA',
        replace: true,
        scope: { title: '@', content: '@', placement: '@', popupClass: '@', animation: '&', isOpen: '&' },
        templateUrl: 'template/popover/popover.html',
        link: function ($scope, element, attrs) {

            element.find('.closeErrorLog').on('click', function (e) {

                var $$popups = document.querySelectorAll('.popover');
                if ($$popups) {
                    var $$popupElement = angular.element($$popups[0]);
                    if ($$popupElement) {
                        $$popupElement.scope().$parent.isOpen = false;
                        $$popupElement.remove();
                    }
                }
            });

        }
    };
})

.directive('popover', ['$tooltip', function ($tooltip) {
    return $tooltip('popover', 'popover', 'click');
}]);










var DRUiModule = angular.module('ui.diary', ['ui.bootstrap']);

DRUiModule.factory('$modalService', ['$modal',function ($modal) {

    var $modalScope = {};

    $modalScope.show = function (modalData)
    {
       
        var modalInstance = $modal.open({
            animation: false,
            templateUrl: 'myModalContent.html',
            controller: 'ModalInstanceCtrl',
            size: 'slim',//custom class
            windowClass: 'center-modal',//custom class
            resolve: {
                modalData: function () {
                    return modalData;
                }
            }
        });

        modalInstance.result.then(function (selectedItem) {
            modalData.okCallBack(selectedItem);
        }, function () {
            modalData.cancelCallBack();
            // $log.info('Modal dismissed at: ' + new Date());
        });
    };

    return $modalScope;
}]);







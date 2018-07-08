
var DRUiModalController = DRUiModule.controller('ModalInstanceCtrl', ['$scope', '$modalInstance', 'modalData',

    function ($scope, $modalInstance, modalData, modalService) {

    $scope.modalData = angular.extend({

        id: 1,
        title: 'Modal',
        description: 'Are you sure to continue ?',
        ok: 'Ok',
        cancel: 'Cancel'

    }, modalData);//Copying data from popUpData to $scope.popUpData (if no value is provided for each property default value will retained)

    $scope.ok = function () {
        $modalInstance.close($scope.modalData.id);
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
}]);
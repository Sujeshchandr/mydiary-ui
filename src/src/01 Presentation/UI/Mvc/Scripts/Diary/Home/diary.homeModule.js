//Home Module
var DRHomeAngularModule = angular.module('homeModule', [ 'ui.bootstrap', 'ngSanitize',
                                                         'ui.diary', 'diary.common',
                                                         'chart.js'])


.config(['ChartJsProvider', function (ChartJsProvider) {

    // Configure all charts
    ChartJsProvider.setOptions({
        colours: ['#FF5252', '#AA5444'],
        responsive: true
    });

    // Configure all line charts
    ChartJsProvider.setOptions('Line', {
        datasetFill: false
    });
}]);
var DRHomeController = (function () {

    DRHomeAngularModule.controller('homeController', ['$scope', '$http', '$timeout', '$document',
        function ($scope, $http, $timeout, $document) {
        
            var mydiaryStorageDomainWithVersion = "http://localhost:6488/v1";

            $scope.chartDataByMonth = [];
            $scope.chartDataByWeek = [];

            $scope.labelsOfMonth = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "Decmeber"];
            $scope.labelsOfWeek = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"];
            $scope.diaryChartSeries = ['Expenses', 'Incomes'];

            $document.ready(function () {
                debugger;
                bindEvents();
                init(); ////Explicitly calling the Initialization
            });

            function bindEvents() {

            };

            function init() {

                getExpenseMonthlySummary(1, '2015')
                 .done(function (expenseData) {

                     $scope.expensesByMonth = expenseData;

                     getExpenseMonthlySummary(1, '2014') // For testing.........
                     //getIncomeData(6, 'Monthly')
                     .done(function (incomeData) {

                         $scope.incomesByMonth = incomeData;

                         $scope.expensesByWeek = [
                              {
                                  seqNumber: 1,
                                  amount: 100
                              },
                              {
                                  seqNumber: 2,
                                  amount: 200
                              },
                              {
                                  seqNumber: 3,
                                  amount: 300
                              },
                              {
                                  seqNumber: 4,
                                  amount: 400
                              },
                              {
                                  seqNumber: 5,
                                  amount: 500
                              },
                              {
                                  seqNumber: 6,
                                  amount: 600
                              },
                              {
                                  seqNumber: 7,
                                  amount: 700
                              }
                         ];

                         ////Initialize
                         $scope.chartDataByMonth.push(new Array(), new Array());

                         $scope.chartDataByWeek.push(new Array());

                         angular.forEach($scope.expensesByMonth, function (expenseByMonth, key) {
                             this.push(expenseByMonth.amount);
                         }, $scope.chartDataByMonth[0]);

                         angular.forEach($scope.incomesByMonth, function (incomeByMonth, key) {
                             this.push(incomeByMonth.amount);
                         }, $scope.chartDataByMonth[1]);

                         angular.forEach($scope.expensesByWeek, function (expenseByWeek, key) {
                             this.push(expenseByWeek.amount);
                         }, $scope.chartDataByWeek[0]);

                         $scope.slides = [
                                          {
                                              image: document.getElementById('chartByYear').toDataURL()
                                          },
                                          {
                                              image: document.getElementById('chartByWeek').toDataURL()
                                          },
                                          {
                                              image: document.getElementById('chartByWeek').toDataURL()
                                          },
                                          {
                                              image: document.getElementById('chartByWeek').toDataURL()
                                          }
                         ];
                     })
                     .fail(function (data, status, headers, config) {
                         DRError({
                             Type: "Error",
                             Message: "Failed to get Income Data."
                         });
                     });
                 
                 })
                 .fail(function (data, status, headers, config) {
                     DRError({
                         Type: "Error",
                         Message: "Failed to get Expense Data."
                     });
                 });
            };

            function getExpenseData(userId, chartDataType)
            {
                var defered = $.Deferred();

                $http.get("http://localhost:64888/mydiary/v2/Expenses/get.chartData(userId=" + userId + ",type=type.ChartType'" + chartDataType + "')",
                          { headers: { 'X-Requested-With': 'XMLHttpRequest' } })
                     .success(function (expenseChartSummary, status, headers, config) {
                         defered.resolve(expenseChartSummary.Charts);
                     })
                     .error(function (data, status, headers, config) {
                         defered.reject();
                     });
                return defered.promise();
            }

            function getExpenseMonthlySummary(userId, year) {

                var defered = $.Deferred();

                $http.get(mydiaryStorageDomainWithVersion + "/expenses/get.monthlySummary(userId=" + userId + ",year="+ year +")",
                          { headers: { 'X-Requested-With': 'XMLHttpRequest' } })
                     .success(function (expenseChartSummary, status, headers, config) {
                         defered.resolve(expenseChartSummary.charts);
                     })
                     .error(function (data, status, headers, config) {
                         defered.reject();
                     });

                return defered.promise();
            }

        function getIncomeData(userId, chartDataType) {
            var defered = $.Deferred();

            $http.get("http://localhost:64888/mydiary/v2/Incomes/get.chartData(userId=" + userId + ",type=type.ChartType'" + chartDataType + "')",
                      { headers: { 'X-Requested-With': 'XMLHttpRequest' } })
                 .success(function (expenseChartSummary, status, headers, config) {
                     defered.resolve(expenseChartSummary.Charts);
                 })
                 .error(function (data, status, headers, config) {
                     defered.reject();
                 });
            return defered.promise();
        }
    }]);

    return {

        scope: function () {
            return angular.element(document.querySelector('#homeDetailsContainer')).scope(); //returning the homeController scope by using id of the controller where it is using
        }
    };
})();
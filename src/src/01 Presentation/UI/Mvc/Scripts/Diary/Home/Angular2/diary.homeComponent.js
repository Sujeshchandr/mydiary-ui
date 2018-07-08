System.register(['@angular/core', '@angular/http', 'rxjs/add/operator/map', 'ng2-charts/ng2-charts', './Services/diary.homeService.js'], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1, http_1, ng2_charts_1, diary_homeService_js_1;
    var LineChartDemoComponent, MonthlyChart;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (http_1_1) {
                http_1 = http_1_1;
            },
            function (_1) {},
            function (ng2_charts_1_1) {
                ng2_charts_1 = ng2_charts_1_1;
            },
            function (diary_homeService_js_1_1) {
                diary_homeService_js_1 = diary_homeService_js_1_1;
            }],
        execute: function() {
            LineChartDemoComponent = (function () {
                function LineChartDemoComponent(http, homeService) {
                    this.http = http;
                    this.homeService = homeService;
                    this.baseUrl = 'http://localhost:6488/v1';
                    this.userId = 1;
                    this.year = 2015;
                    this.lineChartData = [{ label: 'Expenses', data: [] }, { label: 'Incomes', data: [] }];
                    this.lineChartLabels = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "Decmeber"];
                    this.lineChartOptions = {
                        responsive: true,
                        scales: {
                            yAxes: [{
                                    ticks: {
                                        beginAtZero: true
                                    }
                                }]
                        }
                    };
                    this.lineChartLegend = true;
                    this.lineChartType = 'line';
                    this.doughnutChartLabels = ['Download Sales', 'In-Store Sales', 'Mail-Order Sales'];
                    this.doughnutChartData = [350, 450, 100];
                    this.doughnutChartType = 'doughnut';
                    this.barChartLabels = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"];
                    this.barChartType = 'bar';
                    this.barChartLegend = true;
                    this.barChartData = [
                        { data: [65, 59, 80, 81, 56, 55, 40], label: 'Expenses' }
                    ];
                    this.barChartOptions = {
                        scales: {
                            yAxes: [{
                                    ticks: {
                                        beginAtZero: true
                                    }
                                }]
                        }
                    };
                    this.chartColors = [
                        {
                            backgroundColor: 'rgba(148,159,177,0.2)',
                            borderColor: 'rgba(148,159,177,1)',
                            pointBackgroundColor: 'rgba(148,159,177,1)',
                            pointBorderColor: '#fff',
                            pointHoverBackgroundColor: '#fff',
                            pointHoverBorderColor: 'rgba(148,159,177,0.8)'
                        },
                        {
                            backgroundColor: 'rgba(33,83,96,0.2)',
                            borderColor: 'rgba(77,83,96,1)',
                            pointBackgroundColor: 'rgba(77,83,96,1)',
                            pointBorderColor: '#fff',
                            pointHoverBackgroundColor: '#fff',
                            pointHoverBorderColor: 'rgba(77,83,96,1)'
                        }
                    ];
                }
                LineChartDemoComponent.prototype.ngOnInit = function () {
                    var _this = this;
                    var _lineChartExpenseData = [];
                    var _lineChartIncomeData = [];
                    this.homeService
                        .getExpenses()
                        .subscribe(
                    /* happy path */ function (successResponse) {
                        successResponse.forEach(function (f) {
                            _lineChartExpenseData.push(f.amount);
                            _lineChartIncomeData.push(f.amount + 20000);
                            _this.lineChartData = [];
                            _this.lineChartData.push({ label: 'Expenses', data: _lineChartExpenseData });
                            _this.lineChartData.push({ label: 'Incomes', data: _lineChartIncomeData });
                            _this.refresh_chart();
                        });
                    }, 
                    /* error path */ function (errResponse) {
                        console.log(errResponse);
                    }, 
                    /* onCompleted */ function () {
                        console.log('getExpenses completed');
                    });
                };
                LineChartDemoComponent.prototype.refresh_chart = function () {
                    var _this = this;
                    setTimeout(function () {
                        if (_this.chart && _this.chart.chart && _this.chart.chart.config) {
                            _this.chart.chart.config.data.datasets = _this.lineChartData;
                            _this.chart.chart.update();
                        }
                    });
                };
                // events
                LineChartDemoComponent.prototype.chartClicked = function (e) {
                };
                LineChartDemoComponent.prototype.chartHovered = function (e) {
                };
                __decorate([
                    core_1.ViewChild(ng2_charts_1.BaseChartDirective), 
                    __metadata('design:type', ng2_charts_1.BaseChartDirective)
                ], LineChartDemoComponent.prototype, "chart", void 0);
                LineChartDemoComponent = __decorate([
                    core_1.Component({
                        selector: 'mydiary-chart',
                        providers: [diary_homeService_js_1.HomeService],
                        template: "\n    <div>\n      <div class=\"row-fluid\">\n      <div class=\"col-md-6\">\n         <canvas baseChart width=\"521\" height=\"350\"\n                [datasets]=\"lineChartData\"\n                [labels]=\"lineChartLabels\"\n                [options]=\"lineChartOptions\"\n                [colors]=\"chartColors\"\n                [legend]=\"lineChartLegend\"\n                [chartType]=\"lineChartType\"\n                (chartHover)=\"chartHovered($event)\"\n                (chartClick)=\"chartClicked($event)\">\n          </canvas>\n       </div>\n       </div>\n      <div class=\"row-fluid\">\n      <div class=\"col-md-6\">\n         <canvas baseChart width=\"521\" height=\"350\"\n                [datasets]=\"barChartData\"\n                [labels]=\"barChartLabels\"\n                [colors]=\"chartColors\"\n                [options]=\"barChartOptions\"\n                [chartType]=\"barChartType\"\n                [legend]=\"barChartLegend\"\n                (chartHover)=\"chartHovered($event)\"\n                (chartClick)=\"chartClicked($event)\">\n          </canvas>\n       </div>\n       </div>\n    </div>\n  "
                    }),
                    core_1.Injectable(), 
                    __metadata('design:paramtypes', [http_1.Http, diary_homeService_js_1.HomeService])
                ], LineChartDemoComponent);
                return LineChartDemoComponent;
            }());
            exports_1("LineChartDemoComponent", LineChartDemoComponent);
            MonthlyChart = (function () {
                function MonthlyChart() {
                }
                return MonthlyChart;
            }());
        }
    }
});
//# sourceMappingURL=diary.homeComponent.js.map
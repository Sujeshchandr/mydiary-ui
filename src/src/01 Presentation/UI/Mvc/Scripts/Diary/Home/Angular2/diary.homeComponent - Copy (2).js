System.register(["@angular/core"], function (exports_1, context_1) {
    "use strict";
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1, LineChartDemoComponent1;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [
            function (core_1_1) {
                core_1 = core_1_1;
            }
        ],
        execute: function () {
            LineChartDemoComponent1 = /** @class */ (function () {
                function LineChartDemoComponent1() {
                    this.doughnutChartLabels = ['Download Sales', 'In-Store Sales', 'Mail-Order Sales'];
                    this.doughnutChartData = [350, 450, 100];
                    this.doughnutChartType = 'doughnut';
                    this.barChartLabels = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"];
                    this.barChartType = 'bar';
                    this.barChartLegend = true;
                    this.barChartData = [
                        { data: [65, 59, 80, 81, 56, 55, 40], label: 'Expenses' }
                        //{ data: [28, 48, 40, 19, 86, 27, 90], label: 'Series B' }
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
                    // lineChart
                    this.lineChartData = [
                        { data: [65, 59, 80, 81, 56, 55, 40, 36, 40, 55, 40, 60], label: 'Expenses' },
                        { data: [28, 48, 40, 19, 86, 27, 90, 80, 81, 56, 55, 40], label: 'Incomes' }
                    ];
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
                            backgroundColor: 'rgba(77,83,96,0.2)',
                            borderColor: 'rgba(77,83,96,1)',
                            pointBackgroundColor: 'rgba(77,83,96,1)',
                            pointBorderColor: '#fff',
                            pointHoverBackgroundColor: '#fff',
                            pointHoverBorderColor: 'rgba(77,83,96,1)'
                        }
                    ];
                    this.name = 'Angular2';
                }
                // events
                LineChartDemoComponent1.prototype.chartClicked = function (e) {
                    console.log(e);
                };
                LineChartDemoComponent1.prototype.chartHovered = function (e) {
                    console.log(e);
                };
                LineChartDemoComponent1 = __decorate([
                    core_1.Component({
                        selector: 'mydiary-chart',
                        template: "\n    <div>\n      <div class=\"row-fluid\">\n      <div class=\"col-md-6\">\n         <canvas baseChart width=\"521\" height=\"350\"\n                [datasets]=\"lineChartData\"\n                [labels]=\"lineChartLabels\"\n                [options]=\"lineChartOptions\"\n                [colors]=\"chartColors\"\n                [legend]=\"lineChartLegend\"\n                [chartType]=\"lineChartType\"\n                (chartHover)=\"chartHovered($event)\"\n                (chartClick)=\"chartClicked($event)\">\n          </canvas>\n       </div>\n       </div>\n      <div class=\"row-fluid\">\n      <div class=\"col-md-6\">\n         <canvas baseChart width=\"521\" height=\"350\"\n                [datasets]=\"barChartData\"\n                [labels]=\"barChartLabels\"\n                [colors]=\"chartColors\"\n                [options]=\"barChartOptions\"\n                [chartType]=\"barChartType\"\n                [legend]=\"barChartLegend\"\n                (chartHover)=\"chartHovered($event)\"\n                (chartClick)=\"chartClicked($event)\">\n          </canvas>\n       </div>\n       </div>\n    </div>\n  "
                    }),
                    __metadata("design:paramtypes", [])
                ], LineChartDemoComponent1);
                return LineChartDemoComponent1;
            }());
            exports_1("LineChartDemoComponent1", LineChartDemoComponent1);
        }
    };
});
//# sourceMappingURL=diary.homeComponent%20-%20Copy%20(2).js.map
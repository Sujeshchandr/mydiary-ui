System.register(["@angular/core"], function (exports_1, context_1) {
    "use strict";
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __moduleName = context_1 && context_1.id;
    var core_1, LineChartDemoComponent;
    return {
        setters: [
            function (core_1_1) {
                core_1 = core_1_1;
            }
        ],
        execute: function () {
            LineChartDemoComponent = (function () {
                function LineChartDemoComponent() {
                    // lineChart
                    this.lineChartData = [
                        { data: [65, 59, 80, 81, 56, 55, 40], label: 'Series A' },
                        { data: [28, 48, 40, 19, 86, 27, 90], label: 'Series B' },
                        { data: [18, 48, 77, 9, 100, 27, 40], label: 'Series C' }
                    ];
                    this.lineChartLabels = ['January', 'February', 'March', 'April', 'May', 'June', 'July'];
                    this.lineChartOptions = {
                        responsive: true
                    };
                    this.lineChartColors = [
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
                        },
                        {
                            backgroundColor: 'rgba(148,159,177,0.2)',
                            borderColor: 'rgba(148,159,177,1)',
                            pointBackgroundColor: 'rgba(148,159,177,1)',
                            pointBorderColor: '#fff',
                            pointHoverBackgroundColor: '#fff',
                            pointHoverBorderColor: 'rgba(148,159,177,0.8)'
                        }
                    ];
                    this.lineChartLegend = true;
                    this.lineChartType = 'line';
                }
                LineChartDemoComponent.prototype.randomize = function () {
                    var _lineChartData = new Array(this.lineChartData.length);
                    for (var i = 0; i < this.lineChartData.length; i++) {
                        _lineChartData[i] = { data: new Array(this.lineChartData[i].data.length), label: this.lineChartData[i].label };
                        for (var j = 0; j < this.lineChartData[i].data.length; j++) {
                            _lineChartData[i].data[j] = Math.floor((Math.random() * 100) + 1);
                        }
                    }
                    this.lineChartData = _lineChartData;
                };
                // events
                LineChartDemoComponent.prototype.chartClicked = function (e) {
                    console.log(e);
                };
                LineChartDemoComponent.prototype.chartHovered = function (e) {
                    console.log(e);
                };
                return LineChartDemoComponent;
            }());
            LineChartDemoComponent = __decorate([
                core_1.Component({
                    selector: 'line-chart-demo',
                    //template: `<h1>Home page is loading using {{name}}</h1>`
                    templateUrl: "./Scripts/Diary/Home/Angular2/line-chart-demo.html"
                })
            ], LineChartDemoComponent);
            exports_1("LineChartDemoComponent", LineChartDemoComponent);
        }
    };
});
//# sourceMappingURL=diary.homeComponent - Copy.js.map
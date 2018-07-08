import { Component, OnInit, AfterContentInit, Injectable, ViewChild } from '@angular/core';
import { Http, Response, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { BaseChartDirective } from 'ng2-charts/ng2-charts';

import { HomeService } from './Services/diary.homeService.js';

@Component({
    selector: 'mydiary-chart',
    providers: [HomeService],
    template: `
    <div>
      <div class="row-fluid">
      <div class="col-md-6">
         <canvas baseChart width="521" height="350"
                [datasets]="lineChartData"
                [labels]="lineChartLabels"
                [options]="lineChartOptions"
                [colors]="chartColors"
                [legend]="lineChartLegend"
                [chartType]="lineChartType"
                (chartHover)="chartHovered($event)"
                (chartClick)="chartClicked($event)">
          </canvas>
       </div>
       </div>
      <div class="row-fluid">
      <div class="col-md-6">
         <canvas baseChart width="521" height="350"
                [datasets]="barChartData"
                [labels]="barChartLabels"
                [colors]="chartColors"
                [options]="barChartOptions"
                [chartType]="barChartType"
                [legend]="barChartLegend"
                (chartHover)="chartHovered($event)"
                (chartClick)="chartClicked($event)">
          </canvas>
       </div>
       </div>
    </div>
  `
})

@Injectable()
export class LineChartDemoComponent implements OnInit {

    @ViewChild(BaseChartDirective) chart: BaseChartDirective;

    private baseUrl: string = 'http://localhost:6488/v1';
    private name: string;
    private userId = 1;
    private year = 2015;

    lineChartData: Array<MonthlyChart> = [{ label: 'Expenses', data: [] }, { label: 'Incomes', data: [] }];
    lineChartLabels: Array<any> = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "Decmeber"];
    lineChartOptions: any = {
        responsive: true,
        scales: {
            yAxes: [{
                ticks: {
                    beginAtZero: true
                }
            }]
        }
    };
    lineChartLegend: boolean = true;
    lineChartType: string = 'line';

    constructor(private http: Http,
                private homeService: HomeService) { }

    ngOnInit() {

        let _lineChartExpenseData: Array<number> = [];
        let _lineChartIncomeData: Array<number> = [];

        this.homeService
            .getExpenses()
            .subscribe(
            /* happy path */ (successResponse) => {
                successResponse.forEach(f => {

                    _lineChartExpenseData.push(f.amount);
                    _lineChartIncomeData.push(f.amount + 20000);

                    this.lineChartData = [];

                    this.lineChartData.push({ label: 'Expenses', data: _lineChartExpenseData })
                    this.lineChartData.push({ label: 'Incomes', data: _lineChartIncomeData });

                    this.refresh_chart();

                });
            },
            /* error path */(errResponse) => {
                console.log(errResponse);
            },
            /* onCompleted */() => {
                console.log('getExpenses completed');
            });
    }

    public doughnutChartLabels: string[] = ['Download Sales', 'In-Store Sales', 'Mail-Order Sales'];
    public doughnutChartData: number[] = [350, 450, 100];
    public doughnutChartType: string = 'doughnut';

    public barChartLabels: string[] = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"];
    public barChartType: string = 'bar';
    public barChartLegend: boolean = true;
    public barChartData: any[] = [
        { data: [65, 59, 80, 81, 56, 55, 40], label: 'Expenses' }
        //{ data: [28, 48, 40, 19, 86, 27, 90], label: 'Series B' }
    ];
    public barChartOptions: any = {
        scales: {
            yAxes: [{
                ticks: {
                    beginAtZero: true
                }
            }]
        }
    };

    public chartColors: Array<any> = [
        { // grey for expenses
            backgroundColor: 'rgba(148,159,177,0.2)',
            borderColor: 'rgba(148,159,177,1)',
            pointBackgroundColor: 'rgba(148,159,177,1)',
            pointBorderColor: '#fff',
            pointHoverBackgroundColor: '#fff',
            pointHoverBorderColor: 'rgba(148,159,177,0.8)'
        },
        { // dark grey for incomes
            backgroundColor: 'rgba(33,83,96,0.2)',
            borderColor: 'rgba(77,83,96,1)',
            pointBackgroundColor: 'rgba(77,83,96,1)',
            pointBorderColor: '#fff',
            pointHoverBackgroundColor: '#fff',
            pointHoverBorderColor: 'rgba(77,83,96,1)'
        }
    ];

    private refresh_chart() {
        setTimeout(() => {
            if (this.chart && this.chart.chart && this.chart.chart.config) {
                this.chart.chart.config.data.datasets = this.lineChartData;
                this.chart.chart.update();
            }
        });
    }

    // events
    public chartClicked(e: any): void {
    }

    public chartHovered(e: any): void {
    }
}

class MonthlyChart {

    public data: Array<number>;

    public label: string;
}


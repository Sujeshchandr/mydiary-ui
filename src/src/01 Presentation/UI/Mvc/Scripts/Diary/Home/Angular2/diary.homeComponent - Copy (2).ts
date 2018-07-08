import { Component } from '@angular/core';

@Component({
    selector: 'mydiary-chart',
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

export class LineChartDemoComponent1 {
    name: string;
    constructor() {
        this.name = 'Angular2'
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

    // lineChart
    public lineChartData: Array<any> = [
        { data: [65, 59, 80, 81, 56, 55, 40, 36, 40, 55, 40, 60], label: 'Expenses' },
        { data: [28, 48, 40, 19, 86, 27, 90, 80, 81, 56, 55, 40], label: 'Incomes' }
    ];
    public lineChartLabels: Array<any> = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "Decmeber"];
    public lineChartOptions: any = {
        responsive: true,
        scales: {
            yAxes: [{
                ticks: {
                    beginAtZero: true
                }
            }]
        }
    };

    public lineChartLegend: boolean = true;
    public lineChartType: string = 'line';


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
            backgroundColor: 'rgba(77,83,96,0.2)',
            borderColor: 'rgba(77,83,96,1)',
            pointBackgroundColor: 'rgba(77,83,96,1)',
            pointBorderColor: '#fff',
            pointHoverBackgroundColor: '#fff',
            pointHoverBorderColor: 'rgba(77,83,96,1)'
        }
    ];

    // events
    public chartClicked(e: any): void {
        console.log(e);
    }

    public chartHovered(e: any): void {
        console.log(e);
    }
}



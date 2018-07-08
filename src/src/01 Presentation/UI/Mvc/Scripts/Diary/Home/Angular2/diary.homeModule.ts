import { NgModule } from '@angular/core';

import { BrowserModule } from '@angular/platform-browser';
import { ChartsModule } from 'ng2-charts/ng2-charts';
import { HttpModule } from '@angular/http';

import { LineChartDemoComponent } from './diary.homeComponent';

@NgModule({
    imports: [BrowserModule, ChartsModule, HttpModule ],
    declarations: [LineChartDemoComponent],
    bootstrap: [LineChartDemoComponent]
})
export class AppModule { }

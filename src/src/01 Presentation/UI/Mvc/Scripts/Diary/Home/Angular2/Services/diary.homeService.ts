import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Injectable()
export class HomeService {

    private baseUrl: string = 'http://localhost:6488/v1';
    private name: string;
    private userId = 1;
    private year = 2015;

    constructor(private http: Http) {
    }

    getExpenses(): Observable<Array<any>> {
        let people$ = this.http
            .get(`${this.baseUrl}/expenses/get.monthlySummary(userId=${this.userId},year=${this.year})`, { headers: this.getHeaders() })
            .map(ToExpenses)
            .catch(handleError);

        return people$;
    }

    private getHeaders() {
        let headers = new Headers();
        headers.append('Accept', 'application/json');
        return headers;
    }    
}

function ToExpenses(response: Response): Array<any> {
    return response.json().charts;
}

// this could also be a private method of the component class
function handleError(error: any) {
    // log error
    // could be something more sofisticated
    let errorMsg = error.message || `An unhandled exception occured in the server and we couldn't retrieve your data!`
    console.error(errorMsg);

    // throw an application level error
    return Observable.throw(errorMsg);
}
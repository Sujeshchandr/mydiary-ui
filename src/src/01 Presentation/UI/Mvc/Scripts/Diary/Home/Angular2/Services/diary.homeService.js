System.register(["@angular/core", "@angular/http", "rxjs/Observable", "rxjs/add/operator/map", "rxjs/add/operator/catch", "rxjs/add/observable/throw"], function (exports_1, context_1) {
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
    var __moduleName = context_1 && context_1.id;
    function ToExpenses(response) {
        return response.json().charts;
    }
    // this could also be a private method of the component class
    function handleError(error) {
        // log error
        // could be something more sofisticated
        var errorMsg = error.message || "An unhandled exception occured in the server and we couldn't retrieve your data!";
        console.error(errorMsg);
        // throw an application level error
        return Observable_1.Observable.throw(errorMsg);
    }
    var core_1, http_1, Observable_1, HomeService;
    return {
        setters: [
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (http_1_1) {
                http_1 = http_1_1;
            },
            function (Observable_1_1) {
                Observable_1 = Observable_1_1;
            },
            function (_1) {
            },
            function (_2) {
            },
            function (_3) {
            }
        ],
        execute: function () {
            HomeService = (function () {
                function HomeService(http) {
                    this.http = http;
                    this.baseUrl = 'http://localhost:6488/v1';
                    this.userId = 1;
                    this.year = 2015;
                }
                HomeService.prototype.getExpenses = function () {
                    var people$ = this.http
                        .get(this.baseUrl + "/expenses/get.monthlySummary(userId=" + this.userId + ",year=" + this.year + ")", { headers: this.getHeaders() })
                        .map(ToExpenses)
                        .catch(handleError);
                    return people$;
                };
                HomeService.prototype.getHeaders = function () {
                    var headers = new http_1.Headers();
                    headers.append('Accept', 'application/json');
                    return headers;
                };
                return HomeService;
            }());
            HomeService = __decorate([
                core_1.Injectable(),
                __metadata("design:paramtypes", [http_1.Http])
            ], HomeService);
            exports_1("HomeService", HomeService);
        }
    };
});
//# sourceMappingURL=diary.homeService.js.map
var DRCommonModule = angular.module('diary.common', []);

DRCommonModule.factory('$httpBatch', ['$http', function ($http) {


    //TODO--> Override all $http methods to support  CORS including IE<-9
    var $httpCorsScope = {};

    $httpCorsScope.batch = function (options) { //THIS IS NOT WORKING $$$$$$$$$$$$$$$$

        var requestData = {

            batchRequests: []
        };


        var requestData = {

            __batchRequests: [{
                __changeRequests: [
                    {
                        requestUri: "/odata/Customers(1)",
                        method: "PATCH",
                        data: {
                            Name: "S Ravi Kiran"
                        }
                    },
                    {
                        requestUri: "/odata/Customers(2)",
                        data: {
                            Name: "Alex Moore",
                            Department: "Marketing",
                            City: "Atlanta"
                        }
                    },
        {
            requestUri: "/odata/Customers",
            method: "POST",
            data: {
                Name: "Henry",
                Department: "IT",
                City: "Las Vegas"
            }
        }
                ]
            }]
        };
    };

    return $httpCorsScope;
}]); //NOT WORKING >>>

DRCommonModule.provider('$httpCors', function () {

    var optionsConfig = {

        rootUrl: '',
        withCredentials: false
    };

    this.defaults = optionsConfig,

    this.$get = function ($http,$q) {//provider is expecting a $get method ,
        //if we use factory no need to specify $get , as angular will internally assign $get to the passed factory function

        var httpCorsProvider = function (options) {

            this.defaults = {

                url: '',
                type: 'GET',
                dataType: optionsConfig.dataType,
                withCredentials: optionsConfig.withCredentials,
                data: {}
            };

            this.defaults = angular.extend(defaults, options);

            if (optionsConfig.rootUrl != '')
                this.defaults.url = defaults.rootUrl + this.defaults.url;

            return $http(this.defaults);

        };

        httpCorsProvider.get = function (url, options) {

           return $http.get(url, options);

        };

        httpCorsProvider.post = function (url, data, options) {

            return $http.post(url, data, options);
        };

        httpCorsProvider.put = function (url, data, options) {

            return $http.put(url, data, options);
        };

        httpCorsProvider.delete = function (url, options) {

            return $http.delete(url, options);
        };

        return httpCorsProvider;
    };

});
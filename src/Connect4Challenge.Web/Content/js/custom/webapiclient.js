app.factory('WebApiClient', function ($http, $location, $timeout, $q) {
    var client = {};

    /* private */
    var o = {
        url: null,
        postList: [],
        requestMethod: null,
        host: "/api",
        ctrlUrl: "/game",
    };

    /* public */

    client.HttpPost = function (actionUrl, postList) {
        o.url = this.host + this.ctrlUrl + actionUrl;
        o.postList = postList;
        o.requestMethod = "POST";
        return this.call();
    }
    client.HttpGet = function (actionUrl) {
        o.url = o.host + o.ctrlUrl + actionUrl;
        o.requestMethod = "GET";
        return call();
    }

    function call() {
        pv = o.postList;
        var promise = $http({
            url: o.url,
            method: o.requestMethod,
            //withCredentials: true,
            data: JSON.stringify(pv),
        });

        /* Anstatt .success() und .error(), arbeiten wir hier mit .then()
         Hauptgrund ist, dass die Chain von allen .success()- und .error()-Functions PARALLEL(!) ausgeführt werden.
         Das parallele Ausführen der Funktionen im Controller ist vielleicht noch ok - aber nicht im WebApiClient, wo der Response noch bearbeitet werden muss, bevor er an die Controller geht.
         Die Chain von .then() werden der Reihe nach ausgeführt, wie sie dem Promise hinzugefügt wurden!
         Erklärung: http://stackoverflow.com/a/23805864 

        (angularjs v1.3.15) Zeile 9406 bzw. 9413 beweist das Gegenteil:
         
         promise.success = function(fn) {
                promise.then(function(response) {
                        fn(response.data, response.status, response.headers, config);
                });
                
                return promise;
         };*/

        var deferred = $q.defer();
        promise.then(
            function (response) {
                /* HTTP success */
                deferred.resolve(response.data);
            },
            function (response) {
                /* HTTP error */
                deferred.reject(response.data);
            });
        return promise; //this.extendPromise(deferred.promise);
    }
    function redirectToLoginPage() {
        var returnUrl = window.location;
        window.location = "/security/login" + "?returnUrl=" + window.location;
    };

    return client;
});
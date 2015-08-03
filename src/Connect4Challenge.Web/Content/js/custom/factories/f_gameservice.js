app.factory('Gameservice', function (WebApiClient) {
    var service = {};

    service.test = function () {
        var promise = WebApiClient.HttpGet("/");

        return promise;
    }

    return service;
});
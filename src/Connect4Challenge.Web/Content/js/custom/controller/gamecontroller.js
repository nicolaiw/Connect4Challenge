app.controller('Game_Ctrl', function ($scope, Gameservice) {
    $scope.result = null;

    $scope.init = function () {
        Gameservice.test()
            .success(function (response) {
                $scope.result = response;
            })
            .error(function (data, status, headers, config) {
                alert("data: " + data + "\n" +
                    "data: " + data + "\n" +
                    "status: " + status + "\n" +
                    "headers: " + headers + "\n" +
                    "config: " + config + "\n");
            })
            .finally(function () {

            });
    };
});
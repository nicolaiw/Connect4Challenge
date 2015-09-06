app.controller('Game_Ctrl', function ($scope, Gameservice, Upload) {
    $scope.result = null;
    $scope.assemblyfiles = [];
    $scope.playerAssemblyfile = null;
    $scope.enemyAssemblyfile = null;
    $scope.bots = [];
    $scope.selectedBot = null;

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
        $scope.bots.push({ name: 'Random Bot', key: 1 });
    };

    $scope.$watch('assemblyfile', function () {
        $scope.assemblyfiles[0] = $scope.playerAssemblyfile;
    });

    $scope.$watch('enemyAssemblyfile', function () {
        $scope.assemblyfiles[1] = $scope.enemyAssemblyfile;
    });

    $scope.upload = function () {
        if ($scope.assemblyFiles && $scope.assemblyFiles[0]) {
            Upload.upload({
                url: '/api/Game/UploadAssembly',
                file: $scope.assemblyFiles,
                data: { BotType: $scope.selectedBot.key }
            });
        }
    };
});
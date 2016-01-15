app.controller('Game_Ctrl', function ($scope, Gameservice, Upload) {
    $scope.result = null;
    $scope.assemblyFiles = [];
    $scope.playerAssemblyFile = null;
    $scope.enemyAssemblyFile = null;
    //$scope.bots = [];
    //$scope.selectedBot = null;

    $scope.init = function () {
        //Gameservice.test()
        //    .success(function (response) {
        //        $scope.result = response;
        //    })
        //    .error(function (data, status, headers, config) {
        //        alert("data: " + data + "\n" +
        //            "data: " + data + "\n" +
        //            "status: " + status + "\n" +
        //            "headers: " + headers + "\n" +
        //            "config: " + config + "\n");
        //    })
        //    .finally(function () {

        //    });
        //$scope.bots.push({ name: 'Random Bot', key: 1 });
    };

    $scope.$watch('playerAssemblyFile', function () {
        $scope.assemblyFiles[0] = $scope.playerAssemblyFile;
    });

    $scope.$watch('enemyAssemblyFile', function () {
        $scope.assemblyFiles[1] = $scope.enemyAssemblyFile;
    });

    $scope.upload = function () {
        if ($scope.playerAssemblyFile && $scope.enemyAssemblyFile) {
            Upload.upload({
                url: '/api/Game/UploadAssembly',
                file: $scope.assemblyFiles,
                //data: { BotType: $scope.selectedBot.key }
            }).then(function(res) {
                $scope.result = res.data;
            });
        }
    };
});
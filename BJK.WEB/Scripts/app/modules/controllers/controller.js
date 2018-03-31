App.controller('userCtrl', ['$scope', 'AppService', "ngDialog", function ($scope, AppService, ngDialog) {

    $scope.userlist = {};

    $scope.GetUserDetails = function () {
        AppService.getUsers().then(function (data) {
            console.log("get user data");
            console.log(data);
            console.log("status : " + data.status);

            if (data.status == true) {
                $scope.userlist = data.result;
                console.log("userlist : " + $scope.userlist);
            }
            else {

            }
        });
    };


    $scope.OpenCreateUserDialog = function () {
        ngDialog.open({
            template: 'firstDialogId',
            //controller: 'InsideCtrl',
            className: 'ngdialog-theme-default'
        });
    }

}]);
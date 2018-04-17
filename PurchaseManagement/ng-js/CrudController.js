var app = angular.module("CrudDemo",[]);
app.controller("ListCtrl", function ($scope, $http) {
    GetList();
    function GetList() {
        $http.get("/Test/List").success(function (result) {
            $scope.list = result;
        });
    };

    $scope.edit = function (item) {
        console.log(item);
    };
});
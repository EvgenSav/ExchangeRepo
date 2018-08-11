var app = angular.module("app", []);
app.controller("mainController", function ($http) {
    this.AddTool = function (toolName) {
        $http.post(`http://${document.location.host}/Home/AddTool?toolname=${toolName}`).then(
                function successCallback(response) {
                    console.log(response.data);
                }, function errorCallbasck(response) {
                }
                );
    }
});
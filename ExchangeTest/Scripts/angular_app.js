var app = angular.module("app", []);
app.factory("myFactory", function () {
    let transactions;
    return {
        Tools: [],
        Participants: [],
        get Transactions() {
            return this.transactions;
        },
        set Transactions(value) {
            this.transactions = value;
        }
    };
});
app.controller("mainController", function ($http, myFactory) {
    this.myFactory = myFactory;
    this.GetData = function () {
        $http.get(`http://${document.location.host}/Home/GetData`).then(
            function successCallback(response) {
                console.log(response.data);
                myFactory.Tools = response.data.Tools;
                myFactory.Participants = response.data.Participants;
                myFactory.Transactions = response.data.Transactions;
                //convert datetime to date js using moment
                for (var i = 0; i < response.data.Transactions.length; i++) {
                    myFactory.Transactions[i].Time = moment(response.data.Transactions[i].Time).toDate();
                }
            }, function errorCallback(response) {

            }
        );
    };
    this.AddTool = function (toolName) {
        $http.post(`http://${document.location.host}/Home/AddTool?toolname=${toolName}`).then(
            function successCallback(response) {
                console.log(response.data);
            }, function errorCallbasck(response) {
            }
        );
    };
    this.DrawGraph = function (transactions) {
        var ctx = document.getElementById('myChart').getContext('2d');
        var transactionsPrices = [];
        var transactionsIds = [];
        for (let i = 0; i < transactions.length; i++) {
            transactionsPrices.push(transactions[i].Price);
        }
        for (let i = 0; i < transactions.length; i++) {
            transactionsIds.push(transactions[i].Time);
        }
        var chart = new Chart(ctx, {
            // The type of chart we want to create
            type: 'line',

            // The data for our dataset
            data: {
                labels: transactionsIds,
                datasets: [{
                    label: "My First dataset",
                    backgroundColor: 'rgb(255, 99, 132)',
                    borderColor: 'rgb(255, 99, 132)',
                    data: transactionsPrices,
                }]
            },
            // Configuration options go here
            options: {}
        });
    }

});
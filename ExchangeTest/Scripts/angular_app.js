var app = angular.module("app", []);
app.factory("myFactory", function () {
    var transactionsPrices = [];
    var transactionsIds = [];
    let chart;
    let data = {
        labels: transactionsIds,
        datasets: [{
            label: "My First dataset",
            backgroundColor: 'rgb(255, 99, 132)',
            borderColor: 'rgb(255, 99, 132)',
            data: transactionsPrices
        }]
    };
    let options = {};
    return {
        Tools: [],
        Participants: [],
        get Transactions() {
            return this.transactions;
        },
        set Transactions(value) {
            this.transactions = value;
            for (let i = 0; i < this.transactions.length; i++) {
                transactionsPrices.push(this.transactions[i].Price);
            }
            for (let i = 0; i < this.transactions.length; i++) {
                transactionsIds.push(this.transactions[i].Time);
            }
        },
        DrawGraph: function (transactions) {
            var ctx = document.getElementById('myChart').getContext('2d');

            chart = new Chart(ctx, {
                // The type of chart we want to create
                type: 'line',

                // The data for our dataset
                data: data,
                // Configuration options go here
                options: options
            });
        },
        UpdateGraph: function () {
            let grlength = chart.data.datasets[0].data.length;
            chart.data.datasets[0].data = transactionsPrices;
            chart.data.labels = transactionsIds;
            chart.update();
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
    this.AddTool = function (name) {
        $http.post(`http://${document.location.host}/Home/AddTool?name=${name}`).then(
            function successCallback(response) {
                console.log(response.data);
            }, function errorCallbasck(response) {
            }
        );
    };
    this.AddParticipant = function (name) {
        $http.post(`http://${document.location.host}/Home/AddParticipant?name=${name}`).then(
            function successCallback(response) {
                console.log(response.data);
            }, function errorCallbasck(response) {
            }
        );
    };
    this.AddTransaction = function (transaction) {
        console.log(transaction.selectedTool.Name);
        console.log(transaction.selectedBuyer.Name);
        console.log(transaction.selectedSeller.Name);
        console.log(transaction.price);
        console.log(transaction.amount);
        let data = {
            ToolId: transaction.selectedTool.Id,
            BuyerId: transaction.selectedBuyer.Id,
            SellerId: transaction.selectedSeller.Id,
            Price: transaction.price,
            Amount: transaction.amount
        }
        $http.post(`http://${document.location.host}/Home/AddTransaction/`,transaction).then(
            function successCallback(response) {
                console.log(response.data);
                myFactory.Transactions = response.data;
                //convert datetime to date js using moment
                for (var i = 0; i < response.data.length; i++) {
                    myFactory.Transactions[i].Time = moment(response.data[i].Time).toDate();
                }
                myFactory.UpdateGraph();
            }, function errorCallbasck(response) {
            }
        );
    };

});
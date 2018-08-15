var app = angular.module("app", []);
app.factory("myFactory", function () {
    let transactions;
    return {
        Tools: [],
        Participants: [],
        Transactions: [],
        TransactionPrice: [],
        TransactionTime: []
    };
});
app.controller("mainController", function ($http, myFactory) {
    this.myFactory = myFactory;
    let ParseTimeData = function () {
        //adjust datetime like in RazorView
        for (var i = 0; i < myFactory.Transactions.length; i++) {
            myFactory.Transactions[i].TimeF = myFactory.Transactions[i].Time;
            let time = moment(myFactory.Transactions[i].TimeF).format('DD.MM.YYYY HH:DD:SS');
            myFactory.Transactions[i].TimeF = time;
        }
    };
    this.InitApp = function () {
        $http.get(`http://${document.location.host}/Home/GetData`).then(
            function successCallback(response) {
                console.log(response.data);
                myFactory.Tools = response.data.Tools;
                myFactory.Participants = response.data.Participants;
                myFactory.Transactions = response.data.Transactions;
                ParseTimeData();
            }, function errorCallback(response) {
            }
        );
    };
    this.AddTool = function (name) {
        $http.post(`http://${document.location.host}/Home/AddTool?name=${name}`).then(
            function successCallback(response) {
                console.log(response.data);
                myFactory.Tools = response.data;
            }, function errorCallbasck(response) {
            }
        );
    };
    this.AddParticipant = function (name) {
        $http.post(`http://${document.location.host}/Home/AddParticipant?name=${name}`).then(
            function successCallback(response) {
                console.log(response.data);
                myFactory.Participants = response.data;
            }, function errorCallbasck(response) {
            }
        );
    };
    this.AddTransaction = function (transaction) {
        let tsData = {
            Tool: transaction.selectedTool,
            Buyer: transaction.selectedBuyer,
            Seller: transaction.selectedSeller,
            Price: transaction.price,
            Amount: transaction.amount
        };
        console.log(tsData);
        $http.post(`http://${document.location.host}/Home/AddTransaction/`, tsData).then(
            function successCallback(response) {
                console.log(response.data);
                myFactory.Transactions = response.data;
                ParseTimeData();
            }, function errorCallbasck(response) {
            }
        );
    };
});
app.controller("anotherController", function ($http, myFactory) {
    this.myFactory = myFactory;
    let ParseChartData = function (transactions) {
        transactions.forEach(function (ts, i, transactions) {
            myFactory.TransactionTime.push(ts.Time);
            myFactory.TransactionPrice.push(ts.Price);
        });
    };

    let ParseTimeData = function () {
        //adjust datetime like in RazorView
        for (var i = 0; i < myFactory.Transactions.length; i++) {
            myFactory.Transactions[i].TimeF = myFactory.Transactions[i].Time;
            let time = moment(myFactory.Transactions[i].TimeF).format('DD.MM.YYYY HH:DD:SS');
            myFactory.Transactions[i].TimeF = time;
        }
    };
    
    this.InitApp = function () {
        $http.get(`http://${document.location.host}/Home/GetData`).then(
            function successCallback(response) {
                console.log(response.data);
                myFactory.Tools = response.data.Tools;
                myFactory.Participants = response.data.Participants;
                myFactory.Transactions = response.data.Transactions;

                ParseTimeData();
                ParseChartData(response.data.Transactions);
                DrawGraph();
            }, function errorCallback(response) {
            }
        );
    };

    this.AddTransaction = function (transaction) {
        let tsData = {
            Tool: transaction.selectedTool,
            Buyer: transaction.selectedBuyer,
            Seller: transaction.selectedSeller,
            Price: transaction.price,
            Amount: transaction.amount
        };
        $http.post(`http://${document.location.host}/Home/AddTransaction/`, tsData).then(
            function successCallback(response) {
                console.log(response.data);
                myFactory.Transactions = response.data;
                ParseTimeData();
                myFactory.TransactionTime = [];
                myFactory.TransactionPrice = [];
                ParseChartData(myFactory.Transactions);
                UpdateGraph();
            }, function errorCallbasck(response) {
            }
        );
    };
    let chart;
    let chartData = {
        labels: [],
        datasets: [{
            label: "Transaction price",
            //backgroundColor: 'rgb(255, 99, 132)',
            borderColor: 'rgb(255, 99, 132)',
            data: []
        }]
    };
    let options = {};

    let DrawGraph = function () {
        var ctx = document.getElementById('myChart2').getContext('2d');

        chart = new Chart(ctx, {
            // The type of chart we want to create
            type: 'line',
            // The data for our dataset
            data: chartData,
            // Configuration options go here
            options: {
                scales: {
                    xAxes: [{
                        type: 'time',
                        time: {
                            unit: 'day',
                            displayFormats: {
                                day: 'MMM D'
                            }
                        }
                    }]
                }
            }
        });
        chart.data.datasets[0].data = myFactory.TransactionPrice;
        chart.data.labels = myFactory.TransactionTime;
        chart.update();
    };
    let UpdateGraph = function () {
        chart.data.datasets[0].data = myFactory.TransactionPrice;
        chart.data.labels = myFactory.TransactionTime;
        chart.update();
    };
});

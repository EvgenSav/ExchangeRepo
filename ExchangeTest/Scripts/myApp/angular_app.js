var app = angular.module("app", []);
//app.config(['$locationProvider', function ($locationProvider) {
//    $locationProvider.html5Mode({
//        enabled: true,
//        requireBase: false
//    });
//}]);
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


    DrawGraph = function () {
        var ctx = document.getElementById('myChart').getContext('2d');

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
    }

    UpdateGraph = function () {
        chart.data.datasets[0].data = myFactory.TransactionPrice;
        chart.data.labels = myFactory.TransactionTime;
        chart.update();
    }
    let options = {};
    this.myFactory = myFactory;
    this.InitApp = function () {
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
                //prepare data for chart
                var arr = myFactory.Transactions;
                arr.forEach(function (ts, i, arr) {
                    myFactory.TransactionTime.push(ts.Time);
                    myFactory.TransactionPrice.push(ts.Price);
                });
                DrawGraph();
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
        let tsData = {
            ToolId: transaction.selectedTool.Id,
            BuyerId: transaction.selectedBuyer.Id,
            SellerId: transaction.selectedSeller.Id,
            Price: transaction.price,
            Amount: transaction.amount
        }
        $http.post(`http://${document.location.host}/Home/AddTransaction/`, tsData).then(
            function successCallback(response) {
                console.log(response.data);
                myFactory.Transactions[response.data.Id] = response.data;
                //convert datetime to date js using moment
                myFactory.Transactions[response.data.Id].Time = moment(response.data.Time).toDate();

                myFactory.TransactionTime.push(myFactory.Transactions[response.data.Id].Time);
                myFactory.TransactionPrice.push(myFactory.Transactions[response.data.Id].Price);

                UpdateGraph();
            }, function errorCallbasck(response) {
            }
        );
    };
});
app.controller("anotherController", function ($http, myFactory) {
    this.myFactory = myFactory;
    this.InitApp = function () {
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
                //prepare data for chart
                var arr = myFactory.Transactions;
                arr.forEach(function (ts, i, arr) {
                    myFactory.TransactionTime.push(ts.Time);
                    myFactory.TransactionPrice.push(ts.Price);
                });
                DrawGraph();
            }, function errorCallback(response) {
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


    DrawGraph = function () {
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
    }

    this.AddTransaction = function (transaction) {
        let tsData = {
            ToolId: transaction.selectedTool.Id,
            BuyerId: transaction.selectedBuyer.Id,
            SellerId: transaction.selectedSeller.Id,
            Price: transaction.price,
            Amount: transaction.amount
        }
        $http.post(`http://${document.location.host}/Home/AddTransaction/`, tsData).then(
            function successCallback(response) {
                console.log(response.data);
                myFactory.Transactions[response.data.Id] = response.data;
                //convert datetime to date js using moment
                myFactory.Transactions[response.data.Id].Time = moment(response.data.Time).toDate();

                myFactory.TransactionTime.push(myFactory.Transactions[response.data.Id].Time);
                myFactory.TransactionPrice.push(myFactory.Transactions[response.data.Id].Price);

                UpdateGraph();
            }, function errorCallbasck(response) {
            }
        );
    };

    UpdateGraph = function () {
        chart.data.datasets[0].data = myFactory.TransactionPrice;
        chart.data.labels = myFactory.TransactionTime;
        chart.update();
    }
    let options = {};
});
app.controller("navController", function (myFactory, $location) {
    this.myFactory = myFactory;
    this.IsActive = function (loc) {
        return loc === $location.path;
    }
});
var app = angular.module('CarApp', []);

app.directive('ngConfirmClick', [
    function () {
        return {
            link: function (scope, element, attr) {
                var msg = attr.ngConfirmClick || "Are you sure?";
                var clickAction = attr.confirmedClick;
                element.bind('click', function (event) {
                    if (window.confirm(msg)) {
                        scope.$eval(clickAction)
                    }
                });
            }
        };
    }])

app.controller('CarController', ['$scope', '$http', function ($scope, $http) {

    $scope.cars = [];
    $scope.head = "Vehicles administration."
    $scope.IsNew = true
    $scope.brands = {
        model: null,
        availableOptions: [
            { name: 'Ford' },
            { name: 'Chevrolet' },
            { name: 'Nissan' },
            { name: 'Volkswagen' },
            { name: 'Toyota' },
            { name: 'Honda' },
        ]
    };
    $scope.btnText = "Save";

    $scope.Save = function (form) {
        $scope.btnText = "Please Wait...";

        if (form) {
            form.$setPristine();
            form.$setUntouched();
        }



        if ($scope.IsNew) {
            console.log("New Record")
            jQuery.ajax({
                url: 'Car/CreateCar',
                type: "POST",
                data: { newcar: $scope.register },
                async: false,
                success: function (response) {
                    $scope.btnText = "Save";
                    $scope.register = null
                    alert("Successful operation.");
                    $scope.getcars()
                    $scope.$apply()
                },
                error: function (response) {
                    console.log("Error in request")
                    console.log(response)
                }
            });
        } else {
            console.log("Update")
            jQuery.ajax({
                url: 'Car/UpdateCar',
                type: "POST",
                data: { cartoupdate: $scope.register },
                async: false,
                success: function (response) {
                    $scope.btnText = "Save";
                    $scope.register = null
                    alert("Successful operation.");
                    $scope.getcars()
                    $scope.IsNew = true
                    $scope.$apply()
                },
                error: function (response) {
                    console.log("Error in request")
                    console.log(response)
                    $scope.IsNew = false
                }
            });
        }
    }


    $scope.reset = function (form) {
        $scope.register = {};
        $scope.IsNew = true
        if (form) {
            form.$setPristine();
            form.$setUntouched();
        }
    };



    $scope.loadCar = function (id) {
        let params = { carid: id }
        jQuery.ajax({
            url: 'Car/GerCar',
            type: "POST",
            data: params,
            async: false,
            success: function (response) {
                $scope.register = JSON.parse(response)
                $scope.$apply()
            },
            error: function (response) {
                console.log("Error in request")
                console.log(response)
            }
        });
        $scope.IsNew = false
    }

    $scope.deleteCar = function (id) {
        let params = { carid: id }
        jQuery.ajax({
            url: 'Car/DeleteCar',
            type: "POST",
            data: params,
            async: false,
            success: function (response) {
                alert("Successful operation.");
                $scope.getcars()
                $scope.$apply()
            },
            error: function (response) {
                console.log("Error in request")
                console.log(response)
            }
        });
    }

    $scope.getcars = function () {
        jQuery.ajax({
            url: 'Car/GetCars',
            type: "GET",
            async: false,
            success: function (response) {
                $scope.cars = JSON.parse(response)
                $scope.$apply()
            },
            error: function (response) {
                console.log("Error in request")
                console.log(response)
            }
        });
    }

    // Load grid
    $scope.getcars()

}]);
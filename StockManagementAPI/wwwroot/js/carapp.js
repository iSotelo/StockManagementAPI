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

    $scope.Save = function () {
        $scope.btnText = "Please Wait...";

        var formData = new FormData();
        console.log($scope.register)
        formData.append('CarId', $scope.register.CarId);
        formData.append('Model', $scope.register.Model);
        formData.append('Description', $scope.register.Description);
        formData.append('Year', $scope.register.Year);
        formData.append('Brand', $scope.register.Brand);
        formData.append('Kilometers', $scope.register.Kilometers);
        formData.append('Price', $scope.register.Price);
        formData.append('Image', $('#imageurl')[0].files[0]);

        if ($scope.IsNew) {
            $.ajax({
                url: 'Car/CreateCar',
                type: "POST",
                processData: false,  // tell jQuery not to process the data
                contentType: false,  // tell jQuery not to set contentType
                data: formData,
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
            $.ajax({
                url: 'Car/UpdateCar',
                type: "POST",
                processData: false,  // tell jQuery not to process the data
                contentType: false,  // tell jQuery not to set contentType
                data: formData,
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
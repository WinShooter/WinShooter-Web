/// <reference path="/Scripts/App/common.js" />
/// <reference path="/Scripts/angular-ui/ui-bootstrap.js" />
/// <reference path="/Scripts/angular-ui/ui-bootstrap-tpls.js" />
/// <reference path="/Scripts/angular.js" />
/// <reference path="/Scripts/angular-route.js" />
/// <reference path="/Scripts/angular-sanitize.js"/>

// Here the module for the station page
angular.module('winshooter').controller('UserEditController', function ($scope, $routeParams, $modal, $http, $location, usersFactory) {
    console.log("PatrolsController: starting");
    if ($routeParams.competitionId !== undefined) {
        $scope.sharedData.competitionId = $routeParams.competitionId;
    }

    $scope.notInitialized = true;
    $scope.showUserInfo = false;
    $scope.userInfo = {};

    $scope.init = function() {
        console.log("UserEditController: Initializing.");

        console.log("Quering for user " + $scope.sharedData.currentUser.UserId);
        $scope.patrols = usersFactory.query({ UserId: $scope.sharedData.currentUser.UserId }, function (data) {

            console.log("UserEditController: Got " + JSON.stringify(data));

            $scope.notInitialized = false;
            $scope.showUserInfo = true;
            $scope.userInfo = data;

        }, function(data) {
            console.log("UserEditController: Failed to get current user: " + JSON.stringify(data));

            var error = "Misslyckades med att hämta din information";
            if (data !== undefined && data.data !== undefined && data.data.ResponseStatus !== undefined && data.data.ResponseStatus.Message !== undefined) {
                error += ":<br />" + JSON.stringify(data.data.ResponseStatus.Message);
            } else {
                error += ".";
            }
            // Show error dialog.
            var modal = $modal.open({
                templateUrl: 'errorModalContent',
                controller: DialogConfirmController,
                resolve: {
                    items: function() {
                        return {
                            header: "Ett fel inträffade",
                            body: error
                        };
                    }
                }
            });
        });
    };

    $scope.$watch('sharedData.currentUser.$resolved', function () {
        console.log("UserEditController: AppController has changed sharedData.currentUser.");

        if ($scope.sharedData.currentUser.$resolved) {
            console.log("UserEditController: AppController has resolved sharedData.currentUser. Run our initialization");
            $scope.init();
        }
    });

    $scope.update = function () {
        $scope.userInfo.$save();

        $location.url("/");
    }

    $scope.continue = function() {
        $location.url("/");
    }

    $scope.cancel = function () {
        $location.url("/");
    }
});

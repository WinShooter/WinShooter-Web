/// <reference path="/Scripts/App/common.js" />
/// <reference path="/Scripts/ui-bootstrap-0.9.0.js" />
/// <reference path="/Scripts/ui-bootstrap-tpls-0.9.0.js" />
/// <reference path="/Scripts/angular.js" />
/// <reference path="/Scripts/angular-route.js" />
/// <reference path="/Scripts/angular-sanitize.js"/>

// Here the module for the station page
angular.module('winshooter').controller('PatrolsController', function ($scope, $routeParams, $modal, $http, patrolsFactory) {
    console.log("PatrolsController: starting");
    if ($routeParams.competitionId !== undefined) {
        $scope.sharedData.competitionId = $routeParams.competitionId;
    }

    $scope.userCanCreatePatrol = false;
    $scope.userCanUpdatePatrol = false;
    $scope.userCanDeletePatrol = false;
    $scope.isEditing = false;
    $scope.stationToEdit = {};

    // Attributes for adding new competition
    $scope.patrols = [];

    $scope.initRights = function () {
        console.log("PatrolsController: Rights initializing.");
        $scope.userCanCreatePatrol = $scope.sharedData.userHasRight("CreatePatrol");
        $scope.userCanUpdatePatrol = $scope.sharedData.userHasRight("UpdatePatrol");
        $scope.userCanDeletePatrol = $scope.sharedData.userHasRight("DeletePatrol");
        console.log("PatrolsController: Rights initialized.");
    };

    $scope.init = function () {
        console.log("PatrolsController: Initializing.");
        $scope.isEditing = false;
        $scope.patrolToEdit = {};
        $scope.initRights();

        console.log("Quering for stations");
        $scope.patrols = patrolsFactory.query({ CompetitionId: $scope.sharedData.competitionId }, function (data) {
            // Nothing to do here. Carry on!
            console.log("PatrolsController: Got " + data.length + " patrols.");
        }, function (data) {
            console.log("PatrolsController: Failed to get stations: " + JSON.stringify(data));

            var error = "Misslyckades med att hämta stationerna";
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
                    items: function () {
                        return {
                            header: "Ett fel inträffade",
                            body: error
                        };
                    }
                }
            });
        });
    };
    
    $scope.$watch('sharedData.competitionId', function () {
        console.log("PatrolsController: competition has changed, re-initialize");
        $scope.init();
    });

    $scope.$watch('sharedData.rights', function () {
        console.log("PatrolsController: rights has changed, re-initialize");
        $scope.initRights();
    });

    $scope.addNewPatrol = function () {
        var patrol = {
            CompetitionId: $scope.sharedData.competitionId,
            PatrolId: "",
            "PatrolNumber": -1,
            "StartTime": "",
            "PatrolClass": 0
        };

        $http.post(patrolsApiUrl, patrol)
            .success(function (data, status) {
                $scope.init();
            }).error(function (data, status) {
                var error = "Misslyckades med att lägga till tävlingen";
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
                        items: function () {
                            return {
                                header: "Ett fel inträffade",
                                body: error
                            };
                        }
                    }
                });
            });
    };

    $scope.startEdit = function (station) {
        console.log("Starting to edit patrol: " + JSON.stringify(station));
        $scope.stationToEdit = station;
        $scope.isEditing = true;
    };

    $scope.saveEdit = function () {
        console.log("Save patrolToEdit: " + JSON.stringify($scope.stationToEdit));
        $scope.stationToEdit.$save(function() {
                $scope.init();
            }
        );
    };

    $scope.deleteStation = function () {
        console.log("Delete edited patrol: " + JSON.stringify($scope.stationToEdit));
        $scope.stationToEdit.$delete();
        $scope.init();
    };

    $scope.cancelEdit = function () {
        console.log("Cancel editing patrol");
        $scope.init();
    };
});

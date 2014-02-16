﻿/// <reference path="/Scripts/App/common.js" />
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

    $scope.userCanCreateStation = false;
    $scope.userCanUpdateStation = false;
    $scope.userCanDeleteStation = false;
    $scope.isEditing = false;
    $scope.stationToEdit = {};

    // Attributes for adding new competition
    $scope.stations = [];

    $scope.findStation = function (stationToSelect) {
        for (var i = 0; i < $scope.stations.length; i++) {
            if (stationToSelect.StationId === $scope.stations[i].StationId) {
                return $scope.stations[i];
            }
        }

        return undefined;
    };

    $scope.initRights = function () {
        console.log("PatrolsController: Rights initializing.");
        $scope.userCanCreateStation = $scope.sharedData.userHasRight("CreatePatrol");
        $scope.userCanUpdateStation = $scope.sharedData.userHasRight("UpdatePatrol");
        $scope.userCanDeleteStation = $scope.sharedData.userHasRight("DeletePatrol");
        console.log("PatrolsController: Rights initialized.");
    };

    $scope.init = function () {
        console.log("PatrolsController: Initializing.");
        $scope.isEditing = false;
        $scope.stationToEdit = {};
        $scope.initRights();

        console.log("Quering for stations");
        $scope.stations = patrolsFactory.query({ CompetitionId: $scope.sharedData.competitionId }, function (data) {
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
        var station = {
            CompetitionId: $scope.sharedData.competitionId,
            PatrolId: "",
            "StationNumber": -1,
            "Distinguish": false,
            "NumberOfShots": 6,
            "NumberOfTargets": 3,
            "Points": false
        };

        $http.post(stationsApiUrl, station)
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
        console.log("Starting to edit station: " + JSON.stringify(station));
        $scope.stationToEdit = station;
        $scope.isEditing = true;
    };

    $scope.saveEdit = function () {
        console.log("Update stationToEdit with checkbox values");
        $scope.stationToEdit.Points = $("label[for='EditedStationIsPoints']").hasClass('checked');
        $scope.stationToEdit.Distinguish = $("label[for='EditedStationIsDistinguish']").hasClass('checked');

        console.log("Save stationToEdit: " + JSON.stringify($scope.stationToEdit));
        $scope.stationToEdit.$save(function() {
                $scope.init();
            }
        );
    };

    $scope.deleteStation = function () {
        console.log("Delete edited station: " + JSON.stringify($scope.stationToEdit));
        $scope.stationToEdit.$delete();
        $scope.init();
    };

    $scope.cancelEdit = function () {
        console.log("Cancel editing station");
        $scope.init();
    };
});

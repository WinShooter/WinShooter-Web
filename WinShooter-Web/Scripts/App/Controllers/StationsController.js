/// <reference path="/Scripts/App/common.js" />
/// <reference path="/Scripts/ui-bootstrap-0.9.0.js" />
/// <reference path="/Scripts/ui-bootstrap-tpls-0.9.0.js" />
/// <reference path="/Scripts/angular.js" />
/// <reference path="/Scripts/angular-route.js" />
/// <reference path="/Scripts/angular-sanitize.js"/>

// Here the module for the station page
angular.module('winshooter').controller('StationsController', function ($scope, $routeParams, $modal, $http, currentUserFactory, stationsFactory) {
    console.log("StationsController: starting");
    if ($routeParams.competitionId !== undefined) {
        $scope.sharedData.competitionId = $routeParams.competitionId;
    }

    $scope.userCanCreateStation = false;
    $scope.userCanUpdateStation = false;
    $scope.userCanDeleteStation = false;

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
        console.log("StationsController: Rights initializing.");
        $scope.userCanCreateStation = $scope.sharedData.userHasRight("CreateStation");
        $scope.userCanUpdateStation = $scope.sharedData.userHasRight("UpdateStation");
        $scope.userCanDeleteStation = $scope.sharedData.userHasRight("DeleteStation");
        console.log("StationsController: Rights initialized.");
    };

    $scope.init = function () {
        console.log("StationsController: Initializing.");
        $scope.initRights();

        console.log("Quering for stations");
        $scope.stations = stationsFactory.query({ CompetitionId: $scope.sharedData.competitionId }, function (data) {
            // Nothing to do here. Carry on!
            console.log("Got " + data.length + " stations.");
        }, function (data) {
            console.log("Filed to get stations: " + JSON.stringify(data));

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
        console.log("StationsController: competition has changed, re-initialize");
        $scope.init();
    });

    $scope.$watch('sharedData.rights', function () {
        console.log("StationsController: rights has changed, re-initialize");
        $scope.initRights();
    });

    $scope.addNewStation = function () {
        var station = {
            CompetitionId: $scope.sharedData.competitionId,
            StationId: "",
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

    $scope.updateStation = function (station) {
        alert(JSON.station);

        var stationToUpdate = $scope.findStation(station);
    };

    $scope.deleteStation = function (station) {
        alert(station);
    };
});

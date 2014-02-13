/// <reference path="/Scripts/App/common.js" />
/// <reference path="/Scripts/ui-bootstrap-0.9.0.js" />
/// <reference path="/Scripts/ui-bootstrap-tpls-0.9.0.js" />
/// <reference path="/Scripts/angular.js" />
/// <reference path="/Scripts/angular-route.js" />
/// <reference path="/Scripts/angular-sanitize.js"/>

// Here the module for the station page
angular.module('winshooter').controller('StationsController', function ($scope, $routeParams, $modal, $http, currentUserFactory, stationsFactory) {
    if ($routeParams.competitionId != undefined) {
        window.competitionId = $routeParams.competitionId;
    }

    $scope.currentUser = { IsLoggedIn: false, Rights: [] };
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

    $scope.init = function () {
        $scope.currentUser = currentUserFactory.search({ CompetitionId: window.competitionId }, function (currentUserData) {
            // Get data
            if (currentUserData.IsLoggedIn) {
                $scope.displayName = currentUserData.DisplayName;
                $scope.rights = currentUserData.CompetitionRights;

                $scope.userCanCreateStation = -1 !== $.inArray("CreateStation", $scope.rights);
                $scope.userCanUpdateStation = -1 !== $.inArray("UpdateStation", $scope.rights);
                $scope.userCanDeleteStation = -1 !== $.inArray("DeleteStation", $scope.rights);
            } else {
                $scope.userCanUpdateStation = false;
            }
        }, function (data) {
            var error = "Misslyckades med att hämta användaruppgifter";
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

        $scope.stations = stationsFactory.query({ CompetitionId: window.competitionId }, function () {
            // Nothing to do here. Carry on!
        }, function () {
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

    // Init our page
    $scope.init();

    $scope.addNewStation = function () {
        var station = {
            CompetitionId: window.competitionId,
            StationId: "",
            "StationNumber": -1,
            "Distinguish": false,
            "NumberOfShots": 3,
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

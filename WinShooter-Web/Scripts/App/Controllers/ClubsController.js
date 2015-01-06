/// <reference path="/Scripts/App/common.js" />
/// <reference path="/Scripts/angular-ui/ui-bootstrap.js" />
/// <reference path="/Scripts/angular-ui/ui-bootstrap-tpls.js" />
/// <reference path="/Scripts/angular.js" />
/// <reference path="/Scripts/angular-route.js" />
/// <reference path="/Scripts/angular-sanitize.js"/>

// Here the module for the station page
angular.module('winshooter').controller('ClubsController', function($scope, $routeParams, $modal, $http, clubsFactory) {
    console.log("ClubsController: instanciating");
    if ($routeParams.competitionId !== undefined) {
        $scope.sharedData.competitionId = $routeParams.competitionId;
    }

    $scope.userCanCreateClub = false;
    $scope.userCanUpdateClub = false;
    $scope.userCanDeleteClub = false;
    $scope.isEditing = false;
    $scope.isAdding = false;
    $scope.clubToEdit = {};
    
    // The clubs
    $scope.clubs = [];

    $scope.initRights = function() {
        console.log("ClubsController: Rights initializing.");
        $scope.userCanCreateClub = $scope.sharedData.userHasRight("CreateClub");
        $scope.userCanUpdateClub = $scope.sharedData.userHasRight("UpdateClub");
        $scope.userCanDeleteClub = $scope.sharedData.userHasRight("DeleteClub");
        console.log("ClubsController: Rights initialized.");
    };

    $scope.init = function() {
        console.log("ClubsController: Initializing.");
        $scope.isEditing = false;
        $scope.isAdding = false;
        $scope.clubToEdit = {};
        $scope.initRights();

        console.log("Quering for clubs");
        $scope.clubs = clubsFactory.query({}, function (data) {
            // Nothing to do here. Carry on!
            console.log("ClubsController: Got clubs: " + JSON.stringify(data));
            if ($scope.clubs.length > 0) {
                $scope.clubToEdit = $scope.clubs[0];
            }

        }, function(data) {
            console.log("ClubsController: Failed to get clubs: " + JSON.stringify(data));

            var error = "Misslyckades med att hämta klubbarna";
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

    $scope.$watch('sharedData.rights', function() {
        console.log("ClubsController: rights has changed, re-initialize");
        $scope.initRights();
    });

    $scope.init();

    $scope.addNewClub = function () {
        var club = new clubsFactory();
        club.Bankgiro = "";
        club.ClubID = "";
        club.Country = "SE";
        club.Email = "";
        club.Name = "Klubbens namn";
        club.Plusgiro = "";

        console.log("Setting a new club to edit: " + JSON.stringify(club));

        $scope.clubToEdit = club;
        $scope.isEditing = true;
        $scope.isAdding = true;

        /*$http.post(clubsApiUrl, club)
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
            });*/
    };

    $scope.startEdit = function () {
        console.log("Starting to edit: " + JSON.stringify($scope.clubToEdit));
        $scope.isEditing = true;
    };

    $scope.save = function () {
        console.log("Save edited: " + JSON.stringify($scope.clubToEdit));
        $scope.clubToEdit.$save(function () {
                $scope.init();
            }
        );
    };

    $scope.delete = function () {
        console.log("Delete: " + JSON.stringify($scope.clubToEdit));
        $scope.clubToEdit.$delete(function () {
            $scope.init();
        });
    };

    $scope.cancel = function () {
        console.log("Cancel editing");
        $scope.init();
    };
});

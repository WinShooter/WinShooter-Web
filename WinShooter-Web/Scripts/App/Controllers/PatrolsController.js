/// <reference path="/Scripts/App/common.js" />
/// <reference path="/Scripts/ui-bootstrap-0.9.0.js" />
/// <reference path="/Scripts/ui-bootstrap-tpls-0.9.0.js" />
/// <reference path="/Scripts/angular.js" />
/// <reference path="/Scripts/angular-route.js" />
/// <reference path="/Scripts/angular-sanitize.js"/>

// Here the module for the station page
angular.module('winshooter').controller('PatrolsController', function ($scope, $routeParams, $modal, currentUserFactory, stationsFactory) {
    $scope.currentUser = { IsLoggedIn: false, Rights: [] };

    $scope.userCanUpdatePatrol = false;

    // Attributes
    $scope.patrols = [];

    // Init our page
    init();

    function init() {
        $scope.currentUser = currentUserFactory.query(function (currentUserData) {
            // Get data
            $scope.shouldShowLoginLink = !currentUserData.IsLoggedIn;
            $scope.isLoggedIn = currentUserData.IsLoggedIn;

            if (currentUserData.IsLoggedIn) {
                $scope.displayName = currentUserData.DisplayName;
                $scope.rights = currentUserData.CompetitionRights;

                $scope.userCanUpdatePatrol = -1 !== $.inArray("UpdatePatrol", $scope.rights);
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
    }
});
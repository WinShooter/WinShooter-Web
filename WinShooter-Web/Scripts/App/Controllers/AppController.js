/// <reference path="/Scripts/App/common.js" />
/// <reference path="/Scripts/ui-bootstrap-0.9.0.js" />
/// <reference path="/Scripts/ui-bootstrap-tpls-0.9.0.js" />
/// <reference path="/Scripts/angular.js" />
/// <reference path="/Scripts/angular-route.js" />
/// <reference path="/Scripts/angular-sanitize.js"/>

// Here's the viewModel for the header
angular.module('winshooter').controller('AppController', function ($rootScope, $scope, $routeParams, $location, $modal, currentUserFactory) {
    console.log("Starting AppController. routeParams:" + JSON.stringify($routeParams));
    // Attributes that will be shared with client scopes
    $scope.sharedData = {};
    $scope.sharedData.userHasRight = function (right) {
        var toReturn = -1 !== $.inArray(right, $scope.sharedData.rights);
        console.log("SharedData: User has right '" + right + "': " + toReturn);
        return toReturn;
    };

    $scope.sharedData.competitionId = $routeParams.competitionId;

    $scope.sharedData.currentUser = { IsLoggedIn: false };
    $scope.sharedData.displayName = "";
    $scope.sharedData.isLoggedIn = false;
    $scope.sharedData.rights = [];

    // Attributes that is used by this controller or view
    $scope.shouldShowLoginLink = false;

    $scope.shouldShowCompetitionLink = false;
    $scope.shouldShowStationsLink = false;
    $scope.shouldShowPatrolsLink = false;
    $scope.shouldShowCompetitorsLink = false;

    $scope.shouldShowAddResultsLink = false;
    $scope.shouldShowEditRightsLink = false;
    $scope.shouldShowEditClubsLink = false;
    $scope.shouldShowEditWeaponsLink = false;

    $scope.shouldShowAboutLink = false;
    $scope.shouldShowPrivacyLink = false;

    $scope.init = function () {
        console.log("AppController: Initializing . competitionId=" + $scope.sharedData.competitionId);
        if ($scope.sharedData.competitionId === undefined || $scope.sharedData.competitionId === "") {
            console.log("AppController: competitionId is undefined or empty");
            $scope.shouldShowCompetitionLink = false;
            $scope.shouldShowStationsLink = false;
            $scope.shouldShowPatrolsLink = false;
            $scope.shouldShowCompetitorsLink = false;

            $scope.shouldShowAboutLink = true;
            $scope.shouldShowPrivacyLink = true;
        } else {
            console.log("AppController: competitionId is " + $scope.sharedData.competitionId);
            $scope.shouldShowCompetitionLink = true;
            $scope.shouldShowStationsLink = true;
            $scope.shouldShowPatrolsLink = true;
            $scope.shouldShowCompetitorsLink = true;

            $scope.shouldShowAboutLink = false;
            $scope.shouldShowPrivacyLink = false;
        }

        console.log("AppController: Retrieving user data");
        $scope.sharedData.currentUser = currentUserFactory.search({ CompetitionId: $scope.sharedData.competitionId }, function (currentUserData) {
            console.log("AppController: Got user data: " + JSON.stringify(currentUserData));
            if (currentUserData.IsLoggedIn === undefined) {
                return;
            }

            // Get data
            $scope.shouldShowLoginLink = !currentUserData.IsLoggedIn;
            $scope.sharedData.isLoggedIn = currentUserData.IsLoggedIn;

            if (currentUserData.IsLoggedIn) {
                $scope.sharedData.displayName = currentUserData.DisplayName;
                $scope.sharedData.rights = currentUserData.CompetitionRights;

                $scope.shouldShowAddResultsLink = $scope.sharedData.userHasRight("AddCompetitorResult");
                $scope.shouldShowEditRightsLink = $scope.sharedData.userHasRight("ReadUserCompetitionRole");
                $scope.shouldShowEditClubsLink = $scope.sharedData.userHasRight("UpdateClub");
                $scope.shouldShowEditWeaponsLink = $scope.sharedData.userHasRight("UpdateWeapon");
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
    };

    $scope.$watch('sharedData.competitionId', function () {
        console.log("AppController: competition has changed, re-initialize");
        $scope.init();
    });

    $scope.isActive = function (viewLocation) {
        var path = $location.path().substring(0, viewLocation.length);
        return viewLocation === path;
    };

    $scope.$on("competitionChanged", function () {
        $scope.init();
    });

    $scope.openCompetitionLocation = function (baseUrl) {
        baseUrl = baseUrl + $scope.sharedData.competitionId;
        $location.path(baseUrl);
    };

    $scope.openHome = function () {
        $location.path("/");
    };

    $scope.openCompetition = function () {
        $scope.openCompetitionLocation("/Home/Competition/");
    };

    $scope.openStations = function () {
        $scope.openCompetitionLocation("/Home/Stations/");
    };

    $scope.openClubs = function () {
        $scope.openCompetitionLocation("/Home/Clubs/");
    };

    $scope.openWeapons = function () {
        $scope.openCompetitionLocation("/Home/Weapons/");
    };

    $scope.openPatrols = function () {
        $scope.openCompetitionLocation("/Home/Patrols/");
    };

    $scope.openCompetitors = function () {
        $scope.openCompetitionLocation("/Home/Competitors/");
    };

    $scope.openAbout = function () {
        $location.path("/Home/About");
    };

    $scope.openPrivacy = function () {
        $location.path("/Home/Privacy");
    };
});

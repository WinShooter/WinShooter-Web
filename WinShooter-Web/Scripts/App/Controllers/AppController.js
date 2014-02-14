/// <reference path="/Scripts/App/common.js" />
/// <reference path="/Scripts/ui-bootstrap-0.9.0.js" />
/// <reference path="/Scripts/ui-bootstrap-tpls-0.9.0.js" />
/// <reference path="/Scripts/angular.js" />
/// <reference path="/Scripts/angular-route.js" />
/// <reference path="/Scripts/angular-sanitize.js"/>

// Here's the viewModel for the header
angular.module('winshooter').controller('AppController', function ($rootScope, $scope, $location, $modal, currentUserFactory) {
    // Attributes
    $scope.competitionId = window.competitionId;

    $scope.currentUser = { IsLoggedIn: false };
    $scope.displayName = "";
    $scope.isLoggedIn = false;
    $scope.rights = [];
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
        $scope.competitionId = window.competitionId;
        if (window.competitionId == undefined || window.competitionId == "") {
            $scope.shouldShowCompetitionLink = false;
            $scope.shouldShowStationsLink = false;
            $scope.shouldShowPatrolsLink = false;
            $scope.shouldShowCompetitorsLink = false;

            $scope.shouldShowAboutLink = true;
            $scope.shouldShowPrivacyLink = true;
        } else {
            $scope.shouldShowCompetitionLink = true;
            $scope.shouldShowStationsLink = true;
            $scope.shouldShowPatrolsLink = true;
            $scope.shouldShowCompetitorsLink = true;

            $scope.shouldShowAboutLink = false;
            $scope.shouldShowPrivacyLink = false;
        }

        $scope.currentUser = currentUserFactory.search({ CompetitionId: window.competitionId }, function (currentUserData) {
            if (currentUserData.IsLoggedIn == undefined) {
                return;
            }
            // Get data
            $scope.shouldShowLoginLink = !currentUserData.IsLoggedIn;
            $scope.isLoggedIn = currentUserData.IsLoggedIn;

            if (currentUserData.IsLoggedIn) {
                $scope.displayName = currentUserData.DisplayName;
                $scope.rights = currentUserData.CompetitionRights;

                $scope.shouldShowAddResultsLink = -1 !== $.inArray("AddCompetitorResult", $scope.rights);
                $scope.shouldShowEditRightsLink = -1 !== $.inArray("ReadUserCompetitionRole", $scope.rights);
                $scope.shouldShowEditClubsLink = -1 !== $.inArray("UpdateClub", $scope.rights);
                $scope.shouldShowEditWeaponsLink = -1 !== $.inArray("UpdateWeapon", $scope.rights);
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

    $scope.init();

    $scope.isActive = function (viewLocation) {
        var path = $location.path();
        return viewLocation === path;
    };

    $scope.$on("competitionChanged", function () {
        $scope.init();
    });

    $scope.openCompetitionLocation = function (baseUrl) {
        baseUrl = baseUrl + window.competitionId;
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

/// <reference path="/Scripts/App/common.js" />
/// <reference path="/Scripts/ui-bootstrap-0.9.0.js" />
/// <reference path="/Scripts/ui-bootstrap-tpls-0.9.0.js" />
/// <reference path="/Scripts/angular.js" />
/// <reference path="/Scripts/angular-route.js" />
/// <reference path="/Scripts/angular-sanitize.js"/>

angular.module('winshooter').controller('AccountLoggedInController', function ($rootScope, $scope, $location, $modal, currentUserFactory) {
    $scope.minimumTermsAccepted = 1;
    $scope.currentUser = { DisplayName: '' };
    $scope.firstTimeWinShooter = false;
    $scope.notInitialized = true;
    init();

    function init() {
        $scope.currentUser = currentUserFactory.search({ CompetitionId: window.competitionId }, function (currentUserData) {
            $scope.notInitialized = false;
            if (currentUserData.IsLoggedIn && currentUserData.HasAcceptedTerms >= $scope.minimumTermsAccepted) {
                $location.url('/Home/Index');
                return;
            }

            $scope.firstTimeWinShooter = true;
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

    $scope.accept = function () {
        $scope.currentUser.HasAcceptedTerms = $scope.minimumTermsAccepted;
        $scope.currentUser.$save(function () {
            window.competitionId = '';
            $rootScope.$broadcast("competitionChanged", {});
            $location.url("/");
        }, function (data) {
            // Show error dialog.
            var modal = $modal.open({
                templateUrl: 'errorModalContent',
                controller: DialogConfirmController,
                resolve: {
                    items: function () {
                        return {
                            header: "Ett fel inträffade",
                            body: "Kunde inte spara din acceptans"
                        };
                    }
                }
            });
        });
    };

    $scope.doIHaveAChoice = function () {
        // Show error dialog.
        var modal = $modal.open({
            templateUrl: 'informationModalContent',
            controller: DialogConfirmController,
            resolve: {
                items: function () {
                    return {
                        header: "Personuppgiftslagen",
                        body: "Nja, inte direkt. Du kan såklart fortsätta att titta på resultat och tävlingar, men vill du kunna se dina egna resultat eller hjälpa till i sekretariatet på en tävling, så måste du acceptera."
                    };
                }
            }
        });
    };
});


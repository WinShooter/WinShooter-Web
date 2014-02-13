/// <reference path="/Scripts/App/common.js" />
/// <reference path="/Scripts/ui-bootstrap-0.9.0.js" />
/// <reference path="/Scripts/ui-bootstrap-tpls-0.9.0.js" />
/// <reference path="/Scripts/angular.js" />
/// <reference path="/Scripts/angular-route.js" />
/// <reference path="/Scripts/angular-sanitize.js"/>


// Create the angular module
var winshooterModule = angular.module('winshooter', ['ngRoute', 'ngResource', 'ngSanitize', 'ui.bootstrap'])
    .config(function ($routeProvider, $locationProvider, $logProvider) {
    $routeProvider
        .when("/Account/Login", { templateUrl: "/partials/accountlogin.html" })
        .when("/Account/LoggedIn", { templateUrl: "/partials/accountloggedin.html", controller: "AccountLoggedInController" })
        .when("/Home/NewCompetition", { templateUrl: "/partials/newcompetition.html", controller: "NewCompetitionController" })
        .when("/Home/Competition/:competitionId", { templateUrl: "/partials/competition.html", controller: "CompetitionController" })
        .when("/Home/Stations/:competitionId", { templateUrl: "/partials/stations.html", controller: "StationsController" })
        .when("/Home/Patrols/:competitionId", { templateUrl: "/partials/patrols.html", controller: "PatrolsController" })
        .when("/Home/About", { templateUrl: "/partials/about.html" })
        .when("/Home/Privacy", { templateUrl: "/partials/privacy.html" })
        .otherwise({ templateUrl: "/partials/index.html", controller: "IndexController" });

    $locationProvider.html5Mode(true);
    $logProvider.debugEnabled(true);
});

// Create factory for competitions
winshooterModule.factory('competitionsFactory', [
    '$resource', function ($resource) {
        return $resource(competitionsApiUrl, {}, {
            query: { method: 'GET', isArray: true },
            search: { method: 'GET', isArray: false }
        });
    }
]);

// Create factory for competitions
winshooterModule.factory('competitionFactory', [
    '$resource', function ($resource) {
        return $resource(competitionApiUrl, {
            competitionId: '@CompetitionId'
        }, {
            query: { method: 'GET', isArray: true },
            search: { method: 'GET', isArray: false }
        });
    }
]);

// Create factory for current user
winshooterModule.factory('currentUserFactory', [
    '$resource', function ($resource) {
        return $resource(currentUserApiUrl, {
            competitionId: '@CompetitionId'
        }, {
            query: { method: 'GET', isArray: false },
            search: { method: 'GET', isArray: false },
            update: { method: 'POST', isArray: false }
        });
    }
]);

// Create factory for stations
winshooterModule.factory('stationsFactory', [
    '$resource', function ($resource) {
        return $resource(stationsApiUrl, {
            competitionId: '@CompetitionId',
            stationId: '@StationId'
        }, {
            query: { method: 'GET', isArray: true },
            search: { method: 'GET', isArray: false }
        });
    }
]);

// Here's the viewModel for the header
winshooterModule.controller('CurrentUserController', function ($rootScope, $scope, $location, $modal, currentUserFactory) {
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
        }, function(data) {
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

    $scope.init();

    $scope.isActive = function (viewLocation) {
        var path = $location.path();
        return viewLocation === path;
    };

    $scope.$on("competitionChanged", function () {
        $scope.init();
    });

    $scope.openCompetitionLocation = function(baseUrl) {
        baseUrl = baseUrl + window.competitionId;
        $location.path(baseUrl);
    };

    $scope.openHome = function() {
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

    $scope.openAbout = function() {
        $location.path("/Home/About");
    };

    $scope.openPrivacy = function () {
        $location.path("/Home/Privacy");
    };
});

winshooterModule.controller('AccountLoggedInController', function ($rootScope, $scope, $location, $modal, currentUserFactory) {
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
        }, function(data) {
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

// Here the module for the index page
winshooterModule.controller('IndexController', function ($rootScope, $scope, $modal, $location, competitionsFactory) {
    window.competitionId = '';
    $rootScope.$broadcast("competitionChanged", {});

    // Attributes for showing existing competition
    $scope.competitions = [];
    $scope.selectedCompetition = {
        Name: "Laddar tävlingar. Var god vänta...",
        StartDate: "",
        CompetitionId: "",
        IsPublic: false,
        UseNorwegianCount: false
    };

    init();

    function init() {
        $scope.competitions = competitionsFactory.query(function () {
            if ($scope.competitions.length > 0) {
                // We got some competitions back. Select first one.
                $scope.selectedCompetition = $scope.competitions[0];
            } else {
                $scope.selectedCompetition.Name = "Inga tävlingar hittades.";
            }

            $scope.competitions.forEach(function (competition) {
                competition.StartDate = new Date(competition.StartDate);
            });

        }, function (data) {
            var error = "Misslyckades med att hämta tävlingar";
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

    // Selects the current competition
    $scope.selectCompetitionOnServer = function () {
        var newLocation = "/Home/Competition/";
        if ($scope.selectedCompetition !== undefined) {
            window.competitionId = $scope.selectedCompetition.CompetitionId;
            newLocation = newLocation + $scope.selectedCompetition.CompetitionId;
            $location.path(newLocation);
            $rootScope.$broadcast("competitionChanged", {});
        }
    };
});

// Here the module for the competition page
winshooterModule.controller('CompetitionController', function ($scope, $routeParams, $modal, competitionFactory, currentUserFactory) {
    if ($routeParams.competitionId != undefined) {
        window.competitionId = $routeParams.competitionId;
    }

    // Attributes for showing existing competition
    $scope.currentUser = { IsLoggedIn: false, Rights: [] };
    $scope.competition = {};
    $scope.userCanUpdateCompetition = false;
    $scope.userCanDeleteCompetition = false;

    init();

    function init() {
        $scope.currentUser = currentUserFactory.search({ CompetitionId: window.competitionId }, function(currentUserData) {
            $scope.userCanUpdateCompetition = -1 !== $.inArray("UpdateCompetition", currentUserData.CompetitionRights);
            $scope.userCanDeleteCompetition = -1 !== $.inArray("DeleteCompetition", currentUserData.CompetitionRights);
        }, function(data) {
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

        $scope.competition = competitionFactory.search({ CompetitionId: window.competitionId }, function() {
            // Nothing to do here. Carry on!
            $scope.competition.StartDate = new Date($scope.competition.StartDate);
        }, function (data) {
            var error = "Misslyckades med att hämta tävlingar";
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

    $scope.updateCompetition = function() {
        if ($scope.competition === undefined) {
            // What? Should never happen.
            var error = "Du måste välja en tävling att uppdatera.";

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
            return false;
        }

        // For some reason angular cannot have a two way binding to a class.
        // Let's pick it out with jQuery instead.
        $scope.competition.IsPublic = $("label[for='selectedCompetitionIsPublic']").hasClass('checked');
        $scope.competition.UseNorwegianCount = $("label[for='selectedCompetitionUseNorwegianCount']").hasClass('checked');

        $scope.competition.$save(function() {
            // Show success dialog.
            var modal = $modal.open({
                templateUrl: 'informationModalContent',
                controller: DialogConfirmController,
                resolve: {
                    items: function () {
                        return {
                            header: "Sparad",
                            body: "Tävlingen sparades"
                        };
                    }
                }
            });
        }, function() {
            // Show error dialog.
            var modal = $modal.open({
                templateUrl: 'errorModalContent',
                controller: DialogConfirmController,
                resolve: {
                    items: function () {
                        return {
                            header: "Ett fel inträffade",
                            body: "Kunde inte spara tävlingen"
                        };
                    }
                }
            });
        });

        return false;
    };

    $scope.deleteCompetition = function() {
        if ($scope.competition === undefined) {
            // What? Should never happen.
            var error = "Du måste välja en tävling att radera.";
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
            return false;
        }

        // Show confirmation dialog first.
        var modal = $modal.open({
            templateUrl: 'confirmationModalContent',
            controller: DialogConfirmController,
            resolve: {
                items: function () {
                    return {
                        header: "Radera tävling",
                        body: "Är du säker på att du vill radera " + $scope.competition.Name + "?"
                    };
                }
            }
        });

        modal.result.then(function () {
            // In a production site we would submit the order via Ajax here.
            $scope.competition.$delete(function (data, status) {
                var newLocation = "/";
                window.location.href = newLocation;
            }, function (data, status) {
                var error = "Misslyckades med att radera tävling";
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
        }, function () {
            // Do nothing.
        });

        return false;
    };
});

// Controller for dialog modal instances.
var DialogConfirmController = function ($scope, $modalInstance, items) {
    $scope.header = items.header;
    $scope.body = items.body;

    $scope.ok = function () {
        $modalInstance.close();
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};

// Here the module for the competition page
winshooterModule.controller('NewCompetitionController', function ($rootScope, $scope, $modal, $http, $location, competitionFactory, currentUserFactory) {
    $scope.currentUser = { IsLoggedIn: false, Rights: [] };

    // Attributes for adding new competition
    $scope.Name = "";
    $scope.StartDate = new Date();
    $scope.StartDate.setHours(9);
    $scope.StartDate.setMinutes(0);
    $scope.StartDate.setSeconds(0);
    $scope.StartDate.setMilliseconds(0);

    $scope.IsPublic = false;
    $scope.UseNorwegianCount = false;

    // UI attributes
    $scope.datePickerFormat = 'yyyy-MM-dd';
    $scope.datePickerShowWeeks = true;
    $scope.datePickerDateOptions = { 'year-format': "'yy'", 'starting-day': 1 };

    // Init our page
    init();

    function init() {
        $scope.currentUser = currentUserFactory.search({ CompetitionId: window.competitionId }, function (currentUserData) {
            // Nothing to do here. Carry on!
        }, function () {
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

    $scope.addCompetition = function () {
        var competition = {
            CompetitionId: "",
            CompetitionType: "Field",
            Name: $scope.Name,
            UseNorwegianCount: $scope.UseNorwegianCount,
            IsPublic: $scope.IsPublic,
            StartDate: $scope.StartDate.toISOString()
        };

        $http.post(competitionApiUrl, competition)
            .success(function (data, status) {
                window.competitionId = data.CompetitionId;
                $rootScope.$broadcast("competitionChanged", {});
                var newLocation = "/Home/Competition/" + data.CompetitionId;
                $location.url(newLocation);
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
});

// Here the module for the station page
winshooterModule.controller('StationsController', function ($scope, $routeParams, $modal, $http, currentUserFactory, stationsFactory) {
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

    $scope.init = function() {
        $scope.currentUser = currentUserFactory.search({ CompetitionId: window.competitionId }, function(currentUserData) {
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
        }, function(data) {
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
                    items: function() {
                        return {
                            header: "Ett fel inträffade",
                            body: error
                        };
                    }
                }
            });
        });

        $scope.stations = stationsFactory.query({ CompetitionId: window.competitionId }, function() {
            // Nothing to do here. Carry on!
        }, function() {
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
        alert(JSON. station);

        var stationToUpdate = $scope.findStation(station);
    };

    $scope.deleteStation = function(station) {
        alert(station);
    };
});

// Here the module for the station page
winshooterModule.controller('PatrolsController', function ($scope, $routeParams, $modal, currentUserFactory, stationsFactory) {
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
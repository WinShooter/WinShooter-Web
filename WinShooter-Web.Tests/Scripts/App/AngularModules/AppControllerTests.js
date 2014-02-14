///<reference path="~/Scripts/jasmine.js"/>
///<reference path="~/../WinShooter-Web/Scripts/Angular.js"/>
///<reference path="~/../WinShooter-Web/Scripts/angular-resource.js" />
///<reference path="~/../WinShooter-Web/Scripts/angular-sanitize.js"/>
///<reference path="~/../WinShooter-Web/Scripts/angular-mocks.js" />
///<reference path="~/../WinShooter-Web/Scripts/jquery-2.0.3.js" />
///<reference path="~/../WinShooter-Web/Scripts/App/common.js"/>
///<reference path="~/../WinShooter-Web/Scripts/App/AngularModules.js"/>
///<reference path="~/../WinShooter-Web/Scripts/App/Controllers/AppController.js"/>

'use strict';

describe("AngularModules-AppController", function () {
    // For now, disable resharper callback
    //jasmine.getEnv().currentRunner_.finishCallback = function () { };

    //we'll use this scope in our tests
    var scope;
    debugger;

    //mock Application to allow us to inject our own dependencies
    beforeEach(angular.mock.module('winshooter'));

    // backend definition common for all tests
    var $httpBackend;
    beforeEach(angular.mock.inject(function ($injector) {
        // backend definition common for all tests
        $httpBackend = $injector.get('$httpBackend');
    }));

    // Check the httpBackend for all operations
    afterEach(function() {
        $httpBackend.verifyNoOutstandingExpectation();
    });

    // tests start here
    it('Check that the user isnt logged in, no competition selected', function () {
        angular.mock.inject(function ($rootScope, $controller, $location, $modal, currentUserFactory) {
            //create an empty scope
            scope = $rootScope.$new();

            // backend response
            $httpBackend.when('GET', '/api/currentuser?CompetitionId=').respond({ IsLoggedIn: false }, {});
            $httpBackend.expectGET('/api/currentuser?CompetitionId=');

            // Set the current cometition
            window.competitionId = "";

            //declare the controller and inject our empty scope
            var myController = $controller('AppController', {
                $rootScope: scope,
                $scope: scope,
                $location: $location,
                $modal: $modal,
                currentUserFactory: currentUserFactory
            });
            expect(myController).toBeDefined();

            // Run the HTTP request
            $httpBackend.flush();

            // Check the result after HTTP
            expect(scope.displayName).toEqual("");
            expect(scope.isLoggedIn).toEqual(false);
            expect(scope.rights).toEqual([]);
            expect(scope.shouldShowLoginLink).toEqual(true);

            expect(scope.shouldShowCompetitionLink).toEqual(false);
            expect(scope.shouldShowStationsLink).toEqual(false);
            expect(scope.shouldShowPatrolsLink).toEqual(false);
            expect(scope.shouldShowCompetitorsLink).toEqual(false);

            expect(scope.shouldShowAddResultsLink).toEqual(false);
            expect(scope.shouldShowEditRightsLink).toEqual(false);
            expect(scope.shouldShowEditClubsLink).toEqual(false);
            expect(scope.shouldShowEditWeaponsLink).toEqual(false);

            expect(scope.shouldShowAboutLink).toEqual(true);
            expect(scope.shouldShowPrivacyLink).toEqual(true);
        });
    });

    // tests start here
    it('Check that the user isnt logged in, competition is selected', function () {
        angular.mock.inject(function ($rootScope, $controller, $location, $modal, currentUserFactory) {
            //create an empty scope
            scope = $rootScope.$new();

            // backend response
            $httpBackend.when('GET', '/api/currentuser?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA').respond({ IsLoggedIn: false }, {});
            $httpBackend.expectGET('/api/currentuser?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA');

            // Set the current competition
            window.competitionId = "A6109CFD-C4D8-4003-A6E7-A2BB006A81EA";

            //declare the controller and inject our empty scope
            var myController = $controller('AppController', {
                $rootScope: scope,
                $scope: scope,
                $location: $location,
                $modal: $modal,
                currentUserFactory: currentUserFactory
            });

            expect(myController).toBeDefined();

            // Run the HTTP request
            $httpBackend.flush();

            expect(scope.currentUser.IsLoggedIn).toEqual(false);

            expect(scope.displayName).toEqual("");
            expect(scope.isLoggedIn).toEqual(false);
            expect(scope.rights).toEqual([]);
            expect(scope.shouldShowLoginLink).toEqual(true);

            expect(scope.shouldShowCompetitionLink).toEqual(true);
            expect(scope.shouldShowStationsLink).toEqual(true);
            expect(scope.shouldShowPatrolsLink).toEqual(true);
            expect(scope.shouldShowCompetitorsLink).toEqual(true);

            expect(scope.shouldShowAddResultsLink).toEqual(false);
            expect(scope.shouldShowEditRightsLink).toEqual(false);
            expect(scope.shouldShowEditClubsLink).toEqual(false);
            expect(scope.shouldShowEditWeaponsLink).toEqual(false);

            expect(scope.shouldShowAboutLink).toEqual(false);
            expect(scope.shouldShowPrivacyLink).toEqual(false);
        });
    });

    // tests start here
    it('Check that the user is logged in, competition isnt selected', function () {
        angular.mock.inject(function ($rootScope, $controller, $location, $modal, currentUserFactory) {
            //create an empty scope
            scope = $rootScope.$new();

            // backend response
            $httpBackend.when('GET', '/api/currentuser?CompetitionId=').respond({ IsLoggedIn: true, DisplayName: 'John Smith', CompetitionRights: ['right1', 'right2'] }, {});
            $httpBackend.expectGET('/api/currentuser?CompetitionId=');

            // Set the current competition
            window.competitionId = "";

            //declare the controller and inject our empty scope
            var myController = $controller('AppController', {
                $rootScope: scope,
                $scope: scope,
                $location: $location,
                $modal: $modal,
                currentUserFactory: currentUserFactory
            });
            expect(myController).toBeDefined();

            // Run the HTTP request
            $httpBackend.flush();

            expect(scope.currentUser.IsLoggedIn).toEqual(true);

            expect(scope.displayName).toEqual("John Smith");
            expect(scope.isLoggedIn).toEqual(true);
            expect(scope.rights).toEqual(['right1', 'right2']);
            expect(scope.shouldShowLoginLink).toEqual(false);

            expect(scope.shouldShowCompetitionLink).toEqual(false);
            expect(scope.shouldShowStationsLink).toEqual(false);
            expect(scope.shouldShowPatrolsLink).toEqual(false);
            expect(scope.shouldShowCompetitorsLink).toEqual(false);

            expect(scope.shouldShowAddResultsLink).toEqual(false);
            expect(scope.shouldShowEditRightsLink).toEqual(false);
            expect(scope.shouldShowEditClubsLink).toEqual(false);
            expect(scope.shouldShowEditWeaponsLink).toEqual(false);

            expect(scope.shouldShowAboutLink).toEqual(true);
            expect(scope.shouldShowPrivacyLink).toEqual(true);
        });
    });

    // tests start here
    it('Check that the user is logged in, competition is selected', function () {
        angular.mock.inject(function ($rootScope, $controller, $location, $modal, currentUserFactory) {
            //create an empty scope
            scope = $rootScope.$new();

            // backend response
            $httpBackend.when('GET', '/api/currentuser?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA').respond({ IsLoggedIn: true, DisplayName: 'John Smith', CompetitionRights: ['right1', 'right2'] }, {});
            $httpBackend.expectGET('/api/currentuser?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA');

            // Set the current competition
            window.competitionId = "A6109CFD-C4D8-4003-A6E7-A2BB006A81EA";

            //declare the controller and inject our empty scope
            var myController = $controller('AppController', {
                $rootScope: scope,
                $scope: scope,
                $location: $location,
                $modal: $modal,
                currentUserFactory: currentUserFactory
            });
            expect(myController).toBeDefined();

            // Run the HTTP request
            $httpBackend.flush();

            expect(scope.currentUser.IsLoggedIn).toEqual(true);

            expect(scope.displayName).toEqual("John Smith");
            expect(scope.isLoggedIn).toEqual(true);
            expect(scope.rights).toEqual(['right1', 'right2']);
            expect(scope.shouldShowLoginLink).toEqual(false);

            expect(scope.shouldShowCompetitionLink).toEqual(true);
            expect(scope.shouldShowStationsLink).toEqual(true);
            expect(scope.shouldShowPatrolsLink).toEqual(true);
            expect(scope.shouldShowCompetitorsLink).toEqual(true);

            expect(scope.shouldShowAddResultsLink).toEqual(false);
            expect(scope.shouldShowEditRightsLink).toEqual(false);
            expect(scope.shouldShowEditClubsLink).toEqual(false);
            expect(scope.shouldShowEditWeaponsLink).toEqual(false);

            expect(scope.shouldShowAboutLink).toEqual(false);
            expect(scope.shouldShowPrivacyLink).toEqual(false);
        });
    });

    // tests start here
    it('Check that the user is logged in, competition is selected, user has special rights', function () {
        angular.mock.inject(function ($rootScope, $controller, $location, $modal, currentUserFactory) {
            //create an empty scope
            scope = $rootScope.$new();

            // backend response
            $httpBackend.when('GET', '/api/currentuser?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA').respond(
                {
                    IsLoggedIn: true,
                    DisplayName: 'John Smith',
                    CompetitionRights: ['AddCompetitorResult', 'ReadUserCompetitionRole', 'UpdateClub', 'UpdateWeapon']
                }, {});
            $httpBackend.expectGET('/api/currentuser?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA');

            // Set the current competition
            window.competitionId = "A6109CFD-C4D8-4003-A6E7-A2BB006A81EA";

            //declare the controller and inject our empty scope
            var myController = $controller('AppController', {
                $rootScope: scope,
                $scope: scope,
                $location: $location,
                $modal: $modal,
                currentUserFactory: currentUserFactory
            });
            expect(myController).toBeDefined();

            // Run the HTTP request
            $httpBackend.flush();

            expect(scope.currentUser.IsLoggedIn).toEqual(true);

            expect(scope.rights).toEqual(['AddCompetitorResult', 'ReadUserCompetitionRole', 'UpdateClub', 'UpdateWeapon']);

            expect(scope.shouldShowAddResultsLink).toEqual(true);
            expect(scope.shouldShowEditRightsLink).toEqual(true);
            expect(scope.shouldShowEditClubsLink).toEqual(true);
            expect(scope.shouldShowEditWeaponsLink).toEqual(true);
        });
    });
});
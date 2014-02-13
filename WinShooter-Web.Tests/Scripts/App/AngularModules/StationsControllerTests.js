///<reference path="~/Scripts/jasmine.js"/>
///<reference path="~/../WinShooter-Web/Scripts/Angular.js"/>
///<reference path="~/../WinShooter-Web/Scripts/angular-resource.js" />
///<reference path="~/../WinShooter-Web/Scripts/angular-sanitize.js"/>
///<reference path="~/../WinShooter-Web/Scripts/angular-mocks.js" />
///<reference path="~/../WinShooter-Web/Scripts/jquery-2.0.3.js" />
///<reference path="~/../WinShooter-Web/Scripts/App/common.js"/>
///<reference path="~/../WinShooter-Web/Scripts/App/AngularModules.js"/>

'use strict';

describe("AngularModules-StationsController", function () {
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
    it('No logged in user, no stations', function () {
        angular.mock.inject(function ($rootScope, $controller, $routeParams, $modal, competitionFactory, currentUserFactory) {
            //create an empty scope
            scope = $rootScope.$new();

            // backend response
            $httpBackend.when('GET', '/api/currentuser?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA').respond({ IsLoggedIn: false }, {});
            $httpBackend.expectGET('/api/currentuser?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA');
            $httpBackend.when('GET', '/api/stations?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA').respond([], {});
            $httpBackend.expectGET('/api/stations?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA');

            // Set the current cometition
            window.competitionId = "A6109CFD-C4D8-4003-A6E7-A2BB006A81EA";
            $routeParams.competitionId = window.competitionId;

            //declare the controller and inject our empty scope
            var myController = $controller('StationsController', {
                $scope: scope,
                $routeParams: $routeParams,
                $modal: $modal,
                competitionFactory: competitionFactory,
                currentUserFactory: currentUserFactory
            });

            expect(myController).toBeDefined();

            // Check the result before HTTP
            expect(scope.userCanCreateStation).toEqual(false);
            expect(scope.userCanUpdateStation).toEqual(false);
            expect(scope.userCanDeleteStation).toEqual(false);

            // Run the HTTP request
            $httpBackend.flush();

            // Check the result after HTTP
            expect(scope.userCanCreateStation).toEqual(false);
            expect(scope.userCanUpdateStation).toEqual(false);
            expect(scope.userCanDeleteStation).toEqual(false);
            expect(scope.stations.length).toEqual(0);
        });
    });

    // tests start here
    it('Logged in user with rights, no stations', function () {
        angular.mock.inject(function ($rootScope, $controller, $routeParams, $modal, competitionFactory, currentUserFactory) {
            //create an empty scope
            scope = $rootScope.$new();

            // backend response
            $httpBackend.when('GET', '/api/currentuser?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA').respond({ IsLoggedIn: true, CompetitionRights: ['UpdateStation', 'CreateStation', 'DeleteStation'] }, {});
            $httpBackend.expectGET('/api/currentuser?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA');
            $httpBackend.when('GET', '/api/stations?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA').respond([], {});
            $httpBackend.expectGET('/api/stations?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA');

            // Set the current cometition
            window.competitionId = "A6109CFD-C4D8-4003-A6E7-A2BB006A81EA";
            $routeParams.competitionId = window.competitionId;

            //declare the controller and inject our empty scope
            var myController = $controller('StationsController', {
                $scope: scope,
                $routeParams: $routeParams,
                $modal: $modal,
                competitionFactory: competitionFactory,
                currentUserFactory: currentUserFactory
            });

            expect(myController).toBeDefined();

            // Check the result before HTTP
            expect(scope.userCanCreateStation).toEqual(false);
            expect(scope.userCanUpdateStation).toEqual(false);
            expect(scope.userCanDeleteStation).toEqual(false);

            // Run the HTTP request
            $httpBackend.flush();

            // Check the result after HTTP
            expect(scope.userCanCreateStation).toEqual(true);
            expect(scope.userCanUpdateStation).toEqual(true);
            expect(scope.userCanDeleteStation).toEqual(true);
            expect(scope.stations.length).toEqual(0);
        });
    });

    // tests start here
    it('Logged in user with rights, one station', function () {
        angular.mock.inject(function ($rootScope, $controller, $routeParams, $modal, competitionFactory, currentUserFactory) {
            //create an empty scope
            scope = $rootScope.$new();

            // backend response
            $httpBackend.when('GET', '/api/currentuser?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA').respond({ IsLoggedIn: true, CompetitionRights: ['UpdateStation', 'CreateStation', 'DeleteStation'] }, {});
            $httpBackend.expectGET('/api/currentuser?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA');
            $httpBackend.when('GET', '/api/stations?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA').respond([{ "CompetitionId": "74ec4f92-4b72-4c40-927a-de308269e074", "StationId": "74ec4f92-4b72-4c40-927a-de308269e074", "StationNumber": 2, "Distinguish": false, "NumberOfShots": 4, "NumberOfTargets": 3, "Points": false }], {});
            $httpBackend.expectGET('/api/stations?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA');

            // Set the current cometition
            window.competitionId = "A6109CFD-C4D8-4003-A6E7-A2BB006A81EA";
            $routeParams.competitionId = window.competitionId;

            //declare the controller and inject our empty scope
            var myController = $controller('StationsController', {
                $scope: scope,
                $routeParams: $routeParams,
                $modal: $modal,
                competitionFactory: competitionFactory,
                currentUserFactory: currentUserFactory
            });

            expect(myController).toBeDefined();

            // Run the HTTP request
            $httpBackend.flush();

            // Check the result after HTTP
            expect(scope.stations.length).toEqual(1);
            expect(scope.stations[0].StationId).toEqual("74ec4f92-4b72-4c40-927a-de308269e074");
        });
    });

    // tests start here
    it('Logged in user with rights, no stations, add station', function () {
        angular.mock.inject(function ($rootScope, $controller, $routeParams, $modal, $http, competitionFactory, currentUserFactory) {
            //create an empty scope
            scope = $rootScope.$new();

            // backend response
            $httpBackend.when('GET', '/api/currentuser?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA').respond({ IsLoggedIn: true, CompetitionRights: ['UpdateStation', 'CreateStation', 'DeleteStation'] }, {});
            $httpBackend.expectGET('/api/currentuser?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA');
            $httpBackend.when('GET', '/api/stations?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA').respond([], {});
            $httpBackend.expectGET('/api/stations?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA');

            // Set the current cometition
            window.competitionId = "A6109CFD-C4D8-4003-A6E7-A2BB006A81EA";
            $routeParams.competitionId = window.competitionId;

            //declare the controller and inject our empty scope
            var myController = $controller('StationsController', {
                $rootScope: scope,
                $scope: scope,
                $routeParams: $routeParams,
                $modal: $modal,
                $http: $http,
                competitionFactory: competitionFactory,
                currentUserFactory: currentUserFactory
            });

            expect(myController).toBeDefined();

            // Run the HTTP request
            $httpBackend.flush();

            // Check the result after HTTP
            expect(scope.stations.length).toEqual(0);

            // Prepare to add station
            $httpBackend.when('POST', '/api/stations').respond({ 'CompetitionId': 'A6109CFD-C4D8-4003-A6E7-A2BB006A81EA' }, {});
            $httpBackend.expectPOST('/api/stations', "{\"CompetitionId\":\"A6109CFD-C4D8-4003-A6E7-A2BB006A81EA\",\"StationId\":\"\",\"StationNumber\":-1,\"Distinguish\":false,\"NumberOfShots\":3,\"NumberOfTargets\":3,\"Points\":false}");

            $httpBackend.when('GET', '/api/currentuser?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA').respond({ IsLoggedIn: true, CompetitionRights: ['UpdateStation', 'CreateStation', 'DeleteStation'] }, {});
            $httpBackend.expectGET('/api/currentuser?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA');
            $httpBackend.when('GET', '/api/stations?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA').respond([], {});
            $httpBackend.expectGET('/api/stations?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA');

            // Add station
            scope.addNewStation();

            // Run the HTTP request
            $httpBackend.flush();
        });
    });
});
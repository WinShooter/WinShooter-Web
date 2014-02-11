///<reference path="~/Scripts/jasmine.js"/>
///<reference path="~/../WinShooter-Web/Scripts/Angular.js"/>
///<reference path="~/../WinShooter-Web/Scripts/angular-resource.js" />
///<reference path="~/../WinShooter-Web/Scripts/angular-sanitize.js"/>
///<reference path="~/../WinShooter-Web/Scripts/angular-mocks.js" />
///<reference path="~/../WinShooter-Web/Scripts/jquery-2.0.3.js" />
///<reference path="~/../WinShooter-Web/Scripts/App/common.js"/>
///<reference path="~/../WinShooter-Web/Scripts/App/AngularModules.js"/>

'use strict';

describe("AngularModules-CompetitionController", function () {
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
    it('There is no competition to show (error case)', function () {
        angular.mock.inject(function ($rootScope, $controller, $routeParams, $modal, competitionFactory, currentUserFactory) {
            //create an empty scope
            scope = $rootScope.$new();

            // backend response
            $httpBackend.when('GET', '/api/currentuser?CompetitionId=').respond({ IsLoggedIn: false }, {});
            $httpBackend.expectGET('/api/currentuser?CompetitionId=');
            $httpBackend.when('GET', '/api/competition?CompetitionId=').respond({}, {});
            $httpBackend.expectGET('/api/competition?CompetitionId=');

            // Set the current cometition
            window.competitionId = "";

            //declare the controller and inject our empty scope
            var myController = $controller('CompetitionController', {
                $scope: scope,
                $routeParams: $routeParams,
                $modal: $modal,
                competitionFactory: competitionFactory,
                currentUserFactory: currentUserFactory
            });

            expect(myController).toBeDefined();

            // Check the result before HTTP
            expect(scope.userCanUpdateCompetition).toEqual(false);
            expect(scope.userCanDeleteCompetition).toEqual(false);

            // Run the HTTP request
            $httpBackend.flush();
        });
    });

    it('There is one competition to show', function () {
        angular.mock.inject(function ($rootScope, $controller, $routeParams, $modal, competitionFactory, currentUserFactory) {
            //create an empty scope
            scope = $rootScope.$new();

            // backend response
            $httpBackend.when('GET', '/api/currentuser?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA').respond({ IsLoggedIn: false }, {});
            $httpBackend.expectGET('/api/currentuser?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA');
            $httpBackend.when('GET', '/api/competition?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA').respond({ "CompetitionId": "A6109CFD-C4D8-4003-A6E7-A2BB006A81EA", "CompetitionType": "Field", "IsPublic": true, "Name": "Name", "StartDate": "2014-01-31T20:41:00.000Z", "UseNorwegianCount": true }, {});
            $httpBackend.expectGET('/api/competition?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA');

            // Set the current competition
            window.competitionId = "A6109CFD-C4D8-4003-A6E7-A2BB006A81EA";
            $routeParams.competitionId = window.competitionId;

            //declare the controller and inject our empty scope
            var myController = $controller('CompetitionController', {
                $scope: scope,
                $routeParams: $routeParams,
                $modal: $modal,
                competitionFactory: competitionFactory,
                currentUserFactory: currentUserFactory
            });

            expect(myController).toBeDefined();

            // Check the result before HTTP
            expect(scope.competition.Name).toBeUndefined();
            expect(scope.competition.StartDate).toBeUndefined();
            expect(scope.competition.CompetitionId).toBeUndefined();
            expect(scope.competition.IsPublic).toBeUndefined();
            expect(scope.competition.UseNorwegianCount).toBeUndefined();

            // Run the HTTP request
            $httpBackend.flush();

            // Check the result after HTTP
            expect(scope.competition.Name).toEqual("Name");
            expect(scope.competition.StartDate).toEqual(new Date("2014-01-31T20:41:00.000Z"));
            expect(scope.competition.CompetitionId).toEqual("A6109CFD-C4D8-4003-A6E7-A2BB006A81EA");
            expect(scope.competition.IsPublic).toEqual(true);
            expect(scope.competition.UseNorwegianCount).toEqual(true);
        });
    });
});
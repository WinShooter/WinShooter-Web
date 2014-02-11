﻿///<reference path="~/Scripts/jasmine.js"/>
///<reference path="~/../WinShooter-Web/Scripts/Angular.js"/>
///<reference path="~/../WinShooter-Web/Scripts/angular-resource.js" />
///<reference path="~/../WinShooter-Web/Scripts/angular-sanitize.js"/>
///<reference path="~/../WinShooter-Web/Scripts/angular-mocks.js" />
///<reference path="~/../WinShooter-Web/Scripts/jquery-2.0.3.js" />
///<reference path="~/../WinShooter-Web/Scripts/App/common.js"/>
///<reference path="~/../WinShooter-Web/Scripts/App/AngularModules.js"/>

'use strict';

describe("AngularModules-IndexController", function () {
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
    it('There is no competitions to show', function () {
        angular.mock.inject(function ($rootScope, $controller, $location, $modal, competitionsFactory) {
            //create an empty scope
            scope = $rootScope.$new();

            // backend response
            $httpBackend.when('GET', '/api/competitions').respond([], {});
            $httpBackend.expectGET('/api/competitions');

            // Set the current cometition
            window.competitionId = "";

            //declare the controller and inject our empty scope
            var myController = $controller('IndexController', {
                $rootScope: scope,
                $scope: scope,
                $location: $location,
                $modal: $modal,
                competitionsFactory: competitionsFactory
            });

            expect(myController).toBeDefined();

            // Check the result before HTTP
            expect(scope.selectedCompetition.Name).toEqual("Laddar tävlingar. Var god vänta...");
            expect(scope.selectedCompetition.StartDate).toEqual("");
            expect(scope.selectedCompetition.CompetitionId).toEqual("");
            expect(scope.selectedCompetition.IsPublic).toEqual(false);
            expect(scope.selectedCompetition.UseNorwegianCount).toEqual(false);

            // Run the HTTP request
            $httpBackend.flush();

            // Check the result after HTTP
            expect(scope.selectedCompetition.Name).toEqual("Inga tävlingar hittades.");
            expect(scope.selectedCompetition.StartDate).toEqual("");
            expect(scope.selectedCompetition.CompetitionId).toEqual("");
            expect(scope.selectedCompetition.IsPublic).toEqual(false);
            expect(scope.selectedCompetition.UseNorwegianCount).toEqual(false);
        });
    });

    it('There is one competition to show', function () {
        angular.mock.inject(function ($rootScope, $controller, $location, $modal, competitionsFactory) {
            //create an empty scope
            scope = $rootScope.$new();

            // backend response
            $httpBackend.when('GET', '/api/competitions').respond([
                {
                    CompetitionId: 'CompetitionId',
                    CompetitionType: 'Field',
                    IsPublic: true,
                    Name: 'Name',
                    StartDate: '2014-02-10T21:42:00.000Z',
                    UseNorwegianCount: true
                }], {});
            $httpBackend.expectGET('/api/competitions');

            // Set the current cometition
            window.competitionId = "";

            //declare the controller and inject our empty scope
            var myController = $controller('IndexController', {
                $rootScope: scope,
                $scope: scope,
                $location: $location,
                $modal: $modal,
                competitionsFactory: competitionsFactory
            });

            expect(myController).toBeDefined();

            // Check the result before HTTP
            expect(scope.selectedCompetition.Name).toEqual("Laddar tävlingar. Var god vänta...");
            expect(scope.selectedCompetition.StartDate).toEqual("");
            expect(scope.selectedCompetition.CompetitionId).toEqual("");
            expect(scope.selectedCompetition.IsPublic).toEqual(false);
            expect(scope.selectedCompetition.UseNorwegianCount).toEqual(false);

            // Run the HTTP request
            $httpBackend.flush();

            // Check the result after HTTP
            expect(scope.selectedCompetition.Name).toEqual("Name");
            expect(scope.selectedCompetition.StartDate).toEqual("2014-02-10T21:42:00.000Z");
            expect(scope.selectedCompetition.CompetitionId).toEqual("CompetitionId");
            expect(scope.selectedCompetition.IsPublic).toEqual(true);
            expect(scope.selectedCompetition.UseNorwegianCount).toEqual(true);

            // Select competition
            scope.selectCompetitionOnServer();
            expect($location.path()).toEqual('/Home/Competition/CompetitionId');
        });
    });
});
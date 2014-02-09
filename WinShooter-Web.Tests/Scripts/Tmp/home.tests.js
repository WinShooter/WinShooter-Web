///<reference path="~/Scripts/jasmine.js"/>
///<reference path="~/../WinShooter-Web/Scripts/Angular.js"/>
///<reference path="~/../WinShooter-Web/Scripts/angular-resource.js" />
///<reference path="~/../WinShooter-Web/Scripts/angular-mocks.js" />
///<reference path="~/Scripts/Tmp/home.js"/>
'use strict';

describe('A dummy controller tests', function () {
    var scope = { text: "oh" };//we'll use this scope in our tests

    //mock Application to allow us to inject our own dependencies
    beforeEach(angular.mock.module('Application'));

    //mock the controller for the same reason and include $rootScope and $controller
    beforeEach(angular.mock.inject(function ($rootScope, $controller) {
        //create an empty scope
        scope = $rootScope.$new();

        //declare the controller and inject our empty scope
        $controller('MainCtrl', { $scope: scope });
    }));

    // tests start here
    it('should have variable text = "Hello World!"', function () {
        expect(scope.text).toBe('Hello World!');
    });
});
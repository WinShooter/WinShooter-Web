'use strict';

var app = angular.module('Application', ['ngResource']);

app.controller('MainCtrl', function ($scope) {
    $scope.text = 'Hello World!';
});
﻿var app = angular.module('WebmasterApp', [
  'ngRoute',
  'termControllers'
]);

app.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
          when('/term/details/:id', {
              templateUrl: '/Static/Webmaster/partials/term-edit.html',
              controller: 'TermDetailCtrl'
          }).
           when('/term/new/:id', {
               templateUrl: '/Static/Webmaster/partials/term-edit.html',
               controller: 'TermDetailCtrl'
           }).
           when('/term/edit/:id', {
               templateUrl: '/Static/Webmaster/partials/term-edit.html',
               controller: 'TermEditCtrl'
           }).
           when('/term/delete/:id', {
               templateUrl: '/Static/Webmaster/partials/term-edit.html',
               controller: 'TermDetailCtrl'
           }).
          otherwise({
              //redirectTo: '/'
          });
  }]);
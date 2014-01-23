﻿var managementControllers = angular.module('managementControllers', []);

managementControllers.controller('LoginCtrl', ['$scope', '$http',
  function ($scope, $http) {
      //$http.get('phones/phones.json').success(function (data) {
      //    $scope.phones = data;
      //});

      //$scope.orderProp = 'age';
  }]);

//初始用户信息
managementControllers.controller('UserInfoCtrl', ['$scope', '$http',
  function ($scope, $http) {
      $scope.user = {
          IsAuthenticated: false,
          DisplayName: '登录',
          MenuUrl: '',
          InfoCss: 'login',
          InfoHref: '#/login'
      };

      //获取用户验证信息
      $http.get('/api/authentication').
          success(function (data, status) {
              if (data.IsAuthenticated) {
                  $scope.user.IsAuthenticated = true;
                  $scope.user.DisplayName = data.Name;
                  $scope.user.MenuUrl = 'partials/menu.html';
                  $scope.user.InfoCss = 'display-name';
                  $scope.user.InfoHref = '123';//连接地址
              }
          });

  }]);

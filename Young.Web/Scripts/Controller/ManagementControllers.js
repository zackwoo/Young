var managementControllers = angular.module('managementControllers', []);

managementControllers.controller('LoginCtrl', ['$scope', '$http',
  function ($scope, $http) {
      //$http.get('phones/phones.json').success(function (data) {
      //    $scope.phones = data;
      //});

      //$scope.orderProp = 'age';
  }]);
//术语管理
managementControllers.controller('TermCtrl', ['$scope', '$http',
  function ($scope, $http) {
      $("#browser").treeview({
          toggle: function () {
              console.log("%s was toggled.", $(this).find(">span").text());
          }
      });
    
      $('#browser li').contextMenu('termContextMenu', {
          bindings: {
              'add': function (t) {
                  console.log(t);
              },
              'edit': function (t) {
                  alert('Trigger was ' + t.id + '\nAction was Email');
              },
              'delete': function (t) {
                  alert('Trigger was ' + t.id + '\nAction was Delete');
              }
          }
      });
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

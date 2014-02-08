var termControllers = angular.module('termControllers', []);

//术语管理
termControllers.controller('TermInitCtrl', ['$scope', '$http', '$location', '$route',
    function ($scope, $http, $location, $route) {
        $('#term-cx-m').menu({
            onClick: function (item) {
                var node = $('#tree_term').tree('getSelected');
                if (node == null) return;
                
                if (item.id == "cx-m-new") {
                    $location.path("term/new/" + node.id);
                }
                else if (item.id == "cx-m-edit") {
                    $location.path("term/edit/" + node.id);
                }
                else if (item.id == "cx-m-delete") {
                    $location.path("term/delete/" + node.id);
                }
                $route.reload();
            }
        });
        $("#tree_term").tree({
            url: "/api/termapi",
            method: 'get',
            onLoadSuccess: function (node, data) {
                $("#tree_loading").hide();
            },
            onCollapse: function (node) {
                var root = $("#tree_term").tree("getRoot");
                if (node.id==root.id) {
                    $("#tree_term").tree("update", {
                        target: node.target,
                        iconCls: 'icon-book'
                    });
                }
            },
            onExpand: function (node) {
                var root = $("#tree_term").tree("getRoot");
                if (node.id == root.id) {
                    $("#tree_term").tree("update", {
                        target: node.target,
                        iconCls: 'icon-book-open'
                    });
                }
            },
            onClick:function(node) {
                $location.path("term/details/" + node.id);
                $route.reload();
            },
            onContextMenu: function (e, node) {
                e.preventDefault();
                // select the node
                $('#tree_term').tree('select', node.target);
                if (node.attributes.isSystem) {
                    //系统节点不允许编辑和删除
                    var editItem = $('#term-cx-m').menu('findItem', '编辑');
                    var deleteItem = $('#term-cx-m').menu('findItem', '删除');
                    $('#term-cx-m')
                        .menu('disableItem', editItem.target)
                        .menu('disableItem', deleteItem.target);
                }
                // display context menu
                $('#term-cx-m').menu('show', {
                    left: e.pageX,
                    top: e.pageY
                });
            }
        });
    }]);

termControllers.controller('TermDetailCtrl', ['$scope', '$http', function ($scope, $http) {
    $('#btn-save,#btn-reset').linkbutton();
}]);

termControllers.controller('TermEditCtrl', ['$scope', '$http', '$routeParams', function ($scope, $http, $routeParams) {
    var termId = $routeParams.id;
    $http({ method: 'GET', url: '/api/termapi/' + termId }).
         success(function (data, status, headers, config) {
             $scope.Model = data;
         });
    $('#btn-save,#btn-reset').linkbutton();
}]);




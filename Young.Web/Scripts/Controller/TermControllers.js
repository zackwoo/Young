
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
                    $route.reload();
                }
                else if (item.id == "cx-m-edit") {
                    $location.path("term/edit/" + node.id);
                    $route.reload();
                }
                else if (item.id == "cx-m-delete") {
                    $.messager.confirm('删除', '你确定要删除术语"' + node.text + '"吗？', function (r) {
                        if (r) {
                            $http({ method: 'delete', url: '/api/apiterm/' + node.id }).
                                success(function (data, status, headers, config) {
                                    if (data == "false") {
                                        $.messager.alert("消息", "删除术语失败，请联系管理员。", "info");
                                    } else {
                                        $location.path("term/index/");
                                        $route.reload();
                                        $("#tree_term").tree("reload");
                                    }
                                });
                        }
                    });
                }

            }
        });
        $("#tree_term").tree({
            url: "/api/apiterm",
            method: 'get',
            onLoadSuccess: function (node, data) {
                $("#tree_loading").hide();
            },
            onCollapse: function (node) {
                var root = $("#tree_term").tree("getRoot");
                if (node.id == root.id) {
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
            onClick: function (node) {
                $location.path("term/details/" + node.id);
                $route.reload();
            },
            onDblClick: function (node) {
                $('#tree_term').tree('toggle', node.target);
            },
            onContextMenu: function (e, node) {
                e.preventDefault();
                // select the node
                $('#tree_term').tree('select', node.target);
                var editItem = $('#term-cx-m').menu('findItem', '编辑');
                var deleteItem = $('#term-cx-m').menu('findItem', '删除');
                if (node.attributes.isSystem) {
                    //系统节点不允许编辑和删除
                    $('#term-cx-m')
                        .menu('disableItem', editItem.target)
                        .menu('disableItem', deleteItem.target);
                } else {
                    $('#term-cx-m')
                        .menu('enableItem', editItem.target)
                        .menu('enableItem', deleteItem.target);
                }
                // display context menu
                $('#term-cx-m').menu('show', {
                    left: e.pageX,
                    top: e.pageY
                });
            }
        });
    }]);

termControllers.controller('TermDetailCtrl', ['$scope', '$http', '$routeParams', function ($scope, $http, $routeParams) {
    var termId = $routeParams.id;
    $http({ method: 'GET', url: '/api/apiterm/' + termId }).
        success(function (data, status, headers, config) {
            $scope.Model = data;
        });
    $('#btn-save,#btn-reset').linkbutton();
}]);

termControllers.controller('TermEditCtrl', ['$scope', '$http', '$routeParams', '$location', '$route', function ($scope, $http, $routeParams, $location, $route) {
    var termId = $routeParams.id;
    //重置时候使用
    var text = "";
    var description = "";
    $scope.save = function () {
        $http({
            method: "put",
            url: "/api/apiterm",
            data: $scope.Model
        }).success(function (data, status, headers, config) {
            $("#tree_term").tree("reload");
            $location.path('term/details/' + $scope.Model.id);
            $route.reload();

        });
    };
    $scope.reset = function () {
        $scope.Model.text = text;
        $scope.Model.Description = description;
    };
    $http({ method: 'GET', url: '/api/apiterm/' + termId }).
         success(function (data, status, headers, config) {
             $scope.Model = data;
             text = data.text;
             description = data.Description;
         });
    $('#btn-save,#btn-reset').linkbutton();
}]);

termControllers.controller('TermNewCtrl', ['$scope', '$http', '$routeParams', '$location', '$route', function ($scope, $http, $routeParams, $location, $route) {
    var termParentId = $routeParams.id;
    $scope.Model = { text: '', Description: '', id: termParentId };
    $scope.save = function () {
        $http({
            method: "post",
            url: "/api/apiterm",
            data: $scope.Model
        }).success(function (data, status, headers, config) {
            $("#tree_term").tree("reload");
            $location.path('term/details/' + data.id);
            $route.reload();

        });
    };
    $scope.reset = function () {
        $scope.Model.text = "";
        $scope.Model.Description = "";
    };

    //easy ui
    $('#btn-save,#btn-reset').linkbutton();
}]);




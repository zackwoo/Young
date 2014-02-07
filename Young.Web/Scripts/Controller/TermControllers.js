var termControllers = angular.module('termControllers', []);

//术语管理
termControllers.controller('TermInitCtrl', ['$scope', '$http',
    function ($scope, $http) {
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
                
            }
        });

    }]);



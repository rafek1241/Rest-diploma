(function () {
    angular.module('app.shared')
        .directive('mainMenu', mainMenu);

    mainMenu.$inject = ['$rootScope', '$http'];

    function mainMenu($rootScope, $http) {
        var directive = {
            templateUrl: "app/shared/main-menu/main-menu.html",
            restrict: "A",
            link: link
        };
        return directive;

        function link(scope, element, attr) {
            scope.selectMenu = selectMenu;

            $http.get('api/menu/getList').then(function (response) {
                scope.links = response.data;
            });

            function selectMenu(link) {

                links.forEach(function (item) {
                    item.active = false;
                });
                link.active = true;

            }
        }
    }
})();
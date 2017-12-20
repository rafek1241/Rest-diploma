(function () {
    'use strict';

    angular.module('app.products')
        .factory("productsService", productsService);

    productsService.$inject = ['$http', '$route'];

    function productsService($http, $route) {

        var entityUrl = "api/product";

        return {
            getList: getList,
            getOne: getOne,
            set: set,
            update: update,
            remove: remove
        };

        function getList(page, perPage) {
            return $http.get(entityUrl,
                {
                    params: {
                        page: page,
                        perPage: perPage
                    }
                });
        }

        function getOne(id) {
            return $http.get(entityUrl + "/" + id);
        }

        function set(model) {
            return $http.post(entityUrl, model).then(function () {
                $route.reload();
                $(".modal-backdrop").remove();
            });

        }

        function update(id, model) {
            return $http.put(entityUrl, [id, model]).then(function () {
                $route.reload();
            });
        }

        function remove(id) {
            return $http.delete(entityUrl + "/"  + id).then(function () {
                $route.reload();
            });
        }

    }

})();
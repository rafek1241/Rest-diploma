(function () {
    'use strict';

    angular.module('app.categories')
        .factory("categoriesService", categoriesService);

    categoriesService.$inject = ['$http', '$route'];

    function categoriesService($http, $route) {

        var entityUrl = "api/category";

        return {
            getCategories: getCategories,
            getCategory: getCategory,
            setCategory: setCategory,
            updateCategory: updateCategory,
            removeCategory: removeCategory,
            getCategoriesContainsString: getCategoriesContainsString
        };

        function getCategoriesContainsString(query) {
            return $http.get(entityUrl,
                {
                    params: {
                        name: query
                    }
                });
        }

        function getCategories() {
            return $http.get(entityUrl);
        }

        function getCategory(id) {
            return $http.get(entityUrl + "/" + id);
        }

        function setCategory(model) {
            return $http.post(entityUrl, model);
        }

        function updateCategory(id, model) {
            return $http.put(entityUrl + "/" + id, model);
        }

        function removeCategory(id) {
            return $http.delete(entityUrl + "/" + id);
        }

    }

})();
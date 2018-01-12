(function () {
    'use strict';
    angular.module('app.categories')
        .controller("CategoriesController", CategoriesController);

    CategoriesController.$inject = ['$scope', '$q', 'categoriesService'];

    function CategoriesController($scope, $q, categoriesService) {
        var vm = this;
        vm.categoriesService = categoriesService;
        vm.clearForm = clearForm;

        active();

        function active() {
            vm.categories = categoriesService.categories;
        }

        vm.setCategory = function (category) {
            var defer = $q.defer();

            categoriesService.setCategory(category).then(function (response) {
                vm.categories = response.data;
                clearForm();
                $("#addCategoryModal .close").click();
                defer.resolve();
            });

            return defer.promise;
        }
        vm.removeCategory = function (id) {
            categoriesService.removeCategory(id).then(function (response) {
                vm.categories = response.data;
            });
        }

        function clearForm() {
            $scope.category = {};
        }
    }

})();
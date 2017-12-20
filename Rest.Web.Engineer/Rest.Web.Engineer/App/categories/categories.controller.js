(function () {
    'use strict';
    angular.module('app.categories')
        .controller("CategoriesController", CategoriesController);

    CategoriesController.$inject = ['$scope', 'categoriesService'];

    function CategoriesController($scope, categoriesService) {
        var vm = this;
        vm.categoriesService = categoriesService;
        vm.clearForm = clearForm;

        active();

        function active() {
            categoriesService.getCategories().then(function (response) {
                vm.categories = response.data;
            });
        }

        function clearForm() {
            $scope.category = {};
        }
    }

})();
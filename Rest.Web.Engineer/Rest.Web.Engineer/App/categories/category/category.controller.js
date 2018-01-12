(function () {
    'use strict';
    angular.module('app.categories')
        .controller("CategoryController", categoryController);

    categoryController.$inject = ['$scope', '$routeParams', 'categoriesService', 'cartService'];

    function categoryController($scope, $routeParams, categoriesService, cartService) {
        var vm = this;
        vm.categoriesService = categoriesService;
        vm.cartService = cartService;
        vm.category = categoriesService.category;

        vm.removeProductFromCategory = function(id) {
            var products = [];

            vm.category.products.forEach(function(item) {
                if (item.productId !== id) {
                    products.push(item);
                }
            });
            vm.category.products = products;
        }

        vm.updateCategory = updateCategory;

         function updateCategory() {
            return vm.categoriesService.updateCategory($routeParams.categoryId, vm.category).then(function (response) {
                vm.category = response.data;
            });
        }
    }
})();
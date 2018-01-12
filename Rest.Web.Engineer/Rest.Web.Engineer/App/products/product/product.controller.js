(function () {
    'use strict';
    angular.module('app.products')
        .controller("ProductController", productController);

    productController.$inject = ['productsService', 'categoriesService'];

    function productController(productsService, categoriesService) {
        var vm = this;
        vm.categoriesService = categoriesService;
        vm.productsService = productsService;

        activate();

        function activate() {
            vm.product = productsService.product;
        };

    }

})();
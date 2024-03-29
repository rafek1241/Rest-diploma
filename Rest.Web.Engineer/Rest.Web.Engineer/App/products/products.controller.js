﻿(function () {
    'use strict';
    angular.module('app.products')
        .controller("ProductsController", productsController);

    productsController.$inject = ['$scope', 'productsService', 'categoriesService', 'cartService', '$routeParams'];

    function productsController($scope, productsService, categoriesService, cartService, $routeParams) {
        var vm = this;
        vm.productsService = productsService;
        vm.categoriesService = categoriesService;
        vm.cartService = cartService;

        vm.clearForm = clearForm;
        active();

        function active() {

            if (Object.keys($routeParams).length > 0) {
                vm.currentParams = $routeParams;
            } else {
                vm.currentParams = {
                    page: 1,
                    perPage: 12
                };
            }

            productsService.getList(vm.currentParams.page, vm.currentParams.perPage).then(function (response) {
                vm.products = response.data;
            });
        }
        vm.remove = function (id) {
            productsService.remove(id).then(function (response) {
                vm.products = response.data;
            });
        }

        function clearForm() {
            $scope.product = {};
        }
    }

})();
(function () {
    'use strict';
    angular.module('app.categories')
        .controller("CategoryController", categoryController);

    categoryController.$inject = ['$scope', 'categoriesService'];

    function categoryController($scope, categoriesService) {
        var vm = this;
        vm.categoriesService = categoriesService;
        vm.category = categoriesService.category;

    }

})();
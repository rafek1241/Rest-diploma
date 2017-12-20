(function () {
    angular.module('app')
        .config(config);

    config.$inject = ['$routeProvider', '$locationProvider'];

    function config($routeProvider, $locationProvider) {
        $locationProvider.html5Mode(true);
        $routeProvider
            .when('/', {
                templateUrl: '/app/homepage/home.html',
                controller: 'HomepageController',
                controllerAs: 'vm'
            })
            .when('/categories/:categoryId', {
                templateUrl: '/app/categories/category/category.html',
                controller: 'CategoryController',
                controllerAs: 'vm',
                resolve: {
                    'getCategory': ['$route', 'categoriesService',
                        function ($route, categoriesService) {
                            return categoriesService.getCategory($route.current.params.categoryId).then(function (response) {
                                categoriesService.category = response.data;
                            });
                        }]
                }
            })
            .when('/categories', {
                templateUrl: '/app/categories/categories.html',
                controller: 'CategoriesController',
                controllerAs: 'vm'
            })
            .when('/products/:productId', {
                templateUrl: '/app/products/product/product.html',
                controller: 'ProductController',
                controllerAs: 'vm'
            })
            .when('/products', {
                templateUrl: '/app/products/products.html',
                controller: 'ProductsController',
                controllerAs: 'vm'
            })
            .otherwise({ redirectTo: '/404' });
    }
})();
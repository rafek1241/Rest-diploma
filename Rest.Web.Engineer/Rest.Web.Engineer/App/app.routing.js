(function () {
    angular.module('app')
        .config(config);

    config.$inject = ['$routeProvider', '$locationProvider'];

    function config($routeProvider, $locationProvider) {
        $locationProvider.html5Mode(true);
        $routeProvider
            .when('/',
            {
                templateUrl: '/app/homepage/home.html',
                controller: 'HomepageController',
                controllerAs: 'vm'
            })
            .when('/categories/:categoryId',
            {
                templateUrl: '/app/categories/category/category.html',
                controller: 'CategoryController',
                controllerAs: 'vm',
                resolve: {
                    'getCategory': [
                        '$route', 'categoriesService',
                        function ($route, categoriesService) {
                            return categoriesService.getCategory($route.current.params.categoryId).then(
                                function (response) {
                                    categoriesService.category = response.data;
                                });
                        }
                    ]
                }
            })
            .when('/categories',
            {
                templateUrl: '/app/categories/categories.html',
                controller: 'CategoriesController',
                controllerAs: 'vm',
                resolve: {
                    'getCategories': [
                        'categoriesService',
                        function (categoriesService) {
                            return categoriesService.getCategories().then(function (response) {
                                categoriesService.categories = response.data;
                            });
                        }
                    ]
                }
            })
            .when('/order',
            {
                templateUrl: '/app/order/order.html',
                controller: 'OrderController',
                controllerAs: 'vm',
                resolve: {
                    'getCart': ['cartService',
                        function (cartService) {
                            return cartService.getData();
                        }]
                }
            })
            .when('/products/:productId',
            {
                templateUrl: '/app/products/product/product.html',
                controller: 'ProductController',
                controllerAs: 'vm',
                resolve: {
                    'getProduct': [
                        '$route', 'productsService',
                        function ($route, productsService) {
                            return productsService.getOne($route.current.params.productId).then(
                                function (response) {
                                    productsService.product = response.data;
                                });
                        }
                    ]
                }
            })
            .when('/products',
            {
                templateUrl: '/app/products/products.html',
                controller: 'ProductsController',
                controllerAs: 'vm'
            })
            .when('/carts',
            {
                templateUrl: '/app/cart/cart.html',
                controller: 'CartController',
                controllerAs: 'vm',
                resolve: {
                    'getCart': ['cartService', function (cartService) {
                        return cartService.getData();
                    }]
                }
            })
            .otherwise({ redirectTo: '/404' });
    }
})();
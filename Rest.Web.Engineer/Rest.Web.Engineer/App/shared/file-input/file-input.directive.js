(function () {
    'use strict';

    angular.module('app.shared')
        .directive('fileInput', fileInput);

    fileInput.$inject = ['$rootScope'];

    function fileInput($rootScope) {
        return {
            templateUrl: "app/shared/file-input/file-input.html",
            restrict: "E",
            scope: {
                model: '='
            },
            link: link
        };


        function link(scope, element, attr) {

            element.on('change',
                function () {
                    scope.$apply(function () {
                        scope.previews = [];
                    });

                    scope.model.forEach(function (item) {

                        var reader = new FileReader();
                        reader.addEventListener('load', readFile);
                        reader.readAsDataURL(item);
                    });

                });

            function readFile(event) {
                scope.$apply(function () {
                    scope.previews.push(event.target.result);
                });
            }
        }
    }

})();
(function () {
    'use strict';

    angular
        .module('rtApp')
        .controller('activitiesController', activitiesController);

    activitiesController.$inject = ['$scope', 'Activities'];

    function activitiesController($scope, Activities) {
        $scope.title = 'activitiesController';
        $scope.activities = Activities.query();
    }
})();

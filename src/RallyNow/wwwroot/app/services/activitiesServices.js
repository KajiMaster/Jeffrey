(function () {
    'use strict';
    var activitiesService = angular.module('activitiesService', ['ngResource']);

    activitiesService.factory('Activities', ['$resource',
      function ($resource) {
          return $resource('/api/activities/', {}, {
              query: { method: 'GET', params: {}, isArray: true }
          });
      }]);
})();
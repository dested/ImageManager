var module = angular.module('AnaSal.Client');

module.controller('mainCtrl', function ($scope) {
  $scope.model = {};
  $scope.callback = {};

});

module.config(function ($locationProvider, $stateProvider, $urlRouterProvider) {
  $stateProvider
    .state('main', {
      //abstract: true,
      url: '/',
      controller: 'mainCtrl',
      templateUrl: 'components/main/main.tpl.html'
    })
});
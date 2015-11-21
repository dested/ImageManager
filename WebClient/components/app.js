var module=angular.module('AnaSal.Client',[
  'ui.router'
]);


module.config(function ($locationProvider, $stateProvider, $urlRouterProvider) {
  $urlRouterProvider.otherwise('/');
});
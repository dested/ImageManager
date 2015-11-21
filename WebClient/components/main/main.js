var module = angular.module('AnaSal.Client');

module.controller('mainCtrl', function ($scope) {
  $scope.model = {};
  $scope.callback = {};

  $scope.callback.doit = function () {
    var img = document.getElementById('shoes');
    EXIF.getData(img, function() {
      console.log(EXIF.pretty(this));
    });

  }
});

module.config(function ($locationProvider, $stateProvider, $urlRouterProvider) {
  $stateProvider
    .state('main', {
      //abstract: true,
      url: '/main',
      controller: 'mainCtrl',
      templateUrl: 'components/main/main.tpl.html'
    })
});
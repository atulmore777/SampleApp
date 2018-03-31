/**=========================================================
 * Module: config.js
 * App routes and resources configuration
 =========================================================*/

App.config(['$stateProvider', '$locationProvider', '$urlRouterProvider', 'RouteHelpersProvider',
function ($stateProvider, $locationProvider, $urlRouterProvider, helper) {
  'use strict';

  // Set the following to true to enable the HTML5 Mode
  // You may have to set <base> tag in index and a routing configuration in your server
  $locationProvider.html5Mode(false);

  // default route
 // $urlRouterProvider.otherwise('/SingleView');
  $urlRouterProvider.otherwise('/home');
  // 
  // Application Routes
  // -----------------------------------   
  $stateProvider
            .state('app', {
                //url: '/',
                abstract: true,
                controller: 'AppController',
                resolve: helper.resolveFor('icons')
            })
           .state('app.home', {
               url: '/home',
               title: "Home",            
               templateUrl: helper.basepath('App/Home')
           })
           .state('admin', {
               url: '/admin',
               //templateUrl: helper.basepath('Admin/Index'),
               resolve: helper.resolveFor('modernizr', 'icons'),
               controller: ["$rootScope", function ($rootScope) {
                   $rootScope.app.layout.isFixed = true;
                   $rootScope.app.layout.isCollapsed = false;
                   $rootScope.app.layout.isRTL = false;
                   $rootScope.app.layout.horizontal = false;
                   $rootScope.app.layout.isFloat = false;
                   $rootScope.app.layout.asideHover = false;
                   $rootScope.app.layout.isBoxed = false;
                   $rootScope.app.layout.theme = "css/theme-b.css";
                   $rootScope.app.layout.isAdmin = true;                
               }]
           })
        .state('admin.singleview', {
            url: '/SingleView',
            title: 'Single View',
            templateUrl: helper.basepath('Admin/SingleView')
        })
        .state('admin.userview', {
            url: '/UserView',
            title: 'User View',
            templateUrl: helper.basepath('Admin/UserView'),
            resolve: angular.extend(helper.resolveFor('ngDialog'))
        });


}]).config(['$ocLazyLoadProvider', 'APP_REQUIRES', function ($ocLazyLoadProvider, APP_REQUIRES) {
    'use strict';

    // Lazy Load modules configuration
    $ocLazyLoadProvider.config({
      debug: false,
      events: true,
      modules: APP_REQUIRES.modules
    });

}]).config(['$controllerProvider', '$compileProvider', '$filterProvider', '$provide',
    function ( $controllerProvider, $compileProvider, $filterProvider, $provide) {
      'use strict';
      // registering components after bootstrap
      App.controller = $controllerProvider.register;
      App.directive  = $compileProvider.directive;
      App.filter     = $filterProvider.register;
      App.factory    = $provide.factory;
      App.service    = $provide.service;
      App.constant   = $provide.constant;
      App.value      = $provide.value;

}]).config(['$translateProvider', function ($translateProvider) {

    $translateProvider.useStaticFilesLoader({
        prefix : '/i18n/',
        suffix : '.json'
    });
    $translateProvider.preferredLanguage('en');
    $translateProvider.useLocalStorage();
    $translateProvider.usePostCompiling(true);

}]).config(['cfpLoadingBarProvider', function(cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeBar = true;
    cfpLoadingBarProvider.includeSpinner = false;
    cfpLoadingBarProvider.latencyThreshold = 500;
    cfpLoadingBarProvider.parentSelector = '.wrapper > section';
  }])
;

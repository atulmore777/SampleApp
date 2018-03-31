if("undefined"==typeof $)throw new Error("This application's JavaScript requires jQuery");var App=angular.module("angle",["ngRoute","ngAnimate","ngStorage","ngCookies","pascalprecht.translate","ui.bootstrap","ui.router","oc.lazyLoad","cfp.loadingBar","ngSanitize","ngResource","ui.utils","app.services"]).run(["$rootScope","$state","$stateParams","$window","$templateCache","AppService",function(e,o,t,n,r,i){e.$state=o,e.$stateParams=t,e.$storage=n.localStorage,e.app={name:"Angle",description:"Angular Bootstrap Admin Template",year:(new Date).getFullYear(),layout:{isFixed:!1,isCollapsed:!1,isBoxed:!1,isRTL:!1,horizontal:!1,isFloat:!1,asideHover:!1,isAdmin:!1},useFullLayout:!1,hiddenFooter:!1,viewAnimation:"ng-fadeInUp"},e.user={name:"John",job:"ng-Dev",picture:"app/img/user/02.jpg"}}]);App.config(["$stateProvider","$locationProvider","$urlRouterProvider","RouteHelpersProvider",function(e,o,t,n){"use strict";o.html5Mode(!1),t.otherwise("/home"),e.state("app",{abstract:!0,controller:"AppController",resolve:n.resolveFor("icons")}).state("app.home",{url:"/home",title:"Home",templateUrl:n.basepath("App/Home")}).state("admin",{url:"/admin",resolve:n.resolveFor("modernizr","icons"),controller:["$rootScope",function(e){e.app.layout.isFixed=!0,e.app.layout.isCollapsed=!1,e.app.layout.isRTL=!1,e.app.layout.horizontal=!1,e.app.layout.isFloat=!1,e.app.layout.asideHover=!1,e.app.layout.isBoxed=!1,e.app.layout.theme="css/theme-b.css",e.app.layout.isAdmin=!0}]}).state("admin.singleview",{url:"/SingleView",title:"Single View",templateUrl:n.basepath("Admin/SingleView")}).state("admin.userview",{url:"/UserView",title:"User View",templateUrl:n.basepath("Admin/UserView"),resolve:angular.extend(n.resolveFor("ngDialog"))})}]).config(["$ocLazyLoadProvider","APP_REQUIRES",function(e,o){"use strict";e.config({debug:!1,events:!0,modules:o.modules})}]).config(["$controllerProvider","$compileProvider","$filterProvider","$provide",function(e,o,t,n){"use strict";App.controller=e.register,App.directive=o.directive,App.filter=t.register,App.factory=n.factory,App.service=n.service,App.constant=n.constant,App.value=n.value}]).config(["$translateProvider",function(e){e.useStaticFilesLoader({prefix:"/i18n/",suffix:".json"}),e.preferredLanguage("en"),e.useLocalStorage(),e.usePostCompiling(!0)}]).config(["cfpLoadingBarProvider",function(e){e.includeBar=!0,e.includeSpinner=!1,e.latencyThreshold=500,e.parentSelector=".wrapper > section"}]),App.constant("APP_COLORS",{primary:"#5d9cec",success:"#27c24c",info:"#23b7e5",warning:"#ff902b",danger:"#f05050",inverse:"#131e26",green:"#37bc9b",pink:"#f532e5",purple:"#7266ba",dark:"#3a3f51",yellow:"#fad732","gray-darker":"#232735","gray-dark":"#3a3f51",gray:"#dde6e9","gray-light":"#e4eaec","gray-lighter":"#edf1f2"}).constant("APP_MEDIAQUERY",{desktopLG:1200,desktop:992,tablet:768,mobile:480}).constant("APP_REQUIRES",{scripts:{modernizr:["/Vendor/modernizr/modernizr.js"],icons:["/Vendor/fontawesome/css/font-awesome.min.css","/Vendor/simple-line-icons/css/simple-line-icons.css"]},modules:[{name:"ngDialog",files:["vendor/ngDialog/js/ngDialog.min.js","vendor/ngDialog/css/ngDialog.min.css","vendor/ngDialog/css/ngDialog-theme-default.min.css"]}]}),App.controller("userCtrl",["$scope","AppService","ngDialog",function(o,e,t){o.userlist={},o.GetUserDetails=function(){e.getUsers().then(function(e){console.log("get user data"),console.log(e),console.log("status : "+e.status),1==e.status&&(o.userlist=e.result,console.log("userlist : "+o.userlist))})},o.OpenCreateUserDialog=function(){t.open({template:"firstDialogId",className:"ngdialog-theme-default"})}}]),App.controller("AppController",["$rootScope","$scope","$state","$translate","$window","$localStorage","$timeout","toggleStateService","colors","browser","cfpLoadingBar",function(i,t,a,n,s,e,l,o,r,c,u){"use strict";var p;i.app.layout.horizontal="app-h"==i.$stateParams.layout,i.$on("$stateChangeStart",function(e,o,t,n,r){$(".wrapper > section").length&&(p=l(function(){u.start()},0))}),i.$on("$stateChangeSuccess",function(e,o,t,n,r){e.targetScope.$watch("$viewContentLoaded",function(){l.cancel(p),u.complete()})}),i.$on("$stateNotFound",function(e,o,t,n){console.log(o.to),console.log(o.toParams),console.log(o.options)}),i.$on("$stateChangeError",function(e,o,t,n,r,i){console.log(i)}),i.$on("$stateChangeSuccess",function(e,o,t,n,r){s.scrollTo(0,0),i.currTitle=a.current.title}),i.currTitle=a.current.title,i.pageTitle=function(){var e=i.app.name+" - "+(i.currTitle||i.app.description);return document.title=e},i.$watch("app.layout.isCollapsed",function(e,o){!1===e&&i.$broadcast("closeSidebarMenu")}),angular.isDefined(e.layout)?t.app.layout=e.layout:e.layout=t.app.layout,i.$watch("app.layout",function(){e.layout=t.app.layout},!0),t.colorByName=r.byName,t.language={listIsOpen:!1,available:{en:"English",es_AR:"Español"},init:function(){var e=n.proposedLanguage()||n.use(),o=n.preferredLanguage();t.language.selected=t.language.available[e||o]},set:function(e,o){n.use(e),t.language.selected=t.language.available[e],t.language.listIsOpen=!t.language.listIsOpen}},t.language.init(),o.restoreState($(document.body)),i.cancel=function(e){e.stopPropagation()}}]),App.controller("SidebarController",["$rootScope","$scope","$state","$http","$timeout","Utils",function(n,r,o,t,e,i){var a=[];n.$watch("app.layout.asideHover",function(e,o){!1===o&&!0===e&&l(-1)});var s=function(e){if(e){if(e.sref&&"#"!=e.sref)return o.is(e.sref)||o.includes(e.sref);var t=!1;return angular.forEach(e.submenu,function(e,o){s(e)&&(t=!0)}),t}};function l(e){for(var o in e+="",a)(e<0||e.indexOf(o)<0)&&(a[o]=!0)}r.getMenuItemPropClasses=function(e){return(e.heading?"nav-heading":"")+(s(e)?" active":"")},r.loadSidebarMenu=function(){var e="/Sidebar/sidebar-menu.json?v="+(new Date).getTime();t.get(e).success(function(e){r.menuItems=e}).error(function(e,o,t,n){alert("Failure loading menu")})},r.loadSidebarMenu(),r.addCollapse=function(e,o){a[e]=!!n.app.layout.asideHover||!s(o)},r.isCollapse=function(e){return a[e]},r.toggleCollapse=function(e,o){return i.isSidebarCollapsed()||n.app.layout.asideHover||(angular.isDefined(a[e])?r.lastEventFromChild||(a[e]=!a[e],l(e)):o&&l(-1),r.lastEventFromChild="string"==typeof(t=e)&&!(t.indexOf("-")<0)),!0;var t}}]),App.directive("searchOpen",["navSearch",function(t){"use strict";return{restrict:"A",controller:function(e,o){o.on("click",function(e){e.stopPropagation()}).on("click",t.toggle)}}}]).directive("searchDismiss",["navSearch",function(t){"use strict";return{restrict:"A",controller:function(e,o){$('.navbar-form input[type="text"]').on("click",function(e){e.stopPropagation()}).on("keyup",function(e){27==e.keyCode&&t.dismiss()}),$(document).on("click",t.dismiss),o.on("click",function(e){e.stopPropagation()}).on("click",t.dismiss)}}}]),App.directive("sidebar",["$rootScope","$window","Utils",function(i,e,a){var l,c,u=$(e),s=$("body");i.$state.current.name;return{restrict:"EA",template:'<nav class="sidebar" ng-transclude></nav>',transclude:!0,replace:!0,link:function(e,o,t){l=e,c=o;var n=a.isTouch()?"click":"mouseenter",r=$();c.on(n,".nav > li",function(){(a.isSidebarCollapsed()||i.app.layout.asideHover)&&(r.trigger("mouseleave"),r=function(e){d();var o=e.children("ul");if(!o.length)return $();if(e.hasClass("open"))return p(e),$();var t=$(".aside"),n=$(".aside-inner"),r=parseInt(n.css("padding-top"),0)+parseInt(t.css("padding-top"),0),i=o.clone().appendTo(t);p(e);var a=e.position().top+r-c.scrollTop(),s=u.height();return i.addClass("nav-floating").css({position:l.app.layout.isFixed?"fixed":"absolute",top:a,bottom:i.outerHeight(!0)+a>s?0:"auto"}),i.on("mouseleave",function(){p(e),i.remove()}),i}($(this)),$("<div/>",{class:"dropdown-backdrop"}).insertAfter(".aside-inner").on("click mouseenter",function(){d()}))}),e.$on("closeSidebarMenu",function(){d()}),u.on("resize",function(){a.isMobile()||s.removeClass("aside-toggled")}),i.$on("$stateChangeStart",function(e,o,t,n,r){o.name,$("body.aside-toggled").removeClass("aside-toggled"),i.$broadcast("closeSidebarMenu")})}};function p(e){e.siblings("li").removeClass("open").end().toggleClass("open")}function d(){$(".dropdown-backdrop").remove(),$(".sidebar-subnav.nav-floating").remove(),$(".sidebar li.open").removeClass("open")}}]),App.directive("toggleState",["toggleStateService",function(r){"use strict";return{restrict:"A",link:function(e,o,t){var n=$("body");$(o).on("click",function(e){e.preventDefault();var o=t.toggleState;o&&(n.hasClass(o)?(n.removeClass(o),t.noPersist||r.removeState(o)):(n.addClass(o),t.noPersist||r.addState(o)))})}}}]),angular.module("app.services",[]).factory("AppService",["$rootScope","$http","$q","$state","$cookieStore",function(e,t,r,o,n){var i="http://localhost:59181/v1api/",a="72f303a7-f1f0-45a0-ad2b-e6db29328b1a";return{login:function(e){console.log(e);var o=i+"Auth/Authenticate",n=r.defer();return t({method:"POST",url:o,data:e,headers:{"Content-Type":"application/json; charset=utf-8",AppToken:a}}).success(function(e,o,t){n.resolve(e),e.result}).error(function(e,o){n.reject(o)}),n.promise},getHeaders:function(){return{"Content-Type":"application/x-www-form-urlencoded; charset=UTF-8"}},getUsers:function(){var e=i+"User",n=r.defer();return t({method:"Get",url:e,headers:{"Content-Type":"application/json; charset=utf-8",AppToken:a}}).success(function(e,o,t){n.resolve(e),e.result}).error(function(e,o){n.reject(o)}),n.promise}}}]),App.service("browser",function(){"use strict";var e,o;if(o={},(e=function(e){e=e.toLowerCase();var o=/(opr)[\/]([\w.]+)/.exec(e)||/(chrome)[ \/]([\w.]+)/.exec(e)||/(version)[ \/]([\w.]+).*(safari)[ \/]([\w.]+)/.exec(e)||/(webkit)[ \/]([\w.]+)/.exec(e)||/(opera)(?:.*version|)[ \/]([\w.]+)/.exec(e)||/(msie) ([\w.]+)/.exec(e)||0<=e.indexOf("trident")&&/(rv)(?::| )([\w.]+)/.exec(e)||e.indexOf("compatible")<0&&/(mozilla)(?:.*? rv:([\w.]+)|)/.exec(e)||[],t=/(ipad)/.exec(e)||/(iphone)/.exec(e)||/(android)/.exec(e)||/(windows phone)/.exec(e)||/(win)/.exec(e)||/(mac)/.exec(e)||/(linux)/.exec(e)||/(cros)/i.exec(e)||[];return{browser:o[3]||o[1]||"",version:o[2]||"0",platform:t[0]||""}}(window.navigator.userAgent)).browser&&(o[e.browser]=!0,o.version=e.version,o.versionNumber=parseInt(e.version)),e.platform&&(o[e.platform]=!0),(o.android||o.ipad||o.iphone||o["windows phone"])&&(o.mobile=!0),(o.cros||o.mac||o.linux||o.win)&&(o.desktop=!0),(o.chrome||o.opr||o.safari)&&(o.webkit=!0),o.rv){e.browser="msie",o.msie=!0}if(o.opr){e.browser="opera",o.opera=!0}if(o.safari&&o.android){var t="android";o[e.browser=t]=!0}return o.name=e.browser,o.platform=e.platform,o}),App.factory("colors",["APP_COLORS",function(o){return{byName:function(e){return o[e]||"#fff"}}}]),App.service("navSearch",function(){var t="form.navbar-form";return{toggle:function(){var e=$(t);e.toggleClass("open");var o=e.hasClass("open");e.find("input")[o?"focus":"blur"]()},dismiss:function(){$(t).removeClass("open").find('input[type="text"]').blur().val("")}}}),App.provider("RouteHelpers",["APP_REQUIRES",function(s){"use strict";this.basepath=function(e){return"/"+e},this.resolveFor=function(){var a=arguments;return{deps:["$ocLazyLoad","$q",function(t,e){for(var n=e.when(1),o=0,r=a.length;o<r;o++)n=i(a[o]);return n;function i(o){return"function"==typeof o?n.then(o):n.then(function(){var e=function(e){if(s.modules)for(var o in s.modules)if(s.modules[o].name&&s.modules[o].name===e)return s.modules[o];return s.scripts&&s.scripts[e]}(o);return e?t.load(e):$.error("Route resolve: Bad resource name ["+o+"]")})}}]}},this.$get=function(){}}]),App.service("toggleStateService",["$rootScope",function(t){var n="toggleState",r={hasWord:function(e,o){return new RegExp("(^|\\s)"+o+"(\\s|$)").test(e)},addWord:function(e,o){if(!this.hasWord(e,o))return e+(e?" ":"")+o},removeWord:function(e,o){if(this.hasWord(e,o))return e.replace(new RegExp("(^|\\s)*"+o+"(\\s|$)*","g"),"")}};return{addState:function(e){var o=angular.fromJson(t.$storage[n]);o=o?r.addWord(o,e):e,t.$storage[n]=angular.toJson(o)},removeState:function(e){var o=t.$storage[n];o&&(o=r.removeWord(o,e),t.$storage[n]=angular.toJson(o))},restoreState:function(e){var o=angular.fromJson(t.$storage[n]);o&&e.addClass(o)}}}]),App.service("Utils",function(e,o){"use strict";var t,n,r=angular.element("html"),l=angular.element(e),i=angular.element("body");return{support:{transition:(n=function(){var e,o=document.body||document.documentElement,t={WebkitTransition:"webkitTransitionEnd",MozTransition:"transitionend",OTransition:"oTransitionEnd otransitionend",transition:"transitionend"};for(e in t)if(void 0!==o.style[e])return t[e]}(),n&&{end:n}),animation:(t=function(){var e,o=document.body||document.documentElement,t={WebkitAnimation:"webkitAnimationEnd",MozAnimation:"animationend",OAnimation:"oAnimationEnd oanimationend",animation:"animationend"};for(e in t)if(void 0!==o.style[e])return t[e]}(),t&&{end:t}),requestAnimationFrame:window.requestAnimationFrame||window.webkitRequestAnimationFrame||window.mozRequestAnimationFrame||window.msRequestAnimationFrame||window.oRequestAnimationFrame||function(e){window.setTimeout(e,1e3/60)},touch:"ontouchstart"in window&&navigator.userAgent.toLowerCase().match(/mobile|tablet/)||window.DocumentTouch&&document instanceof window.DocumentTouch||window.navigator.msPointerEnabled&&0<window.navigator.msMaxTouchPoints||window.navigator.pointerEnabled&&0<window.navigator.maxTouchPoints||!1,mutationobserver:window.MutationObserver||window.WebKitMutationObserver||window.MozMutationObserver||null},isInView:function(e,o){var t=$(e);if(!t.is(":visible"))return!1;var n=l.scrollLeft(),r=l.scrollTop(),i=t.offset(),a=i.left,s=i.top;return o=$.extend({topoffset:0,leftoffset:0},o),s+t.height()>=r&&s-o.topoffset<=r+l.height()&&a+t.width()>=n&&a-o.leftoffset<=n+l.width()},langdirection:"rtl"==r.attr("dir")?"right":"left",isTouch:function(){return r.hasClass("touch")},isSidebarCollapsed:function(){return i.hasClass("aside-collapsed")},isSidebarToggled:function(){return i.hasClass("aside-toggled")},isMobile:function(){return l.width()<o.tablet}}});
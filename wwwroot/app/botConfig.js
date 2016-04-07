import homeTemplate from './views/home/home.view.html';
import clientsTemplate from './views/clients/clients.view.html';
import clientDetailTemplate from './views/clientDetail/clientDetail.view.html';
import itemsTemplate from './views/items/items.view.html';
import itemDetailTemplate from './views/itemDetail/itemDetail.view.html';


import clientsController from './views/clients/clients.controller';
import clientDetailController from './views/clientDetail/clientDetail.controller';
import itemsController from './views/items/items.controller';
import itemDetailController from './views/itemDetail/itemDetail.controller';


export default function _config($stateProvider, $urlRouterProvider,$locationProvider) {
    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });
    

    $stateProvider
        .state('home', {
            url: '/home',
            template: homeTemplate
        })
        .state('clients', {
            url: '/clients',
            template: clientsTemplate,
            controller: clientsController
        })
        .state('client', {
            url: "/client/:clientId", 
            template : clientDetailTemplate,
            controller: clientDetailController
        })
        .state('items', {
            url: '/items',
            template: itemsTemplate,
            controller: itemsController
        })
        .state('item', {
            url: "/item/:itemId", 
            template : itemDetailTemplate,
            controller: itemDetailController
        })
    $urlRouterProvider.otherwise('home');
        
   // $routeProvider.otherwise('home');
}
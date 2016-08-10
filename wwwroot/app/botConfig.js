import homeTemplate from './views/home/home.view.html';
import clientsTemplate from './views/clients/clients.view.html';
import clientDetailTemplate from './views/clientDetail/clientDetail.view.html';
import itemsTemplate from './views/items/items.view.html';
import itemDetailTemplate from './views/itemDetail/itemDetail.view.html';

import createOrderTemplate from './views/createOrder/createOrder.view.html';
import createOrderController from './views/createOrder/createOrder.controller';

import ordersTemplate from './views/orders/orders.view.html';
import ordersController from './views/orders/orders.controller';

import clientsController from './views/clients/clients.controller';
import clientDetailController from './views/clientDetail/clientDetail.controller';
import itemsController from './views/items/items.controller';
import itemDetailController from './views/itemDetail/itemDetail.controller';


export default function _config($stateProvider, $urlRouterProvider,$locationProvider, $mdIconProvider) {
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
            controller: clientsController,
            params: {
                options: null,
                order: null
            }
        })
        .state('client', {
            url: "/client/:clientId", 
            template : clientDetailTemplate,
            controller: clientDetailController,
            params: {
                _data: null,
                clientId: null
            },
            data: { hide:true }
        })
        .state('items', {
            url: '/items',
            template: itemsTemplate,
            controller: itemsController,
            params: {
                page: null,
                options: null,
                orderedItems: null
            }
        })
        .state('item', {
            url: "/item/:itemId", 
            template : itemDetailTemplate,
            controller: itemDetailController,
            params: {
                itemId: null,
                page: null
            }
        })
        .state('orders', {
            url: "/orders", 
            template : ordersTemplate,
            controller: ordersController,
            params: {
            }
        })
        .state('createOrder', {
            url: "/order/creating/stage/:stage", 
            template : createOrderTemplate,
            controller: createOrderController,
            params: {
                stage: null,
                orderedItems: null,
                order: null
            }
        });
    $urlRouterProvider.otherwise('home');
        
    $mdIconProvider
        .icon('call:phone', 'app/icons/ic_view_headline_black_24px.svg');
        
   // $routeProvider.otherwise('home');
}
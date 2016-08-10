import angular from 'angular';
import router from 'angular-ui-router';

import 'angular-material/angular-material.css';
import 'angular-material-data-table/dist/md-data-table.css';
import './app-styles.css';

import material from 'angular-material';
import animate from 'angular-animate';
import aria from 'angular-aria';
import messages from 'angular-messages'
import table from 'angular-material-data-table'

import _ from 'lodash'

import config from './botConfig';
import CacheService from './services/cacheService';
import HttpService from './services/httpService';
import ClientService from './services/clientService';
import ItemService from './services/itemService';
import OrderService from './services/orderService';


var dependencies = [router, material, animate, aria, table, messages];

angular
    .module('Manicure', dependencies)
    .constant('_', _)
    .service('CacheService', CacheService)
    .service('HttpService', HttpService)
    .service('ClientService', ClientService)
    .service('ItemService', ItemService)
    .service('OrderService', OrderService)
    .config(config)


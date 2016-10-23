const ORDER_URL = 'orders/';
const CITIES_URL = 'cities/'

let _OrderService, _ClientService, _ItemService, _$q;

export default class OrderService {
    constructor(HttpService, ClientService, ItemService, CacheService, $q) {
        'ngInject';
        this.CacheService = CacheService;
        this.HttpService = HttpService;
        this.ClientService = ClientService;
        _OrderService = this;
        _ClientService = ClientService;
        _ItemService = ItemService;
        this.$q = $q;
        _$q = $q;
    }

    getCities() {
        return this.CacheService.get(CITIES_URL);
    }

    saveCity(city) {
        this.CacheService.clearCache(CITIES_URL);
        if (city.id !== undefined) {
            var route = CITIES_URL + city.id;
            return this.HttpService.put(route, city);
        } else {
            var route = CITIES_URL + city.id;
            return this.HttpService.post(route, city);
        }
    }

    getOrders(query) {
        let correctQuery = _correctPages(query);
        var prefix = ORDER_URL + 'query/';
        return this.HttpService.post(prefix, correctQuery)
            .then(result => {
                return _$q.all(_.map(result.order, (order) => {
                    return _fillWithCities(order)
                        .then(_fillWithClient)
                        .then(_fillWithItems)
                })).then((transfromedOrders) => {
                    result.order = transfromedOrders;
                    return result;
                })
            });
    }

    getOrder(id) {
        var prefix = ORDER_URL + id;
        return this.HttpService.get(prefix)
            .then(_fillWithCities)
            .then(_fillWithClient)
            .then(_fillWithItems)
    }

    saveOrder(entity) {
        if (entity.id) {
            var prefix = ORDER_URL + entity.id;
            return this.HttpService.put(prefix, entity);
        }
        else {
            var prefix = ORDER_URL;
            return this.HttpService.post(prefix, entity);
        }
    }

    deleteOrder(entity) {
        var prefix = ORDER_URL + entity.id;
        return this.HttpService.remove(prefix, entity);
    }
}

function _correctPages(query) {
    let newQuery = {};
    newQuery.perPage = query.limit;
    newQuery.Page = query.page;
    newQuery.filterText = query.filterText;
    return newQuery;
}

function _fillWithItems(order) {
    return _$q.all(_.map(order.items, (orderItem) => {
        return _ItemService.getItem(orderItem.itemId)
            .then(item => {
                orderItem.item = item;
                return orderItem;
            });
    })).then((items) => {
        order.items = items;
        return order;
    });
}
function _fillWithClient(order) {
    return _ClientService.getClient(order.clientId).then(client => {
        order.client = client;
        return order;
    });
}
function _fillWithCities(order) {
    return _OrderService.getCities().then(cities => {
        let orderCity = _.find(cities, { 'id': order.cityId });
        order.city = orderCity;
        return order;
    });
}

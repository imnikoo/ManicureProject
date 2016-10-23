const ITEM_URL = 'items/';
const ORDER_URL = 'orders/'
const CATEGORIES_URL = 'categories/'
const PLACES_URL = 'PurchasePlaces/'
const CHECK_NAME_URL = 'CheckName/';

let _ItemService;

export default class ItemService {
    constructor(HttpService, CacheService, $q) {
        'ngInject';
        this.CacheService = CacheService;
        this.HttpService = HttpService;
        _ItemService = this;
        this.$q = $q;
    }

    getPurchasePlaces() {
        return this.CacheService.get(PLACES_URL);
    }

    getItems(query) {
        let correctQuery = _correctPages(query);
        var prefix = ITEM_URL + 'query/';
        return this.HttpService.post(prefix, correctQuery).then(queryResult => {
            queryResult.item = _.map(queryResult.item, _transform);
            return queryResult;
        });
    }

    saveCategory(category) {
        this.CacheService.clearCache(CATEGORIES_URL);
        if (category.id !== undefined) {
            var ending = CATEGORIES_URL + category.id;
            return this.HttpService.put(ending, category);
        } else {
            var ending = CATEGORIES_URL + category.id;
            return this.HttpService.post(ending, category);
        }
    }

    checkName(name) {
        var route = ITEM_URL + CHECK_NAME_URL;
        return this.HttpService.post(route, { name });
    }

    getItem(id) {
        var prefix = ITEM_URL + id;
        return this.HttpService.get(prefix).then(value => {
            return _transform(value);
        });
    }

    getOrders() {
        var prefix = ORDER_URL;
        return this.HttpService.get(prefix);
    }

    getCategories() {
        return this.CacheService.get(CATEGORIES_URL);
    }

    saveItem(item) {
        _transformBack(item);
        if (item.id !== undefined) {
            var prefix = ITEM_URL + item.id;
            return this.HttpService.put(prefix, item);
        }
        else {
            var prefix = ITEM_URL;
            return this.HttpService.post(prefix, item);
        }
    }

    deleteItem(entity) {
        var prefix = ITEM_URL + entity.id;
        this.HttpService.remove(prefix, entity);
    }
}

function _correctPages(query) {
    let newQuery = {};
    newQuery.perPage = query.limit;
    newQuery.Page = query.page;
    newQuery.filterText = query.filterText;
    return newQuery;
}

function _transformBack(item) {
    let newItem = _updateBackCategories(item);
    newItem - _updateBackPurchasePlaces(newItem);
    return newItem;
}

function _updateBackPurchasePlaces(item) {
    if (item.purchases.length) {
        item.purchases = _.map(item.purchases, (purchase) => {
            purchase.purchasePlaceId = purchase.purchasePlace.id;
            return purchase;
        });
    }
    return item;
}


function _updateBackCategories(item) {
    item.categoryId = item.category.id;
    return item;
}

function _transform(item) {
    let newItem = _fillWithCategories(item);
    newItem - _fillWithPurchasePlaces(newItem);
    return _fillWithAmountOfOrderedByUser(newItem);
}

function _fillWithPurchasePlaces(item) {
    _ItemService.getPurchasePlaces().then(purchasePlaces => {
        if (item.purchases.length) {
            item.purchases = _.map(item.purchases, (purchase) => {
                purchase.purchasePlace = _.find(purchasePlaces, { 'id': purchase.purchasePlaceId });
                return purchase;
            });
        }
    });
    return item;
}

function _fillWithCategories(item) {
    _ItemService.getCategories().then(categories => {
        let itemCategory = _.find(categories, { 'id': item.categoryId });
        item.category = itemCategory;
    });
    return item;
}

function _fillWithAmountOfOrderedByCustomers(item) {
    //TODO: implement after OrderService is implements
}

function _fillWithAmountOfOrderedByUser(item) {
    if (item.purchases.length) {
        item.orderedByUser = _.reduce(item.purchases, (purchaseCount, purchase) => {
            return purchaseCount + purchase.amount;
        }, 0);
    }
    return item;
}
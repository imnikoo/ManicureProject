const ITEM_URL = 'items/';
const ORDER_URL = 'orders/'
const CATEGORIES_URL = 'categories/'
const PLACES_URL = 'PurchasePlaces/'

export default class ItemService {
    constructor(HttpService) {
        'ngInject';
        this.HttpService = HttpService;
    }

    getPurchasePlaces() {
        var prefix = PLACES_URL;
        return this.HttpService.get(prefix);
    }

    getItems() {
        var prefix = ITEM_URL;
        return this.HttpService.get(prefix);
    }
	
    getItem(id) {
        var prefix = ITEM_URL + id;
        return this.HttpService.get(prefix);
    }

    getOrders() {
        var prefix = ORDER_URL;
        return this.HttpService.get(prefix);
    }

    getCategories() {
        var prefix = CATEGORIES_URL;
        return this.HttpService.get(prefix);
    }
	
    saveItem(entity){
        if(entity.id !== undefined){
            var prefix = ITEM_URL + entity.id;
            return this.HttpService.put(prefix, entity);
        }
        else{
            var prefix =  ITEM_URL;
            return this.HttpService.post(prefix, entity);
        }
    }

    deleteItem(entity) {
        var prefix = ITEM_URL + entity.id;
        this.HttpService.remove(prefix, entity);
    }
}
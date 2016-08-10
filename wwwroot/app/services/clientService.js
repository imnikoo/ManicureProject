const CLIENT_URL = 'clients/';
const CITIES_URL = 'cities/'

let _ClientService;

export default class ClientService {
    constructor(HttpService, CacheService, $q) {
        'ngInject';
        this.CacheService = CacheService;
        this.HttpService = HttpService;
        _ClientService = this;
        this.$q = $q;
    }

    getCities() {
        return this.CacheService.get(CITIES_URL);
    }

    getClients(query) {
        let correctQuery = _correctPages(query);
        var prefix = CLIENT_URL + 'query/';
        return this.HttpService.post(prefix, correctQuery);
    }

    getClient(id) {
        var prefix = CLIENT_URL + id;
        return this.HttpService.get(prefix)
            .then(_fillWithCities); 
    }

    saveClient(entity) {
        if (entity.id) {
            var prefix = CLIENT_URL + entity.id;
            return this.HttpService.put(prefix, entity);
        }
        else {
            var prefix = CLIENT_URL;
            return this.HttpService.post(prefix, entity);
        }
    }

    deleteClient(entity) {
        var prefix = CLIENT_URL + entity.Id;
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

function _fillWithCities(client) {
    let promise = _ClientService.getCities().then(cities => {
        let clientCity = _.find(cities, { 'id': client.cityId });
        client.city = clientCity;
        return client;
    });
    return promise;
}

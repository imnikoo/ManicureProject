const BASE_URL ='http://localhost:62748/api/'// //;//'http://imnikoo-001-site1.ftempurl.com/api/'//

export default class HttpService {
    constructor($http) {
        'ngInject';
        this.http = $http;
    }

    get(prefix) {
        return this.ajax('get', prefix);
    }

    post(prefix, entity) {
        return this.ajax('post', prefix, entity);
    }

    put(prefix, entity) {
        return this.ajax('put', prefix, entity);
    }
	 
    remove(prefix, entity){
        return this.ajax('delete', prefix + entity.id);
	 }

    ajax(method, prefix, entity) {
        var options = {
            method: method,
            url: BASE_URL + prefix,
            headers: {
                'Content-Type': 'application/json'
            }
        };
        if (entity) {
            options.data = entity;
        }
        return this.http(options).then(successCallback, errorCallback);
    }
}

function successCallback(response) {
    return response.data;
}

function errorCallback(response) {
    console.log(response.status);
}
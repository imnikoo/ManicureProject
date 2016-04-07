const CLIENT_URL = 'clients/';
const CITIES_URL = 'cities/'

export default class ClientService {
    constructor(HttpService) {
        'ngInject';
        this.HttpService = HttpService;
    }

    getCities() {
        var prefix = CITIES_URL;
        return this.HttpService.get(prefix);
    }

    getClients() {
        var prefix = CLIENT_URL;
		  return this.HttpService.get(prefix);
    }
	
    getClient(id) {
	     var prefix = CLIENT_URL + id;
	     return this.HttpService.get(prefix);
    }
	
    saveClient(entity){
		 if(entity.Id !== undefined){
		     var prefix = CLIENT_URL + entity.Id;
		     return this.HttpService.put(prefix, entity);
		 }
		 else{
		     var prefix =  CLIENT_URL;
		     return this.HttpService.post(prefix, entity);
		 }
	 }

    deleteClient(entity) {
	     var prefix = CLIENT_URL + entity.Id;
	     this.HttpService.remove(prefix, entity);
    }
}
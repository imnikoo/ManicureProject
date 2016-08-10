export default function OrdersController($scope, $state, $stateParams, $mdDialog, $mdMedia, OrderService) {
	'ngInject';
	var vm = $scope;
	vm.pageIsLoaded = false;
	vm.orders = [];
	vm.selectedOrders = [];

    vm.options = $stateParams.options ? $stateParams.options : {
        autoSelect: false,
        boundaryLinks: false,
        largeEditDialog: false,
        pageSelector: false,
        rowSelection: false
    };

    vm.query = {
        order: '-id',
        limit: 10,
		page: $stateParams.page ? $stateParams.page : 1,
		filterText: ''
    };

	vm.orderItemsToShow = (items) => {
		debugger;
		let quantity = items[0].quantity;
		let isOrderWithTheSameOrderedQuantities = true;
		_.forEach(items, (orderItem)=> {
			if(quantity !== orderItem.quantity) {
				isOrderWithTheSameOrderedQuantities = false; 
			}
		});
		let itemsToReturn = [];
		if(isOrderWithTheSameOrderedQuantities) {
			itemsToReturn = _.take(_.sortBy(items, (orderItem) => {
				return orderItem.item.marginalPrice;
			}), 3);
		} else {
			itemsToReturn =  _.take(_.sortBy(items, (orderItem) => {
				return orderItem.quantity;
			}), 3);
		}
		return _.reverse(itemsToReturn);
	};


	vm.getOrders = (page, limit) => {
		let query = {
			'limit': limit,
			'page': page,
			'filterText': vm.query.filterText
		};
		if (!page) {
            query = vm.query;
        }
		vm.performSearch(query);
	};

	vm.performSearch = (query) => {
		vm.promise = OrderService.getOrders(query).then(value => {
			vm.orders = value.order;
			vm.queryResult = {
				'total': value.total,
			};
			vm.pageIsLoaded = true;
		});
	};

    vm.goToOrder = (orderId) => {
        //$state.go('item', { orderId, page: vm.query.page });
    };
    vm.getOrders();
}
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
		_.throttle(() => {
			vm.promise = OrderService.getOrders(query).then(value => {
			vm.orders = value.order;
			vm.queryResult = {
				'total': value.total,
			};
			vm.pageIsLoaded = true;
			});
		}, 500, { 'trailing': false })();
	};
		
	vm.getOrderClass = (order) => {
		if(order.state === 2) {
			return 'payed';
		} else if(order.state === 1) {
			return 'pre-payed';
		} else if(order.state === 3) {
			return 'closed';
		}
		return '';
	}

    vm.goToOrder = (order) => {
        $state.go('createOrder', { stage: 2, order: order });
    };
    vm.getOrders();
}
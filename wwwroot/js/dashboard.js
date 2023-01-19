"use strict"

var connection = new signalR.HubConnectionBuilder().withUrl("/dashboardHub").build()

$(function () {
	connection.start().then(function () {
		// alert("connected to dashboarsdHub")

		InvokeProducts()
		InvokeSales()
		InvokeCustomers()
	}).catch(function (err) {
		return alert(err.toString())
	});
})

// Product
function InvokeProducts() {
	// alert("InvokeProducts")
	connection.invoke("SendProducts").catch(function (err) {
		return console.error(err.toString())
	})
}

connection.on("ReceivedProducts", function (products) {
	// console.table(products)
	BindProductsToGrid(products)
})

function BindProductsToGrid(products) {
	$('#tblProduct tbody').empty()

	var tr
	$.each(products, function (index, product) {
		tr = $('<tr/>')
		//tr.append(`<td>${(index + 1)}</td>`)
		tr.append(`<td>${product.id}</td>`)
		tr.append(`<td>${product.name}</td>`)
		tr.append(`<td>${product.category}</td>`)
		tr.append(`<td>${product.price}</td>`)
		$('#tblProduct tbody').append(tr)
	})
}

connection.on("ReceivedProductsForGraph", function (productsForGraph) {
	console.table(productsForGraph)
	BindProductsForGraph(productsForGraph)
})

function BindProductsForGraph(productsForGraph) {
	let labels = []
	let data = []
	$.each(productsForGraph, function (index, productForGraph) {
		labels.push(productForGraph.category)
		data.push(productForGraph.products)
	})

	DestroyCanvasIfExists('canvasProducts')

	const context = document.getElementById('canvasProducts').getContext('2d')
	const myChart = new Chart(context, {
		type: 'doughnut',
		data: {
			labels: labels,
			datasets: [{
				label: '# of Products',
				data: data,
				backgroundColor: backgroundColors,
				borderColor: borderColors,
				borderWidth: 1
			}]
		},
		options: {
			scales: {
				y: {
					beginAtZero: true
				}
			}
		}
	})
}


// Sale
function InvokeSales() {
	connection.invoke("SendSales").catch(function (err) {
		return console.error(err.toString())
	})
}

connection.on("ReceivedSales", function (sales) {
	// console.table(sales)
	BindSalesToGrid(sales)
})

function BindSalesToGrid(sales) {
	$('#tblSale tbody').empty()

	var tr
	$.each(sales, function (index, sale) {
		tr = $('<tr/>')
		//tr.append(`<td>${(index + 1)}</td>`)
		tr.append(`<td>${sale.id}</td>`)
		tr.append(`<td>${sale.customer}</td>`)
		tr.append(`<td>${sale.amount}</td>`)
		tr.append(`<td>${sale.purchasedOn}</td>`)
		$('#tblSale tbody').append(tr)
	})
}

connection.on('ReceivedSalesForGraph', function (salesForGraph) {
	// console.table(salesForGraph)
	BindSalesForGraph(salesForGraph)
})

function BindSalesForGraph(salesForGraph) {
	let labels = []
	let data = []

	$.each(salesForGraph, function (index, saleForGraph) {
		labels.push(saleForGraph.purchasedOn)
		data.push(saleForGraph.amount)
	})

	DestroyCanvasIfExists('canvasSales')

	const context = document.getElementById('canvasSales').getContext('2d')
	const myChart = new Chart(context, {
		type: 'line',
		data: {
			labels: labels,
			datasets: [{
				label: 'Sales',
				data: data,
				backgroundColor: backgroundColors,
				borderColor: borderColors,
				borderWidth: 1
			}]
		},
		options: {
			scales: {
				y: {
					beginAtZero: true
				}
			}
		}
	})
}


// Customer
function InvokeCustomers() {
	connection.invoke("SendCustomers").catch(function (err) {
		return console.error(err.toString())
	})
}

connection.on("ReceivedCustomers", function (customers) {
	// console.table(customers)
	BindCustomersToGrid(customers)
})

function BindCustomersToGrid(customers) {
	$('#tblCustomer tbody').empty()

	var tr
	$.each(customers, function (index, customer) {
		tr = $('<tr/>')
		tr.append(`<td>${customer.id}</td>`)
		tr.append(`<td>${customer.name}</td>`)
		tr.append(`<td>${customer.gender}</td>`)
		tr.append(`<td>${customer.mobile}</td>`)
		$('#tblCustomer tbody').append(tr)
	})
}

connection.on("ReceivedCustomersForGraph", function (customersForGraph) {
	// console.table(customersForGraph)
	BindCustomersForGraph(customersForGraph)
})

function BindCustomersForGraph(customersForGraph) {
	let datasets = []
	let labels = ['Customers']
	let data = []

	$.each(customersForGraph, function (index, customerForGraph) {
		data = []
		data.push(customerForGraph.customers)
		const dataset = {
			label: customerForGraph.gender,
			data: data,
			backgroundColor: backgroundColors[index],
			borderColor: borderColors[index],
			borderWidth: 1
		}

		datasets.push(dataset)
	})

	DestroyCanvasIfExists('canvasCustomers')

	const context = document.getElementById('canvasCustomers').getContext('2d')
	const myChart = new Chart(context, {
		type: 'bar',
		data: {
			labels: labels,
			datasets: datasets
		},
		options: {
			scales: {
				y: {
					beginAtZero: true
				}
			}
		}
	})
}

// supporting functions for Graphs
function DestroyCanvasIfExists(canvasId) {
	const chartStatus = Chart.getChart(canvasId)
	if (chartStatus != undefined) {
		chartStatus.destroy()
	}
}

var backgroundColors = [
	'rgba(255, 99, 132, 0.2)',
	'rgba(54, 162, 235, 0.2)',
	'rgba(255, 206, 86, 0.2)',
	'rgba(75, 192, 192, 0.2)',
	'rgba(153, 102, 255, 0.2)',
	'rgba(255, 159, 64, 0.2)'
]

var borderColors = [
	'rgba(255, 99, 132, 1)',
	'rgba(54, 162, 235, 1)',
	'rgba(255, 206, 86, 1)',
	'rgba(75, 192, 192, 1)',
	'rgba(153, 102, 255, 1)',
	'rgba(255, 159, 64, 1)'
]
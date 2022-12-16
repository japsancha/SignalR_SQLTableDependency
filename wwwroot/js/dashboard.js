"use strict"

var connection = new signalR.HubConnectionBuilder().withUrl("/dashboardHub").build()

$(function () {
	connection.start().then(function () {
		// alert("connected to dashboarsdHub")

		InvokeProducts()
	}).catch(function (err) {
		return alert(err.toString())
	});
})

// Product
function InvokeProducts() {
	connection.invoke("SendProducts").catch(function (err) {
		return console.error(err.toString())
	})
}

connection.on("ReceivedProducts", function (products) {
	BindProductsToGrid(products)
})

function BindProductsToGrid(products) {
	$('#tblProduct tbody').empty()

	var tr
	$.each(products, function (product) {
		tr = $('<tr/>')
		//tr.append(`<td>${(index + 1)}</td>`)
		tr.append(`<td>${product.id}</td>`)
		tr.append(`<td>${product.name}</td>`)
		tr.append(`<td>${product.category}</td>`)
		tr.append(`<td>${product.price}</td>`)
		$('#tblProduct tbody').append(tr)
	})
}

// Sale
function InvokeSales() {
	connection.invoke("SendSales").catch(function (err) {
		return console.error(err.toString())
	})
}

connection.on("ReceivedSales", function (sales) {
	BindSalesToGrid(sales)
})

function BindSalesToGrid(sales) {
	$('#tblSale tbody').empty()

	var tr
	$.each(sales, function (sale) {
		tr = $('<tr/>')
		//tr.append(`<td>${(index + 1)}</td>`)
		tr.append(`<td>${sale.id}</td>`)
		tr.append(`<td>${sale.name}</td>`)
		tr.append(`<td>${sale.category}</td>`)
		tr.append(`<td>${sale.price}</td>`)
		$('#tblSale tbody').append(tr)
	})
}
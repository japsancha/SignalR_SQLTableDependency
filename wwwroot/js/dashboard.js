"use strict"

var connection = new signalR.HubConnectionBuilder().withUrl("/dashboardHub").build()
$(function () {
	connection.start().then(function () {
		alert("connected to dashboarsdHub")

		InvokeProducts()
	}).catch(function (err) {
		return alert(err.toString())
	});
})

connection.on("ReceivedProducts", function (products) {
	BindProductsToGrid(products)
})

function InvokeProducts() {
	alert("invoke products")
	connection.invoke("SendProducts").catch(function (err) {
		return console.error(err.toString())
	})
}

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

"use strict"

var connection = new signalR.HubConnectionBuilder().withUrl("/dashboardHub").build()

connection.start().then(function () {
	alert("connected to dashboarsdHub")
}).catch(function (err) {
	return alert(err.toString())
});

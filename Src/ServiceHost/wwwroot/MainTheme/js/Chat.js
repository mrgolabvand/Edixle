"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/ChatHub")
    .build();
this.connection.start()
    .catch(function (err) {
        return console.error(err.toString());
    });

connection.on('Refresh',
    function () {
        history.go();
    });
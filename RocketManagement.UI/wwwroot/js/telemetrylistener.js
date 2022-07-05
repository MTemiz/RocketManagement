"use strict";

debugger;

var connection = new signalR.HubConnectionBuilder().configureLogging(signalR.LogLevel.Debug).withUrl("/TelemetryMessageHub", {
    skipNegotiation: true,
    transport: signalR.HttpTransportType.WebSockets
}).build();


connection.on("TelemetryReceived", function (rocketSystemId, altitude, speed, acceleration, thrust, temperature) {
    debugger;

    var row = $("<tr></tr>");
    var col1 = $("<td>" + rocketSystemId + "</td>");
    var col2 = $("<td>" + altitude + "</td>");
    var col3 = $("<td>" + speed + "</td>");
    var col4 = $("<td>" + acceleration + "</td>");
    var col5 = $("<td>" + thrust + "</td>");
    var col6 = $("<td>" + temperature + "</td>");

    row.append(col1, col2, col3, col4, col5, col6);

    $('#telemetryTable').prepend(row);
});

connection.start().then(() => {
    debugger;

    var parameter = getUrlParameter("id");

    JoinGroup(parameter);
});

function JoinGroup(groupName) {
    connection.invoke("AddToGroup", groupName);
}

function getUrlParameter(name) {
    name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
    var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
    var results = regex.exec(location.href);
    return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, '    '));
};
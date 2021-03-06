﻿var $userHelloEmail = $("#userHelloEmail").text().split(" ");
var userEmail = $userHelloEmail[1];

TransactionsViewModel = function (transactions) {
    self.transactions = ko.observableArray(
        ko.utils.arrayMap(transactions, function (t) {
        return {
            UserFrom: setUserFrom(t),
            UserTo: t.UserTo,
            Type: t.Type,
            Date: t.Date,
            Amount: (t.Amount).toFixed(2),
            Id: t.Id,
            incomingOrNot: setIncomingOrNotStyle(t)
        };
        }));
};

function setIncomingOrNotStyle(item) {
    var style;
    if (item.Type === "Transfer") {
        if (item.UserFrom === userEmail)
            style = "text-danger";
        else
            style = "text-success";
    }
    else if (item.Type === "Withdrawal") {
        style = "text-danger";
    }
    else style = "text-success";
    return style;
};

function setUserFrom(item) {
    if (item.Type !== "Transfer")
        return "Internal";
    else return item.UserFrom;
};


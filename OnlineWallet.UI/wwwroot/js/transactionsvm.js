TransactionsViewModel = function (transactions) {
    self.transactions = ko.observableArray(
        ko.utils.arrayMap(transactions, function (t) {
            var user = setUser(t);
        return {
            UserFrom: user,
            UserTo: user,
            Type: t.Type,
            Date: t.Date,
            Amount: (t.Amount).toFixed(2),
            Id: t.Id
        };
        }));

    function setUser(item) {
        if (item.Type !== "Transfer")
            return "Internal";
        else return item.UserFrom;
    };

};
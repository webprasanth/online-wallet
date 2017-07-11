$(document).ready(function () {
    $.ajax({
        type: 'GET',
        url: '/api/User/Dashboard',
        dataType: 'json'
    })
    .done(function(data) {
            var transfers = data.transfersCount;
            var deposits = data.depositsCount;
            var withdrawals = data.withdrawalsCount;
            var incomes = data.incomes.toFixed(2);
            var outcomes = data.outcomes.toFixed(2);

            if (transfers === 0 && deposits === 0 && withdrawals === 0) {
                $("#charts").remove();
                $("body").append('<div class="col-md-12"><h2><center>To display charts you need to do some transaction</center></h2></div>');
            } else {
                createTDWchart(transfers, deposits, withdrawals);
                createIOchart(incomes, outcomes);
            }
        })
    .fail(function (request, status, error) {
            alert("Cannot load history");
        })
});


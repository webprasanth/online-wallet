$(document).ready(function () {
    var currentUserEmail
    $.ajax({
        type: 'GET',
        url: '/api/User',
        dataType: 'json',
        success: function (user) {
            currentUserEmail = user.email;
        },
        error: function (request, status, error) {
            alert("Cannot load history");
        }
    });

    $.ajax({
        type: 'GET',
        url: '/api/Activity',
        dataType: 'json',
        success: function (data) {
            var transfers = 0;
            var deposits = 0;
            var withdrawals = 0;
            var incomes = 0;
            var outcomes = 0;

            $.each(data,
                function (index, val) {
                    if (val.type === "Transfer") {
                        transfers++;
                        if (val.userTo === currentUserEmail) {
                            incomes += val.amount;
                        } else {
                            outcomes += val.amount;
                        }
                    } else if (val.type === "Deposit") {
                        deposits++;
                        incomes += val.amount;
                    } else {
                        withdrawals++;
                        outcomes += val.amount;
                    }
                });
            console.log(transfers+" " + deposits + " " + withdrawals);
            if (transfers === 0 && deposits === 0 && withdrawals === 0) {
                $("#charts").remove();
                $("body").append('<div class="col-md-12"><h2><center>To display charts you need to do some transaction</center></h2></div>');
            } else {
                createTDWchart(transfers, deposits, withdrawals);
                createIOchart(incomes, outcomes);
            }

        },
        error: function (request, status, error) {
            alert("Cannot load history");
        }
    });

});


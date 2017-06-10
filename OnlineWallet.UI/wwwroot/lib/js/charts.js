function createTDWchart(t, d, w) {
    var ctx = document.getElementById('tdwChart').getContext('2d');
    var chart = new Chart(ctx,
        {
            // The type of chart we want to create
            type: 'pie',

            // The data for our dataset
            data: {
                labels: ["Transfers", "Deposits", "Withdrawals"],
                datasets: [
                    {
                        label: "Types",
                        backgroundColor: ['rgb(255, 140, 0)', 'rgb(25, 99, 132)', 'rgb(25, 99, 32)'],
                        borderColor: 'rgb(125, 125, 125)',
                        data: [t, d, w]
                    }
                ]
            },

            // Configuration options go here
            options: {}
        });
};

function createIOchart(t, d, w) {
    var ctx = document.getElementById('ioChart').getContext('2d');
    var chart = new Chart(ctx,
        {
            // The type of chart we want to create
            type: 'pie',

            // The data for our dataset
            data: {
                labels: ["Incomes", "Outcomes"],
                datasets: [
                    {
                        label: "IO",
                        backgroundColor: ['rgb(25, 99, 32)', 'rgb(230, 10, 0)'],
                        borderColor: 'rgb(125, 125, 125)',
                        data: [t, d]
                    }
                ]
            },

            // Configuration options go here
            options: {}
        });
};
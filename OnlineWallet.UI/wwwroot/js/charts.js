function createTDWchart(t, d, w) {
    var ctx = document.getElementById('tdwChart').getContext('2d');
    var chart = new Chart(ctx,
        {
            type: 'pie',

            data: {
                labels: ["Transfers", "Deposits", "Withdrawals"],
                datasets: [
                    {
                        label: "Types",
                        backgroundColor: ['rgb(255, 140, 0)', 'rgb(25, 99, 132)', 'rgb(25, 99, 32)'],
                        borderColor: 'rgb(72,85,99)',
                        data: [t, d, w]
                    }
                ]
            },

            options: {
                legend: {
                    labels: {
                        fontColor: '#ebebeb'
                    }
                }
            }
        });
};

function createIOchart(t, d, w) {
    var ctx = document.getElementById('ioChart').getContext('2d');
    var chart = new Chart(ctx,
        {
            type: 'pie',

            data: {
                labels: ["Incomes", "Outcomes"],
                datasets: [
                    {
                        label: "IO",
                        backgroundColor: ['rgb(25, 99, 32)', 'rgb(230, 10, 0)'],
                        borderColor: 'rgb(72,85,99)',
                        data: [t, d]
                    }
                ]
            },

            options: {
                legend: {
                    labels: {
                        fontColor: '#ebebeb'
                    }
                }
            }
        });
};
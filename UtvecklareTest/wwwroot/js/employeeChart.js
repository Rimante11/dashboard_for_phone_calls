function createEmployeeChart(employeeStats) {
    const employeeCtx = document.getElementById('employeeChart')?.getContext('2d');
    if (!employeeCtx || !employeeStats) return;

    const colors = [
        'rgba(255, 99, 132, 0.8)',
        'rgba(54, 162, 235, 0.8)',
        'rgba(255, 205, 86, 0.8)',
        'rgba(75, 192, 192, 0.8)',
        'rgba(153, 102, 255, 0.8)',
        'rgba(255, 159, 64, 0.8)',
        'rgba(255, 193, 7, 0.8)',
        'rgba(40, 167, 69, 0.8)',
        'rgba(220, 53, 69, 0.8)',
        'rgba(23, 162, 184, 0.8)'
    ];
    
    const backgroundColors = employeeStats.map((_, i) => colors[i % colors.length]);
    const borderColors = backgroundColors.map(c => c.replace("0.8", "1"));
    
    new Chart(employeeCtx, {
        type: 'bar',
        data: {
            labels: employeeStats.map(e => e.name),
            datasets: [{
                label: 'Totala Samtal',
                data: employeeStats.map(e => e.totalCalls),
                backgroundColor: backgroundColors,
                borderColor: borderColors,
                borderWidth: 2
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                tooltip: {
                    callbacks: {
                        label: function(context) {
                            return `${context.label}: ${context.raw} samtal`;
                        }
                    }
                },
                title: {
                    display: true,
                },
                legend: {
                    display: false
                }
            },
            scales: {
                y: {
                    beginAtZero: true,
                    ticks: {
                        stepSize: 1
                    }
                }
            }
        }
    });
}
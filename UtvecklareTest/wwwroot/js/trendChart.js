function createTrendChart(employees, employeeStats, dates) {
    const trendCtx = document.getElementById('trendChart')?.getContext('2d');
    if (!trendCtx || !employees || !employeeStats || !dates) return;

    const datasets = employees.map((employee, index) => {
        const employeeData = employeeStats.find(e => e.name === employee);
        const colors = [
            '#FF6B6B',  
            '#4ECDC4',
            '#45B7D1',
            '#96CEB4',
            '#FFEAA7',
            '#DDA0DD'
        ];
        
        return {
            label: employee,
            data: dates.map(date => {
                const dayData = employeeData.dailyCalls.find(d => d.date === date);
                return dayData ? dayData.calls : 0;
            }),
            borderColor: colors[index % colors.length],
            backgroundColor: colors[index % colors.length],
            fill: false,
            tension: 0.3,
            borderWidth: 2,
            pointRadius: 4,
            pointHoverRadius: 6
        };
    });

    new Chart(trendCtx, {
        type: 'line',
        data: {
            labels: dates.map(date => {
                const d = new Date(date);
                return d.toLocaleDateString('sv-SE', { month: 'short', day: 'numeric' });
            }),
            datasets: datasets
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                title: {
                    display: true,
                },
                legend: {
                    display: true,
                    position: 'bottom'
                },
                tooltip: {
                    mode: 'index',
                    intersect: false,
                    callbacks: {
                        title: function(context) {
                            const date = new Date(dates[context[0].dataIndex]);
                            return date.toLocaleDateString('sv-SE', {
                                year: 'numeric',
                                month: 'long',
                                day: 'numeric'
                            });
                        }
                    }
                }
            },
            scales: {
                y: {
                    beginAtZero: true,
                    ticks: {
                        stepSize: 1
                    }
                },
                x: {
                    ticks: {
                        maxTicksLimit: 10
                    }
                }
            },
            interaction: {
                intersect: false,
                mode: 'index'
            }
        }
    });
}
function createTrendChart(employees, employeeStats, dates) {
    const trendCtx = document.getElementById('trendChart')?.getContext('2d');
    if (!trendCtx || !employees || !employeeStats || !dates) return;

    const barDatasets = employees.map((employee, index) => {
        const employeeData = employeeStats.find(e => e.name === employee);
        const colors = [
            '#3498DB', 
            '#E67E22',
            '#699c5cff',
            '#E74C3C'
        ];
        
        return {
            label: employee,
            data: dates.map(date => {
                const dayData = employeeData.dailyCalls.find(d => d.date === date);
                return dayData ? dayData.calls : 0;
            }),
            backgroundColor: colors[index % colors.length],
            borderColor: colors[index % colors.length],
            borderWidth: 0,
            borderRadius: 2,
            borderSkipped: false
        };
    });

    new Chart(trendCtx, {
        type: 'bar',
        data: {
            labels: dates.map(date => {
                const d = new Date(date);
                return d.toLocaleDateString('sv-SE', { month: 'short', day: 'numeric' });
            }),
            datasets: barDatasets
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                title: {
                    display: false
                },
                legend: {
                    display: true,
                    position: 'bottom',
                    labels: {
                        padding: 20,
                        font: {
                            size: 12
                        },
                        usePointStyle: true,
                        pointStyle: 'rect'
                    }
                },
                tooltip: {
                    mode: 'index',
                    intersect: false,
                    backgroundColor: 'rgba(255, 255, 255, 0.95)',
                    titleColor: '#333',
                    bodyColor: '#666',
                    borderColor: '#ddd',
                    borderWidth: 1,
                    cornerRadius: 8,
                    callbacks: {
                        title: function(context) {
                            return context[0].label;
                        },
                        label: function(context) {
                            return context.dataset.label + ': ' + context.parsed.y + ' samtal';
                        }
                    }
                }
            },
            scales: {
                y: {
                    beginAtZero: true,
                    grid: {
                        color: 'rgba(0,0,0,0.08)',
                        drawBorder: false
                    },
                    ticks: {
                        stepSize: 5,
                        font: {
                            size: 11
                        },
                        color: '#666'
                    }
                },
                x: {
                    grid: {
                        display: false
                    },
                    ticks: {
                        maxTicksLimit: 10,
                        font: {
                            size: 11,
                            weight: '500'
                        },
                        color: '#333'
                    }
                }
            },
            interaction: {
                intersect: true,
                mode: 'index'
            },
            layout: {
                padding: {
                    top: 20,
                    bottom: 10
                }
            }
        }
    });
}
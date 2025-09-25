function createMonthlyChart(employees, employeeStats, dates) {
    const monthlyCtx = document.getElementById('monthlyChart')?.getContext('2d');
    if (!monthlyCtx || !employees || !employeeStats || !dates) return;

    const monthlyData = {};
    
    employeeStats.forEach(employee => {
        monthlyData[employee.name] = {};
        
        employee.dailyCalls.forEach(dailyCall => {
            const date = new Date(dailyCall.date);
            const monthKey = `${date.getFullYear()}-${String(date.getMonth() + 1).padStart(2, '0')}`;
            
            if (!monthlyData[employee.name][monthKey]) {
                monthlyData[employee.name][monthKey] = 0;
            }
            monthlyData[employee.name][monthKey] += dailyCall.calls;
        });
    });
    
    const allMonths = new Set();
    Object.values(monthlyData).forEach(employeeMonths => {
        Object.keys(employeeMonths).forEach(month => allMonths.add(month));
    });
    const sortedMonths = Array.from(allMonths).sort();
    
    const datasets = employees.map((employee, index) => {
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
            data: sortedMonths.map(month => {
                return monthlyData[employee][month] || 0;
            }),
            borderColor: colors[index % colors.length],
            backgroundColor: colors[index % colors.length],
            fill: false,
            tension: 0.3,
            borderWidth: 3,
            pointRadius: 6,
            pointHoverRadius: 8,
            pointBackgroundColor: colors[index % colors.length],
            pointBorderColor: '#fff',
            pointBorderWidth: 2,
            pointHoverBackgroundColor: '#fff',
            pointHoverBorderColor: colors[index % colors.length],
            pointHoverBorderWidth: 3
        };
    });

    new Chart(monthlyCtx, {
        type: 'line',
        data: {
            labels: sortedMonths.map(month => {
                const [year, monthNum] = month.split('-');
                const date = new Date(year, monthNum - 1);
                return date.toLocaleDateString('sv-SE', { month: 'long', year: 'numeric' });
            }),
            datasets: datasets
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    display: false
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
                    displayColors: true,
                    callbacks: {
                        title: function(context) {
                            const monthKey = sortedMonths[context[0].dataIndex];
                            const [year, monthNum] = monthKey.split('-');
                            const date = new Date(year, monthNum - 1);
                            return date.toLocaleDateString('sv-SE', {
                                year: 'numeric',
                                month: 'long'
                            });
                        },
                        label: function(context) {
                            return `${context.dataset.label}: ${context.parsed.y} samtal`;
                        }
                    }
                }
            },
            scales: {
                x: {
                    grid: {
                        display: false
                    },
                    ticks: {
                        maxTicksLimit: 12,
                        font: {
                            size: 12,
                            color: '#666'
                        },
                        maxRotation: 45,
                        minRotation: 0
                    }
                },
                y: {
                    beginAtZero: true,
                    grid: {
                        color: 'rgba(200, 200, 200, 0.3)',
                        drawBorder: false
                    },
                    ticks: {
                        font: {
                            size: 11,
                            color: '#666'
                        },
                        stepSize: 5
                    }
                }
            },
            interaction: {
                intersect: false,
                mode: 'index'
            },
            elements: {
                point: {
                    radius: 4,
                    hoverRadius: 6
                },
                line: {
                    tension: 0.4
                }
            }
        }
    });
}
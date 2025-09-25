document.addEventListener('DOMContentLoaded', function() {
    if (typeof employees !== 'undefined' && typeof employeeStats !== 'undefined' && typeof dates !== 'undefined') {
        
        try {
            createEmployeeChart(employeeStats);
            createMonthlyChart(employees, employeeStats, dates);
            createTrendChart(employees, employeeStats, dates);
        } catch (error) {
            console.error('Error initializing dashboard charts:', error);
        }
    } else {
        console.warn('Dashboard data not available');
    }
});
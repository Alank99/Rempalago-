/**
 * @param {number} alpha Indicated the transparency of the color
 * @returns {string} A string of the form 'rgba(240, 50, 123, 1.0)' that represents a color
 */
function random_color(alpha=1.0)
{
    const r_c = () => Math.round(Math.random() * 255)
    return `rgba(${r_c()}, ${r_c()}, ${r_c()}, ${alpha}`;
}

Chart.defaults.font.size = 16;

const ctx = document.getElementById('chartCanvas').getContext('2d');

const data = {
    labels: ['Índice', 'Valor'],
    datasets: [{
        data: [1, 2],
        backgroundColor: ['#ff6384', '#36a2eb']
    }]
};

const options = {
    plugins: {
        legend: {
            display: ture
        },
        datalabels: {
            color: '#fff'
        }
    }
};

const chart = new Chart(ctx, {
    type: 'table',
    data: data,
    options: options
});
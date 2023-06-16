const url = 'https://fredhut.com/api/'

/**
 * @param {number} alpha Indicated the transparency of the color
 * @returns {string} A string of the form 'rgba(240, 50, 123, 1.0)' that represents a color
 */
function random_color(alpha=1.0)
{
    const r_c = () => Math.round(Math.random() * 255)
    return `rgba(${r_c()}, ${r_c()}, ${r_c()}, ${alpha}`;
}

function convertRange( value, r1, r2 ) { 
    return ( value - r1[ 0 ] ) * ( r2[ 1 ] - r2[ 0 ] ) / ( r1[ 1 ] - r1[ 0 ] ) + r2[ 0 ];
}

function getColor(value, range){
    //value from 0 to 1
    value = convertRange(value, range, [0, 1])
    var hue=((1-value)*120).toString(10);
    return ["hsl(",hue,",100%,50%)"].join("");
}

// Speedrun data
try
{
    const speedrun_response = await fetch(url + 'vistas/topTimes/20',{
        method: 'GET'
    })

    console.log('Got a response correctly')

    if(speedrun_response.ok)
    {
        console.log('Response is ok. Converting to JSON.')

        let results = await speedrun_response.json()

        console.log(results)
        console.log('Data converted correctly. Plotting chart.')

        const values = Object.values(results)

        // In this case, we just separate the data into different arrays using the map method of the values array. This creates new arrays that hold only the data that we need.
        const names = values.map(e => e['UserName'])
        const speedrun_colors = values.map(e => getColor(e['Time'], [0, results.at(-1).Time]))
        const speedrun_borders = values.map(e => 'rgba(0, 0, 255, 1.0)')
        const speedrun_completion = values.map(e => e['Time'])
        
        const ctx_speedrun = document.getElementById('speedrun').getContext('2d');
        const speedrunChart = new Chart(ctx_speedrun, 
            {
                type: 'line',
                data: {
                    labels: names,
                    datasets: [
                        {
                            label: 'Tiempo de compleción',
                            backgroundColor: speedrun_colors,
                            borderColor: speedrun_borders,
                            borderWidth: 2,
                            data: speedrun_completion
                        }
                    ]
                }
            })
    }
}
catch(error)
{
    console.log(error)
}

// Best Weapons data
try
{
    const response = await fetch(url + 'vistas/topWeapons/100',{
        method: 'GET'
    })

    console.log('Got a response correctly')

    if(response.ok)
    {
        console.log('Response is ok. Converting to JSON.')

        let results = await response.json()

        console.log(results)
        console.log('Data converted correctly. Plotting chart.')

        const values = Object.values(results)
        
        // In this case, we just separate the data into different arrays using the map method of the values array. This creates new arrays that hold only the data that we need.
        const names = values.map(e => e['Weapon'])
        const type = values.map(e => getColor(e['Kills'], [0, results.at(0).Kills]))
        const borders = values.map(e => 'rgba(0, 0, 0, 1.0)')
        const kills = values.map(e => e['Kills'])
        
        const ctx = document.getElementById('weapons').getContext('2d');
        const chart = new Chart(ctx, 
            {
                type: 'pie',
                data: {
                    labels: names,
                    datasets: [{
                        data : kills
                    }]
                }
            })
    }
}
catch(error)
{
    console.log(error)
}

// Active users data
try
{
    const response = await fetch(url + 'vistas/new/20',{
        method: 'GET'
    })

    console.log('Got a response correctly')

    if(response.ok)
    {
        console.log('Response is ok. Converting to JSON.')

        let results = await response.json()

        console.log(results)
        console.log('Data converted correctly. Plotting chart.')

        const values = Object.values(results)
        
        // In this case, we just separate the data into different arrays using the map method of the values array. This creates new arrays that hold only the data that we need.
        const names = values.map(e => e['Date'])
        const type = values.map(e => getColor(e['newPlayers'], [0, results.at(0).newPlayers]))
        const borders = values.map(e => 'rgba(0, 0, 0, 1.0)')
        const plays = values.map(e => e['newPlayers'])
        
        const ctx_speedrun = document.getElementById('new').getContext('2d');
        const pieChart = new Chart(ctx_speedrun, 
            {
                type: 'bar',
                data: {
                    labels: names,
                    datasets: [
                        {
                            label: 'Número de nuevos usuarios por dia',
                            backgroundColor: type,
                            borderColor: borders,
                            borderWidth: 2,
                            data: plays
                        }
                    ]
                }
            })
    }
}
catch(error)
{
    console.log(error)
}

// Active users data
try
{
    const response = await fetch(url + 'vistas/active',{
        method: 'GET'
    })

    console.log('Got a response correctly')

    if(response.ok)
    {
        console.log('Response is ok. Converting to JSON.')

        let results = await response.json()

        console.log(results)
        console.log('Data converted correctly. Plotting chart.')

        const values = Object.values(results)
        
        // In this case, we just separate the data into different arrays using the map method of the values array. This creates new arrays that hold only the data that we need.
        const names = values.map(e => e['UserName'])
        const type = values.map(e => getColor(e['TotalPlaythroughs'], [0, results.at(0).TotalPlaythroughs]))
        const borders = values.map(e => 'rgba(0, 0, 0, 1.0)')
        const plays = values.map(e => e['TotalPlaythroughs'])
        
        const ctx_speedrun = document.getElementById('users').getContext('2d');
        const speedrunChart = new Chart(ctx_speedrun, 
            {
                type: 'bar',
                data: {
                    labels: names,
                    datasets: [
                        {
                            label: 'Número de juegos por usuario',
                            backgroundColor: type,
                            borderColor: borders,
                            borderWidth: 2,
                            data: plays
                        }
                    ]
                }
            })
    }
}
catch(error)
{
    console.log(error)
}
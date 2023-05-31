const prefix = "/api/vistas/";

export function addEndpoints(app, conn) {

    const connection = conn;

    // get list loot tables
    app.get(prefix + "playthrough/:id", async (request, response)=>{
        try
        {
            const query = `select * from V_user_playthrough where user_id = ${request.params.id}`
            const [results, fields] = await connection.execute(query);
    
            console.log(`${results.length} rows returned`)
            response.json(results)
        }
        catch(error)
        {
            response.status(500)
            response.json(error)
            console.log(error)
        }
    });

    // get list of drops for given loot table
    app.get(prefix + "active", async (request, response)=>{
        try
        {
            const query = `select * from active_players`
            const [results, fields] = await connection.execute(query);
    
            console.log(`${results.length} rows returned`)
            response.json(results)
        }
        catch(error)
        {
            response.status(500)
            response.json(error)
            console.log(error)
        }
    });


    // get loot info
    app.get(prefix + "inactive", async (request, response)=>{
        try
        {
            const query = `select * from inactive_players`
            const [results, fields] = await connection.execute(query);
    
            console.log(`${results.length} rows returned`)
            response.json(results)
        }
        catch(error)
        {
            response.status(500)
            response.json(error)
            console.log(error)
        }
    });

    // get loot info
    app.get(prefix + "new", async (request, response)=>{
        try
        {
            const query = `select * from new_users LIMIT 10`
            const [results, fields] = await connection.execute(query);
    
            console.log(`${results.length} rows returned`)
            response.json(results)
        }
        catch(error)
        {
            response.status(500)
            response.json(error)
            console.log(error)
        }
    });

    // get loot info
    app.get(prefix + "topTimes", async (request, response)=>{
        try
        {
            const query = `select * from speedruns LIMIT 10`
            const [results, fields] = await connection.execute(query);
    
            console.log(`${results.length} rows returned`)
            response.json(results)
        }
        catch(error)
        {
            response.status(500)
            response.json(error)
            console.log(error)
        }
    });

    // get loot info
    app.get(prefix + "topWeapons", async (request, response)=>{
        try
        {
            const query = `select * from top_weapons LIMIT 10`
            const [results, fields] = await connection.execute(query);
    
            console.log(`${results.length} rows returned`)
            response.json(results)
        }
        catch(error)
        {
            response.status(500)
            response.json(error)
            console.log(error)
        }
    });
}

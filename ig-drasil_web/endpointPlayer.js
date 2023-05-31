const prefix = "/api/player/";

export function addEndpointPlayer(app, conn) {

    const connection = conn;

    // get list player
    app.get(prefix + "", async (request, response)=>{
        try
        {
            const query = `select * from player`
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

    // statistics
    app.post(prefix + "stats/:id", async (request, response)=>{
        try
        {
            // TODO: cambiar a que haga los joins del loot
            const query = `select health,attack,speed, dash, money from player where player_id = ${request.params.id}`
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

    // make a new user
    app.post(prefix + "new", async (request, response)=>{
        try
        {
            request.body.username
            const query = `select * from loot where id = ${request.params.id}`
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

//checkpoint
     app.get(prefix + "last-checkpoint/:id", async (request, response)=>{
        try
        {
            // TODO: cambiar a que haga los joins del loot
            const query = `select C.checkpoint_id, P.player_id, L.level_name  from game.player AS P INNER JOIN checkpoint AS C ON C.checkpoint_id = P.checkpoint_id INNER JOIN level AS L ON C.level_id = L.level_id  where player_id = ${request.params.id}`
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

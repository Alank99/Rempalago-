const prefix = "/api/player/";

export function addEndpointPlayer(app, conn) {

    const connection = conn;

    // get list loot tables
    app.get(prefix + "", async (request, response)=>{
        try
        {
            const query = `select * from user`
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
    app.post(prefix + "login", async (request, response)=>{
        try
        {
            // TODO: cambiar a que haga los joins del loot
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
}

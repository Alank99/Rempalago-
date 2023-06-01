const prefix = "/api/enemy/";

export function addEndpoints(app, conn) {

    const connection = conn;

    // get list enemies
    app.get(prefix + "", async (request, response)=>{
        try
        {
            const query = `select * from enemy`
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
    app.get(prefix + ":id", async (request, response)=>{
        try
        {
            const query = `select * from enemy where id = ${request.params.id}`
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
    app.get(prefix + "kills/:id", async (request, response)=>{
        try
        {
            const query = `select 'kills' from enemy where id = ${request.params.id}`
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

    app.post(prefix + "kills/:id", async (request, response)=>{
        try
        {
            const query = `update enemy SET 'kills' = ((select 'kills' from enemy where id = ${request.params.id}) + 1) where id = ${request.params.id}`
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

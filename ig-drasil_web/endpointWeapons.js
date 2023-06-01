const prefix = "/api/weapons/";

export function addEndpoints(app, conn) {

    const connection = conn;

    // get list weapons
    app.get(prefix + ":id", async (request, response)=>{
        try
        {
            const query = `select * from game.weapon where weapon_id= ${request.params.id}`
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

    // weapons more kills
    app.get(prefix + "more-kills", async (request, response)=>{
        try
        {
            // TODO: cambiar a que haga los joins del loot
            const query = `SELECT name,damage,kills FROM weapon AS W ORDER BY W.kills DESC`
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

    app.post(prefix + "kill-enemy/:id", async (request, response)=>{
        try
        {
            const query = `update weapon SET 'kills' = ((select 'kills' from weapon where weapon_id = ${request.params.id}) + 1) where weapon_id = ${request.params.id}`
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

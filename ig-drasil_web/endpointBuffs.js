const prefix = "/api/buffs/";

export function addEndpointsBuffs(app, connection) {
    app.get(prefix + "", async (request, response)=>{
        try
        {
            const query = `select * from player where player_id= ${request.params.id}`
            connection = await connectToDB()
            const [results, fields] = await connection.execute(query)
    
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

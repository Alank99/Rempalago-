const prefix = "/api/buffs/";

export function addEndpoints(app, conn) {

    const connection = conn;

    // get list of buffs
    app.get(prefix + "", async (request, response)=>{
        try
        {
            const query = `select * from loot where name like 'buff_%'`
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

    // consigue los detalles de un buff especifico
    app.get(prefix + ":id", async (request, response)=>{
        try
        {
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

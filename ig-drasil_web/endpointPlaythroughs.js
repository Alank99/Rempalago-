const prefix = "/api/playthroughs/";

export function addEndpoints(app, conn) {

    const connection = conn;
    app.get(prefix + ':id', async (request, response)=>{
        try
        {
            const query = `SELECT A.player_id, playtime, completed, checkpoint_id, money, health, espada, balero, trompo, dash FROM game.playthrough AS A INNER JOIN game.player as B ON A.player_id = B.player_id where A.user_id = ${request.params.id}`
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
    }),
    //All playthorughs
    app.get(prefix, async (request, response)=>{
    
        try
        {
            const query = `SELECT A.player_id, playtime, completed, checkpoint_id, money, health, espada, balero, trompo, dash FROM game.playthrough AS A INNER JOIN game.player as B ON A.player_id = B.player_id`
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
    })

    // make a new playthrough
    app.post(prefix + "new/:user/:player", async (request, response)=>{
        try
        {
            const query = `INSERT INTO playthrough VALUES(NULL, ${request.params.user}, ${request.params.player}, NOW(), NOW(), 0, 0);`
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
};
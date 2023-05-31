const prefix = "/api/playthroughs/";

export function addEndpoints(app, conn) {

    const connection = conn;
    app.get('/api/playthroughs/:id', async (request, response)=>{
        let connection = null
    
        try
        {
            const query = `SELECT A.player_id, playtime, completed, checkpoint_id, money, health, espada, balero, trompo, dash FROM game.playthrough AS A INNER JOIN game.player as B ON A.player_id = B.player_id where A.user_id = ${request.params.id}`
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



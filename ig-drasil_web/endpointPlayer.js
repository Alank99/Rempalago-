const prefix = "/api/player/";

export function addEndpoints(app, conn) {

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
    app.get(prefix + "stats/:id", async (request, response)=>{
        try
        {
            // TODO: cambiar a que haga los joins del loot
            const query = `select * from player where player_id = ${request.params.id}`
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

    // make a new player
    app.post(prefix + "new", async (request, response)=>{
        try
        {
            const query = `insert into player (checkpoint_id, money, health, attack, speed, espada, balero, trompo, dash) values(1, 0, 100, 1, 10, 1, NULL, NULL, 0) `
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

    app.post(prefix + "update/:id", async (request, response)=>{
        try
        {
            if (request.body.espada == "0")
                request.body.espada = "NULL";
            if (request.body.balero == "0")
                request.body.balero = "NULL";
            if (request.body.trompo == "0")
                request.body.trompo = "NULL";
            const params = `checkpoint_id = ${request.body.checkpoint_id}, money = ${request.body.money}, health = ${request.body.health}, attack = ${request.body.attack}, speed = ${request.body.speed}, espada = ${request.body.espada}, balero = ${request.body.balero}, trompo = ${request.body.trompo}, dash = ${request.body.dash}`;
            const query = `update player SET ` + params + ` WHERE player_id = ${request.params.id}`
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
            const query = `select C.checkpoint_id, C.level_id, C.position_x, C.position_y from game.player AS P INNER JOIN checkpoint AS C ON C.checkpoint_id = P.checkpoint_id where P.player_id = ${request.params.id}`
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

"use strict"

import express from 'express'
import mysql from 'mysql2/promise'
import fs from 'fs'
import * as buffs from './endpointBuffs.js' 
import * as loot from './endpointLoot.js' 
import * as user from './endpointUser.js'
import * as enemy from './endpointEnemy.js'
import * as player from './endpointPlayer.js' 
import * as playthroughs from './endpointPlaythroughs.js' 
import * as weapons from './endpointWeapons.js' 

const app = express()
const port = 5000

app.use(express.json())
app.use(express.static('./public'))

async function connectToDB()
{
    return await mysql.createConnection({
        host:'localhost',
        user:'root',
        password:'123',
        database:'game'
    })
}

let conn = await connectToDB();

buffs.addEndpoints(app, conn);
loot.addEndpoints(app, conn);
user.addEndpoints(app, conn);
enemy.addEndpoints(app, conn);
player.addEndpoints(app, conn);
playthroughs.addEndpoints(app, conn);
weapons.addEndpoints(app, conn);

app.get('/api/get_weapons/', async (request, response)=>{
    let connection = null

    try
    {
        const query = `SELECT * from weapon`
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
    finally
    {
        if(connection!==null) 
        {
            connection.end()
            console.log("Connection closed succesfully!")
        }
    }
})

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
    finally
    {
        if(connection!==null) 
        {
            connection.end()
            console.log("Connection closed succesfully!")
        }
    }
})

app.listen(port, ()=>
{
    console.log(`App listening at http://localhost:${port}`)
})
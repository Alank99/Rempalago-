"use strict"

import express from 'express'
import mysql from 'mysql2/promise'
import fs from 'fs'
import * as buffs from './endpointBuffs.js' 
import * as loot from './endpointLoot.js' 
import * as user from './endpointUser.js'
//import * as player from './endpointPlayer.js' 

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

app.get('/api/get_player/:id', async (request, response)=>{
    let connection = null

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
    finally
    {
        if(connection!==null) 
        {
            connection.end()
            console.log("Connection closed succesfully!")
        }
    }
})

app.get('/api/show_playthroughs/:id', async (request, response)=>{
    let connection = null

    try
    {
        const query = `SELECT * from playthrough INNER JOIN player ON playthrough.player_id = player.player_id WHERE user_id= ${request.params.id}`
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
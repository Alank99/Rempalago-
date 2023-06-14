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
import * as views from './endpointVistas.js'

const app = express()
const hostname = '127.0.0.1'
const port = 5000

app.use(express.json())
app.use(express.static('./public'))

async function connectToDB()
{
    return await mysql.createConnection({
        host:'localhost',
        user:'requester',
        password:'Arbolitos',
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
views.addEndpoints(app, conn);

app.listen(port, hostname, ()=>
{
    console.log(`App listening at http://${hostname}:${port}`)
})
# Explicación sobre nuestro schema
 Para este esquema conceptual se deberá justificar en que forma normal está la VERSION 1.

Es necesario decidir donde crear llaves foráneas (foreign keys) y que atributos deben ser llaves primarias (primary keys) de acuerdo a la lógica del problema y las instrucciones de SQL. También se explica.

- [Explicación sobre nuestro schema](#explicación-sobre-nuestro-schema)
- [Parte 1: Tablas](#parte-1-tablas)
  - [Checkpoint](#checkpoint)
  - [Enemy](#enemy)
  - [Level](#level)
  - [Loot](#loot)
  - [Loot\_MTM](#loot_mtm)
  - [Loot\_table](#loot_table)
  - [Player](#player)
  - [Playthrough](#playthrough)
  - [User](#user)
  - [Weapon](#weapon)
  - [Weapon\_type](#weapon_type)
  - [Parte 1.1: Forma Normal](#parte-11-forma-normal)
- [Parte 2: Vistas](#parte-2-vistas)
  - [V\_user\_playthrough](#v_user_playthrough)
  - [Active\_players](#active_players)
  - [Inactive\_players](#inactive_players)
  - [New\_users](#new_users)
  - [Speedruns](#speedruns)
  - [Top\_weapons](#top_weapons)
- [Parte 3 : Laves foráneas](#parte-3--laves-foráneas)
    - [id\_level en enemy](#id_level-en-enemy)
    - [id\_level en checkpoint](#id_level-en-checkpoint)
    - [checkpoint\_id en player](#checkpoint_id-en-player)
    - [user\_id en playtrough](#user_id-en-playtrough)
    - [player\_id en fallthrough](#player_id-en-fallthrough)
    - [type\_id en weapon](#type_id-en-weapon)
- [Parte 4: instrucciones de MySql que usamos](#parte-4-instrucciones-de-mysql-que-usamos)


# Parte 1: Tablas

Las tablas que se elaboraron fueron las siguientes:

## Checkpoint

Se usa para poder cargar de *playthrough* de un jugador. Esta también sirve para guardar el estado de un jugador para que puede continuar desde aquí.

## Enemy

Se usa para poder guardar los tipos de enemigos y las cantidades de daño que causan. De esta forma se puede observar que enemigos son demasiado débiles y cuales son muy fuertes.

## Level

Permite separar el mapa y ubicar en que partes se encuentran los jugadores.

A pesar de que no es funcional perite organizar las cosas de forma correcta.

## Loot

Permite definir objetos que se puede caer de un enemigo o cofre. El objeto consigue sus propiedades de aquí.

## Loot_MTM

La tabla de relación muchos a muchos para loot permite definir que es lo que se debe tirar y en que cantidad para cada tipo de objeto. 

## Loot_table

Permite definir una lista de objetos que puede tirar una enemigo. Esta lista requiere del uso de una tabla intermedia muchos a muchos.

## Player

Se usa para definir estadísticas sobre el jugador. Se crea un jugador por cada *playthrough* que se cree.

## Playthrough 

Guarda estadísticas del juego. Similar a *player*, pero sobre las acciones y ocurrencias del juego en vez del jugador.

## User 

Guarda la información de un usuario (nombre, usuario, password, etc...).

## Weapon

Guarda los valores de un arma y define sus estadísticas necesarias.

## Weapon_type

Para mantener la forma normal tercera se separa el tipo de arma de Weapon. Existen 3 tipos de arma. 

## Parte 1.1: Forma Normal 

1.- La primera forma normal indica que cada tabla tiene una llave única, y cuyo es elementos sean atómicos e indivisibles. También indica que no se deben tener datos repetitivos.

2.- La segunda forma normal indica que los atributos no claves no dependan de otros que no sean la llave primaria.

3.- La tercera forma normal habla de la transitividad. Las llaves primarias no se deben usar en otras entidades, sin embargo, se usan como atributos en diferentes contextos, por lo cual se consideran atributos normales.

# Parte 2: Vistas 

Las vistas se crearon fueron las siguientes:

## V_user_playthrough  

Facilita la vista para poder ver que usarios jugaron y cuando.

## Active_players

Para las estadísticas de la página web. 

Esta vista sirve para mostrar a los jugadores que están en línea o estuvieron recientemente conectados.

## Inactive_players

Para las estadísticas de la página web.

Con esta vista es posible borrar a los usuarios menos activos.

## New_users

Para las estadísticas de la página web.

Sirve para una bienvenida diaria, por correo, a los nuevos usuarios.

## Speedruns

Para las estadísticas de la página web.

Con esta vista es posible tener una lista de los tiempos más rápidos, así creando un ambiente más competitivo.

## Top_weapons

Para las estadísticas de la página web.

Con esta vista los jugadores pueden ver las estrategias mas usadas, esto facilita que nuevos jugadores tengan menos dificultad, comparando que armas son mejores y para cuales valen la pena ahorrar.

# Parte 3 : Laves foráneas
Las llaver foráneas utilizadas fueron las siguientes:

### id_level en enemy
Se crea esta llave foránea de forma entero con la finalidad de poder organizar las apariciones de los enemigos.

### id_level en checkpoint
Se crea esta llave foránea de forma entero para saber que lugar del mundo se encuentra el *checkpoint*. 

### checkpoint_id en player
Esta llave tiene la finalidad de ubicar en donde el jugador hizo su ultimo checkpoint.

### user_id en playtrough
Se utiliza para conocer al usuario que se paso esa partida.

### player_id en fallthrough
Se utiliza para conocer cual fue el jugador que se paso la partida.

### type_id en weapon
Es útil para conocer el tipo de la arma.


# Parte 4: instrucciones de MySql que usamos 

`SELECT` > Selecciona elementos de una tabla.

`INSERT` > Inserta un nuevo elemento a una tabla.

`CREATE` > Crea las tablas de MySQL.

`DROP` > Se usa para borrar una base de datos. Solo se utilizó al regenerar el schema.

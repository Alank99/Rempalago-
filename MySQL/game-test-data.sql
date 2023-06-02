SET NAMES utf8mb4;
USE game;

-- user_id, name, email, password, first_created
SET AUTOCOMMIT=0;
INSERT INTO user VALUES(1, "root", "root@root.com", "root", NOW()),
(2, "parcivla", "prc129@tec.com", "contraseña", NOW()),
(3, "alex", "barron@hotmail.com", "asdvadf", NOW()),
(4, "alan", "ala_fdz192@icloud.com", "avsdanuw", NOW()),
(5, "pierce", "pie@root.com", "afvw", NOW()),
(6, "terry", "terr@root.com", "helloworld", NOW()),
(7, "lifa", "lef@root.com", "round", NOW()),
(8, "oswal", "deadshot@root.com", "deadline", NOW()),
(9, "rober", "rot@root.com", "veast", NOW()),
(10, "juan", "overjuan@root.com", "mercy", NOW());
COMMIT;

-- level_id, level_name
SET AUTOCOMMIT=0;
INSERT INTO level VALUES(1, "FOREST"),
(2, "TOWN"),
(3, "UNDERGROUND SEWER"),
(4, "DEEP FOREST");
COMMIT;

-- checkpoint_id, level_id, position_x, position_y
SET AUTOCOMMIT=0;
INSERT INTO checkpoint VALUES(1, 1, 0, 0),
(2, 2, 10, 10),
(3, 4, 14, -50),
(4, 3, 60, 50),
(5, 1, 70, 10),
(6, 2, 80, 90),
(7, 3, 10, -40),
(8, 4, 60, -10),
(9, 1, 80, 20),
(10, 1, 30, 80);
COMMIT;

-- enemy_id, enemy_name, kills, level_id, health, attack, speed, money_drop
SET AUTOCOMMIT=0;
INSERT INTO enemy VALUES(1, "slime", 0, 3, 10, 1, 1, 1),
(2, "nahual", 5, 3, 10, 1, 1, 12),
(3, "caravera", 10, 1, 10, 19, 1, 15),
(4, "momia", 20, 1, 10, 20, 5, 20),
(5, "charro negro", 6, 3, 10, 1, 12, 50),
(6, "llorona", 1, 4, 45, 50, 15, 100);
COMMIT;

-- type_id, name
SET AUTOCOMMIT=0;
INSERT INTO weapon_type VALUES(1, "espada"),
(2, "balero"),
(3, "trampo");

COMMIT;

-- weapon_id, name, damage, kills, type_id
SET AUTOCOMMIT=0;
INSERT INTO weapon VALUES(1, "espada de madera", 1, 0, 1),
(2, "balero de mandera", 2, 0, 2),
(3, "trompo de madera", 4, 0, 3),
(4, "espada espiritual", 4, 0, 1),
(5, "espada oscura", 10, 0, 1),
(6, "balero de piedra", 4, 4, 2),
(7, "balero de metal", 8, 15, 2),
(8, "trompo de piedra", 8, 0, 3),
(9, "trompo de metal", 12, 0, 3),
(10, "trompo espiritual", 16, 0, 3);
COMMIT;

-- player_id, checkpoint_id, money, health, attack, speed, espada, balero, trompo, dash
SET AUTOCOMMIT=0;
INSERT INTO player VALUES(1, 1, 0, 100, 1.0, 10, 1, NULL, NULL, 0),
(2, 1, 1000, 100, 1.0, 10, 1, 2, 3, 1),
(3, 2, 200, 70, 1.4, 10, 1, 2, 3, 1),
(4, 2, 40, 17, 1.45, 10, 1, 2, 3, 1),
(5, 2, 500, 18, 1.2, 10, 1, 2, 3, 1),
(6, 3, 700, 15, 1.3, 10, 1, 2, 3, 1),
(7, 4, 900, 20, 1.7, 10, 1, 2, 3, 1),
(8, 3, 250, 40, 1.6, 10, 1, 2, 3, 1),
(9, 2, 120, 98, 1.4, 10, 1, 2, 3, 1),
(10, 1, 1000, 100, 1.0, 10, 1, 2, 3, 1);
COMMIT;

-- play_id, user_id, player_id, first_created, last_updated, playtime, completed
SET AUTOCOMMIT=0;
INSERT INTO playthrough VALUES(1, 1, 1, NOW(), NOW(), 10, 0),
(2, 1, 2, NOW(), NOW(), 10, 0),
(3, 1, 3, NOW(), NOW(), 20, 0),
(4, 1, 4, NOW(), NOW(), 30, 0),
(5, 1, 5, NOW(), NOW(), 45, 0),
(6, 1, 6, NOW(), NOW(), 50, 0),
(7, 1, 7, NOW(), NOW(), 70, 0),
(8, 1, 8, NOW(), NOW(), 84, 0),
(9, 1, 9, NOW(), NOW(), 50, 0),
(10, 1, 10, NOW(), NOW(), 20, 0);
COMMIT;

-- id,name,modifier
SET AUTOCOMMIT=0;
INSERT INTO loot VALUES(1, "coin",3),
(2, "dulce",2),
(3, "quitapena",3);
COMMIT;

-- id, level
SET AUTOCOMMIT=0;
INSERT INTO loot_table VALUES(1,2),
(2,1),
(3,2),
(4,4),
(5,3),
(6,2),
(7,1),
(8,4),
(9,3),
(10,1);
COMMIT;

-- id, loot_id, loot_table_id, ammount
SET AUTOCOMMIT=0;
INSERT INTO loot_mtm VALUES(1,1,1,4),
(2,2,2,4),
(3,3,3,7),
(4,1,4,5),
(5,2,5,2),
(6,2,6,9),
(7,3,7,10),
(8,3,8,2),
(9,2,9,3),
(10,1,10,18);
COMMIT;
SET NAMES utf8mb4;
USE game;

-- user_id, name, email, password, first_created

-- level_id, level_name
SET AUTOCOMMIT=0;
INSERT INTO level VALUES(1, "FOREST"),
(2, "TUTORIAL"),
(3, "TOWN"),
(4, "UNDERGROUND SEWER"),
(5, "DEEP FOREST");
COMMIT;

-- checkpoint_id, level_id, position_x, position_y
SET AUTOCOMMIT=0;
INSERT INTO checkpoint VALUES(1, 1, -29, -2),
(2, 2, 30, -93),
(3, 3, 266, 9),
(4, 4, 434, 9),
(5, 5, 529, -29);
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
(2, "balero de madera", 2, 0, 2),
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


-- play_id, user_id, player_id, first_created, last_updated, playtime, completed


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
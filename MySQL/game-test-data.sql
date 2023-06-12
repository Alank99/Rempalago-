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
INSERT INTO loot VALUES(1, "coin",1),
(2, "elote", 5),
(3, "elote", 10),
(4, "elote", 20),
(5, "pan de muerto", 5),
(6, "pan de muerto", 10),
(7, "pan de muerto", 20),
(8, "mazapan",5),
(9, "mazapan",10),
(10, "mazapan"203),
(11, "oblea",5),
(12, "oblea",10),
(13, "oblea",20),
(14, "Borrachito",5),
(15, "Borrachito",10),
(16, "Borrachito",20),
(17, "concha",5),
(18, "concha",10),
(19, "concha",20);
COMMIT;

-- id, level, nombre
SET AUTOCOMMIT=0;
INSERT INTO loot_table VALUES(1,1, "Calaverita 1"),
(2,1, "Calaverita 2"),
(3,1, "Calaverita 3"),
(4,1, "Momia 1"),
(5,1, "Momia 2"),
(6,1, "Momia 3"),
(7,1, "Rata 1"),
(8,1, "Rata 2"),
(9,1, "Rata 3");
COMMIT;

-- id, loot_id, loot_table_id, amount
SET AUTOCOMMIT=0;
INSERT INTO loot_mtm VALUES(1,8,1,50),
-- calaverita 2
(2, 9, 2, 60),
(2, 3, 2, 60),

-- calaverita 3
(2, 10, 3, 70),
(2, 4, 3, 70),
(2, 13, 3, 70),

-- momia 1
(2, 8, 4, 50),
(2, 2, 4, 50),

-- momia 2
(2, 9, 5, 60),
(2, 3, 5, 60),
(2, 11, 5, 60),

-- momia 3
(2, 10, 6, 70),
(2, 4, 6, 70),
(2, 8, 6, 70),
(2, 13, 6, 70),

-- rata 1
(2, 14, 6, 80),

-- rata 2
(2, 15, 6, 90),
(2, 18, 6, 90),

-- rata 3
(2, 16, 6, 100),
(2, 19, 6, 100),
(2, 11, 6, 100),
COMMIT;
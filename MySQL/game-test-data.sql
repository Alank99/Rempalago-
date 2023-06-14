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
(2, "rata", 0, 3, 10, 1, 1, 12),
(3, "caravera", 0, 1, 10, 19, 1, 15),
(4, "momia", 0, 1, 10, 20, 5, 20),
(5, "charro negro", 0, 3, 10, 1, 12, 50),
(6, "llorona", 0, 4, 45, 50, 15, 100);
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
(6, "balero de piedra", 4, 0, 2),
(7, "balero de metal", 8, 0, 2),
(8, "trompo de piedra", 8, 0, 3),
(9, "trompo de metal", 12, 0, 3),
(10, "trompo espiritual", 16, 0, 3);
COMMIT;

-- player_id, checkpoint_id, money, health, attack, speed, espada, balero, trompo, dash


-- play_id, user_id, player_id, first_created, last_updated, playtime, completed


-- id,name,modifier
SET AUTOCOMMIT=0;
INSERT INTO loot VALUES(1, "coin", 1),
(2, "elote", 5),
(3, "elote", 10),
(4, "elote", 20),
(5, "pan de muerto", 5),
(6, "pan de muerto", 10),
(7, "pan de muerto", 20),
(8, "Borrachito",5),
(9, "Borrachito",10),
(10, "Borrachito", 20),
(11, "oblea",5),
(12, "oblea",10),
(13, "oblea",20),
(14, "mazapan",5),
(15, "mazapan",10),
(16, "mazapan",20),
(17, "concha",5),
(18, "concha",10),
(19, "concha",20);
COMMIT;

-- id, level, nombre
SET AUTOCOMMIT=0;
INSERT INTO loot_table VALUES(2,2, "Calaverita"),
(3,2, "Momia"),
(4,4, "Rata");
COMMIT;

-- id, loot_id, loot_table_id, amount
SET AUTOCOMMIT=0;
INSERT INTO loot_mtm VALUES
-- calaverita
(1, 1, 2, 100),
(2, 8, 2, 50),
(3, 9, 2, 60),
(4, 3, 2, 60),
(5, 10, 2, 70),
(6, 4, 2, 70),
(7, 13, 2, 70),

-- momia
(8, 1, 3, 100),
(9, 8, 3, 50),
(10, 2, 3, 50),
(11, 9, 3, 60),
(12, 3, 3, 60),
(13, 11, 3, 60),
(14, 10, 3, 70),
(15, 4, 3, 70),
(16, 8, 3, 70),
(17, 13, 3, 70),

-- rata
(18, 1, 4, 100),
(19, 14, 4, 80),
(20, 15, 4, 90),
(21, 18, 4, 90),
(22, 16, 4, 100),
(23, 19, 4, 100),
(24, 11, 4, 100);
COMMIT;
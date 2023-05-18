SET NAMES utf8mb4;
USE game;

-- user_id, name, email, password, first_created
SET AUTOCOMMIT=0;
INSERT INTO user VALUES(1, "root", "root@root.com", "root", NOW()),
(2, "mfrias", "mario99fpina@gmail.com", "password", NOW());
COMMIT;

-- level_id, level_name
SET AUTOCOMMIT=0;
INSERT INTO level VALUES(1, "FOREST"),
(2, "TOWN"),
(3, "UNDERGROUND SEWER");
COMMIT;

-- checkpoint_id, level_id, position_x, position_y
SET AUTOCOMMIT=0;
INSERT INTO checkpoint VALUES(1, 1, 0, 0),
(2, 2, 10, 10),
(3, 3, 0, -50);
COMMIT;

-- enemy_id, enemy_name, kills, level_id, health, attack, speed, money_drop
SET AUTOCOMMIT=0;
INSERT INTO enemy VALUES(1, "slime", 0, 3, 10, 1, 1, 1);
COMMIT;

-- type_id, name
SET AUTOCOMMIT=0;
INSERT INTO weapon_type VALUES(1, "wooden snake"),
(2, "balero"),
(3, "top");
COMMIT;

-- weapon_id, name, damage, kills, type_id
SET AUTOCOMMIT=0;
INSERT INTO weapon VALUES(1, "basic sword", 1, 0, 1),
(2, "basic balero", 1, 0, 2),
(3, "basic top", 5, 0, 3);
COMMIT;

-- player_id, checkpoint_id, money, health, attack, speed, espada, balero, trompo, dash
SET AUTOCOMMIT=0;
INSERT INTO player VALUES(1, 3, 1000, 10, 1.0, 1.0, 1, 2, 3, 1);
COMMIT;

-- play_id, user_id, player_id, first_created, last_updated, playtime, completed
SET AUTOCOMMIT=0;
INSERT INTO playthrough VALUES(1, 1, 1, NOW(), NOW(), 0, 0);
COMMIT;
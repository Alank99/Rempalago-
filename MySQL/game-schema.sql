-- Active: 1681846904940@@127.0.0.1@3306@game
-- Creation of database
DROP SCHEMA IF EXISTS game;
CREATE SCHEMA game;
USE game;

-- Creation of Tables
-- Table where the information of each user is saved
CREATE TABLE user (
  user_id SMALLINT UNSIGNED NOT NULL AUTO_INCREMENT,
  name VARCHAR(255) NOT NULL,
  email VARCHAR(255) NOT NULL,
  password VARCHAR(255) NOT NULL,
  first_created TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY  (user_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Table where the different levels are saved
CREATE TABLE level (
  level_id TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
  level_name VARCHAR(255) NOT NULL,
  PRIMARY KEY (level_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Table where the different checkpoints in the game are saved 
CREATE TABLE checkpoint(
  checkpoint_id SMALLINT UNSIGNED NOT NULL AUTO_INCREMENT,
  level_id TINYINT UNSIGNED NOT NULL,
  position_x INT NOT NULL,
  position_y INT NOT NULL,
  CONSTRAINT `fk_level_checkpoint_id` FOREIGN KEY (level_id) REFERENCES level (level_id) ON DELETE RESTRICT ON UPDATE CASCADE,
  PRIMARY KEY (checkpoint_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Table where the different types of enemies are saved
CREATE TABLE enemy (
  enemy_id SMALLINT UNSIGNED NOT NULL AUTO_INCREMENT,
  enemy_name VARCHAR(255) NOT NULL,
  kills INT UNSIGNED NOT NULL DEFAULT 0,
  level_id TINYINT UNSIGNED NOT NULL,
  health SMALLINT UNSIGNED NOT NULL,
  attack SMALLINT UNSIGNED NOT NULL,
  speed SMALLINT UNSIGNED NOT NULL,
  money_drop SMALLINT UNSIGNED NOT NULL,
  CONSTRAINT `fk_level_id` FOREIGN KEY (level_id) REFERENCES level (level_id) ON DELETE RESTRICT ON UPDATE CASCADE,
  PRIMARY KEY (enemy_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Table where the different types of weapons are saved
CREATE TABLE weapon_type (
  type_id TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
  name VARCHAR(32) NOT NULL,
  PRIMARY KEY (type_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Table where the details of all player weapons are saved
CREATE TABLE weapon (
  weapon_id TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
  name VARCHAR(64) NOT NULL,
  damage TINYINT UNSIGNED NOT NULL,
  kills INT UNSIGNED NOT NULL DEFAULT 0,
  type_id TINYINT UNSIGNED NOT NULL,
  CONSTRAINT `fk_weapon_type_id` FOREIGN KEY (type_id) REFERENCES weapon_type (type_id) ON DELETE RESTRICT ON UPDATE CASCADE,
  PRIMARY KEY (weapon_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Table where characteristics of a specific playthorughs character is saved
CREATE TABLE player (
  player_id SMALLINT UNSIGNED NOT NULL AUTO_INCREMENT,
  checkpoint_id SMALLINT UNSIGNED NOT NULL,
  money INT UNSIGNED NOT NULL DEFAULT 0,
  health SMALLINT UNSIGNED NOT NULL DEFAULT 100,
  attack FLOAT NOT NULL DEFAULT 1.0,
  speed FLOAT NOT NULL DEFAULT 1.0,
  espada TINYINT UNSIGNED DEFAULT NULL,
  balero TINYINT UNSIGNED DEFAULT NULL,
  trompo TINYINT UNSIGNED DEFAULT NULL,
  dash BOOLEAN NOT NULL DEFAULT 0,
  CONSTRAINT `fk_checkpoint_player_id` FOREIGN KEY (checkpoint_id) REFERENCES checkpoint (checkpoint_id) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `fk_espada_id` FOREIGN KEY (espada) REFERENCES weapon (weapon_id) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `fk_balero_id` FOREIGN KEY (balero) REFERENCES weapon (weapon_id) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `fk_trompo_id` FOREIGN KEY (trompo) REFERENCES weapon (weapon_id) ON DELETE RESTRICT ON UPDATE CASCADE,
  PRIMARY KEY (player_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Table where all information of a specific playthrough are saved
CREATE TABLE playthrough (
  play_id SMALLINT UNSIGNED NOT NULL AUTO_INCREMENT,
  user_id SMALLINT UNSIGNED NOT NULL,
  player_id SMALLINT UNSIGNED NOT NULL,
  first_created TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  last_updated TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  playtime INT UNSIGNED NOT NULL DEFAULT 0,
  completed BOOLEAN NOT NULL DEFAULT 0,
  PRIMARY KEY (play_id),
  CONSTRAINT `fk_user_id_playthrough` FOREIGN KEY (user_id) REFERENCES user (user_id) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `fk_player_id_playthrough` FOREIGN KEY (player_id) REFERENCES player (player_id) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Creation of views
-- View where you can see the number of playtroughs started through time
CREATE VIEW new_users AS
	SELECT DATE(first_created) Date, COUNT(DISTINCT user_id) newPlayers
    FROM user
    GROUP BY DATE(first_created);
    
-- View where you can see the fastest playtimes where completed
CREATE VIEW speedruns AS
	SELECT user.name UserName, playthrough.playtime Time
    FROM game.user INNER JOIN game.playthrough USING (user_id)
    ORDER BY playthrough.playtime;
    
-- View where you can see the weapons with the most kills 
CREATE VIEW top_weapons AS
	SELECT weapon.name Weapon, weapon_type.name Type, weapon.kills Kills
	FROM game.weapon INNER JOIN game.weapon_type USING (type_id)
    ORDER BY weapon.kills;
    
-- View of most active players

CREATE VIEW active_players AS
SELECT user.name UserName, COUNT(playthrough.play_id) TotalPlaythroughs
FROM game.user
INNER JOIN game.playthrough ON user.user_id = playthrough.user_id
GROUP BY user.user_id
ORDER BY TotalPlaythroughs DESC;

-- View of the less active players

CREATE VIEW inactive_players AS
SELECT user.name UserName, COUNT(playthrough.play_id) TotalPlaythroughs
FROM game.user
LEFT JOIN game.playthrough ON user.user_id = playthrough.user_id
GROUP BY user.user_id
HAVING TotalPlaythroughs = 0
ORDER BY TotalPlaythroughs;

CREATE VIEW V_user_playthrough AS
Select play_id, P.user_id, P.player_id, P.first_created, last_updated, playtime, completed, checkpoint_id, money, health, attack, speed, espada, balero, trompo, dash, name, email, password, U.first_created AS user_first_created from playthrough as P
LEFT JOIN `user` as U ON U.user_id = P.user_id
LEFT JOIN `player` as PL On P.player_id = PL.player_id
ORDER BY P.last_updated;
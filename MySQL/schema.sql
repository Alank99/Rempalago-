
DROP SCHEMA IF EXISTS game;
CREATE SCHEMA game;
USE game;

-- game.`level` definition

CREATE TABLE `level` (
  `level_id` tinyint unsigned NOT NULL AUTO_INCREMENT,
  `level_name` varchar(255) NOT NULL,
  PRIMARY KEY (`level_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- game.loot definition

CREATE TABLE `loot` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  `modifier` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- game.loot_MTM definition

CREATE TABLE `loot_MTM` (
  `id` tinyint NOT NULL AUTO_INCREMENT,
  `loot_id` int NOT NULL,
  `loot_table_id` int NOT NULL,
  `ammount` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- game.loot_table definition

CREATE TABLE `loot_table` (
  `id` int NOT NULL AUTO_INCREMENT,
  `level` int NOT NULL,
  `human_tag` VARCHAR(20),
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- game.`user` definition

CREATE TABLE `user` (
  `user_id` smallint unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `first_created` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- game.weapon_type definition

CREATE TABLE `weapon_type` (
  `type_id` tinyint unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(32) NOT NULL,
  PRIMARY KEY (`type_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- game.checkpoint definition

CREATE TABLE `checkpoint` (
  `checkpoint_id` smallint unsigned NOT NULL AUTO_INCREMENT,
  `level_id` tinyint unsigned NOT NULL,
  `position_x` int NOT NULL,
  `position_y` int NOT NULL,
  PRIMARY KEY (`checkpoint_id`),
  KEY `fk_level_checkpoint_id` (`level_id`),
  CONSTRAINT `fk_level_checkpoint_id` FOREIGN KEY (`level_id`) REFERENCES `level` (`level_id`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- game.enemy definition

CREATE TABLE `enemy` (
  `enemy_id` smallint unsigned NOT NULL AUTO_INCREMENT,
  `enemy_name` varchar(255) NOT NULL,
  `kills` int unsigned NOT NULL DEFAULT '0',
  `level_id` tinyint unsigned NOT NULL,
  `health` smallint unsigned NOT NULL,
  `attack` smallint unsigned NOT NULL,
  `speed` smallint unsigned NOT NULL,
  `money_drop` smallint unsigned NOT NULL,
  PRIMARY KEY (`enemy_id`),
  KEY `fk_level_id` (`level_id`),
  CONSTRAINT `fk_level_id` FOREIGN KEY (`level_id`) REFERENCES `level` (`level_id`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- game.weapon definition

CREATE TABLE `weapon` (
  `weapon_id` tinyint unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(64) NOT NULL,
  `damage` tinyint unsigned NOT NULL,
  `kills` int unsigned NOT NULL DEFAULT '0',
  `type_id` tinyint unsigned NOT NULL,
  PRIMARY KEY (`weapon_id`),
  KEY `fk_weapon_type_id` (`type_id`),
  CONSTRAINT `fk_weapon_type_id` FOREIGN KEY (`type_id`) REFERENCES `weapon_type` (`type_id`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- game.player definition

CREATE TABLE `player` (
  `player_id` smallint unsigned NOT NULL AUTO_INCREMENT,
  `checkpoint_id` smallint unsigned NOT NULL,
  `money` int unsigned NOT NULL DEFAULT '0',
  `health` smallint unsigned NOT NULL DEFAULT '100',
  `attack` float NOT NULL DEFAULT '1',
  `speed` float NOT NULL DEFAULT '1',
  `espada` tinyint unsigned DEFAULT NULL,
  `balero` tinyint unsigned DEFAULT NULL,
  `trompo` tinyint unsigned DEFAULT NULL,
  `dash` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`player_id`),
  KEY `fk_checkpoint_player_id` (`checkpoint_id`),
  KEY `fk_espada_id` (`espada`),
  KEY `fk_balero_id` (`balero`),
  KEY `fk_trompo_id` (`trompo`),
  CONSTRAINT `fk_balero_id` FOREIGN KEY (`balero`) REFERENCES `weapon` (`weapon_id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `fk_checkpoint_player_id` FOREIGN KEY (`checkpoint_id`) REFERENCES `checkpoint` (`checkpoint_id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `fk_espada_id` FOREIGN KEY (`espada`) REFERENCES `weapon` (`weapon_id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `fk_trompo_id` FOREIGN KEY (`trompo`) REFERENCES `weapon` (`weapon_id`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- game.playthrough definition

CREATE TABLE `playthrough` (
  `play_id` smallint unsigned NOT NULL AUTO_INCREMENT,
  `user_id` smallint unsigned NOT NULL,
  `player_id` smallint unsigned NOT NULL,
  `first_created` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `last_updated` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `playtime` int unsigned NOT NULL DEFAULT '0',
  `completed` SMALLINT UNSIGNED NOT NULL DEFAULT '0',
  PRIMARY KEY (`play_id`),
  UNIQUE KEY `player_id_UN` (`player_id`),
  KEY `fk_user_id_playthrough` (`user_id`),
  CONSTRAINT `fk_player_id_playthrough` FOREIGN KEY (`player_id`) REFERENCES `player` (`player_id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `fk_user_id_playthrough` FOREIGN KEY (`user_id`) REFERENCES `user` (`user_id`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- game.V_user_playthrough source

CREATE OR REPLACE
ALGORITHM = UNDEFINED VIEW `game`.`V_user_playthrough` AS
select
    `P`.`play_id` AS `play_id`,
    `P`.`user_id` AS `user_id`,
    `P`.`player_id` AS `player_id`,
    `P`.`first_created` AS `first_created`,
    `P`.`last_updated` AS `last_updated`,
    `P`.`playtime` AS `playtime`,
    `P`.`completed` AS `completed`,
    `PL`.`checkpoint_id` AS `checkpoint_id`,
    `PL`.`money` AS `money`,
    `PL`.`health` AS `health`,
    `PL`.`attack` AS `attack`,
    `PL`.`speed` AS `speed`,
    `PL`.`espada` AS `espada`,
    `PL`.`balero` AS `balero`,
    `PL`.`trompo` AS `trompo`,
    `PL`.`dash` AS `dash`,
    `U`.`name` AS `name`,
    `U`.`email` AS `email`,
    `U`.`password` AS `password`,
    `U`.`first_created` AS `user_first_created`
from
    ((`game`.`playthrough` `P`
left join `game`.`user` `U` on
    ((`U`.`user_id` = `P`.`user_id`)))
left join `game`.`player` `PL` on
    ((`P`.`player_id` = `PL`.`player_id`)))
order by
    `P`.`last_updated`;


-- game.active_players source

CREATE OR REPLACE
ALGORITHM = UNDEFINED VIEW `game`.`active_players` AS
select
    `game`.`user`.`name` AS `UserName`,
    count(`game`.`playthrough`.`play_id`) AS `TotalPlaythroughs`
from
    (`game`.`user`
join `game`.`playthrough` on
    ((`game`.`user`.`user_id` = `game`.`playthrough`.`user_id`)))
group by
    `game`.`user`.`user_id`
order by
    `TotalPlaythroughs` desc;


-- game.inactive_players source

CREATE OR REPLACE
ALGORITHM = UNDEFINED VIEW `game`.`inactive_players` AS
select
    `game`.`user`.`name` AS `UserName`,
    count(`game`.`playthrough`.`play_id`) AS `TotalPlaythroughs`
from
    (`game`.`user`
left join `game`.`playthrough` on
    ((`game`.`user`.`user_id` = `game`.`playthrough`.`user_id`)))
group by
    `game`.`user`.`user_id`
having
    (`TotalPlaythroughs` = 0)
order by
    `TotalPlaythroughs`;


-- game.new_users source

CREATE OR REPLACE
ALGORITHM = UNDEFINED VIEW `game`.`new_users` AS
select
    cast(`game`.`user`.`first_created` as date) AS `Date`,
    count(distinct `game`.`user`.`user_id`) AS `newPlayers`
from
    `game`.`user`
group by
    cast(`game`.`user`.`first_created` as date);


-- game.speedruns source

CREATE OR REPLACE
ALGORITHM = UNDEFINED VIEW `game`.`speedruns` AS
select
    `game`.`user`.`name` AS `UserName`,
    `game`.`playthrough`.`playtime` AS `Time`
from
    (`game`.`user`
join `game`.`playthrough` on
    ((`game`.`user`.`user_id` = `game`.`playthrough`.`user_id`)))
WHERE
    `game`.`playthrough`.`completed` = 1
order by
    `game`.`playthrough`.`playtime`;


-- game.top_weapons source

CREATE OR REPLACE
ALGORITHM = UNDEFINED VIEW `game`.`top_weapons` AS
select
    `game`.`weapon`.`name` AS `Weapon`,
    `game`.`weapon_type`.`name` AS `Type`,
    `game`.`weapon`.`kills` AS `Kills`
from
    (`game`.`weapon`
join `game`.`weapon_type` on
    ((`game`.`weapon`.`type_id` = `game`.`weapon_type`.`type_id`)))
order by
    `game`.`weapon`.`kills` DESC;
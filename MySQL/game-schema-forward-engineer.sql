-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema game
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema game
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `game` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci ;
USE `game` ;

-- -----------------------------------------------------
-- Table `game`.`level`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `game`.`level` (
  `level_id` TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `level_name` VARCHAR(255) NOT NULL,
  PRIMARY KEY (`level_id`))
ENGINE = InnoDB
AUTO_INCREMENT = 4
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `game`.`checkpoint`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `game`.`checkpoint` (
  `checkpoint_id` SMALLINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `level_id` TINYINT UNSIGNED NOT NULL,
  `position_x` INT NOT NULL,
  `position_y` INT NOT NULL,
  PRIMARY KEY (`checkpoint_id`),
  INDEX `fk_level_checkpoint_id` (`level_id` ASC) VISIBLE,
  CONSTRAINT `fk_level_checkpoint_id`
    FOREIGN KEY (`level_id`)
    REFERENCES `game`.`level` (`level_id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 4
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `game`.`enemy`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `game`.`enemy` (
  `enemy_id` SMALLINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `enemy_name` VARCHAR(255) NOT NULL,
  `kills` INT UNSIGNED NOT NULL DEFAULT '0',
  `level_id` TINYINT UNSIGNED NOT NULL,
  `health` SMALLINT UNSIGNED NOT NULL,
  `attack` SMALLINT UNSIGNED NOT NULL,
  `speed` SMALLINT UNSIGNED NOT NULL,
  `money_drop` SMALLINT UNSIGNED NOT NULL,
  PRIMARY KEY (`enemy_id`),
  INDEX `fk_level_id` (`level_id` ASC) VISIBLE,
  CONSTRAINT `fk_level_id`
    FOREIGN KEY (`level_id`)
    REFERENCES `game`.`level` (`level_id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 2
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `game`.`weapon_type`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `game`.`weapon_type` (
  `type_id` TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(32) NOT NULL,
  PRIMARY KEY (`type_id`))
ENGINE = InnoDB
AUTO_INCREMENT = 4
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `game`.`weapon`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `game`.`weapon` (
  `weapon_id` TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(64) NOT NULL,
  `damage` TINYINT UNSIGNED NOT NULL,
  `kills` INT UNSIGNED NOT NULL DEFAULT '0',
  `type_id` TINYINT UNSIGNED NOT NULL,
  PRIMARY KEY (`weapon_id`),
  INDEX `fk_weapon_type_id` (`type_id` ASC) VISIBLE,
  CONSTRAINT `fk_weapon_type_id`
    FOREIGN KEY (`type_id`)
    REFERENCES `game`.`weapon_type` (`type_id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 4
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `game`.`player`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `game`.`player` (
  `player_id` SMALLINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `checkpoint_id` SMALLINT UNSIGNED NOT NULL,
  `money` INT UNSIGNED NOT NULL DEFAULT '0',
  `health` SMALLINT UNSIGNED NOT NULL DEFAULT '100',
  `attack` FLOAT NOT NULL DEFAULT '1',
  `speed` FLOAT NOT NULL DEFAULT '1',
  `espada` TINYINT UNSIGNED NULL DEFAULT NULL,
  `balero` TINYINT UNSIGNED NULL DEFAULT NULL,
  `trompo` TINYINT UNSIGNED NULL DEFAULT NULL,
  `dash` TINYINT(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`player_id`),
  INDEX `fk_checkpoint_player_id` (`checkpoint_id` ASC) VISIBLE,
  INDEX `fk_espada_id` (`espada` ASC) VISIBLE,
  INDEX `fk_balero_id` (`balero` ASC) VISIBLE,
  INDEX `fk_trompo_id` (`trompo` ASC) VISIBLE,
  CONSTRAINT `fk_balero_id`
    FOREIGN KEY (`balero`)
    REFERENCES `game`.`weapon` (`weapon_id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE,
  CONSTRAINT `fk_checkpoint_player_id`
    FOREIGN KEY (`checkpoint_id`)
    REFERENCES `game`.`checkpoint` (`checkpoint_id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE,
  CONSTRAINT `fk_espada_id`
    FOREIGN KEY (`espada`)
    REFERENCES `game`.`weapon` (`weapon_id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE,
  CONSTRAINT `fk_trompo_id`
    FOREIGN KEY (`trompo`)
    REFERENCES `game`.`weapon` (`weapon_id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 2
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `game`.`user`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `game`.`user` (
  `user_id` SMALLINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(255) NOT NULL,
  `email` VARCHAR(255) NOT NULL,
  `password` VARCHAR(255) NOT NULL,
  `first_created` TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`user_id`))
ENGINE = InnoDB
AUTO_INCREMENT = 3
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `game`.`playthrough`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `game`.`playthrough` (
  `play_id` SMALLINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `player_id` SMALLINT UNSIGNED NOT NULL,
  `first_created` TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP,
  `last_updated` TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP,
  `playtime` INT UNSIGNED NOT NULL DEFAULT '0',
  `completed` TINYINT(1) NOT NULL DEFAULT '0',
  `user_user_id` SMALLINT UNSIGNED NOT NULL,
  PRIMARY KEY (`play_id`),
  INDEX `fk_player_id_playthrough` (`player_id` ASC) VISIBLE,
  INDEX `fk_playthrough_user1_idx` (`user_user_id` ASC) VISIBLE,
  CONSTRAINT `fk_player_id_playthrough`
    FOREIGN KEY (`player_id`)
    REFERENCES `game`.`player` (`player_id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE,
  CONSTRAINT `fk_playthrough_user1`
    FOREIGN KEY (`user_user_id`)
    REFERENCES `game`.`user` (`user_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 2
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

USE `game` ;

-- -----------------------------------------------------
-- Placeholder table for view `game`.`new_users`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `game`.`new_users` (`Date` INT, `newPlayers` INT);

-- -----------------------------------------------------
-- Placeholder table for view `game`.`speedruns`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `game`.`speedruns` (`UserName` INT, `Time` INT);

-- -----------------------------------------------------
-- Placeholder table for view `game`.`top_weapons`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `game`.`top_weapons` (`Weapon` INT, `Type` INT, `Kills` INT);

-- -----------------------------------------------------
-- View `game`.`new_users`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `game`.`new_users`;
USE `game`;
CREATE  OR REPLACE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `game`.`new_users` AS select cast(`game`.`user`.`first_created` as date) AS `Date`,count(distinct `game`.`user`.`user_id`) AS `newPlayers` from `game`.`user` group by cast(`game`.`user`.`first_created` as date);

-- -----------------------------------------------------
-- View `game`.`speedruns`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `game`.`speedruns`;
USE `game`;
CREATE  OR REPLACE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `game`.`speedruns` AS select `game`.`user`.`name` AS `UserName`,`game`.`playthrough`.`playtime` AS `Time` from (`game`.`user` join `game`.`playthrough` on((`game`.`user`.`user_id` = `game`.`playthrough`.`user_id`))) order by `game`.`playthrough`.`playtime`;

-- -----------------------------------------------------
-- View `game`.`top_weapons`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `game`.`top_weapons`;
USE `game`;
CREATE  OR REPLACE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `game`.`top_weapons` AS select `game`.`weapon`.`name` AS `Weapon`,`game`.`weapon_type`.`name` AS `Type`,`game`.`weapon`.`kills` AS `Kills` from (`game`.`weapon` join `game`.`weapon_type` on((`game`.`weapon`.`type_id` = `game`.`weapon_type`.`type_id`))) order by `game`.`weapon`.`kills`;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

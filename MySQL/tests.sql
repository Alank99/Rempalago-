-- Active: 1681846904940@@127.0.0.1@3306@game


SELECT P.play_id, MAX(P.last_updated) from `user` as U
left join playthrough as P on U.user_id = P.user_id



Select * from playthrough as P
LEFT JOIN `user` as U ON U.user_id = P.user_id
LEFT JOIN `player` as PL On P.player_id = PL.player_id
ORDER BY P.last_updated

``
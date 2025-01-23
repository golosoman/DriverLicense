--Удаление всех данных из таблицы

--delete from "answer";
--delete from "attempt_ticket";
--delete from "question_sign";
--delete from "question_traffic_light";
--delete from "question_traffic_participant";
--delete from "ticket_question";
--delete from "question";
--delete from "sign";
--delete from "ticket";
--delete from "traffic_light";
--delete from "traffic_participant";
--delete from "user";
--delete from "category";



-- РАБОТА С ПОЛЬЗОВАТЕЛЯМИ
--select * from "user";

insert into "user" ("id","username", "role", "password") values
(1,'admin', 'ROLE_ADMIN', '$2a$10$xjJ62Qx4Sv.GzVAz8tR1FOs4sxef7tBRwMVEkmE6aA784BE5c6Hv.'),
(2, 'golosoman', 'ROLE_USER', '$2a$10$xjJ62Qx4Sv.GzVAz8tR1FOs4sxef7tBRwMVEkmE6aA784BE5c6Hv.');



-- РАБОТА С КАТЕГОРИЯМИ 
--select * from category;

insert into category(name) values
('Категория 1'), 
('Категория 2'), 
('Категория 3');



-- РАБОТА С ВОПРОСАМИ
-- select * from question;
-- select * from traffic_participant;
-- select * from question_traffic_participant;
-- select * from sign;

-- Вопрос 1
INSERT INTO "question" ("category_id", "intersection_type", "question", "explanation")
VALUES (1, 'CrossType1', 'Кто поедет первым?', 'Правильный ответ');

INSERT INTO "traffic_participant" ("participant_type", "model_name", "side_position", "number_position", "direction")
VALUES ('Car', 'CarType1', 'North', 1, 'Left'),
       ('Car', 'CarType1', 'East', 1, 'Forward'),
       ('Car', 'CarType1', 'South', 1, 'Left'),
	   ('Car', 'CarType1', 'West', 1, 'Right');
	   
INSERT INTO "question_traffic_participant" ("question_id", "traffic_participant_id")
VALUES (1, 1),
       (1, 2),
       (1, 3),
	   (1, 4);

INSERT INTO "sign" ("model_name", "side_position")
VALUES ('YieldSignType1', 'North'),
		('YieldSignType1', 'South'),
		('MakeWaySignType1', 'East'),
		('MakeWaySignType1', 'West');

INSERT INTO "question_sign" ("question_id", "sign_id")
VALUES (1, 1),
		(1, 2),
		(1, 3),
		(1, 4);

-- Вопрос 2
INSERT INTO "question" ("category_id", "intersection_type", "question", "explanation")
VALUES (1, 'TShapedType1', 'Какая машина должна уступить?', 'Правильный ответ');

INSERT INTO "traffic_participant" ("participant_type", "model_name", "side_position", "number_position", "direction")
VALUES ('Car', 'CarType1', 'North', 1, 'Forward'),
       ('Car', 'CarType1', 'East', 1, 'Left'),
       ('Car', 'CarType1', 'South', 1, 'Right');
	   
INSERT INTO "question_traffic_participant" ("question_id", "traffic_participant_id")
VALUES (2, 4),
       (2, 5),
       (2, 6);
	   
INSERT INTO "sign" ("model_name", "side_position")
VALUES ('YieldSignType1', 'North'),
		('YieldSignType1', 'South'),
		('MakeWaySignType1', 'East');
		
INSERT INTO "question_sign" ("question_id", "sign_id")
VALUES (2, 5),
		(2, 6),
		(2, 7);

-- Вопрос 3
INSERT INTO "question" ("category_id", "intersection_type", "question", "explanation")
VALUES (1, 'CrossType1', 'Кто поедет первым?', 'Правильный ответ');

INSERT INTO "traffic_participant" ("participant_type", "model_name", "side_position", "number_position", "direction")
VALUES ('Car', 'CarType1', 'North', 1, 'Left'),
       ('Car', 'CarType1', 'East', 1, 'Forward'),
       ('Car', 'CarType1', 'South', 1, 'Forward'),
	   ('Car', 'CarType1', 'West', 1, 'Right');
	   
INSERT INTO "question_traffic_participant" ("question_id", "traffic_participant_id")
VALUES (3, 7),
       (3, 8),
       (3, 9),
	   (3, 10);

INSERT INTO "sign" ("model_name", "side_position")
VALUES ('YieldSignType1', 'North'),
		('YieldSignType1', 'South'),
		('MakeWaySignType1', 'East'),
		('MakeWaySignType1', 'West');

INSERT INTO "question_sign" ("question_id", "sign_id")
VALUES (3, 8),
		(3, 9),
		(3, 10),
		(3, 11);

-- Вопрос 4
INSERT INTO "question" ("category_id", "intersection_type", "question", "explanation")
VALUES (1, 'TShapedType1', 'Какая машина должна уступить?', 'Правильный ответ');

INSERT INTO "traffic_participant" ("participant_type", "model_name", "side_position", "number_position", "direction")
VALUES ('Car', 'CarType1', 'North', 1, 'Forward'),
       ('Car', 'CarType1', 'East', 1, 'Left'),
       ('Car', 'CarType1', 'South', 1, 'Forward');
	   
INSERT INTO "question_traffic_participant" ("question_id", "traffic_participant_id")
VALUES (4, 11),
       (4, 12),
       (4, 13);
	   
INSERT INTO "sign" ("model_name", "side_position")
VALUES ('YieldSignType1', 'North'),
		('YieldSignType1', 'South'),
		('MakeWaySignType1', 'East');
		
INSERT INTO "question_sign" ("question_id", "sign_id")
VALUES (4, 12),
		(4, 13),
		(4, 14);
	
-- Вопрос 5
INSERT INTO "question" ("category_id", "intersection_type", "question", "explanation")
VALUES (1, 'TShapedType1', 'Какая машина должна уступить?', 'Правильный ответ');

INSERT INTO "traffic_participant" ("participant_type", "model_name", "side_position", "number_position", "direction")
VALUES ('Car', 'CarType1', 'North', 1, 'Forward'),
       ('Car', 'CarType1', 'East', 1, 'Right'),
       ('Car', 'CarType1', 'South', 1, 'Forward');
	   
INSERT INTO "question_traffic_participant" ("question_id", "traffic_participant_id")
VALUES (5, 15),
       (5, 16),
       (5, 17);
	   
INSERT INTO "sign" ("model_name", "side_position")
VALUES ('YieldSignType1', 'North'),
		('YieldSignType1', 'South'),
		('MakeWaySignType1', 'East');
		
INSERT INTO "question_sign" ("question_id", "sign_id")
VALUES (5, 15),
		(5, 16),
		(5, 17);
	

	
-- РАБОТА С БИЛЕТАМИ
--select * from "ticket";
	
insert into "ticket" ("name") values
('Билет 1'),
('Билет 2'),
('Билет 3'),
('Билет 4'),
('Билет 5');

insert into "ticket_question" values
(1, 1),
(1, 2),
(1, 3),
(1, 4),
(1, 5);

insert into "ticket_question" values
(2, 5),
(2, 4),
(2, 3),
(2, 2),
(2, 1);

insert into "ticket_question" values
(3, 2),
(3, 3),
(3, 4),
(3, 5),
(3, 1);

insert into "ticket_question" values
(4, 3),
(4, 4),
(4, 5),
(4, 1),
(4, 2);

insert into "ticket_question" values
(5, 4),
(5, 5),
(5, 1),
(5, 2),
(5, 3);
	


-- РАБОТА С ПОПЫТКАМИ
select * from "attempt_ticket";
--select * from "ticket";
--select * from "ticket_question";
select * from "answer";

insert into "attempt_ticket" ("ticket_id", "user_id", "attempt_date", "result") values
(1, 2, now(), false),
(2, 2, now(), true),
(3, 2, now(), true);

insert into "answer" ("attempt_ticket_id", "question_id", "result") values
(1, 1, true),
(1, 2, false),
(1, 3, false),
(1, 4, false),
(1, 5, true);

insert into "answer" ("attempt_ticket_id", "question_id", "result") values
(2, 5, true),
(2, 4, false),
(2, 3, false),
(2, 2, false),
(2, 1, true);

insert into "answer" ("attempt_ticket_id", "question_id", "result") values
(3, 2, true),
(3, 3, false),
(3, 4, false),
(3, 5, false),
(3, 1, true);

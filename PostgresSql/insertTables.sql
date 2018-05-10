INSERT INTO "question" ("intersection_type", "title", "question", "explanation")
VALUES ('CrossType1', 'Ситуация 1: Три машины на перекрестке', 'Кто поедет первым?', 'Правильный ответ');

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

-- Добавление знаков
INSERT INTO "sign" ("model_name", "side_position")
VALUES ('YieldSignType1', 'North'),
		('YieldSignType1', 'South'),
		('MakeWaySignType1', 'East'),
		('MakeWaySignType1', 'West');

-- Добавление светофоров
INSERT INTO "traffic_light" ("model_name", "side_position", "state")
VALUES ('TrafficLightType1', 'South', 'Green'),
		('TrafficLightType1', 'North', 'Green'),
		('TrafficLightType1', 'East', 'Red'),
		('TrafficLightType1', 'West', 'Red');

-- Связывание знаков с тикетом
INSERT INTO "question_sign" ("question_id", "sign_id")
VALUES (1, 1),
		(1, 2),
		(1, 3),
		(1, 4);

-- Связывание светофоров с тикетом
INSERT INTO "question_traffic_light" ("question_id", "traffic_light_id")
VALUES (1, 1),
		(1, 2),
		(1, 3),
		(1, 4);

select * from question;

-- INSERT INTO "question" ("intersection_type", "title", "question", "correctAnswer")
-- VALUES ('TShapedType1', 'Ситуация 2: Три машины на T-образном перекрестке', 'Какая машина должна уступить?', 'Правильный ответ');
-- INSERT INTO "traffic_participant" ("participant_type", "model_name", "side_position", "number_position", "direction")
-- VALUES ('Car', 'CarType1', 'North', 1, 'Forward', NOW(), NOW()),
--        ('Car', 'CarType1', 'East', 1, 'Left', NOW(), NOW()),
--        ('Car', 'CarType1', 'South', 1, 'Right', NOW(), NOW());
-- INSERT INTO "question_traffic_participant" ("ticketId", "roadUserId", "createdAt", "updatedAt")
-- VALUES (2, 4, NOW(), NOW()),
--        (2, 5, NOW(), NOW()),
--        (2, 6, NOW(), NOW());
-- -- Добавление знаков
-- INSERT INTO "sign" ("modelName", "sidePosition", "createdAt", "updatedAt")
-- VALUES ('YieldSignType1', 'East', NOW(), NOW());

-- -- Добавление светофоров
-- INSERT INTO "traffic_light" ("modelName", "sidePosition", "state", "createdAt", "updatedAt")
-- VALUES ('TrafficLightType1', 'North', 'Red', NOW(), NOW());

-- -- Связывание знаков с тикетом
-- INSERT INTO "question_sign" ("ticketId", "signId", "createdAt", "updatedAt")
-- VALUES (2, 2, NOW(), NOW());

-- -- Связывание светофоров с тикетом
-- INSERT INTO "question_traffic_light" ("ticketId", "trafficLightId", "createdAt", "updatedAt")
-- VALUES (2, 2, NOW(), NOW());
INSERT INTO "tickets" ("typeIntersection", "title", "question", "correctAnswer", "createdAt", "updatedAt")
VALUES ('CrossType1', 'Ситуация 1: Три машины на перекрестке', 'Кто поедет первым?', 'Правильный ответ', NOW(), NOW());

INSERT INTO "roadUsers" ("typeParticipant", "modelName", "sidePosition", "numberPosition", "movementDirection", "createdAt", "updatedAt")
VALUES ('Car', 'CarType1', 'North', 1, 'Left', NOW(), NOW()),
       ('Car', 'CarType1', 'East', 1, 'Forward', NOW(), NOW()),
       ('Car', 'CarType1', 'South', 1, 'Left', NOW(), NOW()),
	   ('Car', 'CarType1', 'West', 1, 'Right', NOW(), NOW());

INSERT INTO "ticketRoadUser" ("ticketId", "roadUserId", "createdAt", "updatedAt")
VALUES (1, 1, NOW(), NOW()),
       (1, 2, NOW(), NOW()),
       (1, 3, NOW(), NOW()),
	   (1, 4, NOW(), NOW());

-- Добавление знаков
INSERT INTO "signs" ("modelName", "sidePosition", "createdAt", "updatedAt")
VALUES ('YieldSignType1', 'North', NOW(), NOW());

-- Добавление светофоров
INSERT INTO "trafficLights" ("modelName", "sidePosition", "state", "createdAt", "updatedAt")
VALUES ('TrafficLightType1', 'South', 'Green', NOW(), NOW());

-- Связывание знаков с тикетом
INSERT INTO "ticketSign" ("ticketId", "signId", "createdAt", "updatedAt")
VALUES (1, 1, NOW(), NOW());

-- Связывание светофоров с тикетом
INSERT INTO "ticketTrafficLight" ("ticketId", "trafficLightId", "createdAt", "updatedAt")
VALUES (1, 1, NOW(), NOW());


INSERT INTO "tickets" ("typeIntersection", "title", "question", "correctAnswer", "createdAt", "updatedAt")
VALUES ('TShapedType1', 'Ситуация 2: Три машины на T-образном перекрестке', 'Какая машина должна уступить?', 'Правильный ответ', NOW(), NOW());
INSERT INTO "roadUsers" ("typeParticipant", "modelName", "sidePosition", "numberPosition", "movementDirection", "createdAt", "updatedAt")
VALUES ('Car', 'CarType1', 'North', 1, 'Forward', NOW(), NOW()),
       ('Car', 'CarType1', 'East', 1, 'Left', NOW(), NOW()),
       ('Car', 'CarType1', 'South', 1, 'Right', NOW(), NOW());
INSERT INTO "ticketRoadUser" ("ticketId", "roadUserId", "createdAt", "updatedAt")
VALUES (2, 4, NOW(), NOW()),
       (2, 5, NOW(), NOW()),
       (2, 6, NOW(), NOW());
-- Добавление знаков
INSERT INTO "signs" ("modelName", "sidePosition", "createdAt", "updatedAt")
VALUES ('YieldSignType1', 'East', NOW(), NOW());

-- Добавление светофоров
INSERT INTO "trafficLights" ("modelName", "sidePosition", "state", "createdAt", "updatedAt")
VALUES ('TrafficLightType1', 'North', 'Red', NOW(), NOW());

-- Связывание знаков с тикетом
INSERT INTO "ticketSign" ("ticketId", "signId", "createdAt", "updatedAt")
VALUES (2, 2, NOW(), NOW());

-- Связывание светофоров с тикетом
INSERT INTO "ticketTrafficLight" ("ticketId", "trafficLightId", "createdAt", "updatedAt")
VALUES (2, 2, NOW(), NOW());




select * from "roadUsers";
select * from "signs";
select * from "trafficLights";
select * from "tickets";

DROP TABLE IF EXISTS "ticketTrafficLight";
DROP TABLE IF EXISTS "ticketSign";
DROP TABLE IF EXISTS "ticketRoadUser";
DROP TABLE IF EXISTS "trafficLights";
DROP TABLE IF EXISTS "signs";
DROP TABLE IF EXISTS "roadUsers";
DROP TABLE IF EXISTS "tickets";

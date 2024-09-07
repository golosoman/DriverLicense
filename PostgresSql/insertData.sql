-- Вставьте несколько записей в таблицу машин
INSERT INTO cars ("modelName", "position", "speed", "movementDirection") VALUES
  ('Car', 'left', 60, 'forward'),
  ('Car', 'right', 70, 'backward'),
  ('Car', 'top', 80, 'left'),
  ('Car', 'bottom', 90, 'right');

-- Вставьте несколько записей в таблицу знаков
INSERT INTO signs ("modelName", "position") VALUES
  ('Sign', 'left'),
  ('Sign', 'right'),
  ('Sign', 'top'),
  ('Sign', 'bottom'),
  ('Sign', 'left');

-- Вставьте несколько записей в таблицу светофоров
INSERT INTO traffic_lights ("modelName", "position", "cycle") VALUES
  ('TrafficLight', 'left', '10-30'),
  ('TrafficLight', 'right', '5-15'),
  ('TrafficLight', 'top', '20-40');

-- Вставьте несколько записей в таблицу билетов
INSERT INTO tickets ("type", "question", "correctAnswer") VALUES
  ('CrossIntersection', 'Что означает этот знак?', 'Остановка запрещена'),
  ('TIntersection', 'Что означает этот знак?', 'Движение прямо и направо'),
  ('roundabout', 'Что означает этот знак?', 'Круговое движение'),
  ('pedestrian_crossing', 'Что означает этот знак?', 'Пешеходный переход');

-- Свяжите билеты с машинами
INSERT INTO ticket_cars ("ticketId", "carId") VALUES
  (1, 1),
  (1, 2),
  (2, 3),
  (2, 4),
  (3, 1),
  (3, 3),
  (4, 2),
  (4, 4);

-- Свяжите билеты со знаками
INSERT INTO ticket_signs ("ticketId", "signId") VALUES
  (1, 1),
  (1, 2),
  (2, 3),
  (2, 4),
  (3, 1),
  (3, 5),
  (4, 2),
  (4, 3);

-- Свяжите билеты со светофорами
INSERT INTO ticket_traffic_lights ("ticketId", "trafficLightId") VALUES
  (1, 1),
  (1, 2),
  (2, 2),
  (2, 3),
  (3, 1),
  (3, 3),
  (4, 2),
  (4, 1);

select * from cars
select * from signs
select * from traffic_lights
select * from tickets

DROP TABLE IF EXISTS ticket_traffic_lights;
DROP TABLE IF EXISTS ticket_signs;
DROP TABLE IF EXISTS ticket_cars;
DROP TABLE IF EXISTS traffic_lights;
DROP TABLE IF EXISTS signs;
DROP TABLE IF EXISTS cars;
DROP TABLE IF EXISTS tickets;

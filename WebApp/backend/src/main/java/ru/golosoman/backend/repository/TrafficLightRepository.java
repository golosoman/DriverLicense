package ru.golosoman.backend.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import ru.golosoman.backend.domain.TrafficLight;

public interface TrafficLightRepository extends JpaRepository<TrafficLight, Long> {

}

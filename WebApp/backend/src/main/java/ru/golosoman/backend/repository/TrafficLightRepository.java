package ru.golosoman.backend.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import ru.golosoman.backend.domain.model.TrafficLight;

import java.util.Optional;

@Repository
public interface TrafficLightRepository extends JpaRepository<TrafficLight, Long> {
    Optional<TrafficLight> findByModelNameAndSidePositionAndState(String modelName, String sidePosition, String state);
}

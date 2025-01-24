package ru.golosoman.backend.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import ru.golosoman.backend.domain.model.TrafficParticipant;

import java.util.Optional;

@Repository
public interface TrafficParticipantRepository extends JpaRepository<TrafficParticipant, Long> {
    Optional<TrafficParticipant> findByModelNameAndDirectionAndNumberPositionAndParticipantTypeAndSidePosition(
            String modelName, String direction, String numberPosition, String participantType, String sidePosition);
}

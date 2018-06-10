package ru.golosoman.backend.repository;

import ru.golosoman.backend.domain.TrafficParticipant;
import org.springframework.data.jpa.repository.JpaRepository;

public interface TrafficParticipantRepository extends JpaRepository<TrafficParticipant, Long> {

}

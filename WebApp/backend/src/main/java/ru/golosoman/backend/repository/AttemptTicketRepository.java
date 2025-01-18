package ru.golosoman.backend.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import ru.golosoman.backend.domain.model.AttemptTicket;

import java.util.List;

public interface AttemptTicketRepository extends JpaRepository<AttemptTicket, Long> {
    List<AttemptTicket> findByUserId(Long userId);
}

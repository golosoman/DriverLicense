package ru.golosoman.backend.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import ru.golosoman.backend.domain.model.AttemptTicket;

import java.util.List;

@Repository
public interface AttemptTicketRepository extends JpaRepository<AttemptTicket, Long> {
    List<AttemptTicket> findByUserId(Long userId);
}

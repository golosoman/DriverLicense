package ru.golosoman.backend.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import ru.golosoman.backend.domain.Ticket;

public interface TicketRepository extends JpaRepository<Ticket, Long> {

}

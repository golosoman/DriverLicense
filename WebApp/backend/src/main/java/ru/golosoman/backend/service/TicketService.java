package ru.golosoman.backend.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import ru.golosoman.backend.domain.model.Ticket;
import ru.golosoman.backend.repository.TicketRepository;

import java.util.List;
import java.util.Optional;

@Service
public class TicketService {

    private final TicketRepository ticketRepository;

    @Autowired
    public TicketService(TicketRepository ticketRepository) {
        this.ticketRepository = ticketRepository;
    }

    // Получить все билеты
    public List<Ticket> findAllTickets() {
        return ticketRepository.findAll();
    }

    // Получить билет по ID
    public Optional<Ticket> findTicketById(Long id) {
        return ticketRepository.findById(id);
    }

    // Получить случайный билет
    public Ticket findRandomTicket() {
        return ticketRepository.findRandomTicket();
    }
}

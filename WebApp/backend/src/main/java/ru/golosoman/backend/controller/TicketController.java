package ru.golosoman.backend.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import ru.golosoman.backend.domain.dto.request.CreateTicketRequest;
import ru.golosoman.backend.domain.dto.response.TicketResponse;
import ru.golosoman.backend.service.TicketService;

import java.util.List;

@RestController
@RequestMapping("/api/tickets")
public class TicketController {

    private final TicketService ticketService;

    @Autowired
    public TicketController(TicketService ticketService) {
        this.ticketService = ticketService;
    }

    // Получить все билеты
    @GetMapping
    public ResponseEntity<List<TicketResponse>> getAllTickets() {
        List<TicketResponse> ticketResponses = ticketService.findAllTickets();
        return ResponseEntity.ok(ticketResponses);
    }

    // Получить билет по ID
    @GetMapping("/{id}")
    public ResponseEntity<TicketResponse> getTicketById(@PathVariable Long id) {
        return ticketService.findTicketById(id)
                .map(ResponseEntity::ok)
                .orElseGet(() -> ResponseEntity.notFound().build());
    }

    // Получить случайный билет
    @GetMapping("/random")
    public ResponseEntity<TicketResponse> getRandomTicket() {
        TicketResponse ticketResponse = ticketService.findRandomTicket();
        return ticketResponse != null ? ResponseEntity.ok(ticketResponse) : ResponseEntity.notFound().build();
    }

    // Создать билет
    @PostMapping
    public ResponseEntity<TicketResponse> createTicket(@RequestBody CreateTicketRequest request) {
        TicketResponse ticketResponse = ticketService.createTicket(request);
        return ResponseEntity.status(HttpStatus.CREATED).body(ticketResponse);
    }

    // Обновить билет
    @PutMapping("/{id}")
    public ResponseEntity<TicketResponse> updateTicket(@PathVariable Long id, @RequestBody CreateTicketRequest request) {
        TicketResponse ticketResponse = ticketService.updateTicket(id, request);
        return ResponseEntity.ok(ticketResponse);
    }

    // Удалить билет
    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteTicket(@PathVariable Long id) {
        ticketService.deleteTicket(id);
        return ResponseEntity.noContent().build();
    }
}
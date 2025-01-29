package ru.golosoman.backend.controller;

import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import jakarta.validation.Valid;
import jakarta.validation.constraints.Positive;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import ru.golosoman.backend.domain.dto.request.CreateTicketRequest;
import ru.golosoman.backend.domain.dto.response.TicketResponse;
import ru.golosoman.backend.service.TicketService;

import java.util.List;

@RestController
@RequiredArgsConstructor
@RequestMapping("/api/tickets")
@Tag(name = "Билеты", description = "Управление билетами")
public class TicketController {
    private final TicketService ticketService;

    @Operation(summary = "Получить все билеты")
    @GetMapping
    public ResponseEntity<List<TicketResponse>> getAllTickets() {
        List<TicketResponse> ticketResponses = ticketService.findAllTickets();
        return ResponseEntity.status(HttpStatus.OK).body(ticketResponses);
    }

    @Operation(summary = "Получить билет по ID")
    @GetMapping("/{id}")
    public ResponseEntity<TicketResponse> getTicketById(@PathVariable @Positive(message = "ID должен быть положительным числом") Long id) {
        TicketResponse ticketResponse = ticketService.findTicketById(id);
        return ResponseEntity.status(HttpStatus.OK).body(ticketResponse);
    }

    @Operation(summary = "Получить случайный билет")
    @GetMapping("/random")
    public ResponseEntity<TicketResponse> getRandomTicket() {
        TicketResponse ticketResponse = ticketService.findRandomTicket();
        return ResponseEntity.status(HttpStatus.OK).body(ticketResponse);
    }

    @Operation(summary = "Создать новый билет")
    @PostMapping
    public ResponseEntity<TicketResponse> createTicket(@RequestBody @Valid CreateTicketRequest request) {
        TicketResponse ticketResponse = ticketService.createTicket(request);
        return ResponseEntity.status(HttpStatus.CREATED).body(ticketResponse);
    }

    @Operation(summary = "Обновить билет")
    @PutMapping("/{id}")
    public ResponseEntity<TicketResponse> updateTicket(@PathVariable @Positive(message = "ID должен быть положительным числом") Long id, @RequestBody @Valid CreateTicketRequest request) {
        TicketResponse updatedTicketResponse = ticketService.updateTicket(id, request);
        return ResponseEntity.status(HttpStatus.OK).body(updatedTicketResponse);
    }

    @Operation(summary = "Удалить билет")
    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteTicket(@PathVariable @Positive(message = "ID должен быть положительным числом") Long id) {
        ticketService.deleteTicket(id);
        return ResponseEntity.status(HttpStatus.OK).build();
    }
}


package ru.golosoman.backend.service;

import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;
import ru.golosoman.backend.domain.dto.request.CreateTicketRequest;
import ru.golosoman.backend.domain.dto.response.TicketResponse;
import ru.golosoman.backend.domain.model.Question;
import ru.golosoman.backend.domain.model.Ticket;
import ru.golosoman.backend.exception.ResourceNotFoundException;
import ru.golosoman.backend.repository.QuestionRepository;
import ru.golosoman.backend.repository.TicketRepository;
import ru.golosoman.backend.util.MappingUtil;

import java.util.HashSet;
import java.util.List;
import java.util.Optional;
import java.util.Set;
import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
public class TicketService {
    private final TicketRepository ticketRepository;
    private final QuestionRepository questionRepository;

    // Получить все билеты
    public List<TicketResponse> findAllTickets() {
        List<Ticket> tickets = ticketRepository.findAll();
        return tickets.stream()
                .map(this::mapToTicketResponse)
                .collect(Collectors.toList());
    }

    // Получить билет по ID
    public TicketResponse findTicketById(Long id) {
        return ticketRepository.findById(id)
                .map(this::mapToTicketResponse)
                .orElseThrow(() -> new ResourceNotFoundException("Билет с ID " + id + " не найден."));
    }

    // Получить случайный билет
    public TicketResponse findRandomTicket() {
        Ticket ticket = ticketRepository.findRandomTicket();
        if (ticket == null) {
            throw new ResourceNotFoundException("Не удалось найти случайный билет");
        }
        return mapToTicketResponse(ticket);
    }

    // Создать билет
    public TicketResponse createTicket(CreateTicketRequest request) {
        Set<Question> questions = new HashSet<>(questionRepository.findAllById(request.getQuestionIds()));

        if (questions.size() != request.getQuestionIds().size()) {
            throw new ResourceNotFoundException("Некоторые идентификаторы вопросов не существуют.");
        }

        Ticket ticket = new Ticket();
        ticket.setName(request.getName());
        ticket.setQuestions(questions);
        ticket = ticketRepository.save(ticket);
        return mapToTicketResponse(ticket);
    }

    // Обновить билет
    public TicketResponse updateTicket(Long id, CreateTicketRequest request) {
        Ticket ticket = ticketRepository.findById(id)
                .orElseThrow(() -> new ResourceNotFoundException("Билет с ID " + id + " не найден."));

        Set<Question> questions = new HashSet<>(questionRepository.findAllById(request.getQuestionIds()));

        if (questions.size() != request.getQuestionIds().size()) {
            throw new ResourceNotFoundException("Некоторые идентификаторы вопросов не существуют.");
        }

        ticket.setName(request.getName());
        ticket.setQuestions(questions);
        ticket = ticketRepository.save(ticket);
        return mapToTicketResponse(ticket);
    }

    // Удалить билет
    public void deleteTicket(Long id) {
        if (!ticketRepository.existsById(id)) {
            throw new ResourceNotFoundException("Билет с ID " + id + " не найден.");
        }
        ticketRepository.deleteById(id);
    }

    private TicketResponse mapToTicketResponse(Ticket ticket) {
        return new TicketResponse(
                ticket.getId(),
                ticket.getName(),
                ticket.getQuestions().stream()
                        .map(MappingUtil::mapToQuestionResponse)
                        .collect(Collectors.toList())
        );
    }
}

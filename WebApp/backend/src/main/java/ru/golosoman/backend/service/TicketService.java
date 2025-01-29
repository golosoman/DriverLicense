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
import java.util.Set;
import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
public class TicketService {
    private final TicketRepository ticketRepository;
    private final QuestionRepository questionRepository;

    /**
     * Получает список всех билетов.
     *
     * @return список ответов билетов
     */
    public List<TicketResponse> findAllTickets() {
        List<Ticket> tickets = ticketRepository.findAll();
        return tickets.stream()
                .map(MappingUtil::mapToTicketResponse)
                .collect(Collectors.toList());
    }

    /**
     * Получает билет по идентификатору.
     *
     * @param id идентификатор билета
     * @return ответ билета
     * @throws ResourceNotFoundException если билет не найден
     */
    public TicketResponse findTicketById(Long id) {
        return ticketRepository.findById(id)
                .map(MappingUtil::mapToTicketResponse)
                .orElseThrow(() -> new ResourceNotFoundException("Билет с ID " + id + " не найден."));
    }

    /**
     * Получает случайный билет.
     *
     * @return ответ случайный билет
     * @throws ResourceNotFoundException если не удалось найти случайный билет
     */
    public TicketResponse findRandomTicket() {
        Ticket ticket = ticketRepository.findRandomTicket();
        if (ticket == null) {
            throw new ResourceNotFoundException("Не удалось найти случайный билет");
        }
        return MappingUtil.mapToTicketResponse(ticket);
    }

    /**
     * Создает новый билет.
     *
     * @param request данные для создания билета
     * @return ответ с созданным билетом
     */
    public TicketResponse createTicket(CreateTicketRequest request) {
        Set<Question> questions = new HashSet<>(questionRepository.findAllById(request.getQuestionIds()));

        if (questions.size() != request.getQuestionIds().size()) {
            throw new ResourceNotFoundException("Некоторые идентификаторы вопросов не существуют.");
        }

        Ticket ticket = new Ticket();
        ticket.setName(request.getName());
        ticket.setQuestions(questions);
        ticket = ticketRepository.save(ticket);
        return MappingUtil.mapToTicketResponse(ticket);
    }

    /**
     * Обновляет существующий билет.
     *
     * @param id идентификатор билета
     * @param request данные для обновления билета
     * @return ответ с обновленным билетом
     * @throws ResourceNotFoundException если билет не найден
     */
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
        return MappingUtil.mapToTicketResponse(ticket);
    }

    /**
     * Удаляет билет по идентификатору.
     *
     * @param id идентификатор билета
     * @throws ResourceNotFoundException если билет не найден
     */
    public void deleteTicket(Long id) {
        if (!ticketRepository.existsById(id)) {
            throw new ResourceNotFoundException("Билет с ID " + id + " не найден.");
        }
        ticketRepository.deleteById(id);
    }


}

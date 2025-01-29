package ru.golosoman.backend.service;

import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;
import ru.golosoman.backend.domain.dto.request.CreateStatisticRequest;
import ru.golosoman.backend.domain.dto.request.StatisticAnswerRequest;
import ru.golosoman.backend.domain.dto.response.statistic.*;
import ru.golosoman.backend.domain.model.*;
import ru.golosoman.backend.exception.ResourceNotFoundException;
import ru.golosoman.backend.repository.*;
import ru.golosoman.backend.util.UserMathUtil;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
public class StatisticsService {
    private final AnswerRepository answerRepository;
    private final AttemptTicketRepository attemptTicketRepository;
    private final TicketRepository ticketRepository;
    private final CategoryRepository categoryRepository;
    private final QuestionRepository questionRepository;
    private static final DateTimeFormatter DATE_TIME_FORMATTER = DateTimeFormatter.ofPattern("dd.MM.yyyy HH:mm:ss");

    public List<CategoryStatistics> getCategoryStatisticsForUser (Long userId) {
        List<CategoryStatistics> statistics = new ArrayList<>();
        List<Category> categories = categoryRepository.findAll();

        for (Category category : categories) {
            // Получаем ответы пользователя по вопросам данной категории
            List<Answer> answers = answerRepository.findByUserIdAndCategoryId(userId, category.getId());

            Long totalQuestions = (long) answers.size();
            Long correctAnswersCount = answers.stream().filter(Answer::isResult).count();

            double percentageCorrect = totalQuestions > 0 ? UserMathUtil.round((double) correctAnswersCount / totalQuestions * 100, 2) : 0;

            statistics.add(new CategoryStatistics(category.getId(), category.getName(), percentageCorrect));
        }
        return statistics;
    }

    public List<TicketStatisticsForTrainee> getUserAttemptTickets(Long userId) {
        List<AttemptTicket> attemptTickets = attemptTicketRepository.findByUserId(userId);
        List<TicketStatisticsForTrainee> ticketStats = new ArrayList<>();
        for (AttemptTicket attemptTicket : attemptTickets) {
            int countErrors = (int) attemptTicket.getAnswers().stream().filter(answer -> !answer.isResult()).count();
            String formattedDate = attemptTicket.getAttemptDate().format(DATE_TIME_FORMATTER); // Форматирование даты
            ticketStats.add(new TicketStatisticsForTrainee(
                    attemptTicket.getId(),
                    attemptTicket.getTicket().getName(),
                    formattedDate,
                    countErrors,
                    attemptTicket.isResult()
            ));
        }

        // Сортировка по дате (самая последняя дата должна быть первой)
        ticketStats.sort(Comparator.comparing(TicketStatisticsForTrainee::getDate).reversed());

        return ticketStats;
    }

    public AdminStatisticsResponse getAdminStatistics() {
        List<QuestionStatistics> questionStats = getQuestionStatistics();
        List<TicketStatisticsForAdmin> ticketStats = getTicketStatistics();

        return new AdminStatisticsResponse(questionStats, ticketStats);
    }

    private List<QuestionStatistics> getQuestionStatistics() {
        List<Question> questions = questionRepository.findAll();
        List<QuestionStatistics> stats = new ArrayList<>();

        for (Question question : questions) {
            long totalAnswers = question.getAnswers().size();
            long correctAnswers = question.getAnswers().stream().filter(Answer::isResult).count();
            double percentage = totalAnswers > 0 ? UserMathUtil.round((double) correctAnswers / totalAnswers * 100, 2) : 0.0;

            stats.add(new QuestionStatistics(question.getId(), question.getQuestion(), percentage));
        }

        return stats;
    }

    private List<TicketStatisticsForAdmin> getTicketStatistics() {
        List<AttemptTicket> attempts = attemptTicketRepository.findAll();
        List<TicketStatisticsForAdmin> stats = new ArrayList<>();

        // Группируем по билетам
        Map<Long, List<AttemptTicket>> ticketAttempts = attempts.stream()
                .collect(Collectors.groupingBy(attempt -> attempt.getTicket().getId()));

        for (Map.Entry<Long, List<AttemptTicket>> entry : ticketAttempts.entrySet()) {
            long totalAttempts = entry.getValue().size();
            long successfulAttempts = entry.getValue().stream().filter(AttemptTicket::isResult).count();
            double percentage = totalAttempts > 0 ? UserMathUtil.round((double) successfulAttempts / totalAttempts * 100, 2) : 0.00;

            Ticket ticket = entry.getValue().get(0).getTicket(); // Получаем билет из первой попытки
            stats.add(new TicketStatisticsForAdmin(ticket.getId(), ticket.getName(), percentage));
        }

        return stats;
    }

    public void createStatistic(CreateStatisticRequest request, User user) {

        // Создание новой попытки
        AttemptTicket attemptTicket = new AttemptTicket();
        attemptTicket.setUser (user); // Устанавливаем пользователя
        attemptTicket.setResult(request.getResult());

        Ticket ticket = ticketRepository.findById(request.getTicketId())
                .orElseThrow(() -> new ResourceNotFoundException("Билет с ID " + request.getTicketId() + " не найден."));
        attemptTicket.setTicket(ticket); // Устанавливаем билет

        // Сохранение попытки для получения ID
        attemptTicketRepository.save(attemptTicket);

        // Создание ответов
        for (StatisticAnswerRequest answerRequest : request.getAnswers()) {
            Question question = questionRepository.findById(answerRequest.getQuestionId())
                    .orElseThrow(() -> new ResourceNotFoundException("Вопрос с ID " + answerRequest.getQuestionId() + " не найден."));

            Answer answer = new Answer();
            answer.setAttemptTicket(attemptTicket); // Связываем ответ с попыткой
            answer.setQuestion(question);
            answer.setResult(answerRequest.getResult());

            answerRepository.save(answer); // Сохраняем ответ
        }
    }
}

package ru.golosoman.backend.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import ru.golosoman.backend.domain.dto.response.statistic.*;
import ru.golosoman.backend.domain.model.*;
import ru.golosoman.backend.repository.AttemptTicketRepository;
import ru.golosoman.backend.repository.AnswerRepository;
import ru.golosoman.backend.repository.CategoryRepository;
import ru.golosoman.backend.repository.QuestionRepository;

import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;

@Service
public class StatisticsService {

    @Autowired
    private AnswerRepository answerRepository;

    @Autowired
    private AttemptTicketRepository attemptTicketRepository;

    @Autowired
    private CategoryRepository categoryRepository;

    @Autowired
    private QuestionRepository questionRepository;

    public List<CategoryStatistics> getCategoryStatisticsForUser (Long userId) {
        List<CategoryStatistics> statistics = new ArrayList<>();
        List<Category> categories = categoryRepository.findAll();

        for (Category category : categories) {
            List<Answer> answers = answerRepository.findByCategoryId(category.getId());
            Long totalQuestions = (long) answers.size();
            Long correctAnswersCount = answerRepository.countCorrectAnswersByCategoryId(category.getId());

            double percentageCorrect = totalQuestions > 0 ? (double) correctAnswersCount / totalQuestions * 100 : 0;

            statistics.add(new CategoryStatistics(category.getId(), category.getName(), percentageCorrect));
        }
        return statistics;
    }

    public List<TicketStatisticsForTrainee> getUserAttemptTickets(Long userId) {
        List<AttemptTicket> attemptTickets = attemptTicketRepository.findByUserId(userId);
        List<TicketStatisticsForTrainee> ticketStats = new ArrayList<>();

        for (AttemptTicket attemptTicket : attemptTickets) {
            int countErrors = (int) attemptTicket.getAnswers().stream().filter(answer -> !answer.isResult()).count();
            ticketStats.add(new TicketStatisticsForTrainee(
                    attemptTicket.getId(),
                    attemptTicket.getTicket().getName(),
                    attemptTicket.getAttemptDate(),
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
            double percentage = totalAnswers > 0 ? (double) correctAnswers / totalAnswers * 100 : 0.0;

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
            double percentage = totalAttempts > 0 ? (double) successfulAttempts / totalAttempts * 100 : 0.0;

            Ticket ticket = entry.getValue().get(0).getTicket(); // Получаем билет из первой попытки
            stats.add(new TicketStatisticsForAdmin(ticket.getId(), ticket.getName(), percentage));
        }

        return stats;
    }
}

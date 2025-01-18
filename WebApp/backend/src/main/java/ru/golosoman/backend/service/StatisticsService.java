package ru.golosoman.backend.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import ru.golosoman.backend.domain.dto.response.statistic.CategoryStatistics;
import ru.golosoman.backend.domain.dto.response.statistic.TicketStatistics;
import ru.golosoman.backend.domain.model.AttemptTicket;
import ru.golosoman.backend.domain.model.Answer;
import ru.golosoman.backend.domain.model.Category;
import ru.golosoman.backend.repository.AttemptTicketRepository;
import ru.golosoman.backend.repository.AnswerRepository;
import ru.golosoman.backend.repository.CategoryRepository;

import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;

@Service
public class StatisticsService {

    @Autowired
    private AnswerRepository answerRepository;

    @Autowired
    private AttemptTicketRepository attemptTicketRepository;

    @Autowired
    private CategoryRepository categoryRepository;

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

    public List<TicketStatistics> getUserAttemptTickets(Long userId) {
        List<AttemptTicket> attemptTickets = attemptTicketRepository.findByUserId(userId);
        List<TicketStatistics> ticketStats = new ArrayList<>();

        for (AttemptTicket attemptTicket : attemptTickets) {
            int countErrors = (int) attemptTicket.getAnswers().stream().filter(answer -> !answer.isResult()).count();
            ticketStats.add(new TicketStatistics(
                    attemptTicket.getId(),
                    attemptTicket.getTicket().getName(),
                    attemptTicket.getAttemptDate(),
                    countErrors,
                    attemptTicket.isResult()
            ));
        }

        // Сортировка по дате (самая последняя дата должна быть первой)
        ticketStats.sort(Comparator.comparing(TicketStatistics::getDate).reversed());

        return ticketStats;
    }
}

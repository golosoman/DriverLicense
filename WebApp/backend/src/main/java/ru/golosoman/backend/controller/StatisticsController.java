package ru.golosoman.backend.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;
import ru.golosoman.backend.domain.dto.response.statistic.CategoryStatistics;
import ru.golosoman.backend.domain.dto.response.statistic.StatisticsResponse;
import ru.golosoman.backend.domain.dto.response.statistic.TicketStatistics;
import ru.golosoman.backend.domain.model.User;

import ru.golosoman.backend.service.StatisticsService;
import ru.golosoman.backend.service.UserService;

import java.util.List;

@RestController
@RequestMapping("/api/statistics")
public class StatisticsController {

    @Autowired
    private StatisticsService statisticsService;

    @Autowired
    private UserService userService;

    @GetMapping("/byTrainee")
    public StatisticsResponse getStatisticsByTrainee() {
        User user = userService.getCurrentUser ();

        List<CategoryStatistics> categoryStatistics = statisticsService.getCategoryStatisticsForUser (user.getId());
        List<TicketStatistics> ticketStatistics = statisticsService.getUserAttemptTickets(user.getId());

        return new StatisticsResponse(categoryStatistics, ticketStatistics);
    }
}

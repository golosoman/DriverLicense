package ru.golosoman.backend.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import ru.golosoman.backend.domain.dto.request.CreateStatisticRequest;
import ru.golosoman.backend.domain.dto.response.statistic.AdminStatisticsResponse;
import ru.golosoman.backend.domain.dto.response.statistic.CategoryStatistics;
import ru.golosoman.backend.domain.dto.response.statistic.TraineeStatisticsResponse;
import ru.golosoman.backend.domain.dto.response.statistic.TicketStatisticsForTrainee;
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
    public TraineeStatisticsResponse getStatisticsByTrainee() {
        User user = userService.getCurrentUser();

        List<CategoryStatistics> categoryStatistics = statisticsService.getCategoryStatisticsForUser(user.getId());
        List<TicketStatisticsForTrainee> ticketStatistics = statisticsService.getUserAttemptTickets(user.getId());

        return new TraineeStatisticsResponse(categoryStatistics, ticketStatistics);
    }

    @GetMapping("/byAdmin")
    public AdminStatisticsResponse getStatisticsForAdmin() {
        return statisticsService.getAdminStatistics();
    }

    @PostMapping
    public ResponseEntity<Void> createStatistic(@RequestBody CreateStatisticRequest request) {
        User user = userService.getCurrentUser(); // Получаем текущего пользователя
        statisticsService.createStatistic(request, user); // Передаем пользователя в сервис
        return new ResponseEntity<>(HttpStatus.CREATED);
    }
}

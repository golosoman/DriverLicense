package ru.golosoman.backend.controller;

import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
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
@RequiredArgsConstructor
@RequestMapping("/api/statistics")
@Tag(name = "Статистика", description = "Управление статистикой пользователей и администраторов")
public class StatisticsController {
    private final StatisticsService statisticsService;
    private final UserService userService;

    @Operation(summary = "Получить статистику по ученику")
    @GetMapping("/byTrainee")
    public TraineeStatisticsResponse getStatisticsByTrainee() {
        User user = userService.getCurrentUser();
        List<CategoryStatistics> categoryStatistics = statisticsService.getCategoryStatisticsForUser(user.getId());
        List<TicketStatisticsForTrainee> ticketStatistics = statisticsService.getUserAttemptTickets(user.getId());
        return new TraineeStatisticsResponse(categoryStatistics, ticketStatistics);
    }

    @Operation(summary = "Получить статистику для администратора")
    @GetMapping("/byAdmin")
    public AdminStatisticsResponse getStatisticsForAdmin() {
        return statisticsService.getAdminStatistics();
    }

    @Operation(summary = "Создать новую статистику")
    @PostMapping
    public ResponseEntity<Void> createStatistic(@RequestBody @Valid CreateStatisticRequest request) {
        User user = userService.getCurrentUser();
        statisticsService.createStatistic(request, user);
        return new ResponseEntity<>(HttpStatus.CREATED);
    }
}
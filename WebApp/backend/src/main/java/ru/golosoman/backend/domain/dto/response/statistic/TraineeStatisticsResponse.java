package ru.golosoman.backend.domain.dto.response.statistic;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.List;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class TraineeStatisticsResponse {
    private List<CategoryStatistics> categoryStatistics;
    private List<TicketStatisticsForTrainee> ticketStatistics;
}

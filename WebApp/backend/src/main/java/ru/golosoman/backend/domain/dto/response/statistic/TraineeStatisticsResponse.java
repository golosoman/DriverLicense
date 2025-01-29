package ru.golosoman.backend.domain.dto.response.statistic;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import io.swagger.v3.oas.annotations.media.Schema;

import java.util.List;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Schema(description = "Ответ, содержащий статистику по ученику.")
public class TraineeStatisticsResponse {

    @Schema(description = "Список статистики по категориям", implementation = CategoryStatistics.class)
    private List<CategoryStatistics> categoryStatistics;

    @Schema(description = "Список статистики по билетам для ученика", implementation = TicketStatisticsForTrainee.class)
    private List<TicketStatisticsForTrainee> ticketStatistics;
}

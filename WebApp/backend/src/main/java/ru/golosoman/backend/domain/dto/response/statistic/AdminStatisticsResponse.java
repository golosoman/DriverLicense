package ru.golosoman.backend.domain.dto.response.statistic;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import io.swagger.v3.oas.annotations.media.Schema;

import java.util.List;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Schema(description = "Ответ для администратора, содержащий статистику по вопросам и билетам.")
public class AdminStatisticsResponse {

    @Schema(description = "Список статистики по вопросам", implementation = QuestionStatistics.class)
    private List<QuestionStatistics> questionStatistics;

    @Schema(description = "Список статистики по билетам для администратора", implementation = TicketStatisticsForAdmin.class)
    private List<TicketStatisticsForAdmin> ticketStatistics;
}

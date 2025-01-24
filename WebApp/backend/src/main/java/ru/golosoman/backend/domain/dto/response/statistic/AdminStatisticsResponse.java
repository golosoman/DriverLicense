package ru.golosoman.backend.domain.dto.response.statistic;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.List;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class AdminStatisticsResponse {
    private List<QuestionStatistics> questionStatistics;
    private List<TicketStatisticsForAdmin> ticketStatistics;
}
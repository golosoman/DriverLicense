package ru.golosoman.backend.domain.dto.response.statistic;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class TicketStatisticsForAdmin {
    private Long id;
    private String ticketName;
    private double percentage; // Процент успешных ответов
}

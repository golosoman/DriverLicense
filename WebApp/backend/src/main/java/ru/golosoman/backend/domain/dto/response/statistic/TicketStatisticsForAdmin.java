package ru.golosoman.backend.domain.dto.response.statistic;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import io.swagger.v3.oas.annotations.media.Schema;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Schema(description = "Статистика по билету для администратора.")
public class TicketStatisticsForAdmin {

    @Schema(description = "Идентификатор билета", example = "1")
    private Long id;

    @Schema(description = "Название билета", example = "Билет по правилам дорожного движения")
    private String ticketName;

    @Schema(description = "Процент успешных ответов на билет", example = "85.0")
    private double percentage; // Процент успешных ответов
}
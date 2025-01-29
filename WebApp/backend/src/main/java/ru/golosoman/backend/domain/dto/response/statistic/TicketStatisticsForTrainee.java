package ru.golosoman.backend.domain.dto.response.statistic;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import io.swagger.v3.oas.annotations.media.Schema;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Schema(description = "Статистика по билету для ученика.")
public class TicketStatisticsForTrainee {

    @Schema(description = "Идентификатор билета", example = "1")
    private Long id;

    @Schema(description = "Название билета", example = "Билет по правилам дорожного движения")
    private String ticketName;

    @Schema(description = "Дата выполнения билета", example = "2023-10-01T10:00:00")
    private String date;

    @Schema(description = "Количество ошибок при выполнении билета", example = "3")
    private int countErrors;

    @Schema(description = "Статус выполнения билета (успешно/неуспешно)", example = "true")
    private boolean status;
}
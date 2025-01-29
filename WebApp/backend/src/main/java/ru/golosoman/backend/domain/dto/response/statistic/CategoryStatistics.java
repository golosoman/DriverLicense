package ru.golosoman.backend.domain.dto.response.statistic;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import io.swagger.v3.oas.annotations.media.Schema;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Schema(description = "Статистика по категории.")
public class CategoryStatistics {

    @Schema(description = "Идентификатор категории", example = "1")
    private Long id;

    @Schema(description = "Название категории", example = "Правила дорожного движения")
    private String categoryName;

    @Schema(description = "Процент успешных ответов по категории", example = "75.5")
    private double percentage;
}
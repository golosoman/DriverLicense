package ru.golosoman.backend.domain.dto.response.statistic;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import io.swagger.v3.oas.annotations.media.Schema;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Schema(description = "Статистика по вопросу.")
public class QuestionStatistics {

    @Schema(description = "Идентификатор вопроса", example = "1")
    private Long id;

    @Schema(description = "Текст вопроса", example = "Какой знак означает 'Уступи дорогу'?")
    private String question;

    @Schema(description = "Процент успешных ответов на вопрос", example = "80.0")
    private double percentage; // Процент успешных ответов
}